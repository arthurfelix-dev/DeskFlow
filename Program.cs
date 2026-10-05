using DeskFlow.API.Data;
using DeskFlow.API.Middlewares;
using DeskFlow.API.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configuração de logs
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// Configuração do banco de dados
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

// Registro dos Services
builder.Services.AddScoped<CategoriaService>();
builder.Services.AddScoped<ChamadoService>();
builder.Services.AddScoped<InteracaoService>();

// Controllers
builder.Services.AddControllers();

// OpenAPI
builder.Services.AddOpenApi();

var app = builder.Build();

// Middleware global de tratamento de exceções
app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();