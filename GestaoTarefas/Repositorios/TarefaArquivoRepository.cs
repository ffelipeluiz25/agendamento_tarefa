using AutoMapper;
using GestaoTarefas.Data.Context;
using GestaoTarefas.Data.DTOs;
using GestaoTarefas.Data.Models;
using GestaoTarefas.Repositorios.Interfaces;

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