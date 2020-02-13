using scrweb.DAO;
using System;
using System.Collections.Generic;
using System.Text;

namespace scrweb.Models
{
    internal class Nivel
    {
        private int _id;
        private string _descricao;

        internal string Descricao
        {
            get => _descricao;
            set => _descricao = value;
        }

        internal int Id
        {
            get => _id;
            set => _id = value;
        }

        internal Nivel GetById(int id)
        {
            Nivel nivel = null;
            if (id > 0)
            {
                nivel = new NivelDAO().GetById(id);
            }
            return nivel;
        }

        internal List<Nivel> Get()
        {
            List<Nivel> niveis = null;
            niveis = new NivelDAO().Get();
            return niveis;
        }
    }
}
