namespace GestaoTarefas.Data.DTOs;
public class ResultDTO<T>
{
    public bool Success { get; set; }
    public string Message { get; set; }
    public T? Data { get; set; }

    public ResultDTO() { }

    public ResultDTO(bool success, string message)
    {
        Success = success;
        Message = message;
    }

    public ResultDTO(T data)
    {
        Success = true;
        Data = data;
    }
}