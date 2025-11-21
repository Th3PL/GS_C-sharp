using Business;
using Data;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Connection string em appsettings.json → "ConnectionStrings:Default"
var connectionString = builder.Configuration.GetConnectionString("Default");

// 2) Registrar o DbContext com Oracle
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseOracle(connectionString));

// 3) Registrar suas services (Scoped é o padrão recomendado para DbContext)
builder.Services.AddScoped<IGestorService, GestorService>();
builder.Services.AddScoped<IFuncionarioService, FuncionarioService>();


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
