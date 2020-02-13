using System.Collections.Generic;
using scrweb.DAO;

namespace scrweb.Models
{
    internal class ProdutoTipoCaminhao
    {
        private int _produto;
        private int _tipo;

        internal int Produto { get => _produto; set => _produto = value; }

        internal int Tipo { get => _tipo; set => _tipo = value; }

        internal ProdutoTipoCaminhao GetById(int produto, int tipo)
        {
            return produto > 0 && tipo > 0 ? new ProdutoTipoCaminhaoDAO().GetById(produto, tipo) : null;
        }

        internal List<ProdutoTipoCaminhao> GetAll()
        {
            return new ProdutoTipoCaminhaoDAO().GetAll();
        }

        internal List<ProdutoTipoCaminhao> GetPorProduto(int produto)
        {
            return produto > 0 ? new ProdutoTipoCaminhaoDAO().GetPorProduto(produto) : null;
        }

        internal int Gravar()
        {
            return _produto > 0 && _tipo > 0 ? new ProdutoTipoCaminhaoDAO().Gravar(this) : -5;
        }

        internal int Excluir(int produto, int tipo)
        {
            return produto > 0 && tipo > 0 ? new ProdutoTipoCaminhaoDAO().Excluir(produto, tipo) : -5;
        }

        internal int ExcluirPorProduto(int produto)
        {
            return produto > 0 ? new ProdutoTipoCaminhaoDAO().ExcluirPorProduto(produto) : -5;
        }

        internal int ExcluirPorTipo(int tipo)
        {
            return tipo > 0 ? new ProdutoTipoCaminhaoDAO().ExcluirPorTipo(tipo) : -5;
        }
    }
}