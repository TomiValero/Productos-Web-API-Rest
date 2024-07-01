using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProductosAPI.DTOs;
using ProductosAPI.Models;
using ProductosAPI.Repository;
using ProductosAPI.Services;
using ProductosAPI.Validators;

var builder = WebApplication.CreateBuilder(args);


//Services
builder.Services.AddKeyedScoped<ICommonService<ArticuloDto, ArticuloInsertDto, ArticuloUpdateDto>, ArticuloService>("articuloService");
builder.Services.AddKeyedScoped<ICommonService<Marca, Marca, Marca>, MarcaService>("marcaService");
builder.Services.AddKeyedScoped<ICommonService<Categoria, Categoria, Categoria>, CategoriaService>("categoriaService");

//Repository
builder.Services.AddScoped<IRepository<Articulo>, ArticuloRepository>();
builder.Services.AddScoped<IRepository<Marca>, MarcaRepository>();
builder.Services.AddScoped<IRepository<Categoria>, CategoriaRepository>();

//Entity Framework
//Conexion bd (appsettings.json)
builder.Services.AddDbContext<CatalogoP3DbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("StoreConnection"));
});

//Validators
builder.Services.AddScoped<IValidator<ArticuloInsertDto>, ArticuloInsertValidation>(); 
builder.Services.AddScoped<IValidator<ArticuloUpdateDto>, ArticuloUpdateValidation>();

builder.Services.AddKeyedScoped<IValidator<Marca>, MarcaInsertValidaton>("marcaInsertValidator");
builder.Services.AddKeyedScoped<IValidator<Marca>, MarcaUpdateValidaton>("marcaUpdateValidator");

builder.Services.AddKeyedScoped<IValidator<Categoria>, CategoriaInsertValidaton>("categoriaInsertValidator");
builder.Services.AddKeyedScoped<IValidator<Categoria>, CategoriaUpdateValidaton>("categoriaUpdateValidator");


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
