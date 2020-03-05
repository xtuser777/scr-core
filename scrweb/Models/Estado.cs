using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using scrweb.DAO;

namespace scrweb.Models
{
    public class Estado
    {
		private int _id;
		private string _nome;
		private string _sigla;

		public int Id { get => _id; set => _id = value; }

		public string Nome { get => _nome; set => _nome = value; }

		public string Sigla { get => _sigla; set => _sigla = value; }

		public Estado GetById(int id)
		{
			return id > 0 ? new EstadoDAO().GetById(id) : null;
		}

		public List<Estado> GetAll()
		{
			return new EstadoDAO().GetAll();
		}

		public JObject ToJObject()
		{
			JObject json = new JObject();
			json.Add("id", _id);
			json.Add("nome", _nome);
			json.Add("sigla", _sigla);

			return json;
		}
    }
}
