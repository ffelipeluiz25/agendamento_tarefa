using GestaoTarefas.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace GestaoTarefas.Data.Configuration;
public class AgendamentoTarefasConfiguration : IEntityTypeConfiguration<AgendamentoTarefas>
{
    public void Configure(EntityTypeBuilder<AgendamentoTarefas> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(f => f.Id).ValueGeneratedOnAdd();
        builder.HasOne(x => x.Usuario)
                 .WithMany(y => y.ListaAgendamentoTarefas)
                 .HasForeignKey(x => x.UsuarioId);
    }
}