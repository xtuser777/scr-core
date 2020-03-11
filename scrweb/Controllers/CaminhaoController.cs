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

        public CaminhaoController()
        {
            _caminhoes = new Caminhao().GetAll();
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

        [HttpPost]
        public JsonResult Enviar(string id)
        {
            if (string.IsNullOrEmpty(id)) return Json("Parâmetro iválido...");

            HttpContext.Session.SetString("idcaminhao", id);

            return Json("");
        }

        [HttpPost]
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

        [HttpPost]
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

        [HttpPost]
        public JsonResult Gravar(IFormCollection form)
        {
            string placa = form["placa"];
            string marca = form["marca"];
            string modelo = form["modelo"];
            string ano = form["ano"];
            string tipo = form["tipo"];
            string proprietario = form["proprietario"];

            int.TryParse(tipo, out int idtipo);
            int.TryParse(proprietario, out int idprop);

            int res = new Caminhao()
            {
                Id = 0,
                Placa = placa,
                Marca = marca,
                Modelo = modelo,
                Ano = ano,
                Tipo = new TipoCaminhao() { Id = idtipo },
                Proprietario = new Motorista() { Id = idprop }
            }.Gravar();

            switch (res)
            {
                case -10: return Json("Problema na execução do SQL.");
                case -5: return Json("Um ou mais campos inválidos.");
                default: return Json("");
            }
        }

        [HttpPost]
        public JsonResult Alterar(IFormCollection form)
        {
            string caminhao = form["caminhao"];
            string placa = form["placa"];
            string marca = form["marca"];
            string modelo = form["modelo"];
            string ano = form["ano"];
            string tipo = form["tipo"];
            string proprietario = form["proprietario"];

            int.TryParse(caminhao, out int idcaminhao);
            int.TryParse(tipo, out int idtipo);
            int.TryParse(proprietario, out int idprop);

            int res = new Caminhao()
            {
                Id = idcaminhao,
                Placa = placa,
                Marca = marca,
                Modelo = modelo,
                Ano = ano,
                Tipo = new TipoCaminhao() { Id = idtipo },
                Proprietario = new Motorista() { Id = idprop }
            }.Alterar();

            return Json(res > 0 ? "" : "Problemas na alteração do caminhão.");
        }

        [HttpPost]
        public JsonResult Excluir(int id)
        {
            Caminhao c = _caminhoes.Find(c => c.Id == id);

            int res = new Caminhao().Excluir(id);

            if (res <= 0) return Json("Problemas ao excluir o caminhão.");

            _caminhoes.Remove(c);

            return Json("");
        }
    }
}