using api_bibliotecaICL;
using api_bibliotecaICL.Models;
using api_bibliotecaICL.Repositorio;
using api_bibliotecaICL.Repositorio.IRepositorio;
using Api_Inventariobiblioteca.Models;
using Api_Inventariobiblioteca.Repositorio;
using Api_Inventariobiblioteca.Repositorio.IRepositorio;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DatabaseContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConectionSqlServer"));
});
builder.Services.AddCors(options =>
{
    options.AddPolicy("accesofrontend", app =>
    {
        app.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});
builder.Services.AddAutoMapper(typeof(MappingConfig));
builder.Services.AddScoped<IAutorRepositorio, AutorRepositorio>();
builder.Services.AddScoped<ITipoAutorRepositorio, TipoAutorRepositorio>();
builder.Services.AddScoped<ITipoLibroRepositorio, TipoLibroRepositorio>();
builder.Services.AddScoped<ILibroRepositorio, LibroRepositorio>();
builder.Services.AddScoped<ILibroxAutorRepositorio, LibroxAutorRepositorio>();
builder.Services.AddScoped<Ivlibrorepositorio, vlibrorepositorio>();
builder.Services.AddScoped<IvInventarioRepositorio, vInventarioRepositorio>();
builder.Services.AddScoped<IInventarioRepositorio, InventarioRepositorio>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("accesofrontend");

app.UseAuthorization();

app.MapControllers();

app.Run();
