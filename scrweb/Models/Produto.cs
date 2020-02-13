using System.Collections.Generic;
using scrweb.DAO;

namespace scrweb.Models
{
    internal class Produto
    {
        private int _id;
        private string _descricao;
        private string _medida;
        private decimal _preco;
        private decimal _precoOut;
        private int _representacao;

        internal int Id { get => _id; set => _id = value; }

        internal string Descricao { get => _descricao; set => _descricao = value; }

        internal string Medida { get => _medida; set => _medida = value; }

        internal decimal Preco { get => _preco; set => _preco = value; }

        internal decimal PrecoOut { get => _precoOut; set => _precoOut = value; }

        internal int Representacao { get => _representacao; set => _representacao = value; }

        internal Produto GetById(int id)
        {
            return ((id > 0) ? new ProdutoDAO().GetById(id) : null);
        }

        internal List<Produto> GetAll()
        {
            return new ProdutoDAO().GetAll();
        }

        internal int Gravar()
        {
            return ((_id == 0 && !string.IsNullOrEmpty(_descricao) && !string.IsNullOrEmpty(_medida) && _preco > 0 && _precoOut > 0 && _representacao > 0)
                ? new ProdutoDAO().Gravar(this)
                : -5
            );
        }

        internal int Alterar()
        {
            return ((_id > 0 && !string.IsNullOrEmpty(_descricao) && !string.IsNullOrEmpty(_medida) && _preco > 0 && _precoOut > 0 && _representacao > 0)
                ? new ProdutoDAO().Alterar(this)
                : -5
            );
        }

        internal int Excluir(int id)
        {
            return ((id > 0) ? new ProdutoDAO().Excluir(id) : -5);
        }
    }
}