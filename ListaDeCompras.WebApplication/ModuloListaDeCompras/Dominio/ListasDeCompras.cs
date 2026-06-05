using ListaDeCompras.WebApplication.Compartilhado.Dominio;

namespace ListaDeCompras.WebApplication.ModuloListaDeCompras.Dominio;

public sealed class ListasDeCompras : EntidadeBase<ListasDeCompras>
{
    public string Nome { get; set; } = string.Empty;

    public ListasDeCompras() { }

    public ListasDeCompras(string nome)
    {
        Nome = nome;
    }

    public override List<string> Validar()
    {
        List<string> erros = new List<string>();

        if (string.IsNullOrWhiteSpace(Nome))
            erros.Add("O campo \"Nome\" deve ser preenchido.");

        return erros;
    }

    public override void Atualizar(ListasDeCompras entidadeAtualizada)
    {
        Nome = entidadeAtualizada.Nome;
    }
}