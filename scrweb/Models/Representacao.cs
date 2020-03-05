using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using scrweb.DAO;

namespace scrweb.Models
{
    public class Representacao
    {
        private int _id;
        private DateTime _cadastro;
        private string _unidade;
        private PessoaJuridica _pessoa;

        public int Id { get => _id; set => _id = value; }
        public DateTime Cadastro { get => _cadastro; set => _cadastro = value; }
        public string Unidade { get => _unidade; set => _unidade = value; }
        public PessoaJuridica Pessoa { get => _pessoa; set => _pessoa = value; }

        public Representacao GetById(int id)
        {
            return id > 0 ? new RepresentacaoDAO().GetById(id) : null;
        }

        public List<Representacao> GetAll()
        {
            return new RepresentacaoDAO().GetAll();
        }

        public int Gravar()
        {
            if (_id != 0 || string.IsNullOrEmpty(_unidade) || _pessoa == null) return -5;
            
            return new RepresentacaoDAO().Gravar(this);
        }

        public int Alterar()
        {
            if (_id <= 0 || string.IsNullOrEmpty(_unidade) || _pessoa == null) return -5;
            
            return new RepresentacaoDAO().Alterar(this);
        }

        public int Excluir(int id)
        {
            return id > 0 ? new RepresentacaoDAO().Excluir(id) : -5;
        }

        public JObject ToJObject()
        {
            JObject json = new JObject();
            json.Add("id", _id);
            json.Add("cadastro", _cadastro);
            json.Add("unidade", _unidade);
            json.Add("pessoa", _pessoa.ToJObject());

            return json;
        }
    }
}