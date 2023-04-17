using GestaoTarefas.Data.DTOs;

namespace GestaoTarefas.Repositorios.Interfaces;
public interface ITarefaArquivoRepository
{
    Task<ResultDTO<List<TarefaArquivoDTO>>> RecuperarPorId(int tarefaId);
    ResultDTO<TarefaArquivoDTO> RecuperarPorNome(string nomeArquivo);
    Task<ResultDTO<bool>> TarefaArquivo(TarefaArquivoDTO arquivo);
}