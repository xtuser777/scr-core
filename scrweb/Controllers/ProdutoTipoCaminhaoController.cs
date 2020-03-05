using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using scrweb.Filters;
using scrweb.Models;

namespace scrweb.Controllers
{
    [ValidarUsuario]
    public class ProdutoTipoCaminhaoController : Controller
    {
        private static List<ProdutoTipoCaminhao> _ptcs;
        
        // GET
        public IActionResult Index()
        {
            ViewBag.Tipos = new TipoCaminhao().GetAll();
            
            var produto = Convert.ToInt32(HttpContext.Session.GetString("idprod"));
            
            _ptcs = new ProdutoTipoCaminhao().GetByProduct(produto);

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
            if (_ptcs == null || _ptcs.Count == 0) return Json(new List<ProdutoTipoCaminhao>());
            
            JArray array = new JArray();
            for (int i = 0; i < _ptcs.Count; i++)
            {
                array.Add(_ptcs[i].ToJObject());
            }
            
            return Json(array);
        }

        [HttpPost]
        public JsonResult ObterPorFiltro(string filtro)
        {
            var filtrado = _ptcs.FindAll(p =>
                p.Tipo.Descricao.Contains(filtro, StringComparison.CurrentCultureIgnoreCase)
            );

            JArray array = new JArray();
            for (int i = 0; i < filtrado.Count; i++)
            {
                array.Add(filtrado[i].ToJObject());
            }
            
            return Json(array);
        }

        [HttpPost]
        public JsonResult Ordenar(string ord)
        {
            var ordenado = new List<ProdutoTipoCaminhao>();

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

            JArray array = new JArray();
            for (int i = 0; i < ordenado.Count; i++)
            {
                array.Add(ordenado[i].ToJObject());
            }
            
            return Json(array);
        }

        [HttpPost]
        public JsonResult VerificarTipo(int tipo) 
        {
            return Json(_ptcs != null && _ptcs.Count > 0 && _ptcs.Contains(_ptcs.Find(p => p.Tipo.Id == tipo)));
        }

        [HttpPost]
        public JsonResult Gravar(int tipo)
        {
            var produto = Convert.ToInt32(HttpContext.Session.GetString("idprod"));
            
            var res = new ProdutoTipoCaminhao()
            {
                Produto = new Produto().GetById(produto),
                Tipo = new TipoCaminhao().GetById(tipo)
            }.Gravar();

            if (res > 0)
            {
                _ptcs.Add(new ProdutoTipoCaminhao()
                {
                    Produto = new Produto().GetById(produto),
                    Tipo = new TipoCaminhao().GetById(tipo)
                });
            }

            return Json(res <= 0 ? "Ocorreu um problema ao adicionar o tipo." : "");
        }

        [HttpPost]
        public JsonResult Excluir(int produto, int tipo)
        {
            var res = new ProdutoTipoCaminhao().Excluir(produto, tipo);

            if (res > 0)
            {
                _ptcs.Remove(_ptcs.Find(p => p.Tipo.Id == tipo));
            }
            
            return Json(res <= 0 ? "Ocorreu um problema ao executar o comando SQL." : "");
        }
    }
}