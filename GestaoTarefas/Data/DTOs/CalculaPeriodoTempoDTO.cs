namespace GestaoTarefas.Data.DTOs;
public class CalculaPeriodoTempoDTO
{
    public CalculaPeriodoTempoDTO(DateTime dataEmAndamento, DateTime dataFinalizacao)
    {
        DataEmAndamento = dataEmAndamento;
        DataFinalizacao = dataFinalizacao;

        TimeSpan date = Convert.ToDateTime(dataFinalizacao) - Convert.ToDateTime(dataEmAndamento);

        Dias = date.Days;
        Horas = date.Hours;
        Minutos = date.Minutes;
    }
    public DateTime DataEmAndamento { get; set; }
    public DateTime DataFinalizacao { get; set; }
    public int Dias { get; set; }
    public int Horas { get; set; }
    public int Minutos { get; set; }
    public string Retorno()
    {
        var retorno = $"Tarefa esteve no total de ";
        if (Dias > 0) { retorno += $"{Dias} dias "; }
        if (Horas > 0) { retorno += $"{Horas} horas "; }
        if (Minutos > 0) { retorno += $"{Minutos} minutos "; }

        retorno += $"em andamento";
        return retorno;
    }
}