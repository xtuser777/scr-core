using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using scrlib.Controllers;
using scrlib.ViewModels;

namespace scrweb.Controllers
{
    public class LoginController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Autenticar(string login, string senha)
        {
            UsuarioViewModel usu = null;
            usu = new UsuarioController().Autenticar(login, senha);
            if (usu != null)
            {
                HttpContext.Session.SetString("id", usu.Id.ToString());
                HttpContext.Session.SetString("login", usu.Login);
                HttpContext.Session.SetString("nome", usu.Funcionario.Pessoa.Nome);
                HttpContext.Session.SetString("nivel", usu.Nivel.Id.ToString());
            }
            return Json(usu);
        }
        
        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return Redirect("/login/index");
        }
    }
}