using GestaoTarefas.Data.DTOs;

namespace GestaoTarefas.Repositorios.Interfaces;
public interface IUsuarioRepository
{
    Task<ResultDTO<bool>> Criar(UsuarioDTO usuario);
    Task<ResultDTO<UsuarioDTO>> RecuperarPorId(int usuarioId);
    Task<ResultDTO<List<UsuarioAllDTO>>> RecuperarTodos();
}