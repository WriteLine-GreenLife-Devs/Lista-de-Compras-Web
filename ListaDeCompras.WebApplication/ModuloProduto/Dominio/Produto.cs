using ListaDeCompras.WebApplication.Compartilhado.Dominio;

public sealed class Produto : EntidadeBase<Produto>
{
    public string Nome { get; set; } = string.Empty;
    public string UnidadeDeMedida { get; set; } = string.Empty;
    public float Preco { get; set; } = 0;
    public string CategoriaId { get; set; } = string.Empty;

    public Produto()
    {

    }

    public Produto(string nome, string unidadeDeMedida, float preco, string categoriaId)
    {
        Nome = nome;
        UnidadeDeMedida = unidadeDeMedida;
        Preco = preco;
        CategoriaId = categoriaId;
    }

    public override List<string> Validar()
    {
        List<string> erros = new List<string>();

        if (string.IsNullOrWhiteSpace(Nome))
            erros.Add("O campo \"Nome\" deve ser preenchido.");

        if (string.IsNullOrWhiteSpace(UnidadeDeMedida))
            erros.Add("O campo \"Unidade de Medida\" deve ser preenchido.");

        if (Preco <= 0)
            erros.Add("O campo \"Preço\" deve ser um valor positivo.");

        if (string.IsNullOrWhiteSpace(CategoriaId))
            erros.Add("O campo \"Categoria\" deve ser selecionado.");

        return erros;
    }

    public override void Atualizar(Produto entidadeAtualizada)
    {
        Nome = entidadeAtualizada.Nome;
        UnidadeDeMedida = entidadeAtualizada.UnidadeDeMedida;
        Preco = entidadeAtualizada.Preco;
        CategoriaId = entidadeAtualizada.CategoriaId;
    }
}
