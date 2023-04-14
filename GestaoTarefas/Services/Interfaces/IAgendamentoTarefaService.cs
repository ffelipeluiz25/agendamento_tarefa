using GestaoTarefas.Data.DTOs;
namespace GestaoTarefas.Services.Interfaces;
public interface IAgendamentoTarefaService
{
    Task<ResultDTO<bool>> Agendamento(AgendamentoDTO agendamento);
    Task<ResultDTO<bool>> AtualizarStatus(AtualizaStatusDTO atualizaStatus);
    ResultDTO<StatusAgendamentoDTO> RecuperaStatus();
}