namespace ListaDeCompras.WebApplication.ModuloListaDeCompras.Aplicacao;

public record ListarListasDeComprasDto(
    string Id,
    string Nome,
    DateTime DataCriacao
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