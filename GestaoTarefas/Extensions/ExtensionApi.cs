using GestaoTarefas.Data.Context;
using GestaoTarefas.Mapper;
using Microsoft.EntityFrameworkCore;
namespace GestaoTarefas.Extensions;
public static class ExtensionApi
{
    public static IServiceCollection ConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<DatabaseContext>(options =>
        options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                          o => o.MigrationsAssembly("GestaoTarefas")));
        return services;
    }
    public static IServiceCollection ConfigureMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(cfg => MapperConfigFactory.MapProfiles(cfg));

        return services;
    }
    public static IServiceCollection ConfigureInjections(this IServiceCollection services)
    {
        //services.AddScoped<IAccountJpmService, AccountJpmService>();

        return services;
    }
}