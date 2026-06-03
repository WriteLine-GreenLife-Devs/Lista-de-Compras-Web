using ListaDeCompras.WebApplication.ModuloCategoria.Dominio;
using FluentResults;

namespace ListaDeCompras.WebApplication.ModuloCategoria.Aplicacao;

public class ServicoCategoria
{
    private readonly InterfaceRepositorioCategoria repositorioCategoria;

    public ServicoCategoria(InterfaceRepositorioCategoria repositorioCategoria)
    {
        this.repositorioCategoria = repositorioCategoria;
    }

    public Result Cadastrar(CadastrarCategoriaDto dto)
    {
        Categoria novaCategoria = new Categoria(
            dto.Nome,
            dto.Cor
        );

        List<string> erros = novaCategoria.Validar();

        if (VerificarNomeDuplicado(dto.Nome))
            erros.Add("Já existe uma categoria com este nome.");

        if (erros.Any())
            return RetornarErros(erros);

        repositorioCategoria.Cadastrar(novaCategoria);

        return Result.Ok();
    }

    public Result Editar(EditarCategoriaDto dto)
    {
        Categoria categoriaAtualizada = new Categoria(
            dto.Nome,
            dto.Cor
        );

        List<string> erros = categoriaAtualizada.Validar();

        if (VerificarNomeDuplicado(dto.Nome, dto.Id))
            erros.Add("Já existe uma categoria com este nome.");

        if (erros.Any())
            return RetornarErros(erros);

        repositorioCategoria.Editar(dto.Id, categoriaAtualizada);

        return Result.Ok();
    }

    public Result Excluir(string id)
    {
        List<string> erros = new List<string>();

        if (VerificarProdutoCadastrado(id))
            erros.Add("Não é possível excluir uma categoria que tenha produtos vinculados.");

        if (erros.Any())
            return RetornarErros(erros);

        repositorioCategoria.Excluir(id);

        return Result.Ok();
    }

    public List<ListarCategoriasDto> SelecionarTodos()
    {
        List<Categoria> categorias = repositorioCategoria.SelecionarTodos();

        return categorias.Select(c => new ListarCategoriasDto(c.Id, c.Nome, c.Cor)).ToList();
    }

    public Result<ListarCategoriasDto> SelecionarPorId(string id)
    {
        Categoria? categoria = repositorioCategoria.SelecionarPorId(id);

        if (categoria == null)
            return Result.Fail("Categoria não encontrada.");

        return Result.Ok(new ListarCategoriasDto(categoria.Id, categoria.Nome, categoria.Cor));
    }

    private bool VerificarNomeDuplicado(string nome, string? idIgnorado = null)
    {
        List<Categoria> categorias = repositorioCategoria.SelecionarTodos();

        foreach (Categoria c in categorias)
        {
            if (c.Id != idIgnorado && string.Equals(c.Nome, nome, StringComparison.OrdinalIgnoreCase))
                return true;
        }

        return false;
    }

    private bool VerificarProdutoCadastrado(string nome, string? idIgnorado = null)
    {
        // Não permitir excluir uma categoria caso tenha produtos vinculados
        return false;
    }

    private static Result RetornarErros(List<string> erros)
    {
        List<IError> listaErros = erros.Select(e => new Error(e)).ToList<IError>();

        return Result.Fail(listaErros);
    }
}
