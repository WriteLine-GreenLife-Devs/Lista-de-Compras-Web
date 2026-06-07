namespace ListaDeCompras.WebApplication.ModuloListasDeCompras.Aplicacao;

public record ListarListasDeComprasDto(
    string Id,
    string Nome,
    DateTime DataCriacao,
    int ItensTotais,
    decimal GastoEstimado
);

public record CadastrarListasDeComprasDto(
    string Nome,
    DateTime DataCriacao
);

public record EditarListasDeComprasDto(
    string Id,
    string Nome,
    DateTime DataCriacao
);