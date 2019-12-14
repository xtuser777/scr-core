using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using scrweb.Filters;

namespace scrweb.Controllers
{
    public class InicioController : Controller
    {
        [ValidarUsuario]
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}