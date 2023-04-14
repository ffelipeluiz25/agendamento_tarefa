using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using GestaoTarefas.Data.Core;
namespace GestaoTarefas.Data.Models;
public class Usuarios : BaseClass
{
    [Key]
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Sobrenome { get; set; }
    public  List<AgendamentoTarefas> ListaAgendamentoTarefas { get; set; }
}