using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using scrweb.DAO;

namespace scrweb.Models
{
    public class Produto
    {
        private int _id;
        private string _descricao;
        private string _medida;
        private decimal _preco;
        private decimal _precoOut;
        private Representacao _representacao;

        public int Id { get => _id; set => _id = value; }
        public string Descricao { get => _descricao; set => _descricao = value; }
        public string Medida { get => _medida; set => _medida = value; }
        public decimal Preco { get => _preco; set => _preco = value; }
        public decimal PrecoOut { get => _precoOut; set => _precoOut = value; }
        public Representacao Representacao { get => _representacao; set => _representacao = value; }

        public Produto GetById(int id)
        {
            return id > 0 ? new ProdutoDAO().GetById(id) : null;
        }

        public List<Produto> GetAll()
        {
            return new ProdutoDAO().GetAll();
        }

        public int Gravar()
        {
            if (_id != 0 || string.IsNullOrEmpty(_descricao) || string.IsNullOrEmpty(_medida) || _preco <= 0 ||
                _precoOut <= 0 || _representacao == null) return -5;
            
            return new ProdutoDAO().Gravar(this);
        }

        public int Alterar()
        {
            if (_id <= 0 || string.IsNullOrEmpty(_descricao) || string.IsNullOrEmpty(_medida) || _preco <= 0 ||
                _precoOut <= 0 || _representacao == null) return -5;
            
            return new ProdutoDAO().Alterar(this);
        }

        public int Excluir(int id)
        {
            return id > 0 ? new ProdutoDAO().Excluir(id) : -5;
        }

        public JObject ToJObject()
        {
            JObject json = new JObject();
            json.Add("id", _id);
            json.Add("descricao", _descricao);
            json.Add("medida", _medida);
            json.Add("preco", _preco);
            json.Add("precoOut", _precoOut);
            json.Add("representacao", _representacao.ToJObject());

            return json;
        }
    }
}