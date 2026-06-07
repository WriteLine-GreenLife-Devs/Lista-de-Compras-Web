namespace ListaDeCompras.WebApplication.ModuloItensDaLista.Aplicacao;

public record ListarItensDaListaDto(
    string Id,
    string ProdutoId,
    string ListaId,
    int Quantidade,
    decimal PrecoUnitario,
    decimal ValorTotal
);

public record CadastrarItensDaListaDto(
    string ProdutoId,
    string ListaId,
    int Quantidade,
    decimal PrecoUnitario
);

public record EditarItensDaListaDto(
    string Id,
    string ProdutoId,
    string ListaId,
    int Quantidade,
    decimal PrecoUnitario
);