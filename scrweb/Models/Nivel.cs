using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using scrweb.DAO;

namespace scrweb.Models
{
    public class Nivel
    {
        private int _id;
        private string _descricao;

        public string Descricao { get => _descricao; set => _descricao = value; }
        public int Id { get => _id; set => _id = value; }

        public Nivel GetById(int id)
        {
            return id > 0 ? new NivelDAO().GetById(id) : null;
        }

        public List<Nivel> GetAll()
        {
            return new NivelDAO().GetAll();
        }

        public JObject ToJObject()
        {
            JObject json = new JObject();
            json.Add("id", _id);
            json.Add("descricao", _descricao);

            return json;
        }
    }
}
