namespace ListaDeCompras.WebApplication.ModuloListaDeCompras.Aplicacao;

public record ListarListasDeComprasDto(
    string Id,
    string Nome
);

public record CadastrarListasDeComprasDto(
    string Nome
);

public record EditarListasDeComprasDto(
    string Id,
    string Nome
);