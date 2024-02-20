using FluentValidation;
using FluentValidation.AspNetCore;
using HallOfFame.Domain.Entities;
using HallOfFame.Domain.Interfaces.Repositories;
using HallOfFame.Domain.Validators;
using HallOfFame.Infrastructure.DataBase;
using HallOfFame.Infrastructure.UnitOfWork;
using HallOfFame.Service.Business;
using HallOfFame.Service.Interfaces;
using HallOfFame.Utilities;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var databaseConnection = builder.Configuration.GetConnectionString("DbConnection");

// Add services to the container.
builder.Services.AddDbContext<Context>(options =>
    options.UseNpgsql(databaseConnection, b => b.MigrationsAssembly("HallOfFame")));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IPersonService, PersonService>();
builder.Services.AddScoped<ISkillService, SkillService>();
builder.Services.AddScoped<IValidator<Person>, PersonValidator>();
builder.Services.AddScoped<IValidator<Skill>, SkillValidator>();

builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
