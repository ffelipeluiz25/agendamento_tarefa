using GestaoTarefas.Data.DTOs;
namespace GestaoTarefas.Repositorios.Interfaces;
public interface IAgendamentoTarefaRepository
{
    Task<ResultDTO<bool>> Agendamento(AgendamentoDTO agendamento);
    Task<ResultDTO<bool>> AtualizaStatus(int status, int usuarioId);
    Task<ResultDTO<AgendamentoDTO>> BuscaTarefaPorUsuarioIdAndStatus(int usuarioId, int status);
    Task<ResultDTO<RecuperaPeriodoTempoDTO>> RecuperaPeriodoTempo(int tarefaId);
}