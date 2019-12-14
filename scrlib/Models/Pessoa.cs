using scrlib.DAO;
using System;
using System.Collections.Generic;
using System.Text;

namespace scrlib.Models
{
    internal class Pessoa
    {
        private int _id;
        private int _tipo;
        private string _telefone;
        private string _celular;
        private string _email;
        private int _endereco;

        internal int Id
        {
            get => _id;
            set => _id = value;
        }

        internal int Tipo
        {
            get => _tipo;
            set => _tipo = value;
        }

        internal string Telefone
        {
            get => _telefone;
            set => _telefone = value;
        }

        internal string Celular
        {
            get => _celular;
            set => _celular = value;
        }

        internal string Email
        {
            get => _email;
            set => _email = value;
        }

        internal int Endereco
        {
            get => _endereco;
            set => _endereco = value;
        }

        internal List<Pessoa> Get()
        {
            return new PessoaDAO().Get();
        }
    }
}
