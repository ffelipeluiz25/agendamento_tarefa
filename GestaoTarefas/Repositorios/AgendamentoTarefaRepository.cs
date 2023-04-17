using AutoMapper;
using GestaoTarefas.Data.Context;
using GestaoTarefas.Data.DTOs;
using GestaoTarefas.Data.Models;
using GestaoTarefas.Enumeradores;
using GestaoTarefas.Repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GestaoTarefas.Repositorios;
public class AgendamentoTarefaRepository : IAgendamentoTarefaRepository
{
    private readonly IServiceProvider _services;
    private readonly IMapper _mapper;
    public AgendamentoTarefaRepository(IServiceProvider services, IMapper mapper)
    {
        _services = services;
        _mapper = mapper;
    }

    public async Task<ResultDTO<bool>> Agendamento(AgendamentoDTO agendamentoDTO)
    {
        using (var scope = _services.CreateScope())
        {
            var _context = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
            var agendamentoValid = await _context.AgendamentoTarefa.Where(at => at.UsuarioId.Equals(agendamentoDTO.UsuarioId)
                                                                                && (at.Status.Equals((int)EnumStatusAgentamento.Agendada) || at.Status.Equals((int)EnumStatusAgentamento.EmAndamento))).FirstOrDefaultAsync();
            if (agendamentoValid != null)
                return Utils.RetornoMensagem<bool>.RetornoMensagemErro(string.Format("Usuario ja agendado na data {0}", agendamentoValid.DataCriacao));

            var agendamento = _mapper.Map<AgendamentoTarefas>(agendamentoDTO);
            agendamento.DataCriacao = DateTime.Now;
            agendamento.Status = (int)EnumStatusAgentamento.Agendada;
            _context.AgendamentoTarefa.Add(agendamento);
            await _context.SaveChangesAsync();
        }
        return Utils.RetornoMensagem<bool>.RetornoMensagemSucesso(true);
    }

    public async Task<ResultDTO<bool>> AtualizaStatus(int status, int usuarioId)
    {
        try
        {
            using (var scope = _services.CreateScope())
            {
                var _context = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
                
                switch (status)
                {
                    case (int)EnumStatusAgentamento.EmAndamento:
                        {
                            var agendamento = await _context.AgendamentoTarefa.Where(at => at.UsuarioId.Equals(usuarioId) && !at.DataEmAndamento.HasValue).FirstOrDefaultAsync();
                            if (agendamento != null)
                            {
                                agendamento.DataEmAndamento = DateTime.Now;
                                agendamento.Status = status;
                            }
                            break;
                        }
                    case (int)EnumStatusAgentamento.Finalizada:
                        {
                            var agendamento = await _context.AgendamentoTarefa.Where(at => at.UsuarioId.Equals(usuarioId) && !at.DataFinalizacao.HasValue).FirstOrDefaultAsync();
                            if (agendamento != null)
                            {
                                agendamento.DataFinalizacao = DateTime.Now;
                                agendamento.Status = status;
                            }
                            break;
                        }

                }
                await _context.SaveChangesAsync();
            }
        }
        catch (Exception ec)
        {
            throw;
        }

        return Utils.RetornoMensagem<bool>.RetornoMensagemSucesso(true);
    }

    public async Task<ResultDTO<AgendamentoDTO>> BuscaTarefaPorUsuarioIdAndStatus(int usuarioId, int status)
    {
        using (var scope = _services.CreateScope())
        {
            var _context = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
            var agendamento = await _context.AgendamentoTarefa.FirstOrDefaultAsync(task => task.UsuarioId.Equals(usuarioId)
                                                                                && task.Status.Equals(status));
            return Utils.RetornoMensagem<AgendamentoDTO>.RetornoMensagemSucesso(_mapper.Map<AgendamentoDTO>(agendamento));
        }
    }

    public async Task<ResultDTO<RecuperaPeriodoTempoDTO>> RecuperaPeriodoTempo(int tarefaId)
    {
        using (var scope = _services.CreateScope())
        {
            var _context = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
            var tarefa = await _context.AgendamentoTarefa.FirstOrDefaultAsync(task => task.Id.Equals(tarefaId) && task.DataEmAndamento.HasValue && task.DataFinalizacao.HasValue);
            if (tarefa == null)
                return Utils.RetornoMensagem<RecuperaPeriodoTempoDTO>.RetornoMensagemErro("Tarefa não encontrado ou nao finalizada!");

            CalculaPeriodoTempoDTO calculo = new CalculaPeriodoTempoDTO(tarefa.DataEmAndamento.Value, tarefa.DataFinalizacao.Value);
            var result = new RecuperaPeriodoTempoDTO() { Retorno = calculo.Retorno() };
            return Utils.RetornoMensagem<RecuperaPeriodoTempoDTO>.RetornoMensagemSucesso(result);
        }
    }

    public async Task<ResultDTO<List<AgendamentoTarefasDTO>>> RecuperarTodas()
    {
        using (var scope = _services.CreateScope())
        {
            var _context = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
            var lista = await (from ar in _context.AgendamentoTarefa
                               join u in _context.Usuario on ar.UsuarioId equals u.Id
                               select new AgendamentoTarefasDTO()
                               {
                                   Id = ar.Id,
                                   DataInicio = ar.DataInicio,
                                   DataEmAndamento = ar.DataEmAndamento,
                                   DataFinalizacao = ar.DataFinalizacao,
                                   Duracao = ar.Duracao,
                                   Status = RecuperaStatus.GetStatusAgendamento(ar.Status),
                                   UsuarioId = u.Id,
                                   Usuario = string.Format("{0} {1}", u.Nome, u.Sobrenome)
                               }
            ).ToListAsync();
            if (lista == null)
                return Utils.RetornoMensagem<List<AgendamentoTarefasDTO>>.RetornoMensagemErro("Tarefas não encontradas!");

            return Utils.RetornoMensagem<List<AgendamentoTarefasDTO>>.RetornoMensagemSucesso(lista);
        }


    }
}