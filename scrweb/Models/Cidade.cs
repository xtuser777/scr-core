using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using scrweb.DAO;

namespace scrweb.Models
{
    public class Cidade
    {
		private int _id;
		private string _nome;
		private Estado _estado;

		public Estado Estado { get { return _estado; } set { _estado = value; } }

		public string Nome { get { return _nome; } set { _nome = value; } }

		public int Id { get { return _id; } set { _id = value; } }

		public Cidade GetById(int id)
		{
			return id > 0 ? new CidadeDAO().GetById(id) : null;
		}

		public List<Cidade> GetByEstado(int estado)
		{
			return estado > 0 ? new CidadeDAO().GetByEstado(estado) : null;
		}

		public List<Cidade> GetAll()
		{
			return new CidadeDAO().GetAll();
		}

		public JObject ToJObject()
		{
			JObject json = new JObject();
			json.Add("id", _id);
			json.Add("nome", _nome);
			json.Add("estado", _estado.ToJObject());

			return json;
		}
	}
}
