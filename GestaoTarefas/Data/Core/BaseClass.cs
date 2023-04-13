using System.ComponentModel.DataAnnotations;
namespace GestaoTarefas.Data.Core
{
    public abstract class BaseClass
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public DateTime DataCriacao { get; set; }
    }
}