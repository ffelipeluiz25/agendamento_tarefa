using GestaoTarefas.Data.DTOs;
using GestaoTarefas.Enumeradores;
using GestaoTarefas.Repositorios.Interfaces;
using GestaoTarefas.Services.Interfaces;
namespace GestaoTarefas.Services;
public class AgendamentoTarefaService : IAgendamentoTarefaService
{
    private readonly IAgendamentoTarefaRepository _agendamentoTarefeRepositorio;
    public AgendamentoTarefaService(IAgendamentoTarefaRepository agendamentoTarefeRepositorio)
    {
        _agendamentoTarefeRepositorio = agendamentoTarefeRepositorio;
    }

    public async Task<ResultDTO<bool>> Agendamento(AgendamentoDTO agendamento)
    {
        return await _agendamentoTarefeRepositorio.Agendamento(agendamento);
    }

    public async Task<ResultDTO<bool>> AtualizarStatus(AtualizaStatusDTO atualizaStatus)
    {
        switch (atualizaStatus.Status)
        {
            case (int)EnumStatusAgentamento.Agendada:
                {
                    var tarefa = await _agendamentoTarefeRepositorio.BuscaTarefaPorUsuarioIdAndStatus(atualizaStatus.UsuarioId, (int)EnumStatusAgentamento.Agendada);
                    if (tarefa.Success && tarefa.Data != null) return Utils.RetornoMensagem<bool>.RetornoMensagemErro("Usuario tem ja tarefas agendadas!");

                    return Utils.RetornoMensagem<bool>.RetornoMensagemErro("Não pode ser atualizado o status do agendamento!");
                }
            case (int)EnumStatusAgentamento.EmAndamento:
                {
                    var tarefa = await _agendamentoTarefeRepositorio.BuscaTarefaPorUsuarioIdAndStatus(atualizaStatus.UsuarioId, (int)EnumStatusAgentamento.Agendada);
                    if (tarefa == null || tarefa.Data == null) return Utils.RetornoMensagem<bool>.RetornoMensagemErro("Usuario não tem tarefas agendadas para trocar para em andamento!");
                    break;
                }
            case (int)EnumStatusAgentamento.Finalizada:
                {
                    var tarefa = await _agendamentoTarefeRepositorio.BuscaTarefaPorUsuarioIdAndStatus(atualizaStatus.UsuarioId, (int)EnumStatusAgentamento.Agendada);
                    if (tarefa.Success && tarefa.Data != null) return Utils.RetornoMensagem<bool>.RetornoMensagemErro("Usuario tem tarefas agendadas e nao pode ser finalizada!");

                    tarefa = await _agendamentoTarefeRepositorio.BuscaTarefaPorUsuarioIdAndStatus(atualizaStatus.UsuarioId, (int)EnumStatusAgentamento.EmAndamento);
                    if (tarefa.Success && tarefa.Data == null) return Utils.RetornoMensagem<bool>.RetornoMensagemErro("Usuario não tem tarefas em andamento para ser finalizada!");

                    break;
                }
            default:
                {
                    return Utils.RetornoMensagem<bool>.RetornoMensagemErro("Status inexistente para ser atualizado!");
                }
        }

        await _agendamentoTarefeRepositorio.AtualizaStatus(atualizaStatus.Status, atualizaStatus.UsuarioId);
        return Utils.RetornoMensagem<bool>.RetornoMensagemSucesso(true);
    }

    public async Task<ResultDTO<RecuperaPeriodoTempoDTO>> RecuperaPeriodoTempo(int tarefaId)
    {
        return await _agendamentoTarefeRepositorio.RecuperaPeriodoTempo(tarefaId);
    }

    public ResultDTO<StatusAgendamentoDTO> RecuperaStatus()
    {
        var result = new StatusAgendamentoDTO();
        result.Agendada = (int)EnumStatusAgentamento.Agendada;
        result.EmAndamento = (int)EnumStatusAgentamento.EmAndamento;
        result.Finalizada = (int)EnumStatusAgentamento.Finalizada;
        return Utils.RetornoMensagem<StatusAgendamentoDTO>.RetornoMensagemSucesso(result);
    }
}