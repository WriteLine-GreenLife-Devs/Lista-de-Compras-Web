using System.ComponentModel.DataAnnotations;

namespace ListaDeCompras.WebApplication.ModuloProduto.Apresentacao;

public record ListarProdutosViewModel(
    string Id,
    string Nome,
    string UnidadeDeMedida,
    float Preco,
    string CategoriaNome
);

public record CadastrarProdutoViewModel(
    [Required(ErrorMessage = "O campo \"Nome\" deve ser preenchido.")]
[StringLength(50, ErrorMessage = "O campo \"Nome\" deve conter no máximo 50 caracteres.")]
string Nome,

    [Required(ErrorMessage = "O campo \"Unidade de Medida\" deve ser preenchido.")]
[StringLength(50, ErrorMessage = "O campo \"Unidade de Medida\" deve conter no máximo 50 caracteres.")]
string UnidadeDeMedida,

    [Required(ErrorMessage = "O campo \"Preço\" deve ser preenchido.")]
float Preco,

    [Required(ErrorMessage = "O campo \"Categoria\" deve ser selecionado.")]
string CategoriaId
);

public record EditarProdutoViewModel(
    string Id,

    [Required(ErrorMessage = "O campo \"Nome\" deve ser preenchido.")]
[StringLength(100, ErrorMessage = "O campo \"Nome\" deve conter no máximo 100 caracteres.")]
string Nome,

    [Required(ErrorMessage = "O campo \"Unidade de Medida\" deve ser preenchido.")]
[StringLength(50, ErrorMessage = "O campo \"Unidade de Medida\" deve conter no máximo 50 caracteres.")]
string UnidadeDeMedida,

    [Required(ErrorMessage = "O campo \"Preço\" deve ser preenchido.")]
float Preco,

    [Required(ErrorMessage = "O campo \"Categoria\" deve ser selecionado.")]
string CategoriaId
);

public record ExcluirProdutoViewModel(
    string Id,
    string Nome,
    string UnidadeDeMedida,
    float Preco,
    string CategoriaNome
);
