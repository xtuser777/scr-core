using System;
using System.Collections.Generic;
using scrweb.DAO;

namespace scrweb.Models 
{
	internal class Caminhao
	{
		private int _id;
		private string _placa;
		private string _marca;
		private string _modelo;
		private string _ano;
		private int _tipo;
		private int _proprietario;
		
		internal int Id { get => _id; set => _id = value; }
		
		internal string Placa { get => _placa; set => _placa = value; }
		
		internal string Marca { get => _marca; set => _marca = value; }
		
		internal string Modelo { get => _modelo; set => _modelo = value; }
		
		internal string Ano { get => _ano; set => _ano = value; }
		
		internal int Tipo { get => _tipo; set => _tipo = value; }
		
		internal int Proprietario { get => _proprietario; set => _proprietario = value; }
		
		internal static Caminhao GetById(int id) 
		{
			return id > 0 ? new CaminhaoDAO().GetById(id) : null;
		}

		internal static List<Caminhao> GetAll()
		{
			return new CaminhaoDAO().GetAll();
		}

		internal int Gravar()
		{
			return _id == 0 && _tipo > 0 && _proprietario > 0 && !string.IsNullOrEmpty(_placa) && !string.IsNullOrEmpty(_marca) && !string.IsNullOrEmpty(_modelo) && !string.IsNullOrEmpty(_ano) 
			? new CaminhaoDAO().Gravar(this)
			: -5; 
		}

		internal int Alterar()
		{
			return _id > 0 && _tipo > 0 && _proprietario > 0 && !string.IsNullOrEmpty(_placa) && !string.IsNullOrEmpty(_marca) && !string.IsNullOrEmpty(_modelo) && !string.IsNullOrEmpty(_ano) 
			? new CaminhaoDAO().Gravar(this)
			: -5; 
		}

		internal static int Excluir(int id)
		{
			return id > 0 ? new CaminhaoDAO().Excluir(id) : -5;
		}
	}
}
