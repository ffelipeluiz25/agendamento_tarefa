using GestaoTarefas.Data.DTOs;
namespace GestaoTarefas.Services.Interfaces;
public interface IUsuarioService
{
    Task<ResultDTO<bool>> Criar(UsuarioDTO usuario);
    Task<ResultDTO<UsuarioDTO>> RecuperarPorId(int usuarioId);
    Task<ResultDTO<List<UsuarioAllDTO>>> RecuperarTodos();
}