using GestaoTarefas.Data.DTOs;
using GestaoTarefas.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
namespace GestaoTarefas.Controller;

[ApiController]
[Route("arquivo")]
public class ArquivoTarefaController : ControllerBase
{

    private readonly ITarefaArquivoService _arquivoTarefaService;
    public ArquivoTarefaController(ITarefaArquivoService arquivoTarefaService)
    {
        _arquivoTarefaService = arquivoTarefaService;
    }

    [HttpPost("{tarefaId}")]
    public async Task<IActionResult> ArquivoTarefa(IFormFile arquivoPostado, int tarefaId)
    {
        if (arquivoPostado == null)
            return BadRequest(Utils.RetornoMensagem<bool>.RetornoMensagemErro("Arquivo nao encontrado!"));

        var arquivo = new TarefaArquivoDTO(tarefaId, arquivoPostado);
        byte[] data = new byte[64];
        Array.Clear(data, 0, data.Length);
        if (arquivoPostado.Length > 0)
        {
            using (var stream = new MemoryStream())
            {
                await arquivoPostado.CopyToAsync(stream);
                data = stream.ToArray();
            }
        }

        var result = await _arquivoTarefaService.TarefaArquivo(arquivo);
        if (!result.Success)
            return BadRequest(result);

        return File(data, arquivoPostado.ContentType, "Anexo" + Path.GetExtension(arquivoPostado.FileName));
    }

}