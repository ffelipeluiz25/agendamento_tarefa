using AutoMapper;
using GestaoTarefas.Data.Context;
using GestaoTarefas.Data.DTOs;
using GestaoTarefas.Data.Models;
using GestaoTarefas.Repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GestaoTarefas.Repositorios;
public class TarefaArquivoRepository : ITarefaArquivoRepository
{
    private readonly IServiceProvider _services;
    private readonly IMapper _mapper;
    public TarefaArquivoRepository(IServiceProvider services, IMapper mapper)
    {
        _services = services;
        _mapper = mapper;
    }

    public async Task<ResultDTO<List<TarefaArquivoDTO>>> RecuperarPorId(int tarefaId)
    {
        using (var scope = _services.CreateScope())
        {
            var _context = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
            var lista = await (from ta in _context.TarefaArquivo
                               join t in _context.AgendamentoTarefa on ta.TarefaId equals t.Id
                               where t.Id == tarefaId
                               select new TarefaArquivoDTO()
                               {
                                   Id = ta.Id,
                                   TarefaId = t.Id,
                                   NomeArquivo = ta.NomeArquivo,
                                   Extensao = ta.Extensao,
                                   DataCriacao = ta.DataCriacao,
                               }
            ).ToListAsync();
            if (lista == null)
                return Utils.RetornoMensagem<List<TarefaArquivoDTO>>.RetornoMensagemErro("Arquivos não encontrados!");

            return Utils.RetornoMensagem<List<TarefaArquivoDTO>>.RetornoMensagemSucesso(lista);
        }
    }

    public ResultDTO<TarefaArquivoDTO> RecuperarPorNome(string nomeArquivo)
    {
        using (var scope = _services.CreateScope())
        {
            var _context = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
            var arquivo = (from ta in _context.TarefaArquivo
                              join t in _context.AgendamentoTarefa on ta.TarefaId equals t.Id
                              where ta.NomeArquivo == nomeArquivo
                         select new TarefaArquivoDTO()
                              {
                                  Id = ta.Id,
                                  TarefaId = t.Id,
                                  NomeArquivo = ta.NomeArquivo,
                                  Extensao = ta.Extensao,
                                  DataCriacao = ta.DataCriacao,
                              }
            ).FirstOrDefault();
            if (arquivo == null)
                return Utils.RetornoMensagem<TarefaArquivoDTO>.RetornoMensagemErro("Arquivo não encontrado!");

            return Utils.RetornoMensagem<TarefaArquivoDTO>.RetornoMensagemSucesso(arquivo);
        }
    }

    public async Task<ResultDTO<bool>> TarefaArquivo(TarefaArquivoDTO arquivo)
    {
        using (var scope = _services.CreateScope())
        {
            var _context = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
            var agendamento = _mapper.Map<TarefaArquivos>(arquivo);

            agendamento.DataCriacao = DateTime.Now;
            _context.TarefaArquivo.Add(agendamento);
            await _context.SaveChangesAsync();
        }
        return Utils.RetornoMensagem<bool>.RetornoMensagemSucesso(true);
    }
}