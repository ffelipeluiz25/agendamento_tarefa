using GestaoTarefas.Data.Context;
using GestaoTarefas.Extensions;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.ConfigureDatabase(builder.Configuration);
builder.Services.ConfigureInjections();
builder.Services.ConfigureMapper();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Agendamento Tarefa", Version = "v1" });
    c.CustomSchemaIds(x => x.FullName);
});

builder.Services.AddCors(delegate (CorsOptions options)
{
    options.AddPolicy("development", delegate (CorsPolicyBuilder builder)
    {
        builder.SetIsOriginAllowed((string origin) => new Uri(origin).Host == "localhost" || new Uri(origin).Host.EndsWith(".bmgmoney.com", StringComparison.OrdinalIgnoreCase))
        .WithMethods(new string[5] { "GET", "POST", "PUT", "DELETE", "OPTIONS" })
        .WithHeaders(new string[14]
            {
                "x-request-id", "X-Requested-With", "Accept", "Content-Type", "Origin", "content-type", "use_block_ui", "use_progress_bar", "authorization", "Authorization",
                "Content-Encoding", "reportProgress", "observe", "Cookie"
            })
            .WithExposedHeaders(new string[1] { "X-Token" })
            .AllowCredentials()
            .Build();
    });
});


var app = builder.Build();

app.UseCors("development");
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("v1/swagger.json", "Agendamento Tarefa");
});

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
    db.Database.Migrate();
}
app.UseAuthorization();
app.UseStaticFiles();
app.MapControllers();
app.Run();