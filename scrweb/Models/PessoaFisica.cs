using System;
using Newtonsoft.Json.Linq;
using scrweb.DAO;

namespace scrweb.Models
{
    public class PessoaFisica
    {
        private int _id;
        private string _nome;
        private string _rg;
        private string _cpf;
        private DateTime _nascimento;
        private Contato _contato;

        public int Id { get => _id; set => _id = value; }
        public string Nome { get => _nome; set => _nome = value; }
        public string Rg { get => _rg; set => _rg = value; }
        public string Cpf { get => _cpf; set => _cpf = value; }
        public DateTime Nascimento { get => _nascimento; set => _nascimento = value; }
        public Contato Contato { get => _contato; set => _contato = value; }
        
        public bool VerifyCpf(string cpf)
        {
            return !string.IsNullOrEmpty(cpf) && new PessoaFisicaDAO().CountCpf(cpf) > 0;
        }

        public PessoaFisica GetById(int id)
        {
            return id > 0 ? new PessoaFisicaDAO().GetById(id) : null;
        }

        public int Gravar()
        {
            if (_id != 0 || string.IsNullOrEmpty(_nome) || string.IsNullOrEmpty(_rg) ||
                string.IsNullOrEmpty(_cpf)
            ) return -5;
            
            return new PessoaFisicaDAO().Gravar(this);
        }

        public int Alterar()
        {
            if (_id <= 0 || string.IsNullOrEmpty(_nome) || string.IsNullOrEmpty(_rg) ||
                string.IsNullOrEmpty(_cpf)
            ) return -5;
            
            return new PessoaFisicaDAO().Alterar(this);
        }

        public int Excluir(int id)
        {
            return id > 0 ? new PessoaFisicaDAO().Excluir(id) : -5;
        }

        public JObject ToJObject()
        {
            JObject json = new JObject();
            json.Add("id", _id);
            json.Add("nome", _nome);
            json.Add("rg", _rg);
            json.Add("cpf", _cpf);
            json.Add("nascimento", _nascimento);
            json.Add("contato", _contato.ToJObject());

            return json;
        }
    }
}
