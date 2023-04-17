namespace GestaoTarefas.Enumeradores;
public enum EnumStatusAgentamento
{
    Agendada = 0,
    EmAndamento = 1,
    Finalizada = 2
}

static class RecuperaStatus
{
    public static string GetStatusAgendamento(int status)
    {
        switch (status)
        {
            case (int)EnumStatusAgentamento.Agendada:
                return "Agendada";
            case (int)EnumStatusAgentamento.EmAndamento:
                return "Em Andamento";
            case (int)EnumStatusAgentamento.Finalizada:
                return "Finalizada";
        }
        return "Agendada";
    }
}