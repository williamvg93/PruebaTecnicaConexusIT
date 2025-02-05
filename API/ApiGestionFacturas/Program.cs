using System.Reflection;
using ApiGestionFacturas;
using ApiGestionFacturas.Extensions;
using AspNetCoreRateLimit;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Agregando los controlodores al contenedor de dependencias
builder.Services.AddControllers();

// Agregando los cors y sus configuraciones al contenedor de dependencias
builder.Services.ConfigureCors();

// Agregando el rateLimit y sus configuraciones al contenedor de dependencias
builder.Services.ConfigureRatelimiting();

// Agregando el AutoMapper para los Dtos al contenedor de dependencias y expesificando el metodo de busqueda de Dtos
builder.Services.AddAutoMapper(Assembly.GetEntryAssembly());

// Agregando otras dependencias necesarias como la UnitOfWork al contenedor de dependencias
builder.Services.AddAplicationServices();

// Agregando la conexion a la base de datos y sus credenciales de acceso al contenedor de dependencias
builder.Services.AddDbContext<ApiGestionFacturasContext>(options => {
    string connectionStrings = builder.Configuration.GetConnectionString("SqlServerConne");
    options.UseSqlServer(connectionStrings);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Utilizando el rateLimiting
app.UseIpRateLimiting();

// Mapeando los controladores y activando sus rutas 
app.MapControllers();

// Habilitar los cors como fueron configurados(CorsPolicy) en la app
app.UseCors("CorsPolicy");


app.Run();

