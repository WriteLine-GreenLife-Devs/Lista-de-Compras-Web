using ListaDeCompras.WebApplication.Compartilhado.Infraestrutura.Arquivos;
using ListaDeCompras.WebApplication.ModuloListaDeCompras.Dominio;

namespace ListaDeCompras.WebApplication.ModuloListaDeCompras.Infraestrutura;

public class RepositorioListasDeCompras : RepositorioBase<ListasDeCompras>, InterfaceRepositorioListasDeCompras
{
    public RepositorioListasDeCompras(Serializable serializable) : base(serializable) { }

    protected override List<ListasDeCompras> CarregarRegistros()
    {
        return serializable.ListasDeCompras;
    }
}