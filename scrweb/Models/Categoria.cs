using System;
using System.Collections.Generic;
using scrweb.DAO;

namespace scrweb.Models
{
    internal class Categoria
    {
        private int _id;
        private string _descricao;

        internal string Descricao
        {
            get { return _descricao; }
            set { _descricao = value; }
        }
        
        internal int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        
        internal static Categoria GetById(int id)
        {
            return id > 0 ? new CategoriaDAO().GetById(id) : null;
        }

        internal static List<Categoria> GetAll()
        {
            return new CategoriaDAO().GetAll();
        }

        internal int Gravar()
        {
            return _id == 0 && !string.IsNullOrEmpty(_descricao) ? new CategoriaDAO().Gravar(this) : -5;
        }

        internal int Alterar()
        {
            return _id > 0 && !string.IsNullOrEmpty(_descricao) ? new CategoriaDAO().Alterar(this) : -5;
        }

        internal static int Excluir(int id)
        {
            return id > 0 ? new CategoriaDAO().Excluir(id) : -5;
        }
    }
}