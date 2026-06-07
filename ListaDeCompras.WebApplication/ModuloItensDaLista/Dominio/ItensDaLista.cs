using ListaDeCompras.WebApplication.Compartilhado.Dominio;

namespace ListaDeCompras.WebApplication.ModuloItensDaLista.Dominio;

public sealed class ItensDaLista : EntidadeBase<ItensDaLista>
{
    public string ProdutoId { get; set; } = string.Empty;
    public string ListaId { get; set; } = string.Empty;
    public int Quantidade { get; set; } = 0;
    public decimal PrecoUnitario { get; set; } = 0;
    public decimal ValorTotal { get; private set; } = 0;

    public ItensDaLista() { }

    public ItensDaLista(string produtoId, string listaId, int quantidade, decimal precoUnitario)
    {
        ProdutoId = produtoId;
        ListaId = listaId;
        Quantidade = quantidade;
        PrecoUnitario = precoUnitario;
        CalcularValorTotal();
    }

    private void CalcularValorTotal()
    {
        ValorTotal = PrecoUnitario * Quantidade;
    }

    public override List<string> Validar()
    {
        List<string> erros = new List<string>();

        if (string.IsNullOrWhiteSpace(ProdutoId))
            erros.Add("O campo \"Produto\" deve ser preenchido.");

        if (string.IsNullOrWhiteSpace(ListaId))
            erros.Add("O item deve estar vinculado a uma lista de compras.");

        if (Quantidade <= 0)
            erros.Add("A \"Quantidade\" deve ser maior que zero.");

        if (PrecoUnitario <= 0)
            erros.Add("O campo \"Preço\" deve ser preenchido e maior que zero.");

        return erros;
    }

    public override void Atualizar(ItensDaLista entidadeAtualizada)
    {
        ProdutoId = entidadeAtualizada.ProdutoId;
        ListaId = entidadeAtualizada.ListaId;
        Quantidade = entidadeAtualizada.Quantidade;
        PrecoUnitario = entidadeAtualizada.PrecoUnitario;
        CalcularValorTotal();
    }
}