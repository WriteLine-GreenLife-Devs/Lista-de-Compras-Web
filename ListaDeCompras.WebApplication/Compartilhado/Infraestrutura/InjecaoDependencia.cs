using ListaDeCompras.WebApplication.Compartilhado.Infraestrutura.Arquivos;

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

        // services.AddScoped<IRepositorioCaixa, RepositorioCaixaEmArquivo>();
        // services.AddScoped<IRepositorioRevista, RepositorioRevistaEmArquivo>();
        // services.AddScoped<IRepositorioAmigo, RepositorioAmigoEmArquivo>();
        // services.AddScoped<IRepositorioEmprestimo, RepositorioEmprestimoEmArquivo>();

        #endregion
    }
}
