using System.ComponentModel.DataAnnotations;

namespace ListaDeCompras.WebApplication.ModuloItensDaLista.Apresentacao;

public record ListarItensDaListaViewModel(
    string Id,
    string ProdutoNome,
    string ListaNome,
    int Quantidade,
    decimal PrecoUnitario,
    decimal ValorTotal
);

public record CadastrarItensDaListaViewModel(
    [Required(ErrorMessage = "O campo \"Produto\" deve ser selecionado.")]
    string ProdutoId,

    [Required(ErrorMessage = "O campo \"Lista\" deve ser selecionada.")]
    string ListaId,

    [Range(1, int.MaxValue, ErrorMessage = "A quantidade deve ser maior que zero.")]
    int Quantidade,

    [Range(0.01, double.MaxValue, ErrorMessage = "O preço deve ser maior que zero.")]
    decimal PrecoUnitario
);

public record EditarItensDaListaViewModel(
    string Id,

    [Required(ErrorMessage = "O campo \"Produto\" deve ser selecionado.")]
    string ProdutoId,

    [Required(ErrorMessage = "O campo \"Lista\" deve ser selecionada.")]
    string ListaId,

    [Range(1, int.MaxValue, ErrorMessage = "A quantidade deve ser maior que zero.")]
    int Quantidade,

    [Range(0.01, double.MaxValue, ErrorMessage = "O preço deve ser maior que zero.")]
    decimal PrecoUnitario
);

public record ExcluirItensDaListaViewModel(
    string Id,
    string ProdutoNome,
    string ListaNome,
    int Quantidade,
    decimal PrecoUnitario,
    decimal ValorTotal
);