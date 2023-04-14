using GestaoTarefas.Data.DTOs;

namespace GestaoTarefas.Repositorios.Interfaces;
public interface ITarefaArquivoRepository
{
    Task<ResultDTO<bool>> TarefaArquivo(TarefaArquivoDTO arquivo);
}