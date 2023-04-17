using AutoMapper;
using GestaoTarefas.Data.Context;
using GestaoTarefas.Data.DTOs;
using GestaoTarefas.Data.Models;
using GestaoTarefas.Repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GestaoTarefas.Repositorios;
public class UsuarioRepository : IUsuarioRepository
{
    private readonly IServiceProvider _services;
    private readonly IMapper _mapper;
    public UsuarioRepository(IServiceProvider services, IMapper mapper)
    {
        _services = services;
        _mapper = mapper;
    }

    public async Task<ResultDTO<bool>> Criar(UsuarioDTO usuarioDto)
    {
        using (var scope = _services.CreateScope())
        {
            var _context = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
            var usuarioValid = await _context.Usuario.FirstOrDefaultAsync(usuario => usuario.Nome.Equals(usuarioDto.Nome) && usuario.Sobrenome.Equals(usuarioDto.Sobrenome));
            if (usuarioValid != null)
                return Utils.RetornoMensagem<bool>.RetornoMensagemErro("Usuario ja cadastrado!");

            var usuario = _mapper.Map<Usuarios>(usuarioDto);
            usuario.DataCriacao = DateTime.Now;
            _context.Usuario.Add(usuario);
            await _context.SaveChangesAsync();
        }

        return Utils.RetornoMensagem<bool>.RetornoMensagemSucesso(true);
    }

    public async Task<ResultDTO<UsuarioDTO>> RecuperarPorId(int usuarioId)
    {
        using (var scope = _services.CreateScope())
        {
            var _context = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
            var usuario = await _context.Usuario.FirstOrDefaultAsync(usuario => usuario.Id.Equals(usuarioId));
            if (usuario == null)
                return Utils.RetornoMensagem<UsuarioDTO>.RetornoMensagemErro("Usuario não encontrado!");

            return Utils.RetornoMensagem<UsuarioDTO>.RetornoMensagemSucesso(_mapper.Map<UsuarioDTO>(usuario));
        }
    }

    public async Task<ResultDTO<List<UsuarioAllDTO>>> RecuperarTodos()
    {
        using (var scope = _services.CreateScope())
        {
            var _context = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
            var listaUsuarios = await _context.Usuario.ToListAsync();
            if (listaUsuarios == null)
                return Utils.RetornoMensagem<List<UsuarioAllDTO>>.RetornoMensagemErro("Usuarios não encontrado!");

            return Utils.RetornoMensagem<List<UsuarioAllDTO>>.RetornoMensagemSucesso(_mapper.Map<List<UsuarioAllDTO>>(listaUsuarios));
        }
    }
}