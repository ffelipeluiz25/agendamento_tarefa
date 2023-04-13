using GestaoTarefas.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace GestaoTarefas.Data.Configuration;
public class UsuariosConfiguration : IEntityTypeConfiguration<Usuarios>
{
    public void Configure(EntityTypeBuilder<Usuarios> entity)
    {
        entity.HasKey(x => x.Id);
        entity.Property(f => f.Id).ValueGeneratedOnAdd();
    }
}