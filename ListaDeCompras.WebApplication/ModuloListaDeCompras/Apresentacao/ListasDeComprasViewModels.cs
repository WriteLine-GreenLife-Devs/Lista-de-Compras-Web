using System.ComponentModel.DataAnnotations;

namespace ListaDeCompras.WebApplication.ModuloListaDeCompras.Apresentacao;

public record ListarListasDeComprasViewModel(
    string Id,
    string Nome,
    DateTime DataCriacao,
    int ItensTotais,
    decimal GastoEstimado
);

public record CadastrarListasDeComprasViewModel(
    [Required(ErrorMessage = "O campo \"Nome\" deve ser preenchido.")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "O campo \"Nome\" deve ter entre 3 e 100 caracteres.")]
    string Nome
);

public record EditarListasDeComprasViewModel(
    string Id,
    [Required(ErrorMessage = "O campo \"Nome\" deve ser preenchido.")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "O campo \"Nome\" deve ter entre 3 e 100 caracteres.")]
    string Nome
);

public record ExcluirListasDeComprasViewModel(
    string Id,
    string Nome,
    DateTime DataCriacao,
    int ItensTotais,
    decimal GastoEstimado
);