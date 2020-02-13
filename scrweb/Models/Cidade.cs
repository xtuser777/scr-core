using scrweb.DAO;
using System;
using System.Collections.Generic;
using System.Text;

namespace scrweb.Models
{
    internal class Cidade
    {
		private int _id;
		private string _nome;
		private int _estado;

		internal int Estado { get { return _estado; } set { _estado = value; } }

		internal string Nome { get { return _nome; } set { _nome = value; } }

		internal int Id { get { return _id; } set { _id = value; } }

		internal Cidade GetById(int id)
		{
			return id > 0 ? new CidadeDAO().GetById(id) : null;
		}

		internal List<Cidade> GetByEstado(int estado)
		{
			return estado > 0 ? new CidadeDAO().GetByEstado(estado) : null;
		}
		
		internal List<Cidade> GetByEstAndKey(int estado, string chave)
		{
			if (estado > 0 && !string.IsNullOrEmpty(chave))
			{
				return new CidadeDAO().GetByEstAndKey(estado, chave);
			}
			
			return null;
		}

		internal List<Cidade> Get()
		{
			return new CidadeDAO().Get();
		}
	}
}
