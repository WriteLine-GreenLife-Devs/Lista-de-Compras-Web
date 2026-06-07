using ListaDeCompras.WebApplication.Compartilhado.Infraestrutura.Arquivos;
using ListaDeCompras.WebApplication.ModuloItensDaLista.Dominio;

namespace ListaDeCompras.WebApplication.ModuloItensDaLista.Infraestrutura;

public class RepositorioItensDaLista : RepositorioBase<ItensDaLista>, InterfaceRepositorioItensDaLista
{
    public RepositorioItensDaLista(Serializable serializable) : base(serializable) { }

    protected override List<ItensDaLista> CarregarRegistros()
    {
        return serializable.ItensDaLista;
    }
}