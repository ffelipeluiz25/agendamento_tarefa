using GestaoTarefas.Data.DTOs;
namespace GestaoTarefas.Utils;
public static class RetornoMensagem<T>
{
    internal static ResultDTO<T> RetornoMensagemErro(string message)
    {
        ResultDTO<T> result = new ResultDTO<T>();
        result.Success = false;
        result.Message = message;
        return result;
    }

    internal static ResultDTO<T> RetornoMensagemSucesso(T obj)
    {
        ResultDTO<T> result = new ResultDTO<T>();
        result.Success = true;
        result.Data = obj;
        return result;
    }
}