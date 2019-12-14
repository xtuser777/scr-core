using scrlib.DAO;
using System;
using System.Collections.Generic;
using System.Text;

namespace scrlib.Models
{
    internal class PessoaFisica : Pessoa
    {
        private string _nome;
        private string _rg;
        private string _cpf;
        private DateTime _nascimento;

        internal string Nome
        {
            get => _nome;
            set => _nome = value;
        }

        internal string Rg
        {
            get => _rg;
            set => _rg = value;
        }

        internal string Cpf
        {
            get => _cpf;
            set => _cpf = value;
        }

        internal DateTime Nascimento
        {
            get => _nascimento;
            set => _nascimento = value;
        }

        internal PessoaFisica GetById(int id)
        {
            if (id > 0)
            {
                return new PessoaFisicaDAO().GetById(id);
            }

            return null;
        }

        internal int Gravar()
        {
            if (Id == 0 && Tipo == 1 && !string.IsNullOrEmpty(Telefone) && !string.IsNullOrEmpty(Celular) &&
                !string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(_nome) && !string.IsNullOrEmpty(_rg) &&
                !string.IsNullOrEmpty(_cpf) && Endereco > 0)
            {
                return new PessoaFisicaDAO().Gravar(this);
            }

            return -10;
        }

        internal int Alterar()
        {
            if (Id > 0 && Tipo == 1 && !string.IsNullOrEmpty(Telefone) && !string.IsNullOrEmpty(Celular) &&
                !string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(_nome) && !string.IsNullOrEmpty(_rg) &&
                !string.IsNullOrEmpty(_cpf) && Endereco > 0)
            {
                return new PessoaFisicaDAO().Alterar(this);
            }

            return -10;
        }

        internal int Excluir(int id)
        {
            if (id > 0)
            {
                return new PessoaFisicaDAO().Excluir(id);
            }

            return -1;
        }
    }
}
