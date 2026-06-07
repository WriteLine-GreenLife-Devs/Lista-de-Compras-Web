using AutoMapper;
using ListaDeCompras.WebApplication.ModuloCategoria.Apresentacao;
using ListaDeCompras.WebApplication.ModuloProduto.Apresentacao;
using ListaDeCompras.WebApplication.ModuloListasDeCompras.Apresentacao;

namespace ListaDeCompras.WebApplication.Compartilhado.Apresentacao;

public static class InjecaoDependencia
{
    public static void AddPresentation(this IServiceCollection services)
    {
        services.AddControllersWithViews().AddRazorOptions(options =>
        {
            // Reseta a configuração padrão do MVC
            options.ViewLocationFormats.Clear();

            // Localização das Views dos módulos: /ModuloCaixa/Apresentacao/Views/Listar.cshtml
            options.ViewLocationFormats.Add("/Modulo{1}/Apresentacao/Views/{0}.cshtml");

            // Localização das Views compartilhadas: /Compartilhado/Apresentacao/Views/_Layout.cshtml
            options.ViewLocationFormats.Add("/Compartilhado/Apresentacao/Views/{0}.cshtml");
        });

        services.AddAutoMapper(config =>
        {
            #region Adicionar Profiles

            config.AddProfile<CategoriaProfile>();
            config.AddProfile<ProdutoProfile>();
            config.AddProfile<ListasDeComprasProfile>();

            #endregion
        });
    }
}
