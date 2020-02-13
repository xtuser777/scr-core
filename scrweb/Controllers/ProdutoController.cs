using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using scrweb.ViewModels;
using scrweb.Filters;
using scrweb.ModelControllers;

namespace scrweb.Controllers
{
    [ValidarUsuario]
    public class ProdutoController : Controller
    {
        private static List<ProdutoViewModel> _produtos;

        public ProdutoController()
        {
            _produtos = new ProdutoModelController().GetAll();
        }
        
        // GET
        public IActionResult Index()
        {
            ViewBag.Representacoes = new RepresentacaoModelController().GetAll();
            
            return View();
        }

        public IActionResult Novo()
        {
            ViewBag.Representacoes = new RepresentacaoModelController().GetAll();
            
            return View();
        }

        public IActionResult Detalhes()
        {
            ViewBag.Representacoes = new RepresentacaoModelController().GetAll();
            
            return View();
        }

        public JsonResult Obter()
        {
            return Json(_produtos);
        }

        [HttpPost]
        public JsonResult Enviar(string id)
        {
            if (string.IsNullOrEmpty(id)) return Json("Parâmetro inválido...");
            HttpContext.Session.SetString("idprod", id);
            return Json("");
        }

        public JsonResult ObterDetalhes()
        {
            return Json(
                _produtos.Find(p => 
                    p.Id == Convert.ToInt32(HttpContext.Session.GetString("idprod"))
                )
            );
        }

        [HttpPost]
        public JsonResult ObterPorFiltro(string filtro)
        {
            var filtrado = _produtos.FindAll(p => 
                p.Descricao.Contains(filtro, StringComparison.CurrentCultureIgnoreCase)
            );

            return Json(filtrado);
        }

        [HttpPost]
        public JsonResult ObterPorRepresentacao(string representacao)
        {
            var filtrado = _produtos.FindAll(p => 
                p.Representacao.Id == Convert.ToInt32(representacao)
            );

            return Json(filtrado);
        }

        [HttpPost]
        public JsonResult ObterPorFiltroAndRepresentacao(string filtro, string representacao)
        {
            var filtrado = _produtos.FindAll(p =>
                p.Descricao.Contains(filtro, StringComparison.CurrentCultureIgnoreCase) &&
                p.Representacao.Id == Convert.ToInt32(representacao)
            );

            return Json(filtrado);
        }

        [HttpPost]
        public JsonResult Ordenar(string ord)
        {
            var ordenado = new List<ProdutoViewModel>();

            switch (ord)
            {
                case "1":
                    ordenado = _produtos.OrderBy(p => p.Id).ToList();
                    break;
                case "2":
                    ordenado = _produtos.OrderByDescending(p => p.Id).ToList();
                    break;
                case "3":
                    ordenado = _produtos.OrderBy(p => p.Descricao).ToList();
                    break;
                case "4":
                    ordenado = _produtos.OrderByDescending(p => p.Descricao).ToList();
                    break;
                case "5":
                    ordenado = _produtos.OrderBy(p => p.Medida).ToList();
                    break;
                case "6":
                    ordenado = _produtos.OrderByDescending(p => p.Medida).ToList();
                    break;
                case "7":
                    ordenado = _produtos.OrderBy(p => p.Preco).ToList();
                    break;
                case "8":
                    ordenado = _produtos.OrderByDescending(p => p.Preco).ToList();
                    break;
                case "9":
                    ordenado = _produtos.OrderBy(p => p.Representacao.Id).ToList();
                    break;
                case "10":
                    ordenado = _produtos.OrderByDescending(p => p.Representacao.Id).ToList();
                    break;
            }

            return Json(ordenado);
        }

        [HttpPost]
        public JsonResult Gravar(IFormCollection form)
        {
            var descricao = form["desc"];
            var medida = form["medida"];
            var preco = form["preco"];
            var preco_out = form["preco_out"];
            var representacao = form["representacao"];

            int.TryParse(representacao, out var rep);
            decimal.TryParse(preco, out var dpreco);
            decimal.TryParse(preco_out, out var dpreco_out);

            var res = new ProdutoModelController().Gravar(new ProdutoViewModel()
            {
                Id = 0,
                Descricao = descricao,
                Medida = medida,
                Preco = dpreco,
                PrecoOut = dpreco_out,
                Representacao = new RepresentacaoModelController().GetById(rep)
            });

            switch (res)
            {
                case -10: return Json("Ocorreu um problema na execução do comando SQL.");
                case -5: return Json("Há parâmetros incorretos.");
                default: return Json("");
            }
        }

        [HttpPost]
        public JsonResult Alterar(IFormCollection form)
        {
            var produto = form["produto"];
            var descricao = form["desc"];
            var medida = form["medida"];
            var preco = form["preco"];
            var preco_out = form["preco_out"];
            var representacao = form["representacao"];

            int.TryParse(produto, out var prod);
            int.TryParse(representacao, out var rep);
            decimal.TryParse(preco, out var dpreco);
            decimal.TryParse(preco_out, out var dpreco_out);

            var res = new ProdutoModelController().Alterar(new ProdutoViewModel()
            {
                Id = prod,
                Descricao = descricao,
                Medida = medida,
                Preco = dpreco,
                PrecoOut = dpreco_out,
                Representacao = new RepresentacaoModelController().GetById(rep)
            });

            return Json(res <= 0 ? "Ocorreram problemas ao executar o comando SQL." : "");
        }

        [HttpPost]
        public JsonResult Excluir(int id)
        {
            var res = new ProdutoModelController().Excluir(id);
            if (res <= 0) return Json("Ocorreram problemas ao executar o comando SQL.");
            _produtos.Remove(_produtos.Find(p => p.Id == id));
            return Json("");
        }
    }
}