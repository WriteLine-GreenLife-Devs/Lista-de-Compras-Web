using ListaDeCompras.WebApplication.ModuloCategoria.Aplicacao;
using ListaDeCompras.WebApplication.ModuloProduto.Aplicacao;

namespace ListaDeCompras.WebApplication.Compartilhado.Aplicacao;

public static class InjecaoDependencia
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        #region AdicionarScopeds

        services.AddScoped<ServicoCategoria>();
        services.AddScoped<ServicoProduto>();

        #endregion
    }
}
