using ListaDeCompras.WebApplication.ModuloCategoria.Aplicacao;
using ListaDeCompras.WebApplication.ModuloCategoria.Dominio;
using FluentResults;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ListaDeCompras.WebApplication.Compartilhado.Apresentacao.Extensions;

namespace ListaDeCompras.WebApplication.ModuloCategoria.Apresentacao;

public class CategoriaController(ServicoCategoria servicoCategoria, IMapper mapeador, InterfaceRepositorioCategoria repositorioCategoria) : Controller
{

    [HttpGet]
    public ActionResult Listar()
    {
        List<ListarCategoriasDto> dtos = servicoCategoria.SelecionarTodos();

        List<ListarCategoriasViewModel> listarVms = mapeador.Map<List<ListarCategoriasViewModel>>(dtos);

        return View(listarVms);
    }

    [HttpGet]
    public ActionResult Cadastrar()
    {
        CadastrarCategoriaViewModel cadastrarVm = new CadastrarCategoriaViewModel(
            string.Empty,
            string.Empty
        );

        return View(cadastrarVm);
    }

    [HttpPost]
    public ActionResult Cadastrar(CadastrarCategoriaViewModel cadastrarVm)
    {
        if (!ModelState.IsValid)
            return View(cadastrarVm);

        CadastrarCategoriaDto dto = mapeador.Map<CadastrarCategoriaDto>(cadastrarVm);

        Result resultado = servicoCategoria.Cadastrar(dto);

        if (resultado.IsFailed)
        {
            ModelState.AddModelError(resultado);

            return View(cadastrarVm);
        }

        return RedirectToAction(nameof(Listar));
    }

    [HttpGet]
    public ActionResult Editar(string id)
    {
        Categoria? categoria = repositorioCategoria.SelecionarPorId(id);

        if (categoria == null)
            return RedirectToAction(nameof(Listar));

        EditarCategoriaViewModel editarVm = new EditarCategoriaViewModel(
            id,
            categoria.Nome,
            categoria.Cor
        );

        return View(editarVm);
    }

    [HttpPost]
    public ActionResult Editar(EditarCategoriaViewModel editarVm)
    {
        if (!ModelState.IsValid)
            return View(editarVm);

        EditarCategoriaDto dto = mapeador.Map<EditarCategoriaDto>(editarVm);

        Result resultado = servicoCategoria.Editar(dto);

        if (resultado.IsFailed)
        {
            ModelState.AddModelError(resultado);

            return View(editarVm);
        }

        return RedirectToAction(nameof(Listar));
    }

    [HttpGet]
    public ActionResult Excluir(string id)
    {
        Categoria? categoria = repositorioCategoria.SelecionarPorId(id);

        if (categoria == null)
            return RedirectToAction(nameof(Listar));

        ExcluirCategoriaViewModel excluirVm = new ExcluirCategoriaViewModel(
            id,
            categoria.Nome,
            categoria.Cor
        );

        return View(excluirVm);
    }

    [HttpPost]
    public ActionResult Excluir(ExcluirCategoriaViewModel excluirVm)
    {
        Result resultado = servicoCategoria.Excluir(excluirVm.Id);

        if (resultado.IsFailed)
        {
            ModelState.AddModelError(resultado);

            return View(excluirVm);
        }

        return RedirectToAction(nameof(Listar));
    }
}
