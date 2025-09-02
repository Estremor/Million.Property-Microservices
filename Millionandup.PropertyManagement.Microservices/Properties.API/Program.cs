using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Properties.API.Automapper;
using Properties.Application.Handlers;
using Properties.Domain.Services;
using Properties.Domain.Services.Contracts;
using Properties.Infrastructure;
using Shared.Infrastructure.Base;
using Shared.Infrastructure.Middleware;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<AutoMapperProfile>();
});

// JWT Authentication
var securityKey = builder.Configuration["SecurityKey"];
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey)),
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };
    });

#region FluentValidation
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);
#endregion

// Database configuration
var dbSettings = new DbSettings();
builder.Configuration.Bind("DbSetting", dbSettings);
builder.Services.AddSingleton(dbSettings);
builder.Services.AddApplicationServices(typeof(OwnerConfiguration).Assembly);

#region Domain
builder.Services.AddTransient<IOwnerDomainService, OwnerDomainService>();
builder.Services.AddTransient<IPropertyDomainService, PropertyDomainService>();
builder.Services.AddTransient<IPropertyImageDomainService, PropertyImageDomainService>();
builder.Services.AddTransient<IPropertyTraceDomainService, PropertyTraceDomainService>();
#endregion

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreatePropertyHandler).Assembly));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(UpdatePropertyHandler).Assembly));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateImageHandler).Assembly));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateOwnerHandler).Assembly));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(UpdatePropertyPriceHandler).Assembly));

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

app.Run();
