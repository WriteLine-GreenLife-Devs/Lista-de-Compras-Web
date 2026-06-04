using ListaDeCompras.WebApplication.Compartilhado.Infraestrutura.Arquivos;
using ListaDeCompras.WebApplication.ModuloProduto.Dominio;

namespace ListaDeCompras.WebApplication.ModuloProduto.Infraestrutura;

public class RepositorioProduto : RepositorioBase<Produto>, InterfaceRepositorioProduto
{
    public RepositorioProduto(Serializable serializable) : base(serializable) { }

    protected override List<Produto> CarregarRegistros()
    {
        return serializable.Produtos;
    }
}
