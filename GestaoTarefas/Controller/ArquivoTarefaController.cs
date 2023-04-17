using GestaoTarefas.Data.DTOs;
using GestaoTarefas.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;
using System.Text;
using System.IO;

namespace GestaoTarefas.Controller;

[ApiController]
[Route("arquivo")]
public class ArquivoTarefaController : ControllerBase
{

    private readonly ITarefaArquivoService _arquivoTarefaService;
    private readonly IConfiguration _configuration;

    public ArquivoTarefaController(ITarefaArquivoService arquivoTarefaService, IConfiguration configuration)
    {
        _arquivoTarefaService = arquivoTarefaService;
        _configuration = configuration;
    }

    [HttpGet()]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [SwaggerResponse(StatusCodes.Status200OK, "Recuperar arquivos", typeof(ResultDTO<List<TarefaArquivoDTO>>))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ResultDTO<List<TarefaArquivoDTO>>))]
    public async Task<IActionResult> RecuperarPorId([FromQuery] int tarefaId)
    {
        var result = await _arquivoTarefaService.RecuperarPorId(tarefaId);
        if (!result.Success)
            return BadRequest(result);

        return Ok(result);
    }

    [HttpPost("download")]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [SwaggerResponse(StatusCodes.Status200OK, "Recuperar arquivos", typeof(ResultDTO<TarefaArquivoDownloadDTO>))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ResultDTO<TarefaArquivoDownloadDTO>))]
    public async Task<IActionResult> Download(TarefaArquivoDownloadDTO tarefa)
    {
        var path = Directory.GetCurrentDirectory();
        string folder = path + _configuration["pathArquivos"] + tarefa.ArquivoId.ToString();
        string fileName = string.Empty;

        byte[] file = new byte[] { };
        foreach (string strFile in Directory.GetFiles(folder))
        {
            fileName = strFile.Replace(folder + "\\", "");
            using (var stream = System.IO.File.OpenRead(strFile))
            {
                file = ConverteStreamToByteArray(stream);
            }
        }

        ResultDTO<dynamic> arquivo = new ResultDTO<dynamic>();
        arquivo.Data = new { json = file, name = fileName };
        arquivo.Success = true;
        return Ok(arquivo);
    }

    public static byte[] ConverteStreamToByteArray(Stream stream)
    {
        byte[] byteArray = new byte[16 * 1024];
        using (MemoryStream mStream = new MemoryStream())
        {
            int bit;
            while ((bit = stream.Read(byteArray, 0, byteArray.Length)) > 0)
            {
                mStream.Write(byteArray, 0, bit);
            }
            return mStream.ToArray();
        }
    }

    [HttpPost("updaload"), DisableRequestSizeLimit]
    public async Task<IActionResult> ArquivoTarefa()
    {
        var formCollection = await Request.ReadFormAsync();
        var _file = formCollection.Files.First();

        if (_file == null)
            return BadRequest(Utils.RetornoMensagem<bool>.RetornoMensagemErro("Arquivo nao encontrado!"));

        var splitName = _file.FileName.Split("$_$");
        if (splitName.Length != 3)
            return BadRequest(Utils.RetornoMensagem<bool>.RetornoMensagemErro("Nome do arquivo formatado incorreto!"));

        int tarefaId = Convert.ToInt32(splitName[2]);
        var arquivo = new TarefaArquivoDTO(tarefaId, _file);
        var result = await _arquivoTarefaService.TarefaArquivo(arquivo);
        if (!result.Success)
            return BadRequest(result);


        var arquivoSalvo = _arquivoTarefaService.RecuperarPorNome(splitName[0]);


        byte[] data = new byte[64];
        Array.Clear(data, 0, data.Length);
        if (_file.Length > 0)
            using (var stream = new MemoryStream())
            {
                await _file.CopyToAsync(stream);
                data = stream.ToArray();
            }

        var path = Directory.GetCurrentDirectory();
        string folder = path + _configuration["pathArquivos"] + arquivoSalvo.Data?.Id.ToString();
        string _arquivo = folder + @"\" + splitName[0];

        if (!Directory.Exists(folder))
            Directory.CreateDirectory(folder);

        if (!System.IO.File.Exists(_arquivo))
            System.IO.File.Create(_arquivo);
        else
        {
            System.IO.File.Delete(_arquivo);
            System.IO.File.Create(_arquivo);
        }



        return Ok(result);
    }

}