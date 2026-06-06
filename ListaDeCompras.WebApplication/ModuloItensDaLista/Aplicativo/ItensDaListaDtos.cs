namespace ListaDeCompras.WebApplication.ModuloItensDaLista.Aplicacao;

public record ListarItensDaListaDto(
    string Id,
    string Nome,
    decimal Preco
);

public record CadastrarItensDaListaDto(
    string Nome,
    decimal Preco
);

public record EditarItensDaListaDto(
    string Id,
    string Nome,
    decimal Preco
);