using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using scrweb.DAO;

namespace scrweb.Models
{
    public class Cliente
    {
        private int _id;
        private DateTime _cadastro;
        private int _tipo;
        private PessoaFisica _pessoaFisica;
        private PessoaJuridica _pessoaJuridica;

        public int Id { get => _id; set => _id = value; }
        public DateTime Cadastro { get => _cadastro; set => _cadastro = value; }
        public int Tipo { get => _tipo; set => _tipo = value; }
        public PessoaFisica PessoaFisica { get => _pessoaFisica; set => _pessoaFisica = value; }
        public PessoaJuridica PessoaJuridica { get => _pessoaJuridica; set => _pessoaJuridica = value; }

        public Cliente GetById(int id)
        {
            return id > 0 ? new ClienteDAO().GetById(id) : null;
        }

        public List<Cliente> GetAll()
        {
            return new ClienteDAO().GetAll();
        }

        public int Gravar()
        {
            if (_id != 0 || _tipo <= 0 || (_pessoaFisica == null && _pessoaJuridica == null)) return -5;
            
            return new ClienteDAO().Gravar(this);
        }

        public int Alterar()
        {
            if (_id <= 0 || _tipo <= 0 || (_pessoaFisica == null && _pessoaJuridica == null)) return -5;
            
            return new ClienteDAO().Alterar(this);
        }

        public int Excluir(int id)
        {
            return id > 0 ? new ClienteDAO().Excluir(id) : -5;
        }

        public JObject ToJObject()
        {
            JObject json = new JObject();
            json.Add("id", _id);
            json.Add("cadastro", _cadastro);
            json.Add("tipo", _tipo);
            if (_tipo == 1)
            {
                json.Add("pessoa", _pessoaFisica.ToJObject());
            }
            else
            {
                json.Add("pessoa", _pessoaJuridica.ToJObject());
            }

            return json;
        }
    }
}