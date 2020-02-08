using System;
using System.Collections.Generic;
using scrlib.DAO;

namespace scrlib.Models
{
    internal class FormaPagamento
    {
        private int id;
        private string descricao;
        private int prazo;

        internal int Prazo
        {
            get { return prazo; }
            set { prazo = value; }
        }
        
        internal string Descricao
        {
            get { return descricao; }
            set { descricao = value; }
        }
        
        internal int Id
        {
            get { return id; }
            set { id = value; }
        }
        
        internal static FormaPagamento GetById(int id) 
        {
            return id > 0 ? new FormaPagamentoDAO().GetById(id) : null;
        }

        internal static List<FormaPagamento> GetAll()
        {
            return new FormaPagamentoDAO().GetAll();
        }

        internal int Gravar()
        {
            return id == 0 && prazo > 0 && !string.IsNullOrEmpty(descricao) ? new FormaPagamentoDAO().Gravar(this) : -5;
        }

        internal int Alterar()
        {
            return id > 0 && prazo > 0 && !string.IsNullOrEmpty(descricao) ? new FormaPagamentoDAO().Alterar(this) : -5;
        }

        internal static int Excluir(int id)
        {
            return id > 0 ? new FormaPagamentoDAO().Excluir(id) : -5;
        }
    }
}