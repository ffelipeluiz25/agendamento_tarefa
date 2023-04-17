using GestaoTarefas.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
namespace GestaoTarefas.Controller;


[ApiController]
[Route("autocomplete")]
public class AutocompleteController : ControllerBase
{

    private readonly IUsuarioService _usuarioService;
    public AutocompleteController(IUsuarioService usuarioService)
    {
        _usuarioService = usuarioService;
    }


    [HttpGet]
    public async Task<IActionResult> Get(string q, string program = null, string state = null, string source = null, string is_employer_credit_policy = null)
    {
        try
        {
            var lista = await _usuarioService.RecuperarTodos();
            return Ok(lista);
        }
        catch (Exception ex)
        {
            return StatusCode(500);
        }
    }
}