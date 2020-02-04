using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using scrlib.ViewModels;
using scrweb.Filters;
using cl = scrlib.Controllers;

namespace scrweb.Controllers
{
    [ValidarUsuario]
    public class TipoCaminhaoController : Controller
    {
        private static List<TipoCaminhaoViewModel> _tipos;
        private static TipoCaminhaoViewModel _tipo;

        public TipoCaminhaoController()
        {
            _tipos = new cl.TipoCaminhaoController().GetAll();
        }
        
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
            var id = HttpContext.Session.GetString("idtipo");
            _tipo = _tipos.Find(t => t.Id == Convert.ToInt32(id));

            return View();
        }

        public JsonResult Obter()
        {
            return Json(_tipos);
        }

        public JsonResult ObterDetalhes()
        {
            return Json(_tipo);
        }

        [HttpPost]
        public JsonResult ObterPorFiltro(string filtro)
        {
            var filtrado = _tipos.FindAll(t => t.Descricao.Contains(filtro, StringComparison.CurrentCultureIgnoreCase));

            return Json(filtrado);
        }

        [HttpPost]
        public JsonResult Ordenar(string ord)
        {
            var ordenado = new List<TipoCaminhaoViewModel>();

            switch (ord)
            {
                case "1":
                    ordenado = _tipos.OrderBy(t => t.Id).ToList();
                    break;
                case "2":
                    ordenado = _tipos.OrderByDescending(t => t.Id).ToList();
                    break;
                case "3":
                    ordenado = _tipos.OrderBy(t => t.Descricao).ToList();
                    break;
                case "4":
                    ordenado = _tipos.OrderByDescending(t => t.Descricao).ToList();
                    break;
                case "5":
                    ordenado = _tipos.OrderBy(t => t.Eixos).ToList();
                    break;
                case "6":
                    ordenado = _tipos.OrderByDescending(t => t.Eixos).ToList();
                    break;
                case "7":
                    ordenado = _tipos.OrderBy(t => t.Capacidade).ToList();
                    break;
                case "8":
                    ordenado = _tipos.OrderByDescending(t => t.Capacidade).ToList();
                    break;
            }

            return Json(ordenado);
        }

        [HttpPost]
        public JsonResult Enviar(string id)
        {
            if (string.IsNullOrEmpty(id)) return Json("Parâmetro inválido...");
            HttpContext.Session.SetString("idtipo", id);
            return Json("");
        }

        [HttpPost]
        public JsonResult Gravar(IFormCollection form)
        {
            var descricao = form["desc"];
            var eixos = form["eixos"];
            var capacidade = form["capacidade"];

            int.TryParse(eixos, out var eixosi);
            decimal.TryParse(capacidade, out var cap);
            
            var res = new cl.TipoCaminhaoController().Gravar(new TipoCaminhaoViewModel()
            {
                Id = 0,
                Descricao = descricao,
                Eixos = eixosi,
                Capacidade = cap
            });

            switch (res)
            {
                case -10:
                    return Json("Um problema foi detectado ao executar o comando SQL.");
                case -5:
                    return Json("Um ou mais campos inválidos...");
                default:
                    return Json("");
            }
        }

        [HttpPost]
        public JsonResult Alterar(IFormCollection form)
        {
            var tipo = form["tipo"];
            var descricao = form["desc"];
            var eixos = form["eixos"];
            var capacidade = form["capacidade"];

            int.TryParse(tipo, out var tipo_id);
            int.TryParse(eixos, out var eixosi);
            decimal.TryParse(capacidade, out var cap);
            
            var res = new cl.TipoCaminhaoController().Alterar(new TipoCaminhaoViewModel()
            {
                Id = tipo_id,
                Descricao = descricao,
                Eixos = eixosi,
                Capacidade = cap
            });

            return Json(res <= 0 ? "Ocorreu um problema na execução do comando SQL." : "");
        }

        [HttpPost]
        public JsonResult Excluir(int id)
        {
            var res = new cl.TipoCaminhaoController().Exlcuir(id);
            if (res <= 0) return Json("Problemas foram detectados ao excluir o tipo.");
            _tipos.Remove(_tipos.Find(t => t.Id == id));
            return Json("");
        }
    }
}