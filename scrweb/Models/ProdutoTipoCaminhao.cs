using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using scrweb.DAO;

namespace scrweb.Models
{
    public class ProdutoTipoCaminhao
    {
        private Produto _produto;
        private TipoCaminhao _tipo;

        public Produto Produto { get => _produto; set => _produto = value; }
        public TipoCaminhao Tipo { get => _tipo; set => _tipo = value; }

        public ProdutoTipoCaminhao GetById(int produto, int tipo)
        {
            return produto > 0 && tipo > 0 ? new ProdutoTipoCaminhaoDAO().GetById(produto, tipo) : null;
        }

        public List<ProdutoTipoCaminhao> GetAll()
        {
            return new ProdutoTipoCaminhaoDAO().GetAll();
        }

        public List<ProdutoTipoCaminhao> GetByProduct(int produto)
        {
            return produto > 0 ? new ProdutoTipoCaminhaoDAO().GetPorProduto(produto) : null;
        }

        public int Gravar()
        {
            if (_produto == null || _tipo == null) return -5;
            
            return new ProdutoTipoCaminhaoDAO().Gravar(this);
        }

        public int Excluir(int produto, int tipo)
        {
            return produto > 0 && tipo > 0 ? new ProdutoTipoCaminhaoDAO().Excluir(produto, tipo) : -5;
        }

        public int ExcluirByProduto(int produto)
        {
            return produto > 0 ? new ProdutoTipoCaminhaoDAO().ExcluirPorProduto(produto) : -5;
        }

        public int ExcluirByTipo(int tipo)
        {
            return tipo > 0 ? new ProdutoTipoCaminhaoDAO().ExcluirPorTipo(tipo) : -5;
        }

        public JObject ToJObject()
        {
            JObject json = new JObject();
            json.Add("produto", _produto.ToJObject());
            json.Add("tipo", _tipo.ToJObject());

            return json;
        }
    }
}