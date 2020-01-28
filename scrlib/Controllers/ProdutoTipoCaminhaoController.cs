using System.Collections.Generic;
using System.Linq;
using scrlib.DAO;
using scrlib.Models;
using scrlib.ViewModels;

namespace scrlib.Controllers
{
    public class ProdutoTipoCaminhaoController
    {
        public ProdutoTipoCaminhaoViewModel GetById(int produto, int tipo)
        {
            var item = new ProdutoTipoCaminhao().GetById(produto,tipo);

            return item != null
                ? new ProdutoTipoCaminhaoViewModel()
                {
                    Produto = new ProdutoController().GetById(item.Produto),
                    Tipo = new TipoCaminhaoController().GetById(item.Tipo)
                }
                : null;
        }

        public List<ProdutoTipoCaminhaoViewModel> GetAll()
        {
            var list = new ProdutoTipoCaminhao().GetAll();

            return list != null && list.Count > 0
                ? list.Select(item => new ProdutoTipoCaminhaoViewModel()
                {
                    Produto = new ProdutoController().GetById(item.Produto),
                    Tipo = new TipoCaminhaoController().GetById(item.Tipo)
                }).ToList()
                : new List<ProdutoTipoCaminhaoViewModel>();
        }

        public List<ProdutoTipoCaminhaoViewModel> GetPorProduto(int produto)
        {
            var list = new ProdutoTipoCaminhao().GetPorProduto(produto);

            return list != null
                ? list.Select(item => new ProdutoTipoCaminhaoViewModel()
                {
                    Produto = new ProdutoController().GetById(item.Produto),
                    Tipo = new TipoCaminhaoController().GetById(item.Tipo)
                }).ToList()
                : new List<ProdutoTipoCaminhaoViewModel>();
        }

        public int Gravar(ProdutoTipoCaminhaoViewModel ptcvm)
        {
            return new ProdutoTipoCaminhao()
            {
                Produto = ptcvm.Produto.Id,
                Tipo = ptcvm.Tipo.Id
            }.Gravar();
        }

        public int Excluir(int produto, int tipo)
        {
            return new ProdutoTipoCaminhao().Excluir(produto, tipo);
        }

        public int ExcluirPorProduto(int produto)
        {
            return new ProdutoTipoCaminhao().ExcluirPorProduto(produto);
        }

        public int ExcluirPorTipo(int tipo)
        {
            return new ProdutoTipoCaminhao().ExcluirPorTipo(tipo);
        }
    }
}