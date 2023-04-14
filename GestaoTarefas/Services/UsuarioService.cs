using GestaoTarefas.Data.DTOs;
using GestaoTarefas.Repositorios.Interfaces;
using GestaoTarefas.Services.Interfaces;

namespace GestaoTarefas.Services;
public class UsuarioService : IUsuarioService
{
    private readonly IUsuarioRepository _usuarioRepo;
    public UsuarioService(IUsuarioRepository usuarioRepo)
    {
        _usuarioRepo = usuarioRepo;
    }

    public async Task<ResultDTO<bool>> Criar(UsuarioDTO usuario)
    {
        return await _usuarioRepo.Criar(usuario);
    }

    public async Task<ResultDTO<UsuarioDTO>> RecuperarPorId(int usuarioId)
    {
        return await _usuarioRepo.RecuperarPorId(usuarioId);
    }

    public async Task<ResultDTO<List<UsuarioAllDTO>>> RecuperarTodos()
    {
        return await _usuarioRepo.RecuperarTodos();
    }
}