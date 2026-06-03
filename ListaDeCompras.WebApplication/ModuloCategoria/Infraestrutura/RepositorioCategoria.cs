using ListaDeCompras.WebApplication.Compartilhado.Infraestrutura.Arquivos;
using ListaDeCompras.WebApplication.ModuloCategoria.Dominio;

namespace ListaDeCompras.WebApplication.ModuloCategoria.Infraestrutura;

public class RepositorioCategoria : RepositorioBase<Categoria>, InterfaceRepositorioCategoria
{
    public RepositorioCategoria(Serializable serializable) : base(serializable) { }

    protected override List<Categoria> CarregarRegistros()
    {
        return serializable.Categorias;
    }
}
