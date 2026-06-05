using ListaDeCompras.WebApplication.Compartilhado.Dominio;
using ListaDeCompras.WebApplication.Compartilhado.Infraestrutura.Arquivos;
using ListaDeCompras.WebApplication.ModuloCategoria.Dominio;
using ListaDeCompras.WebApplication.ModuloCategoria.Infraestrutura;
using ListaDeCompras.WebApplication.ModuloListaDeCompras.Dominio;
using ListaDeCompras.WebApplication.ModuloListaDeCompras.Infraestrutura;
using ListaDeCompras.WebApplication.ModuloProduto.Dominio;
using ListaDeCompras.WebApplication.ModuloProduto.Infraestrutura;

namespace ListaDeCompras.WebApplication.Compartilhado.Infraestrutura;

public static class InjecaoDependencia
{
    public static void AddInfraRepositories(this IServiceCollection services)
    {
        services.AddScoped(provider =>
        {
            Serializable serializable = new Serializable();

            serializable.Carregar();

            return serializable;
        });

        #region AdicionarScopeds

        services.AddScoped<InterfaceRepositorioCategoria, RepositorioCategoria>();
        services.AddScoped<InterfaceRepositorioProduto, RepositorioProduto>();
        services.AddScoped<InterfaceRepositorioListasDeCompras, RepositorioListasDeCompras>();

        #endregion
    }
}
