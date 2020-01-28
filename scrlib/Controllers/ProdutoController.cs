using System.Collections.Generic;
using System.Linq;
using scrlib.DAO;
using scrlib.Models;
using scrlib.ViewModels;

namespace scrlib.Controllers
{
    public class ProdutoController
    {
        public ProdutoViewModel GetById(int id)
        {
            var p = new Produto().GetById(id);

            return ((p != null)
                ? new ProdutoViewModel()
                {
                    Id = p.Id,
                    Descricao = p.Descricao,
                    Medida = p.Medida,
                    Preco = p.Preco,
                    PrecoOut = p.PrecoOut,
                    Representacao = new RepresentacaoController().GetById(p.Representacao)
                }
                : null
            );
        }

        public List<ProdutoViewModel> GetAll()
        {
            var p = new Produto().GetAll();

            return ((p != null && p.Count > 0)
                ? p.Select(pd => new ProdutoViewModel()
                {
                    Id = pd.Id,
                    Descricao = pd.Descricao,
                    Medida = pd.Medida,
                    Preco = pd.Preco,
                    PrecoOut = pd.PrecoOut,
                    Representacao = new RepresentacaoController().GetById(pd.Representacao)
                }).ToList()
                : new List<ProdutoViewModel>()
            );
        }

        public int Gravar(ProdutoViewModel pvm)
        {
            return new Produto()
            {
                Id = pvm.Id,
                Descricao = pvm.Descricao,
                Medida = pvm.Medida,
                Preco = pvm.Preco,
                PrecoOut = pvm.PrecoOut,
                Representacao = pvm.Representacao.Id
            }.Gravar();
        }
        
        public int Alterar(ProdutoViewModel pvm)
        {
            return new Produto()
            {
                Id = pvm.Id,
                Descricao = pvm.Descricao,
                Medida = pvm.Medida,
                Preco = pvm.Preco,
                PrecoOut = pvm.PrecoOut,
                Representacao = pvm.Representacao.Id
            }.Alterar();
        }

        public int Excluir(int id)
        {
            return new Produto().Excluir(id);
        }
    }
}