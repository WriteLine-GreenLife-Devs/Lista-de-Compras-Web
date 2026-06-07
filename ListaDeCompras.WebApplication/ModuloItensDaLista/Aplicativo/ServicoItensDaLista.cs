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

    public ServicoItensDaLista(
        InterfaceRepositorioItensDaLista repositorioItensDaLista,
        InterfaceRepositorioProduto repositorioProduto,
        InterfaceRepositorioListasDeCompras repositorioListasDeCompras)
    {
        this.repositorioItensDaLista = repositorioItensDaLista;
        this.repositorioProduto = repositorioProduto;
        this.repositorioListasDeCompras = repositorioListasDeCompras;
    }

    public Result Cadastrar(CadastrarItensDaListaDto dto)
    {
        ItensDaLista novoItem = new ItensDaLista(dto.ProdutoId, dto.ListaId, dto.Quantidade, dto.PrecoUnitario);

        List<string> erros = novoItem.Validar();

        if (VerificarProdutoDuplicado(dto.ProdutoId, dto.ListaId))
            erros.Add("Este produto já foi adicionado nesta lista.");

        if (erros.Any())
            return RetornarErros(erros);

        repositorioItensDaLista.Cadastrar(novoItem);

        AtualizarTotaisLista(dto.ListaId);

        return Result.Ok();
    }

    public Result Editar(EditarItensDaListaDto dto)
    {
        ItensDaLista itemAtualizado = new ItensDaLista(dto.ProdutoId, dto.ListaId, dto.Quantidade, dto.PrecoUnitario);

        List<string> erros = itemAtualizado.Validar();

        if (VerificarProdutoDuplicado(dto.ProdutoId, dto.ListaId, dto.Id))
            erros.Add("Este produto já foi adicionado nesta lista.");

        if (erros.Any())
            return RetornarErros(erros);

        repositorioItensDaLista.Editar(dto.Id, itemAtualizado);

        AtualizarTotaisLista(dto.ListaId);

        return Result.Ok();
    }

    public Result Excluir(string id)
    {
        ItensDaLista? item = repositorioItensDaLista.SelecionarPorId(id);

        if (item == null)
            return Result.Fail("Item não encontrado.");

        repositorioItensDaLista.Excluir(id);

        AtualizarTotaisLista(item.ListaId);

        return Result.Ok();
    }

    public List<ListarItensDaListaDto> SelecionarTodos()
    {
        List<ItensDaLista> itens = repositorioItensDaLista.SelecionarTodos();

        return itens.Select(i => new ListarItensDaListaDto(
            i.Id,
            i.ProdutoId,
            i.ListaId,
            i.Quantidade,
            i.PrecoUnitario,
            i.ValorTotal
        )).ToList();
    }

    public Result<ListarItensDaListaDto> SelecionarPorId(string id)
    {
        ItensDaLista? item = repositorioItensDaLista.SelecionarPorId(id);

        if (item == null)
            return Result.Fail("Item não encontrado.");

        return Result.Ok(new ListarItensDaListaDto(
            item.Id,
            item.ProdutoId,
            item.ListaId,
            item.Quantidade,
            item.PrecoUnitario,
            item.ValorTotal
        ));
    }

    private bool VerificarProdutoDuplicado(string produtoId, string listaId, string? idIgnorado = null)
    {
        List<ItensDaLista> itens = repositorioItensDaLista.SelecionarTodos();

        return itens.Any(i => i.ListaId == listaId && i.ProdutoId == produtoId && i.Id != idIgnorado);
    }

    private void AtualizarTotaisLista(string listaId)
    {
        ListasDeCompras? lista = repositorioListasDeCompras.SelecionarPorId(listaId);

        if (lista == null) return;

        var itensDaLista = repositorioItensDaLista.SelecionarTodos().Where(i => i.ListaId == listaId).ToList();

        lista.ItensTotais = itensDaLista.Sum(i => i.Quantidade);
        lista.GastoEstimado = itensDaLista.Sum(i => i.ValorTotal);

        repositorioListasDeCompras.Editar(lista.Id, lista);
    }

    private static Result RetornarErros(List<string> erros)
    {
        List<IError> listaErros = erros.Select(e => new Error(e)).ToList<IError>();
        return Result.Fail(listaErros);
    }
}