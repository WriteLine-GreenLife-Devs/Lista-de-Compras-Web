using ListaDeCompras.WebApplication.ModuloItensDaLista.Aplicacao;
using ListaDeCompras.WebApplication.ModuloItensDaLista.Dominio;
using FluentResults;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ListaDeCompras.WebApplication.Compartilhado.Apresentacao.Extensions;

namespace ListaDeCompras.WebApplication.ModuloItensDaLista.Apresentacao;

public class ItensDaListaController(ServicoItensDaLista servicoItensDaLista, IMapper mapeador, InterfaceRepositorioItensDaLista repositorioItensDaLista) : Controller
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
        CadastrarItensDaListaViewModel cadastrarVm = new CadastrarItensDaListaViewModel(
            string.Empty
        );

        return View(cadastrarVm);
    }

    [HttpPost]
    public ActionResult Cadastrar(CadastrarItensDaListaViewModel cadastrarVm)
    {
        if (!ModelState.IsValid)
            return View(cadastrarVm);

        CadastrarItensDaListaDto dto = mapeador.Map<CadastrarItensDaListaDto>(cadastrarVm);

        Result resultado = servicoItensDaLista.Cadastrar(dto);

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
        ItensDaLista? itensDaLista = repositorioItensDaLista.SelecionarPorId(id);

        if (itensDaLista == null)
            return RedirectToAction(nameof(Listar));

        EditarItensDaListaViewModel editarVm = new EditarItensDaListaViewModel(
            id,
            itensDaLista.Nome
        );

        return View(editarVm);
    }

    [HttpPost]
    public ActionResult Editar(EditarItensDaListaViewModel editarVm)
    {
        if (!ModelState.IsValid)
            return View(editarVm);

        EditarItensDaListaDto dto = mapeador.Map<EditarItensDaListaDto>(editarVm);

        Result resultado = servicoItensDaLista.Editar(dto);

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
        ItensDaLista? itensDaLista = repositorioItensDaLista.SelecionarPorId(id);

        if (itensDaLista == null)
            return RedirectToAction(nameof(Listar));

        ExcluirItensDaListaViewModel excluirVm = new ExcluirItensDaListaViewModel(
            id,
            itensDaLista.Nome,
            itensDaLista.Preco
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