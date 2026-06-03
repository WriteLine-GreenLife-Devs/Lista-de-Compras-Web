using ListaDeCompras.WebApplication.Compartilhado.Infraestrutura.Arquivos;
using ListaDeCompras.WebApplication.ModuloCategoria.Dominio;
using ListaDeCompras.WebApplication.ModuloCategoria.Infraestrutura;

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
        // services.AddScoped<IRepositorioRevista, RepositorioRevistaEmArquivo>();
        // services.AddScoped<IRepositorioAmigo, RepositorioAmigoEmArquivo>();
        // services.AddScoped<IRepositorioEmprestimo, RepositorioEmprestimoEmArquivo>();

        #endregion
    }
}
