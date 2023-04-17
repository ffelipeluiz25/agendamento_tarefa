namespace GestaoTarefas.Data.DTOs;
public class AgendamentoTarefasDTO
{
    public int Id { get; set; }
    public int UsuarioId { get; set; }
    public DateTime DataInicio { get; set; }
    public DateTime? DataEmAndamento { get; set; }
    public DateTime? DataFinalizacao { get; set; }
    public int Duracao { get; set; }
    public string Status { get; set; }
    public string Usuario { get; set; }
}