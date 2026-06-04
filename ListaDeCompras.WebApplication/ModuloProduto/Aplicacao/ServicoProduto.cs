using FluentResults;
using ListaDeCompras.WebApplication.ModuloCategoria.Dominio;
using ListaDeCompras.WebApplication.ModuloProduto.Dominio;

namespace ListaDeCompras.WebApplication.ModuloProduto.Aplicacao;

public class ServicoProduto
{
    private readonly InterfaceRepositorioProduto repositorioProduto;
    private readonly InterfaceRepositorioCategoria repositorioCategoria;

    public ServicoProduto(InterfaceRepositorioProduto repositorioProduto, InterfaceRepositorioCategoria repositorioCategoria)
    {
        this.repositorioProduto = repositorioProduto;
        this.repositorioCategoria = repositorioCategoria;
    }

    public Result Cadastrar(CadastrarProdutoDto dto)
    {
        Produto novoProduto = new Produto(
            dto.Nome,
            dto.UnidadeDeMedida,
            dto.Preco,
            dto.CategoriaId
        );

        List<string> erros = novoProduto.Validar();

        if (string.IsNullOrWhiteSpace(dto.CategoriaId) || repositorioCategoria.SelecionarPorId(dto.CategoriaId) == null)
            erros.Add("O campo \"Categoria\" deve ser selecionado.");

        if (VerificarNomeDuplicado(dto.Nome))
            erros.Add("Já existe um produto com este nome.");

        if (erros.Any())
            return RetornarErros(erros);

        repositorioProduto.Cadastrar(novoProduto);

        return Result.Ok();
    }

    public Result Editar(EditarProdutoDto dto)
    {
        Produto produtoAtualizado = new Produto(
            dto.Nome,
            dto.UnidadeDeMedida,
            dto.Preco,
            dto.CategoriaId
        );

        List<string> erros = produtoAtualizado.Validar();

        if (string.IsNullOrWhiteSpace(dto.CategoriaId) || repositorioCategoria.SelecionarPorId(dto.CategoriaId) == null)
            erros.Add("O campo \"Categoria\" deve ser selecionado.");

        if (VerificarNomeDuplicado(dto.Nome, dto.Id))
            erros.Add("Já existe um produto com este nome.");

        if (erros.Any())
            return RetornarErros(erros);

        repositorioProduto.Editar(dto.Id, produtoAtualizado);

        return Result.Ok();
    }

    public Result Excluir(string id)
    {
        repositorioProduto.Excluir(id);

        return Result.Ok();
    }

    public List<ListarProdutosDto> SelecionarTodos()
    {
        List<Produto> produtos = repositorioProduto.SelecionarTodos();

        return produtos.Select(p => new ListarProdutosDto(
            p.Id,
            p.Nome,
            p.UnidadeDeMedida,
            p.Preco,
            p.CategoriaId,
            ObterNomeCategoria(p.CategoriaId)
        )).ToList();
    }

    public Result<ListarProdutosDto> SelecionarPorId(string id)
    {
        Produto? produto = repositorioProduto.SelecionarPorId(id);

        if (produto == null)
            return Result.Fail("Produto não encontrado.");

        return Result.Ok(new ListarProdutosDto(
            produto.Id,
            produto.Nome,
            produto.UnidadeDeMedida,
            produto.Preco,
            produto.CategoriaId,
            ObterNomeCategoria(produto.CategoriaId)
        ));
    }

    private bool VerificarNomeDuplicado(string nome, string? idIgnorado = null)
    {
        List<Produto> produtos = repositorioProduto.SelecionarTodos();

        foreach (Produto p in produtos)
        {
            if (p.Id != idIgnorado && string.Equals(p.Nome, nome, StringComparison.OrdinalIgnoreCase))
                return true;
        }

        return false;
    }

    private string ObterNomeCategoria(string categoriaId)
    {
        Categoria? categoria = repositorioCategoria.SelecionarPorId(categoriaId);
        return categoria?.Nome ?? string.Empty;
    }

    private static Result RetornarErros(List<string> erros)
    {
        List<IError> listaErros = erros.Select(e => new Error(e)).ToList<IError>();

        return Result.Fail(listaErros);
    }
}
