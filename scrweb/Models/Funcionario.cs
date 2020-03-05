using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using scrweb.DAO;

namespace scrweb.Models
{
    public class Funcionario
    {
        private int _id;
        private int _tipo;
        private DateTime _admissao;
        private DateTime? _demissao;
        private PessoaFisica _pessoa;

        public int Id { get => _id; set => _id = value; }
        public int Tipo { get => _tipo; set => _tipo = value; }
        public DateTime Admissao { get => _admissao; set => _admissao = value; }
        public DateTime? Demissao { get => _demissao; set => _demissao = value; }
        public PessoaFisica Pessoa { get => _pessoa; set => _pessoa = value; }

        public Funcionario GetById(int id)
        {
            if (id > 0)
            {
                return new FuncionarioDAO().GetById(id);
            }
            return null;
        }

        public List<Funcionario> GetAll()
        {
            return new FuncionarioDAO().GetAll();
        }

        public Funcionario GetVendedorById(int id)
        {
            if (id > 0)
            {
                return new FuncionarioDAO().GetVendedorById(id);
            }
            return null;
        }

        public List<Funcionario> GetVendedores()
        {
            return new FuncionarioDAO().GetVendedores();
        }

        public int Gravar()
        {
            if (_id == 0 && _tipo > 0 && _pessoa != null)
            {
                return new FuncionarioDAO().Gravar(this);
            }
            return -10;
        }

        public int Alterar()
        {
            if (_id > 0 && _tipo > 0 && _pessoa != null)
            {
                return new FuncionarioDAO().Alterar(this);
            }
            return -10;
        }

        public int Excluir(int id)
        {
            if (id > 0)
            {
                return new FuncionarioDAO().Excluir(id);
            }
            return -10;
        }

        public int Desativar(int id)
        {
            if (id > 0)
            {
                return new FuncionarioDAO().Desativar(id);
            }
            return -10;
        }

        public int Reativar(int id)
        {
            if (id > 0)
            {
                return new FuncionarioDAO().Reativar(id);
            }
            return -10;
        }

        public JObject ToJObject()
        {
            JObject json = new JObject();
            json.Add("id", _id);
            json.Add("tipo", _tipo);
            json.Add("admissao", _admissao);
            json.Add("demissao", _demissao);
            json.Add("pessoa", _pessoa.ToJObject());

            return json;
        }
    }
}
