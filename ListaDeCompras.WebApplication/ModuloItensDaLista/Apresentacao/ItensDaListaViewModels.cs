using System.ComponentModel.DataAnnotations;

namespace ListaDeCompras.WebApplication.ModuloItensDaLista.Apresentacao;

public record ListarItensDaListaViewModel(
    string Id,
    string Nome,
    decimal Preco
);

public record CadastrarItensDaListaViewModel(
    [Required(ErrorMessage = "O campo \"Nome\" deve ser preenchido.")]
[StringLength(100, ErrorMessage = "O campo \"Nome\" deve conter no máximo 100 caracteres.")]
string Nome
);

public record EditarItensDaListaViewModel(
    string Id,

    [Required(ErrorMessage = "O campo \"Nome\" deve ser preenchido.")]
[StringLength(100, ErrorMessage = "O campo \"Nome\" deve conter no máximo 100 caracteres.")]
string Nome
);

public record ExcluirItensDaListaViewModel(
    string Id,
    string Nome,
    decimal Preco
);