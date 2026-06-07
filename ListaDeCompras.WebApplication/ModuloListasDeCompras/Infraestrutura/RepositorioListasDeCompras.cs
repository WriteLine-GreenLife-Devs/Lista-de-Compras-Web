using ListaDeCompras.WebApplication.Compartilhado.Infraestrutura.Arquivos;
using ListaDeCompras.WebApplication.ModuloListasDeCompras.Dominio;

namespace ListaDeCompras.WebApplication.ModuloListasDeCompras.Infraestrutura;

public class RepositorioListasDeCompras : RepositorioBase<ListasDeCompras>, InterfaceRepositorioListasDeCompras
{
    public RepositorioListasDeCompras(Serializable serializable) : base(serializable) { }

    protected override List<ListasDeCompras> CarregarRegistros()
    {
        return serializable.ListasDeCompras;
    }
}