
public record ListarProdutosDto(
    string Id,
    string Nome,
    string UnidadeDeMedida,
    float Preco,
    string CategoriaId,
    string CategoriaNome
);

public record CadastrarProdutoDto(
    string Nome,
    string UnidadeDeMedida,
    float Preco,
    string CategoriaId
);

public record EditarProdutoDto(
    string Id,
    string Nome,
    string UnidadeDeMedida,
    float Preco,
    string CategoriaId
);
