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
            var agendamento = _mapper.Map<AgendamentoTarefas>(agendamentoDTO);

            agendamento.DataCriacao = DateTime.Now;
            _context.AgendamentoTarefa.Add(agendamento);
            await _context.SaveChangesAsync();
        }
        return Utils.RetornoMensagem<bool>.RetornoMensagemSucesso(true);
    }

    public async Task<ResultDTO<bool>> AtualizaStatus(int status, int usuarioId)
    {
        using (var scope = _services.CreateScope())
        {
            var _context = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
            var result = _context.AgendamentoTarefa.SingleOrDefault(b => b.Id == usuarioId);
            if (result != null)
            {
                result.Status = status;
                switch (status)
                {
                    case (int)EnumStatusAgentamento.EmAndamento:
                        {
                            result.DataEmAndamento = DateTime.Now;
                            break;
                        }
                    case (int)EnumStatusAgentamento.Finalizada:
                        {
                            result.DataFinalizacao = DateTime.Now;
                            break;
                        }

                }
                await _context.SaveChangesAsync();
            }
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
}