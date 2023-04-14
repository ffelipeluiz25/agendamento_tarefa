using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using GestaoTarefas.Enumeradores;
using GestaoTarefas.Data.Core;
namespace GestaoTarefas.Data.Models;
public class AgendamentoTarefas : BaseClass
{
    [Key]
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [Required]
    public DateTime DataInicio { get; set; }
    public DateTime? DataEmAndamento { get; set; }
    public DateTime? DataFinalizacao { get; set; }
    [Required]
    public int Duracao { get; set; }
    [Required]
    public int Status { get; set; }
    [Required]
    public int UsuarioId { get; set; }

    [NotMapped]
    public EnumStatusAgentamento eStatus { get; set; }
    [NotMapped]
    public Usuarios Usuario { get; set; }
    public List<TarefaArquivos> ListaTarefaArquivos { get; set; }
}