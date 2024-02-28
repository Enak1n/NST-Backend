using FluentValidation;
using FluentValidation.AspNetCore;
using HallOfFame.Domain.Interfaces.Repositories;
using HallOfFame.Infrastructure.DataBase;
using HallOfFame.Infrastructure.UnitOfWork;
using HallOfFame.Service.Business;
using HallOfFame.Service.Interfaces;
using HallOfFame.Utilities;
using HallOfFame.Utilities.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using NLog;
using NLog.Web;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
var databaseConnection = builder.Configuration.GetConnectionString("DbConnection");
LogManager.Setup().LoadConfigurationFromFile(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));

// Add services to the container.
builder.Services.AddDbContext<Context>(options =>
    options.UseNpgsql(databaseConnection, b => b.MigrationsAssembly("HallOfFame.Infrastructure")));

builder.Logging.ClearProviders();
builder.Host.UseNLog();

builder.Services.AddSingleton<ILoggerManager, LoggerManager>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IPersonService, PersonService>();

builder.Services.AddValidatorsFromAssembly(typeof(PersonValidator).Assembly);
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "HallOfFame API", Version = "v1" });
});

var app = builder.Build();

using (var serviceScope = app.Services.CreateScope())
{
    var dbContext = serviceScope.ServiceProvider.GetRequiredService<Context>();
    dbContext.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<ErrorHandlingMiddleware>();

app.MapControllers();

app.Run();