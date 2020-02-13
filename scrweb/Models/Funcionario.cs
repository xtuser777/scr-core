using scrweb.DAO;
using System;
using System.Collections.Generic;
using System.Text;

namespace scrweb.Models
{
    internal class Funcionario
    {
        private int _id;
        private int _tipo;
        private DateTime _admissao;
        private DateTime? _demissao;
        private int _pessoa;

        internal int Id { get => _id; set => _id = value; }
        internal int Tipo { get => _tipo; set => _tipo = value; }
        internal DateTime Admissao { get => _admissao; set => _admissao = value; }
        internal DateTime? Demissao { get => _demissao; set => _demissao = value; }
        internal int Pessoa { get => _pessoa; set => _pessoa = value; }

        internal Funcionario GetById(int id)
        {
            if (id > 0)
            {
                return new FuncionarioDAO().GetById(id);
            }
            return null;
        }

        internal List<Funcionario> Get()
        {
            return new FuncionarioDAO().Get();
        }

        internal Funcionario GetVendedorById(int id)
        {
            if (id > 0)
            {
                return new FuncionarioDAO().GetVendedorById(id);
            }
            return null;
        }

        internal List<Funcionario> GetVendedores()
        {
            return new FuncionarioDAO().GetVendedores();
        }

        internal int Gravar()
        {
            if (_id == 0 && _tipo > 0 && _pessoa > 0)
            {
                return new FuncionarioDAO().Gravar(this);
            }
            return -10;
        }

        internal int Alterar()
        {
            if (_id > 0 && _tipo > 0 && _pessoa > 0)
            {
                return new FuncionarioDAO().Alterar(this);
            }
            return -10;
        }

        internal int Excluir(int id)
        {
            if (id > 0)
            {
                return new FuncionarioDAO().Excluir(id);
            }
            return -10;
        }

        internal int Desativar(int id)
        {
            if (id > 0)
            {
                return new FuncionarioDAO().Desativar(id);
            }
            return -10;
        }

        internal int Reativar(int id)
        {
            if (id > 0)
            {
                return new FuncionarioDAO().Reativar(id);
            }
            return -10;
        }
    }
}
