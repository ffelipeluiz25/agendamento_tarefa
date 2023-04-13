using GestaoTarefas.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace GestaoTarefas.Data.Configuration;
public class TarefaArquivosConfiguration : IEntityTypeConfiguration<TarefaArquivos>
{
    public void Configure(EntityTypeBuilder<TarefaArquivos> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(f => f.Id).ValueGeneratedOnAdd();
    }
}