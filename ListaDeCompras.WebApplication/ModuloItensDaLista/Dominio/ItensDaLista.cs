using ListaDeCompras.WebApplication.Compartilhado.Dominio;

namespace ListaDeCompras.WebApplication.ModuloItensDaLista.Dominio;

public sealed class ItensDaLista : EntidadeBase<ItensDaLista>
{
    public string Nome { get; set; } = string.Empty;
    public decimal Preco { get; set; } = 0;

    public ItensDaLista() { }

    public ItensDaLista(string nome, decimal preco)
    {
        Nome = nome;
        Preco = preco;
    }

    public override List<string> Validar()
    {
        List<string> erros = new List<string>();

        if (string.IsNullOrWhiteSpace(Nome))
            erros.Add("O campo \"Nome\" deve ser preenchido.");

        if (Preco == 0)
            erros.Add("O campo \"Preço\" deve ser preenchido.");

        return erros;
    }

    public override void Atualizar(ItensDaLista entidadeAtualizada)
    {
        Nome = entidadeAtualizada.Nome;
        Preco = entidadeAtualizada.Preco;
    }
}