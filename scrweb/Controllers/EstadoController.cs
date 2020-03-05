using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using scrweb.DAO;
using scrweb.Models;

namespace scrweb.Controllers
{
    public class EstadoController : Controller
    {
        public JsonResult Obter()
        {
            JArray jarray = new JArray();
            List<Estado> estados = new Estado().GetAll();

            if (estados == null || estados.Count == 0) return Json(new List<Estado>());

            for (int i = 0; i < estados.Count; i++)
            {
                jarray.Add(estados[i].ToJObject());
            }
            
            return Json(jarray);
        }

        [HttpPost]
        public JsonResult ObterPorId(int id)
        {
            Estado estado = new EstadoDAO().GetById(id);

            return Json(estado == null ? null : estado.ToJObject());
        }
    }
}