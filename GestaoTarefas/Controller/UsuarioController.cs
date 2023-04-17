using GestaoTarefas.Data.DTOs;
using GestaoTarefas.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

namespace GestaoTarefas.Controller;
[ApiController]
[Route("usuario")]
public class UsuarioController : ControllerBase
{

    private readonly IUsuarioService _usuarioService;

    public UsuarioController(IUsuarioService usuarioService)
    {
        _usuarioService = usuarioService;
    }

    [HttpGet()]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [SwaggerResponse(StatusCodes.Status200OK, "Recuperar usuários", typeof(ResultDTO<List<UsuarioAllDTO>>))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ResultDTO<List<UsuarioAllDTO>>))]
    public async Task<IActionResult> RecuperarTodos()
    {
        var result = await _usuarioService.RecuperarTodos();
        if (!result.Success)
            return BadRequest(result);

        return Ok(result);
    }

    [HttpGet("{usuarioId}")]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [SwaggerResponse(StatusCodes.Status200OK, "Recuperar usuário por id", typeof(ResultDTO<UsuarioDTO>))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ResultDTO<UsuarioDTO>))]
    public async Task<IActionResult> RecuperarPorId(int usuarioId)
    {
        var result = await _usuarioService.RecuperarPorId(usuarioId);
        if (!result.Success)
            return BadRequest(result);

        return Ok(result);
    }

    [HttpPost("criar")]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [SwaggerResponse(StatusCodes.Status200OK, "Criar Usuário", typeof(ResultDTO<bool>))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ResultDTO<bool>))]
    public async Task<IActionResult> Post(UsuarioDTO usuario)
    {
        var result = await _usuarioService.Criar(usuario);
        if (!result.Success)
            return Conflict();
        return Ok(result);
    }

}