using ListaDeCompras.WebApplication.ModuloListasDeCompras.Dominio;
using ListaDeCompras.WebApplication.ModuloProduto.Dominio;
using ListaDeCompras.WebApplication.ModuloItensDaLista.Dominio;
using FluentResults;

namespace ListaDeCompras.WebApplication.ModuloItensDaLista.Aplicacao;

public class ServicoItensDaLista
{
    private readonly InterfaceRepositorioProduto repositorioProduto;
    private readonly InterfaceRepositorioListasDeCompras repositorioListasDeCompras;
    private readonly InterfaceRepositorioItensDaLista repositorioItensDaLista;

    public ServicoItensDaLista(InterfaceRepositorioItensDaLista repositorioItensDaLista, InterfaceRepositorioProduto repositorioProduto, InterfaceRepositorioListasDeCompras repositorioListasDeCompras)
    {
        this.repositorioItensDaLista = repositorioItensDaLista;
        this.repositorioProduto = repositorioProduto;
        this.repositorioListasDeCompras = repositorioListasDeCompras;
    }

    public Result Cadastrar(CadastrarItensDaListaDto dto)
    {
        ItensDaLista novoItensDaLista = new ItensDaLista(
            dto.Nome,
            dto.Preco
        );

        List<string> erros = novoItensDaLista.Validar();

        if (VerificarNomeDuplicado(dto.Nome))
            erros.Add("Já existe um item com este nome.");

        if (erros.Any())
            return RetornarErros(erros);

        repositorioItensDaLista.Cadastrar(novoItensDaLista);

        return Result.Ok();
    }

    public Result Editar(EditarItensDaListaDto dto)
    {
        ItensDaLista itensDaListaAtualizada = new ItensDaLista(
            dto.Nome,
            dto.Preco
        );

        List<string> erros = itensDaListaAtualizada.Validar();

        if (VerificarNomeDuplicado(dto.Nome, dto.Id))
            erros.Add("Já existe um item com este nome.");

        if (erros.Any())
            return RetornarErros(erros);

        repositorioItensDaLista.Editar(dto.Id, itensDaListaAtualizada);

        return Result.Ok();
    }

    public Result Excluir(string id)
    {
        List<string> erros = new List<string>();

        if (VerificarListaCadastrada(id))
            erros.Add("Não é possível excluir um item que esteja em alguma lista de compras.");

        if (erros.Any())
            return RetornarErros(erros);

        repositorioItensDaLista.Excluir(id);

        return Result.Ok();
    }

    public List<ListarItensDaListaDto> SelecionarTodos()
    {
        List<ItensDaLista> itensDaLista = repositorioItensDaLista.SelecionarTodos();

        return itensDaLista.Select(idl => new ListarItensDaListaDto(idl.Id, idl.Nome, idl.Preco)).ToList();
    }

    public Result<ListarItensDaListaDto> SelecionarPorId(string id)
    {
        ItensDaLista? itensDaLista = repositorioItensDaLista.SelecionarPorId(id);

        if (itensDaLista == null)
            return Result.Fail("Item não encontrado.");

        return Result.Ok(new ListarItensDaListaDto(itensDaLista.Id, itensDaLista.Nome, itensDaLista.Preco));
    }

    private bool VerificarNomeDuplicado(string nome, string? idIgnorado = null)
    {
        List<ItensDaLista> itensDaLista = repositorioItensDaLista.SelecionarTodos();

        foreach (ItensDaLista idl in itensDaLista)
        {
            if (idl.Id != idIgnorado && string.Equals(idl.Nome, nome, StringComparison.OrdinalIgnoreCase))
                return true;
        }

        return false;
    }

    private bool VerificarListaCadastrada(string idProduto)
    {
        return repositorioListasDeCompras.SelecionarTodos().Any(ldc => ldc.idProduto == idProduto);
    }

    private static Result RetornarErros(List<string> erros)
    {
        List<IError> listaErros = erros.Select(e => new Error(e)).ToList<IError>();

        return Result.Fail(listaErros);
    }
}