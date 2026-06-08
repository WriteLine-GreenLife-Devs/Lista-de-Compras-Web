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
    InterfaceRepositorioProduto repositorioProduto,
    InterfaceRepositorioListasDeCompras repositorioListasDeCompras
) : Controller
{
    [HttpGet]
    public ActionResult Listar(string? listaId = null)
    {
        ViewBag.Listas = repositorioListasDeCompras.SelecionarTodos();
        ViewBag.ListaSelecionada = listaId;

        List<ListarItensDaListaDto> dtos = servicoItensDaLista.SelecionarTodos();

        if (!string.IsNullOrEmpty(listaId))
            dtos = dtos.Where(i => i.ListaId == listaId).ToList();

        List<ListarItensDaListaViewModel> vms = mapeador.Map<List<ListarItensDaListaViewModel>>(dtos);

        return View(vms);
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

        CadastrarItensDaListaViewModel vm = new CadastrarItensDaListaViewModel(
            produtoPadraoId,
            listaPadraoId,
            1,
            0
        );

        return View(vm);
    }

    [HttpPost]
    public ActionResult Cadastrar(CadastrarItensDaListaViewModel vm)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Produtos = repositorioProduto.SelecionarTodos();
            ViewBag.Listas = repositorioListasDeCompras.SelecionarTodos();
            return View(vm);
        }

        CadastrarItensDaListaDto dto = mapeador.Map<CadastrarItensDaListaDto>(vm);
        Result resultado = servicoItensDaLista.Cadastrar(dto);

        if (resultado.IsFailed)
        {
            ModelState.AddModelError(resultado);
            ViewBag.Produtos = repositorioProduto.SelecionarTodos();
            ViewBag.Listas = repositorioListasDeCompras.SelecionarTodos();
            return View(vm);
        }

        return RedirectToAction(nameof(Listar));
    }

    [HttpGet]
    public ActionResult Editar(string id)
    {
        Result<ListarItensDaListaDto> dto = servicoItensDaLista.SelecionarPorId(id);
        if (dto.IsFailed) return RedirectToAction(nameof(Listar));

        List<Produto> produtos = repositorioProduto.SelecionarTodos();
        List<ListasDeCompras> listas = repositorioListasDeCompras.SelecionarTodos();

        ViewBag.Produtos = produtos;
        ViewBag.Listas = listas;
        
        string produtoId = produtos.FirstOrDefault(p => p.Nome == dto.Value.ProdutoNome)?.Id ?? string.Empty;
        string listaId = listas.FirstOrDefault(l => l.Nome == dto.Value.ListaNome)?.Id ?? string.Empty;

        EditarItensDaListaViewModel vm = new EditarItensDaListaViewModel(
            dto.Value.Id,
            produtoId,
            listaId,
            dto.Value.Quantidade,
            dto.Value.PrecoUnitario
        );

        return View(vm);
    }

    [HttpPost]
    public ActionResult Editar(EditarItensDaListaViewModel vm)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Produtos = repositorioProduto.SelecionarTodos();
            ViewBag.Listas = repositorioListasDeCompras.SelecionarTodos();
            return View(vm);
        }

        EditarItensDaListaDto dto = mapeador.Map<EditarItensDaListaDto>(vm);
        Result resultado = servicoItensDaLista.Editar(dto);

        if (resultado.IsFailed)
        {
            ModelState.AddModelError(resultado);
            ViewBag.Produtos = repositorioProduto.SelecionarTodos();
            ViewBag.Listas = repositorioListasDeCompras.SelecionarTodos();
            return View(vm);
        }

        return RedirectToAction(nameof(Listar));
    }

    [HttpGet]
    public ActionResult Excluir(string id)
    {
        Result<ListarItensDaListaDto> dto = servicoItensDaLista.SelecionarPorId(id);
        if (dto.IsFailed) return RedirectToAction(nameof(Listar));

        ExcluirItensDaListaViewModel vm = mapeador.Map<ExcluirItensDaListaViewModel>(dto.Value);
        return View(vm);
    }

    [HttpPost]
    public ActionResult Excluir(ExcluirItensDaListaViewModel vm)
    {
        Result resultado = servicoItensDaLista.Excluir(vm.Id);

        if (resultado.IsFailed)
        {
            ModelState.AddModelError(resultado);
            return View(vm);
        }

        return RedirectToAction(nameof(Listar));
    }
}