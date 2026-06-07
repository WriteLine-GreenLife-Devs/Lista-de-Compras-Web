using ListaDeCompras.WebApplication.ModuloItensDaLista.Aplicacao;
using ListaDeCompras.WebApplication.ModuloItensDaLista.Dominio;
using FluentResults;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ListaDeCompras.WebApplication.Compartilhado.Apresentacao.Extensions;
using ListaDeCompras.WebApplication.ModuloProduto.Dominio;
using ListaDeCompras.WebApplication.ModuloListasDeCompras.Dominio;

namespace ListaDeCompras.WebApplication.ModuloItensDaLista.Apresentacao;

public class ItensDaListaController(
    ServicoItensDaLista servicoItensDaLista,
    IMapper mapeador,
    InterfaceRepositorioItensDaLista repositorioItensDaLista,
    InterfaceRepositorioProduto repositorioProduto,
    InterfaceRepositorioListasDeCompras repositorioListasDeCompras
) : Controller
{
    [HttpGet]
    public ActionResult Listar()
    {
        List<ListarItensDaListaDto> dtos = servicoItensDaLista.SelecionarTodos();
        List<ListarItensDaListaViewModel> listarVms = mapeador.Map<List<ListarItensDaListaViewModel>>(dtos);
        return View(listarVms);
    }

    [HttpGet]
    public ActionResult Cadastrar()
    {
        List<Produto> produtos = repositorioProduto.SelecionarTodos();
        List<ListasDeCompras> listas = repositorioListasDeCompras.SelecionarTodos();

        ViewBag.Produtos = produtos;
        ViewBag.Listas = listas;

        string produtoPadraoId = produtos.FirstOrDefault()?.Id ?? string.Empty;
        string listaPadraoId = listas.FirstOrDefault()?.Id ?? string.Empty;

        CadastrarItensDaListaViewModel cadastrarVm = new CadastrarItensDaListaViewModel(
            produtoPadraoId,
            listaPadraoId,
            1,
            0
        );

        return View(cadastrarVm);
    }

    [HttpPost]
    public ActionResult Cadastrar(CadastrarItensDaListaViewModel cadastrarVm)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Produtos = repositorioProduto.SelecionarTodos();
            ViewBag.Listas = repositorioListasDeCompras.SelecionarTodos();
            return View(cadastrarVm);
        }

        CadastrarItensDaListaDto dto = mapeador.Map<CadastrarItensDaListaDto>(cadastrarVm);
        Result resultado = servicoItensDaLista.Cadastrar(dto);

        if (resultado.IsFailed)
        {
            ModelState.AddModelError(resultado);
            ViewBag.Produtos = repositorioProduto.SelecionarTodos();
            ViewBag.Listas = repositorioListasDeCompras.SelecionarTodos();
            return View(cadastrarVm);
        }

        return RedirectToAction(nameof(Listar));
    }

    [HttpGet]
    public ActionResult Editar(string id)
    {
        ItensDaLista? item = repositorioItensDaLista.SelecionarPorId(id);
        if (item == null) return RedirectToAction(nameof(Listar));

        ViewBag.Produtos = repositorioProduto.SelecionarTodos();
        ViewBag.Listas = repositorioListasDeCompras.SelecionarTodos();

        EditarItensDaListaViewModel editarVm = new EditarItensDaListaViewModel(
            item.Id,
            item.ProdutoId,
            item.ListaId,
            item.Quantidade,
            item.PrecoUnitario
        );

        return View(editarVm);
    }

    [HttpPost]
    public ActionResult Editar(EditarItensDaListaViewModel editarVm)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Produtos = repositorioProduto.SelecionarTodos();
            ViewBag.Listas = repositorioListasDeCompras.SelecionarTodos();
            return View(editarVm);
        }

        EditarItensDaListaDto dto = mapeador.Map<EditarItensDaListaDto>(editarVm);
        Result resultado = servicoItensDaLista.Editar(dto);

        if (resultado.IsFailed)
        {
            ModelState.AddModelError(resultado);
            ViewBag.Produtos = repositorioProduto.SelecionarTodos();
            ViewBag.Listas = repositorioListasDeCompras.SelecionarTodos();
            return View(editarVm);
        }

        return RedirectToAction(nameof(Listar));
    }

    [HttpGet]
    public ActionResult Excluir(string id)
    {
        ItensDaLista? item = repositorioItensDaLista.SelecionarPorId(id);
        if (item == null) return RedirectToAction(nameof(Listar));

        ExcluirItensDaListaViewModel excluirVm = new ExcluirItensDaListaViewModel(
            item.Id,
            item.ProdutoId,
            item.ListaId,
            item.Quantidade,
            item.PrecoUnitario,
            item.ValorTotal
        );

        return View(excluirVm);
    }

    [HttpPost]
    public ActionResult Excluir(ExcluirItensDaListaViewModel excluirVm)
    {
        Result resultado = servicoItensDaLista.Excluir(excluirVm.Id);

        if (resultado.IsFailed)
        {
            ModelState.AddModelError(resultado);
            return View(excluirVm);
        }

        return RedirectToAction(nameof(Listar));
    }
}