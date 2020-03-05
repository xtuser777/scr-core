using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using scrweb.DAO;

namespace scrweb.Models 
{
	public class Caminhao
	{
		private int _id;
		private string _placa;
		private string _marca;
		private string _modelo;
		private string _ano;
		private TipoCaminhao _tipo;
		private Motorista _proprietario;
		
		public int Id { get => _id; set => _id = value; }
		public string Placa { get => _placa; set => _placa = value; }
		public string Marca { get => _marca; set => _marca = value; }
		public string Modelo { get => _modelo; set => _modelo = value; }
		public string Ano { get => _ano; set => _ano = value; }
		public TipoCaminhao Tipo { get => _tipo; set => _tipo = value; }
		public Motorista Proprietario { get => _proprietario; set => _proprietario = value; }

		public Caminhao GetById(int id)
		{
			return id > 0 ? new CaminhaoDAO().GetById(id) : null;
		}

		public List<Caminhao> GetAll()
		{
			return new CaminhaoDAO().GetAll();
		}

		public int Gravar()
		{
			if (_id != 0 || string.IsNullOrEmpty(_placa) || string.IsNullOrEmpty(_marca) ||
			    string.IsNullOrEmpty(_modelo) || string.IsNullOrEmpty(_ano) || _tipo == null ||
			    _proprietario == null) return -5;
			
			return new CaminhaoDAO().Gravar(this);
		}

		public int Alterar()
		{
			if (_id <= 0 || string.IsNullOrEmpty(_placa) || string.IsNullOrEmpty(_marca) ||
			    string.IsNullOrEmpty(_modelo) || string.IsNullOrEmpty(_ano) || _tipo == null ||
			    _proprietario == null) return -5;
			
			return new CaminhaoDAO().Alterar(this);
		}

		public int Excluir(int id)
		{
			return id > 0 ? new CaminhaoDAO().Excluir(id) : -5;
		}

		public JObject ToJObject()
		{
			JObject json = new JObject();
			json.Add("id", _id);
			json.Add("placa", _placa);
			json.Add("marca", _marca);
			json.Add("modelo", _modelo);
			json.Add("ano", _ano);
			json.Add("tipo", _tipo.ToJObject());
			json.Add("proprietario", _proprietario.ToJObject());

			return json;
		}
	}
}
