using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
namespace GestaoTarefas.Controller;
[ApiController]
[Route("account")]
public class TarefaController : ControllerBase
{

    private readonly ILogger<TarefaController> _logger;

    public TarefaController(ILogger<TarefaController> logger)
    {
        _logger = logger;
    }

    [HttpPost("create")]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult Post(List<dynamic> listAccount)
    {
        try
        {
            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"[Error] Method: create-account | {ex.Message}");
            return StatusCode(500);
        }
    }

    [HttpGet("get")]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Get()
    {
        try
        {
            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"[Error] Method: get-account-filters | {ex.Message}");
            return StatusCode(500);
        }
    }
}