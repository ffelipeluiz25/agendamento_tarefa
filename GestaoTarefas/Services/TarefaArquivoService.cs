using GestaoTarefas.Data.DTOs;
using GestaoTarefas.Repositorios.Interfaces;
using GestaoTarefas.Services.Interfaces;
namespace GestaoTarefas.Services;
public class TarefaArquivoService : ITarefaArquivoService
{
    private readonly ITarefaArquivoRepository _arquivoTarefeRepositorio;
    public TarefaArquivoService(ITarefaArquivoRepository arquivoTarefeRepositorio)
    {
        _arquivoTarefeRepositorio = arquivoTarefeRepositorio;
    }

    public async Task<ResultDTO<bool>> TarefaArquivo(TarefaArquivoDTO arquivo)
    {
        return await _arquivoTarefeRepositorio.TarefaArquivo(arquivo);
    }
}