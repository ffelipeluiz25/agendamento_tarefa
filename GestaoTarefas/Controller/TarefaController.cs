using GestaoTarefas.Data.DTOs;
using GestaoTarefas.Enumeradores;
using GestaoTarefas.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

namespace GestaoTarefas.Controller;
[ApiController]
[Route("tarefa")]
public class TarefaController : ControllerBase
{
    private readonly IAgendamentoTarefaService _agendamentoTarefaService;
    public TarefaController(IAgendamentoTarefaService agendamentoTarefaService)
    {
        _agendamentoTarefaService = agendamentoTarefaService;
    }

    [HttpGet("status")]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [SwaggerResponse(StatusCodes.Status200OK, "Recuperar status de agendamentos", typeof(ResultDTO<StatusAgendamentoDTO>))]
    [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ResultDTO<StatusAgendamentoDTO>))]
    public IActionResult RecuperaStatus()
    {
        var result = _agendamentoTarefaService.RecuperaStatus();
        if (!result.Success)
            return BadRequest(result);
        return Ok(result);
    }

    [HttpGet("recupera-periodo-tempo/{tarefaId}")]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [SwaggerResponse(StatusCodes.Status200OK, "Recuperar status de agendamentos", typeof(ResultDTO<RecuperaPeriodoTempoDTO>))]
    [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ResultDTO<RecuperaPeriodoTempoDTO>))]
    public async Task<IActionResult> RecuperaPeriodoTempo(int tarefaId)
    {
        var result = await _agendamentoTarefaService.RecuperaPeriodoTempo(tarefaId);
        if (!result.Success)
            return BadRequest(result);
        return Ok(result);
    }

    [HttpPost("agendamento")]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [SwaggerResponse(StatusCodes.Status200OK, "Criar agendamento", typeof(ResultDTO<bool>))]
    [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ResultDTO<bool>))]
    public async Task<IActionResult> Agendamento(AgendamentoDTO agendamento)
    {
        var result = await _agendamentoTarefaService.Agendamento(agendamento);
        if (!result.Success)
            return BadRequest(result);
        return Ok(result);
    }

    [HttpPost("atualiza-status")]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [SwaggerResponse(StatusCodes.Status200OK, "Atualizar status", typeof(ResultDTO<bool>))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ResultDTO<bool>))]
    public async Task<IActionResult> AtualizarStatus(AtualizaStatusDTO atualizaStatus)
    {
        var result = await _agendamentoTarefaService.AtualizarStatus(atualizaStatus);
        if (!result.Success)
            return BadRequest(result);

        return Ok(result);
    }
}