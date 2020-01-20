using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using scrlib.ViewModels;
using scrweb.Filters;
using cl=scrlib.Controllers;

namespace scrweb.Controllers
{
    [ValidarUsuario]
    public class RepresentacaoController : Controller
    {
        private static List<RepresentacaoViewModel> _representacoes;
        
        // GET
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Novo()
        {
            return View();
        }

        public IActionResult Detalhes()
        {
            return View();
        }

        public JsonResult Obter()
        {
            _representacoes = new cl.RepresentacaoController().GetAll();
            return Json(_representacoes);
        }
        
        public JsonResult ObterEstados()
        {
            return Json(new cl.EstadoController().Get());
        }

        [HttpPost]
        public JsonResult ObterCidades(IFormCollection form)
        {
            return Json(new cl.CidadeController().GetByEstado(Convert.ToInt32(form["estado"])));
        }
        
        [HttpPost]
        public JsonResult VerificarCnpj(string cnpj)
        {
            return Json(new cl.PessoaJuridicaController().VerifyCnpj(cnpj));
        }

        public JsonResult Gravar(IFormCollection form)
        {
            return Json("");
        }

        public JsonResult Alterar(IFormCollection form)
        {
            return Json("");
        }

        public JsonResult Excluir(int id)
        {
            return Json("");
        }
    }
}