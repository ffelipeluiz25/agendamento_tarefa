using GestaoTarefas.Data.Core;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace GestaoTarefas.Data.Models;
public class TarefaArquivos : BaseClass
{
    [Key]
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [Required]
    public int TarefaId { get; set; }
    [Required]
    public string NomeArquivo { get; set; }
    [Required]
    public string Extensao { get; set; }
    public AgendamentoTarefas AgendamentoTarefa { get; set; }
}