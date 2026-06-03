using System.ComponentModel.DataAnnotations;

namespace ListaDeCompras.WebApplication.ModuloCategoria.Apresentacao;

public record ListarCategoriasViewModel(
    string Id,
    string Nome,
    string Cor
);

public record CadastrarCategoriaViewModel(
    [Required(ErrorMessage = "O campo \"Nome\" deve ser preenchido.")]
[StringLength(100, ErrorMessage = "O campo \"Nome\" deve conter no máximo 100 caracteres.")]
string Nome,

    [Required(ErrorMessage = "O campo \"Cor\" deve ser preenchido.")]
[StringLength(50, ErrorMessage = "O campo \"Cor\" deve conter no máximo 50 caracteres.")]
string Cor
);

public record EditarCategoriaViewModel(
    string Id,

    [Required(ErrorMessage = "O campo \"Nome\" deve ser preenchido.")]
[StringLength(100, ErrorMessage = "O campo \"Nome\" deve conter no máximo 100 caracteres.")]
string Nome,

    [Required(ErrorMessage = "O campo \"Cor\" deve ser preenchido.")]
[StringLength(50, ErrorMessage = "O campo \"Cor\" deve conter no máximo 50 caracteres.")]
string Cor
);

public record ExcluirCategoriaViewModel(
    string Id,
    string Nome,
    string Cor
);
