using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using scrweb.DAO;

namespace scrweb.Models
{
    public class Motorista
    {
        private int _id;
        private DateTime _cadastro;
        private PessoaFisica _pessoa;

        public int Id { get => _id; set => _id = value; }
        public DateTime Cadastro { get => _cadastro; set => _cadastro = value; }
        public PessoaFisica Pessoa { get => _pessoa; set => _pessoa = value; }

        public Motorista GetById(int id)
        {
            return id > 0 ? new MotoristaDAO().GetById(id) : null;
        }

        public List<Motorista> GetAll()
        {
            return new MotoristaDAO().GetAll();
        }

        public int Gravar()
        {
            if (_id != 0 || _pessoa == null) return -5;
            
            return new MotoristaDAO().Gravar(this);
        }

        public int Alterar()
        {
            if (_id <= 0 || _pessoa == null) return -5;
            
            return new MotoristaDAO().Alterar(this);
        }

        public int Excluir(int id)
        {
            return id > 0 ? new MotoristaDAO().Excluir(id) : -5;
        }

        public JObject ToJObject()
        {
            JObject json = new JObject();
            json.Add("id", _id);
            json.Add("cadastro", _cadastro);
            json.Add("pessoa", _pessoa.ToJObject());

            return json;
        }
    }
}