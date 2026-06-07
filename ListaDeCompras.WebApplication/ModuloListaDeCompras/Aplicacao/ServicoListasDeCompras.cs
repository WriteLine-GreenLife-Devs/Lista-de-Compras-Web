using ListaDeCompras.WebApplication.ModuloListaDeCompras.Dominio;
using ListaDeCompras.WebApplication.ModuloProduto.Dominio;
using FluentResults;

namespace ListaDeCompras.WebApplication.ModuloListaDeCompras.Aplicacao;

public class ServicoListasDeCompras
{
    private readonly InterfaceRepositorioProduto repositorioProduto;
    private readonly InterfaceRepositorioListasDeCompras repositorioListasDeCompras;

    public ServicoListasDeCompras(InterfaceRepositorioListasDeCompras repositorioListasDeCompras, InterfaceRepositorioProduto repositorioProduto)
    {
        this.repositorioListasDeCompras = repositorioListasDeCompras;
        this.repositorioProduto = repositorioProduto;
    }

    public Result Cadastrar(CadastrarListasDeComprasDto dto)
    {
        var novaLista = new ListasDeCompras(dto.Nome, dto.DataCriacao);

        var erros = novaLista.Validar();

        if (VerificarNomeDuplicado(dto.Nome))
            erros.Add("Já existe uma lista de compras com este nome.");

        if (erros.Any())
            return RetornarErros(erros);

        repositorioListasDeCompras.Cadastrar(novaLista);

        return Result.Ok();
    }

    public Result Editar(EditarListasDeComprasDto dto)
    {
        var listaAtualizada = new ListasDeCompras(dto.Nome, dto.DataCriacao);

        var erros = listaAtualizada.Validar();

        if (VerificarNomeDuplicado(dto.Nome, dto.Id))
            erros.Add("Já existe uma lista de compras com este nome.");

        if (erros.Any())
            return RetornarErros(erros);

        repositorioListasDeCompras.Editar(dto.Id, listaAtualizada);

        return Result.Ok();
    }

    public Result Excluir(string id)
    {
        var erros = new List<string>();

        if (VerificarProdutoCadastrado(id))
            erros.Add("Não é possível excluir uma lista de compras que tenha produtos vinculados.");

        if (erros.Any())
            return RetornarErros(erros);

        repositorioListasDeCompras.Excluir(id);

        return Result.Ok();
    }

    public List<ListarListasDeComprasDto> SelecionarTodos()
    {
        var listas = repositorioListasDeCompras.SelecionarTodos();

        return listas.Select(ldc => new ListarListasDeComprasDto(
            ldc.Id,
            ldc.Nome,
            ldc.DataCriacao,
            ldc.ItensTotais,
            ldc.GastoEstimado
        )).ToList();
    }

    public Result<ListarListasDeComprasDto> SelecionarPorId(string id)
    {
        var lista = repositorioListasDeCompras.SelecionarPorId(id);

        if (lista == null)
            return Result.Fail("Lista de compras não encontrada.");

        return Result.Ok(new ListarListasDeComprasDto(
            lista.Id,
            lista.Nome,
            lista.DataCriacao,
            lista.ItensTotais,
            lista.GastoEstimado
        ));
    }

    private bool VerificarNomeDuplicado(string nome, string? idIgnorado = null)
    {
        var listas = repositorioListasDeCompras.SelecionarTodos();

        return listas.Any(ldc => ldc.Id != idIgnorado && 
            string.Equals(ldc.Nome, nome, StringComparison.OrdinalIgnoreCase));
    }

    private bool VerificarProdutoCadastrado(string idLista)
    {
        return repositorioProduto.SelecionarTodos().Any(p => p.CategoriaId == idLista);
    }

    private static Result RetornarErros(List<string> erros)
    {
        var listaErros = erros.Select(e => new Error(e)).ToList<IError>();
        return Result.Fail(listaErros);
    }
}