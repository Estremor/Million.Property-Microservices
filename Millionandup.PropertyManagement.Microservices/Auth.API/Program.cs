using Auth.Application.Handlers;
using Auth.Domain.Entities;
using Auth.Domain.Services;
using Auth.Domain.Services.Contracts;
using Auth.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Shared.Infrastructure.Base;
using Shared.Infrastructure.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
var dbSettings = new DbSettings();
builder.Configuration.Bind("DbSetting", dbSettings);
builder.Services.AddSingleton(dbSettings);

builder.Services
    .AddApplicationServices(typeof(UserConfiguration).Assembly)
    .AddTransient<ILoginDomainService, LoginDomainService>();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(LoginCommandHandler).Assembly));

builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();


#region Creacion de usurio de prueba
try
{
    IServiceScope scope = app.Services.CreateScope();
    IServiceProvider services = scope.ServiceProvider;
    var context = services.GetRequiredService<DbContext>();
    var entity = context.Set<User>();
    if (!entity.Any())
    {
        await entity.AddAsync(new User { UserName = "Million.property", Password = "M3110n" });
        await context.SaveChangesAsync();
    }
}
catch (Exception e)
{
    throw;
}
#endregion Creacion de usurio de prueba

app.Run();
