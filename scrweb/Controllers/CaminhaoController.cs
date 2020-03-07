using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using scrweb.Models;

namespace scrweb.Controllers
{
    public class CaminhaoController : Controller
    {
        private static List<Caminhao> _caminhoes;
        
        // GET
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Novo()
        {
            return View();
        }

        public JsonResult Obter()
        {
            if (_caminhoes == null || _caminhoes.Count == 0) return Json(new List<Caminhao>());
            
            JArray array = new JArray();
            for (int i = 0; i < _caminhoes.Count; i++)
            {
                array.Add(_caminhoes[i].ToJObject());
            }

            return Json(array);
        }

        public JsonResult ObterDetalhes()
        {
            return Json(
                _caminhoes.Find(c => 
                    c.Id == Convert.ToInt32(HttpContext.Session.GetString("idcaminhao"))
                ).ToJObject()
            );
        }

        public JsonResult Enviar(string id)
        {
            if (string.IsNullOrEmpty(id)) return Json("Parâmetro iválido...");

            HttpContext.Session.SetString("idcaminhao", id);

            return Json("");
        }

        public JsonResult ObterPorFiltro(string filtro)
        {
            List<Caminhao> filtrado = _caminhoes.FindAll(c =>
                c.Marca.Contains(filtro, StringComparison.CurrentCultureIgnoreCase) ||
                c.Modelo.Contains(filtro, StringComparison.CurrentCultureIgnoreCase)
            );
            
            JArray array = new JArray();
            for (int i = 0; i < filtrado.Count; i++)
            {
                array.Add(filtrado[i].ToJObject());
            }

            return Json(array);
        }

        public JsonResult Ordenar(string coluna)
        {
            List<Caminhao> ordenado = new List<Caminhao>();

            switch (coluna)
            {
                case "1":
                    ordenado = _caminhoes.OrderBy(c => c.Id).ToList();
                    break;
                case "2":
                    ordenado = _caminhoes.OrderByDescending(c => c.Id).ToList();
                    break;
                case "3":
                    ordenado = _caminhoes.OrderBy(c => c.Placa).ToList();
                    break;
                case "4":
                    ordenado = _caminhoes.OrderByDescending(c => c.Placa).ToList();
                    break;
                case "5":
                    ordenado = _caminhoes.OrderBy(c => c.Marca).ToList();
                    break;
                case "6":
                    ordenado = _caminhoes.OrderByDescending(c => c.Marca).ToList();
                    break;
                case "7":
                    ordenado = _caminhoes.OrderBy(c => c.Modelo).ToList();
                    break;
                case "8":
                    ordenado = _caminhoes.OrderByDescending(c => c.Modelo).ToList();
                    break;
                case "9":
                    ordenado = _caminhoes.OrderBy(c => c.Ano).ToList();
                    break;
                case "10":
                    ordenado = _caminhoes.OrderByDescending(c => c.Ano).ToList();
                    break;
            }
            
            JArray array = new JArray();
            for (int i = 0; i < ordenado.Count; i++)
            {
                array.Add(ordenado[i].ToJObject());
            }

            return Json(array);
        }
    }
}