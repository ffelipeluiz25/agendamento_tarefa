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
                    return Utils.RetornoMensagem<bool>.RetornoMensagemErro("Usuario não tem tarefas agendadas!");
                }
            case (int)EnumStatusAgentamento.EmAndamento:
                {
                    var tarefa = await _agendamentoTarefeRepositorio.BuscaTarefaPorUsuarioIdAndStatus(atualizaStatus.UsuarioId, (int)EnumStatusAgentamento.Agendada);
                    if (tarefa == null) return Utils.RetornoMensagem<bool>.RetornoMensagemErro("Usuario não tem tarefas agendadas para trocar para em andamento!");
                    break;
                }
            case (int)EnumStatusAgentamento.Finalizada:
                {
                    var tarefa = await _agendamentoTarefeRepositorio.BuscaTarefaPorUsuarioIdAndStatus(atualizaStatus.UsuarioId, (int)EnumStatusAgentamento.EmAndamento);
                    if (tarefa == null) return Utils.RetornoMensagem<bool>.RetornoMensagemErro("Usuario não tem tarefas em andamento para ser finalizada!");
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

    public ResultDTO<StatusAgendamentoDTO> RecuperaStatus()
    {
        var result = new StatusAgendamentoDTO();
        result.Agendada = (int)EnumStatusAgentamento.Agendada;
        result.EmAndamento = (int)EnumStatusAgentamento.EmAndamento;
        result.Finalizada = (int)EnumStatusAgentamento.Finalizada;
        return Utils.RetornoMensagem<StatusAgendamentoDTO>.RetornoMensagemSucesso(result);
    }
}