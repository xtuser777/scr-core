using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using scrlib.ViewModels;
using scrweb.Filters;
using cl = scrlib.Controllers;

namespace scrweb.Controllers
{
    [ValidarUsuario]
    public class ProdutoController : Controller
    {
        private static List<ProdutoViewModel> _produtos;
        private static ProdutoViewModel _produto;

        public ProdutoController()
        {
            _produtos = new cl.ProdutoController().GetAll();
        }
        
        // GET
        public IActionResult Index()
        {
            ViewBag.Representacoes = new cl.RepresentacaoController().GetAll();
            
            return View();
        }

        public JsonResult Obter()
        {
            return Json(_produtos);
        }
    }
}