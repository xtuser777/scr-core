using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using scrweb.ViewModels;
using scrweb.Filters;
using scrweb.ModelControllers;

namespace scrweb.Controllers
{
    [ValidarUsuario]
    public class ProdutoTipoCaminhaoController : Controller
    {
        private static List<ProdutoTipoCaminhaoViewModel> _ptcs;
        // GET
        
        public IActionResult Index()
        {
            ViewBag.Tipos = new TipoCaminhaoModelController().GetAll();
            
            var produto = Convert.ToInt32(HttpContext.Session.GetString("idprod"));
            
            _ptcs = new ProdutoTipoCaminhaoModelController().GetPorProduto(produto);
            
            return View();
        }
        
        [HttpPost]
        public JsonResult Enviar(string id)
        {
            if (string.IsNullOrEmpty(id)) return Json("Parâmetro inválido...");
            HttpContext.Session.SetString("idprod", id);
            return Json("");
        }

        public JsonResult Obter()
        {
            return Json(_ptcs);
        }

        [HttpPost]
        public JsonResult ObterPorFiltro(string filtro)
        {
            var filtrado = _ptcs.FindAll(p =>
                p.Tipo.Descricao.Contains(filtro, StringComparison.CurrentCultureIgnoreCase)
            );

            return Json(filtrado);
        }

        [HttpPost]
        public JsonResult Ordenar(string ord)
        {
            var ordenado = new List<ProdutoTipoCaminhaoViewModel>();

            switch (ord)
            {
                case "1":
                    ordenado = _ptcs.OrderBy(p => p.Tipo.Id).ToList();
                    break;
                case "2":
                    ordenado = _ptcs.OrderByDescending(p => p.Tipo.Id).ToList();
                    break;
                case "3":
                    ordenado = _ptcs.OrderBy(p => p.Tipo.Descricao).ToList();
                    break;
                case "4":
                    ordenado = _ptcs.OrderByDescending(p => p.Tipo.Descricao).ToList();
                    break;
                case "5":
                    ordenado = _ptcs.OrderBy(p => p.Tipo.Eixos).ToList();
                    break;
                case "6":
                    ordenado = _ptcs.OrderByDescending(p => p.Tipo.Eixos).ToList();
                    break;
                case "7":
                    ordenado = _ptcs.OrderBy(p => p.Tipo.Capacidade).ToList();
                    break;
                case "8":
                    ordenado = _ptcs.OrderByDescending(p => p.Tipo.Capacidade).ToList();
                    break;
            }

            return Json(ordenado);
        }

        [HttpPost]
        public JsonResult VerificarTipo(int tipo) 
        {
            return Json(_ptcs.Contains(_ptcs.Find(p => p.Tipo.Id == tipo)));
        }

        [HttpPost]
        public JsonResult Gravar(int tipo)
        {
            var produto = Convert.ToInt32(HttpContext.Session.GetString("idprod"));
            
            var res = new ProdutoTipoCaminhaoModelController().Gravar(new ProdutoTipoCaminhaoViewModel()
            {
                Produto = new ProdutoModelController().GetById(produto),
                Tipo = new TipoCaminhaoModelController().GetById(tipo)
            });

            if (res > 0)
            {
                _ptcs.Add(new ProdutoTipoCaminhaoViewModel()
                {
                    Produto = new ProdutoModelController().GetById(produto),
                    Tipo = new TipoCaminhaoModelController().GetById(tipo)
                });
            }

            return Json(res <= 0 ? "Ocorreu um problema ao adicionar o tipo." : "");
        }

        [HttpPost]
        public JsonResult Excluir(int produto, int tipo)
        {
            var res = new ProdutoTipoCaminhaoModelController().Excluir(produto, tipo);

            if (res > 0)
            {
                _ptcs.Remove(_ptcs.Find(p => p.Tipo.Id == tipo));
            }
            
            return Json(res <= 0 ? "Ocorreu um problema ao executar o comando SQL." : "");
        }
    }
}