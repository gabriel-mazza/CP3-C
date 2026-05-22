using Microsoft.EntityFrameworkCore;
using FarmaciaDigital.Data;
using FarmaciaDigital.Interfaces;
using FarmaciaDigital.Repositories;
using FarmaciaDigital.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<FarmaciaContext>(options =>
    options.UseOracle(builder.Configuration.GetConnectionString("OracleFIAP")));

builder.Services.AddScoped<IFilialRepository, FilialRepository>();
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<IPlanoSaudeService, PlanoSaudeService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new()
    {
        Title = "Farmácia Digital API",
        Version = "v1",
        Description = "Backend da farmácia digital — FIAP 3ESR 2026. Produto: Plano de Saúde com mensalidade variável."
    });
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
