using GestaoTarefas.Data.Configuration;
using GestaoTarefas.Data.Models;
using Microsoft.EntityFrameworkCore;
namespace GestaoTarefas.Data.Context;
public class DatabaseContext : DbContext
{
    public DatabaseContext() { }

    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

    public virtual DbSet<AgendamentoTarefas> AgendamentoTarefa { get; set; }
    public virtual DbSet<Usuarios> Usuario { get; set; }
    public virtual DbSet<TarefaArquivos> TarefaArquivo { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new AgendamentoTarefasConfiguration());
        modelBuilder.ApplyConfiguration(new UsuariosConfiguration());
        modelBuilder.ApplyConfiguration(new TarefaArquivosConfiguration());
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    }
}