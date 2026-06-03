using ListaDeCompras.WebApplication.Compartilhado.Dominio;

namespace ListaDeCompras.WebApplication.ModuloCategoria.Dominio;

public sealed class Categoria : EntidadeBase<Categoria>
{
    public string Nome { get; set; } = string.Empty;
    public string Cor { get; set; } = string.Empty;

    public Categoria()
    {

    }
    public Categoria(string nome, string cor)
    {
        Nome = nome;
        Cor = cor;
    }

    public override List<string> Validar()
    {
        List<string> erros = new List<string>();

        if (string.IsNullOrWhiteSpace(Nome))
            erros.Add("O campo \"Nome\" deve ser preenchido.");

        if (string.IsNullOrWhiteSpace(Cor))
            erros.Add("O campo \"Cor\" deve ser preenchido.");

        return erros;
    }

    public override void Atualizar(Categoria entidadeAtualizada)
    {
        Nome = entidadeAtualizada.Nome;
        Cor = entidadeAtualizada.Cor;
    }
}
