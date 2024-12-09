using Microsoft.AspNetCore.Mvc;
using Proyecto_Salvacion.Models;


namespace Proyecto_Salvacion.Controllers
{
    public class HomeController1 : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
