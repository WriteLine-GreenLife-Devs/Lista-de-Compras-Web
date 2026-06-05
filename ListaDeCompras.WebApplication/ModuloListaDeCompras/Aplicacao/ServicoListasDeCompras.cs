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
        ListasDeCompras novaListasDeCompras = new ListasDeCompras(
            dto.Nome
        );

        List<string> erros = novaListasDeCompras.Validar();

        if (VerificarNomeDuplicado(dto.Nome))
            erros.Add("Já existe uma lista de compras com este nome.");

        if (erros.Any())
            return RetornarErros(erros);

        repositorioListasDeCompras.Cadastrar(novaListasDeCompras);

        return Result.Ok();
    }

    public Result Editar(EditarListasDeComprasDto dto)
    {
        ListasDeCompras listasDeComprasAtualizada = new ListasDeCompras(
            dto.Nome
        );

        List<string> erros = listasDeComprasAtualizada.Validar();

        if (VerificarNomeDuplicado(dto.Nome, dto.Id))
            erros.Add("Já existe uma lista de compras com este nome.");

        if (erros.Any())
            return RetornarErros(erros);

        repositorioListasDeCompras.Editar(dto.Id, listasDeComprasAtualizada);

        return Result.Ok();
    }

    public Result Excluir(string id)
    {
        List<string> erros = new List<string>();

        if (VerificarProdutoCadastrado(id))
            erros.Add("Não é possível excluir uma lista de compras que tenha produtos vinculados.");

        if (erros.Any())
            return RetornarErros(erros);

        repositorioListasDeCompras.Excluir(id);

        return Result.Ok();
    }

    public List<ListarListasDeComprasDto> SelecionarTodos()
    {
        List<ListasDeCompras> listasDeCompras = repositorioListasDeCompras.SelecionarTodos();

        return listasDeCompras.Select(ldc => new ListarListasDeComprasDto(ldc.Id, ldc.Nome)).ToList();
    }

    public Result<ListarListasDeComprasDto> SelecionarPorId(string id)
    {
        ListasDeCompras? listasDeCompras = repositorioListasDeCompras.SelecionarPorId(id);

        if (listasDeCompras == null)
            return Result.Fail("Lista de compras não encontrada.");

        return Result.Ok(new ListarListasDeComprasDto(listasDeCompras.Id, listasDeCompras.Nome));
    }

    private bool VerificarNomeDuplicado(string nome, string? idIgnorado = null)
    {
        List<ListasDeCompras> listasDeCompras = repositorioListasDeCompras.SelecionarTodos();

        foreach (ListasDeCompras ldc in listasDeCompras)
        {
            if (ldc.Id != idIgnorado && string.Equals(ldc.Nome, nome, StringComparison.OrdinalIgnoreCase))
                return true;
        }

        return false;
    }

    private bool VerificarProdutoCadastrado(string idCategoria)
    {
        return repositorioProduto.SelecionarTodos().Any(p => p.CategoriaId == idCategoria);
    }

    private static Result RetornarErros(List<string> erros)
    {
        List<IError> listaErros = erros.Select(e => new Error(e)).ToList<IError>();

        return Result.Fail(listaErros);
    }
}