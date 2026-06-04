using FluentResults;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ListaDeCompras.WebApplication.Compartilhado.Apresentacao.Extensions;
using ListaDeCompras.WebApplication.ModuloProduto.Aplicacao;
using ListaDeCompras.WebApplication.ModuloProduto.Dominio;
using ListaDeCompras.WebApplication.ModuloProduto.Apresentacao;
using ListaDeCompras.WebApplication.ModuloCategoria.Dominio;

public class ProdutoController(ServicoProduto servicoProduto, IMapper mapeador, InterfaceRepositorioProduto repositorioProduto, InterfaceRepositorioCategoria repositorioCategoria) : Controller
{
    [HttpGet]
    public ActionResult Listar()
    {
        List<ListarProdutosDto> dtos = servicoProduto.SelecionarTodos();

        List<ListarProdutosViewModel> listarVms = mapeador.Map<List<ListarProdutosViewModel>>(dtos);

        return View(listarVms);
    }

    [HttpGet]
    public ActionResult Cadastrar()
    {
        List<Categoria> categorias = repositorioCategoria.SelecionarTodos();
        ViewBag.Categorias = categorias;

        string categoriaPadraoId = categorias.FirstOrDefault()?.Id ?? string.Empty;

        CadastrarProdutoViewModel cadastrarVm = new CadastrarProdutoViewModel(
            string.Empty,
            string.Empty,
            0,
            categoriaPadraoId
        );

        return View(cadastrarVm);
    }

    [HttpPost]
    public ActionResult Cadastrar(CadastrarProdutoViewModel cadastrarVm)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Categorias = repositorioCategoria.SelecionarTodos();
            return View(cadastrarVm);
        }

        CadastrarProdutoDto dto = mapeador.Map<CadastrarProdutoDto>(cadastrarVm);

        Result resultado = servicoProduto.Cadastrar(dto);

        if (resultado.IsFailed)
        {
            ModelState.AddModelError(resultado);
            ViewBag.Categorias = repositorioCategoria.SelecionarTodos();

            return View(cadastrarVm);
        }

        return RedirectToAction(nameof(Listar));
    }

    [HttpGet]
    public ActionResult Editar(string id)
    {
        Produto? produto = repositorioProduto.SelecionarPorId(id);

        if (produto == null)
            return RedirectToAction(nameof(Listar));

        List<Categoria> categorias = repositorioCategoria.SelecionarTodos();
        ViewBag.Categorias = categorias;

        EditarProdutoViewModel editarVm = new EditarProdutoViewModel(
            id,
            produto.Nome,
            produto.UnidadeDeMedida,
            produto.Preco,
            produto.CategoriaId
        );

        return View(editarVm);
    }

    [HttpPost]
    public ActionResult Editar(EditarProdutoViewModel editarVm)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Categorias = repositorioCategoria.SelecionarTodos();
            return View(editarVm);
        }

        EditarProdutoDto dto = mapeador.Map<EditarProdutoDto>(editarVm);

        Result resultado = servicoProduto.Editar(dto);

        if (resultado.IsFailed)
        {
            ModelState.AddModelError(resultado);
            ViewBag.Categorias = repositorioCategoria.SelecionarTodos();

            return View(editarVm);
        }

        return RedirectToAction(nameof(Listar));
    }

    [HttpGet]
    public ActionResult Excluir(string id)
    {
        Produto? produto = repositorioProduto.SelecionarPorId(id);

        if (produto == null)
            return RedirectToAction(nameof(Listar));

        Categoria? categoria = repositorioCategoria.SelecionarPorId(produto.CategoriaId);

        ExcluirProdutoViewModel excluirVm = new ExcluirProdutoViewModel(
            id,
            produto.Nome,
            produto.UnidadeDeMedida,
            produto.Preco,
            categoria?.Nome ?? string.Empty
        );

        return View(excluirVm);
    }

    [HttpPost]
    public ActionResult Excluir(ExcluirProdutoViewModel excluirVm)
    {
        Result resultado = servicoProduto.Excluir(excluirVm.Id);

        if (resultado.IsFailed)
        {
            ModelState.AddModelError(resultado);

            return View(excluirVm);
        }

        return RedirectToAction(nameof(Listar));
    }
}
