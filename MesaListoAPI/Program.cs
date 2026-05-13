using MesaListo.Application.Interfaces;
using MesaListo.Application.Services;
using MesaListo.Infrastructure.DependencyInjection; //Inyección de dependencias para la capa de infraestructura


var builder = WebApplication.CreateBuilder(args);
const string CorsPolicyName = "MesaListoFrontendPolicy";

// Add services to the container.
builder.Services.AddControllers();

// Configuración de CORS para permitir solicitudes desde el frontend (Angular)
builder.Services.AddCors(options =>
{
    options.AddPolicy(CorsPolicyName, policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});


// Swagger / OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddInfrastructure(); // Agrega los servicios de infraestructura, incluyendo SqlConnectionFactory
builder.Services.AddScoped<AuthService>(); // Agrega el servicio de aplicación para manejar la lógica de negocio relacionada con la autenticación y gestión de usuarios
builder.Services.AddScoped<IPasswordService, PasswordService>(); // Agrega el servicio de aplicación para manejar la lógica de negocio relacionada con el hashing y validación de contraseñas, utilizando la implementación PasswordService
builder.Services.AddScoped<JuegoService>(); // Agrega el servicio de aplicación para manejar la lógica de negocio relacionada con los juegos
builder.Services.AddScoped<ComunidadService>(); // Agrega el servicio de aplicación para manejar la lógica de negocio relacionada con las comunidades
builder.Services.AddScoped<NoticiaService>(); // Agrega el servicio de aplicación para manejar la lógica de negocio relacionada con las noticias
builder.Services.AddScoped<ReplicaService>();// Agrega el servicio de aplicación para manejar la lógica de negocio relacionada con las réplicas
builder.Services.AddScoped<EventoService>(); // Agrega el servicio de aplicación para manejar la lógica de negocio relacionada con los eventos

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(CorsPolicyName);

app.UseAuthorization();

app.MapControllers();

app.Run();