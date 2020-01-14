using System;
using System.Collections.Generic;
using scrlib.DAO;

namespace scrlib.Models
{
    internal class Cliente
    {
        private int _id;
        private DateTime _cadastro;
        private int _tipo;
        private int _pessoa;

        internal int Id
        {
            get => _id;
            set => _id = value;
        }

        internal DateTime Cadastro
        {
            get => _cadastro;
            set => _cadastro = value;
        }

        internal int Tipo
        {
            get => _tipo;
            set => _tipo = value;
        }

        internal int Pessoa
        {
            get => _pessoa;
            set => _pessoa = value;
        }

        internal Cliente GetById(int id)
        {
            return id > 0 ? new ClienteDAO().GetById(id) : null;
        }

        internal List<Cliente> GetByFilter(string chave)
        {
            return !string.IsNullOrEmpty(chave) ? new ClienteDAO().GetByFilter(chave) : null;
        }

        internal List<Cliente> GetByCad(DateTime cadastro)
        {
            return new ClienteDAO().GetByCad(cadastro);
        }

        internal List<Cliente> GetByFilterCad(string chave, DateTime cadastro)
        {
            return !string.IsNullOrEmpty(chave) ? new ClienteDAO().GetByFilterAndCad(chave, cadastro) : null;
        }

        internal List<Cliente> GetAll()
        {
            return new ClienteDAO().GetAll();
        }

        internal int Gravar()
        {
            if (_id == 0 && _tipo > 0 && _pessoa > 0)
            {
                return new ClienteDAO().Gravar(this);
            }

            return -5;
        }

        internal int Alterar()
        {
            if (_id > 0 && _tipo > 0 && _pessoa > 0)
            {
                return new ClienteDAO().Alterar(this);
            }

            return -5;
        }

        internal int Excluir(int id)
        {
            return id > 0 ? new ClienteDAO().Excluir(id) : -5;
        }
    }
}