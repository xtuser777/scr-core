using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using scrweb.DAO;

namespace scrweb.Models
{
    public class Endereco
    {
        private int _id;
        private string _rua;
        private string _numero;
        private string _bairro;
        private string _complemento;
        private string _cep;
        private Cidade _cidade;

        public int Id { get => _id; set => _id = value; }
        public string Rua {get => _rua; set => _rua = value; }
        public string Numero { get => _numero; set => _numero = value; }
        public string Bairro { get => _bairro; set => _bairro = value; }
        public string Complemento { get => _complemento; set => _complemento = value; }
        public string Cep { get => _cep; set => _cep = value; }
        public Cidade Cidade { get => _cidade; set => _cidade = value; }

        public Endereco GetById(int id)
        {
            return id > 0 ? new EnderecoDAO().GetById(id) : null;
        }

        public List<Endereco> GetAll()
        {
            return new EnderecoDAO().GetAll();
        }

        public int Gravar()
        {
            if (_id != 0 || string.IsNullOrEmpty(_rua) || string.IsNullOrEmpty(_numero) ||
                string.IsNullOrEmpty(_bairro) || string.IsNullOrEmpty(_cep) || _cidade == null
            ) return -5;
            
            return new EnderecoDAO().Gravar(this);
        }

        public int Alterar()
        {
            if (_id <= 0 || string.IsNullOrEmpty(_rua) || string.IsNullOrEmpty(_numero) ||
                string.IsNullOrEmpty(_bairro) || string.IsNullOrEmpty(_cep) || _cidade == null
            ) return -5;
            
            return new EnderecoDAO().Alterar(this);
        }

        public int Excluir(int id)
        {
            return id > 0 ? new EnderecoDAO().Excluir(id) : -5;
        }

        public JObject ToJObject()
        {
            JObject json = new JObject();
            json.Add("id", _id);
            json.Add("rua", _rua);
            json.Add("numero", _numero);
            json.Add("bairro", _bairro);
            json.Add("complemento", _complemento);
            json.Add("cep", _cep);
            json.Add("cidade", _cidade.ToJObject());

            return json;
        }
    }
}
