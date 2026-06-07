using ListaDeCompras.WebApplication.ModuloListaDeCompras.Aplicacao;
using ListaDeCompras.WebApplication.ModuloListaDeCompras.Dominio;
using FluentResults;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ListaDeCompras.WebApplication.Compartilhado.Apresentacao.Extensions;

namespace ListaDeCompras.WebApplication.ModuloListaDeCompras.Apresentacao;

public class ListasDeComprasController(
    ServicoListasDeCompras servicoListasDeCompras, 
    IMapper mapeador, 
    InterfaceRepositorioListasDeCompras repositorioListasDeCompras
) : Controller
{
    [HttpGet]
    public ActionResult Listar()
    {
        var dtos = servicoListasDeCompras.SelecionarTodos();
        var listarVms = mapeador.Map<List<ListarListasDeComprasViewModel>>(dtos);

        return View(listarVms);
    }

    [HttpGet]
    public ActionResult Cadastrar()
    {
        return View(new CadastrarListasDeComprasViewModel(string.Empty));
    }

    [HttpPost]
    public ActionResult Cadastrar(CadastrarListasDeComprasViewModel cadastrarVm)
    {
        if (!ModelState.IsValid)
            return View(cadastrarVm);

        var dto = mapeador.Map<CadastrarListasDeComprasDto>(cadastrarVm);
        var resultado = servicoListasDeCompras.Cadastrar(dto);

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
        var lista = repositorioListasDeCompras.SelecionarPorId(id);
        if (lista == null)
            return RedirectToAction(nameof(Listar));

        var editarVm = new EditarListasDeComprasViewModel(id, lista.Nome);
        return View(editarVm);
    }

    [HttpPost]
    public ActionResult Editar(EditarListasDeComprasViewModel editarVm)
    {
        if (!ModelState.IsValid)
            return View(editarVm);

        var dto = mapeador.Map<EditarListasDeComprasDto>(editarVm);
        var resultado = servicoListasDeCompras.Editar(dto);

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
        var lista = repositorioListasDeCompras.SelecionarPorId(id);
        if (lista == null)
            return RedirectToAction(nameof(Listar));

        var excluirVm = new ExcluirListasDeComprasViewModel(
            id,
            lista.Nome,
            lista.DataCriacao,
            lista.ItensTotais,
            lista.GastoEstimado
        );

        return View(excluirVm);
    }

    [HttpPost]
    public ActionResult Excluir(ExcluirListasDeComprasViewModel excluirVm)
    {
        var resultado = servicoListasDeCompras.Excluir(excluirVm.Id);

        if (resultado.IsFailed)
        {
            ModelState.AddModelError(resultado);
            return View(excluirVm);
        }

        return RedirectToAction(nameof(Listar));
    }
}