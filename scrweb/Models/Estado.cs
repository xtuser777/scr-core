using scrweb.DAO;
using System;
using System.Collections.Generic;
using System.Text;

namespace scrweb.Models
{
    internal class Estado
    {
		private int _id;
		private string _nome;
		private string _sigla;

		internal string Sigla
		{
			get { return _sigla; }
			set { _sigla = value; }
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

		internal Estado GetById(int id)
		{
			Estado estado = null;
			if (id > 0)
			{
				estado = new EstadoDAO().GetById(id);
			}
			return estado;
		}

		internal List<Estado> Get()
		{
			List<Estado> estados = null;
			estados = new EstadoDAO().Get();
			return estados;
		}
		
		internal List<Estado> GetByFilter(string chave)
		{
			return !string.IsNullOrEmpty(chave) ? new EstadoDAO().GetByFilter(chave) : null;
		}
	}
}
