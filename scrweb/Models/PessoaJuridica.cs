using System;
using Newtonsoft.Json.Linq;
using scrweb.DAO;

namespace scrweb.Models
{
    public class PessoaJuridica
    {
        private int _id;
        private string _razao_social;
        private string _nome_fantasia;
        private string _cnpj;
        private Contato _contato;

        public int Id { get => _id; set => _id = value; }
        public string RazaoSocial { get => _razao_social; set => _razao_social = value; }
        public string NomeFantasia { get => _nome_fantasia; set => _nome_fantasia = value; }
        public string Cnpj { get => _cnpj; set => _cnpj = value; }
        public Contato Contato { get => _contato; set => _contato = value; }
        
        public bool VerifyCnpj(string cnpj)
        {
            return !string.IsNullOrEmpty(cnpj) && new PessoaJuridicaDAO().CountCnpj(cnpj) > 0;
        }

        public PessoaJuridica GetById(int id)
        {
            return id > 0 ? new PessoaJuridicaDAO().GetById(id) : null;
        }

        public int Gravar()
        {
            if (_id != 0 || string.IsNullOrEmpty(_razao_social) || string.IsNullOrEmpty(_nome_fantasia) ||
                string.IsNullOrEmpty(_cnpj) || _contato == null
            ) return -5;
            
            return new PessoaJuridicaDAO().Gravar(this);
        }

        public int Alterar()
        {
            if (_id <= 0 || string.IsNullOrEmpty(_razao_social) || string.IsNullOrEmpty(_nome_fantasia) ||
                string.IsNullOrEmpty(_cnpj) || _contato == null
            ) return -5;
            
            return new PessoaJuridicaDAO().Alterar(this);
        }

        public int Excluir(int id)
        {
            return id > 0 ? new PessoaJuridicaDAO().Excluir(id) : -5;
        }

        public JObject ToJObject()
        {
            JObject json = new JObject();
            json.Add("id", _id);
            json.Add("razaoSocial", _razao_social);
            json.Add("nomeFantasia", _nome_fantasia);
            json.Add("cnpj", _cnpj);
            json.Add("contato", _contato.ToJObject());

            return json;
        }
    }
}
