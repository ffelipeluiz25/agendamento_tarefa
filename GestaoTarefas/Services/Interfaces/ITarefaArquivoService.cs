using GestaoTarefas.Data.DTOs;

namespace GestaoTarefas.Services.Interfaces;
public interface ITarefaArquivoService
{
    Task<ResultDTO<bool>> TarefaArquivo(TarefaArquivoDTO arquivo);
}