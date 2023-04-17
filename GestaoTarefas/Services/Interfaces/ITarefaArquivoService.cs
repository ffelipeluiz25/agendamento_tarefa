using GestaoTarefas.Data.DTOs;

namespace GestaoTarefas.Services.Interfaces;
public interface ITarefaArquivoService
{
    Task<ResultDTO<List<TarefaArquivoDTO>>> RecuperarPorId(int tarefaId);
    ResultDTO<TarefaArquivoDTO> RecuperarPorNome(string nomeArquivo);
    Task<ResultDTO<bool>> TarefaArquivo(TarefaArquivoDTO arquivo);
}