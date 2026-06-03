using ListaDeCompras.WebApplication.Compartilhado.Aplicacao;
using ListaDeCompras.WebApplication.Compartilhado.Apresentacao;
using ListaDeCompras.WebApplication.Compartilhado.Infraestrutura;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfraRepositories();

builder.Services.AddApplicationServices();

builder.Services.AddPresentation();

var app = builder.Build();

// Configuração de Middlewares
app.UseStaticFiles();

app.UseRouting();
app.MapDefaultControllerRoute();

// Execução do Servidor
app.Run();
