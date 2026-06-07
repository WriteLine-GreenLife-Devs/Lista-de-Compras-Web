using ListaDeCompras.WebApplication.ModuloListasDeCompras.Aplicacao;
using ListaDeCompras.WebApplication.ModuloListasDeCompras.Dominio;
using FluentResults;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ListaDeCompras.WebApplication.Compartilhado.Apresentacao.Extensions;

namespace ListaDeCompras.WebApplication.ModuloListasDeCompras.Apresentacao;

public class ListasDeComprasController(
    ServicoListasDeCompras servicoListasDeCompras, 
    IMapper mapeador, 
    InterfaceRepositorioListasDeCompras repositorioListasDeCompras
) : Controller
{
    [HttpGet]
    public ActionResult Listar()
    {
        List<ListarListasDeComprasDto> dtos = servicoListasDeCompras.SelecionarTodos();

        List<ListarListasDeComprasViewModel> listarVms = mapeador.Map<List<ListarListasDeComprasViewModel>>(dtos);

        return View(listarVms);
    }

    [HttpGet]
    public ActionResult Cadastrar()
    {
        CadastrarListasDeComprasViewModel cadastrarVm = new CadastrarListasDeComprasViewModel(
            string.Empty
        );

        return View(cadastrarVm);
    }

    [HttpPost]
    public ActionResult Cadastrar(CadastrarListasDeComprasViewModel cadastrarVm)
    {
        if (!ModelState.IsValid)
            return View(cadastrarVm);

        CadastrarListasDeComprasDto dto = mapeador.Map<CadastrarListasDeComprasDto>(cadastrarVm);

        Result resultado = servicoListasDeCompras.Cadastrar(dto);

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
        ListasDeCompras? listasDeCompras = repositorioListasDeCompras.SelecionarPorId(id);

        if (listasDeCompras == null)
            return RedirectToAction(nameof(Listar));

        EditarListasDeComprasViewModel editarVm = new EditarListasDeComprasViewModel(id, listasDeCompras.Nome);
        return View(editarVm);
    }

    [HttpPost]
    public ActionResult Editar(EditarListasDeComprasViewModel editarVm)
    {
        if (!ModelState.IsValid)
            return View(editarVm);

        EditarListasDeComprasDto dto = mapeador.Map<EditarListasDeComprasDto>(editarVm);

        Result resultado = servicoListasDeCompras.Editar(dto);

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
        ListasDeCompras? listasDeCompras = repositorioListasDeCompras.SelecionarPorId(id);

        if (listasDeCompras == null)
            return RedirectToAction(nameof(Listar));

        ExcluirListasDeComprasViewModel excluirVm = new ExcluirListasDeComprasViewModel(
            id,
            listasDeCompras.Nome,
            listasDeCompras.DataCriacao,
            listasDeCompras.ItensTotais,
            listasDeCompras.GastoEstimado,
            listasDeCompras.Status
        );

        return View(excluirVm);
    }

    [HttpPost]
    public ActionResult Excluir(ExcluirListasDeComprasViewModel excluirVm)
    {
        Result resultado = servicoListasDeCompras.Excluir(excluirVm.Id);

        if (resultado.IsFailed)
        {
            ModelState.AddModelError(resultado);
            return View(excluirVm);
        }

        return RedirectToAction(nameof(Listar));
    }
}