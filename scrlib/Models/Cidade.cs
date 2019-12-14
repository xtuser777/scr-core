using scrlib.DAO;
using System;
using System.Collections.Generic;
using System.Text;

namespace scrlib.Models
{
    internal class Cidade
    {
		private int _id;
		private string _nome;
		private int _estado;

		internal int Estado
		{
			get { return _estado; }
			set { _estado = value; }
		}

		internal string Nome
		{
			get { return _nome; }
			set { _nome = value; }
		}

		internal int Id
		{
			get { return _id; }
			set { _id = value; }
		}

		internal Cidade GetById(int id)
		{
			Cidade cidade = null;
			if (id > 0)
			{
				cidade = new CidadeDAO().GetById(id);
			}
			return cidade;
		}

		internal List<Cidade> GetByEstado(int estado)
		{
			List<Cidade> cidades = null;
			if (estado > 0)
			{
				cidades = new CidadeDAO().GetByEstado(estado);
			}
			return cidades;
		}

		internal List<Cidade> Get()
		{
			List<Cidade> cidades = null;
			cidades = new CidadeDAO().Get();
			return cidades;
		}
	}
}
