using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.OData;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using Properties.API.Automapper;
using Properties.Application.Dto;
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


#region Odata
static IEdmModel GetModel()
{
    ODataConventionModelBuilder builder = new();
    builder.EntitySet<PropertyReadDto>("PropertyData");
    return builder.GetEdmModel();
}

void AddFormatters(IServiceCollection services)
{
    services.AddMvcCore(option =>
    {
        foreach (var outputFormatter in option.OutputFormatters.OfType<ODataOutputFormatter>().Where(_ => _.SupportedMediaTypes.Count == 0))
        {
            outputFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/prs.odatatestxx-odata"));
        }

        foreach (var inputFormatter in option.InputFormatters.OfType<ODataInputFormatter>().Where(_ => _.SupportedMediaTypes.Count == 0))
        {
            inputFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/prs.odatatestxx-odata"));
        }
    });
}

builder.Services.AddControllers().AddOData((op, df) =>
{
    op.Expand().Filter().OrderBy().Select().SetMaxTop(100);
    op.AddRouteComponents(GetModel());
});

AddFormatters(builder.Services);
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

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetPropertiesHandler).Assembly));

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
