using Microsoft.AspNetCore.Mvc;

namespace ListaDeCompras.WebApplication.Compartilhado.Apresentacao;

public class HomeController : Controller
{
    [HttpGet]
    public ActionResult Index()
    {
        return View();
    }
}
