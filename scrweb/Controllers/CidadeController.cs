using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using scrweb.DAO;
using scrweb.Models;

namespace scrweb.Controllers
{
    public class CidadeController : Controller
    {
        public JsonResult Obter()
        {
            List<Cidade> cidades = new Cidade().GetAll();

            if (cidades == null || cidades.Count == 0) return Json(new List<Cidade>());
            
            JArray array = new JArray();

            for (int i = 0; i < cidades.Count; i++)
            {
                array.Add(cidades[i].ToJObject());
            }

            return Json(array);
        }

        [HttpPost]
        public JsonResult ObterPorId(int id)
        {
            Cidade c = new CidadeDAO().GetById(id);

            return Json(c == null ? null : c.ToJObject());
        }

        [HttpPost]
        public JsonResult ObterPorEstado(int estado)
        {
            List<Cidade> cidades = new Cidade().GetByEstado(estado);

            if (cidades == null || cidades.Count == 0) return Json(new List<Cidade>());
            
            JArray array = new JArray();

            for (int i = 0; i < cidades.Count; i++)
            {
                array.Add(cidades[i].ToJObject());
            }

            return Json(array);
        }
    }
}