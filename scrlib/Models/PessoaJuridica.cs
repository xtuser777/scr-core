using scrlib.DAO;
using System;
using System.Collections.Generic;
using System.Text;

namespace scrlib.Models
{
    internal class PessoaJuridica : Pessoa
    {
        private string _razao_social;
        private string _nome_fantasia;
        private string _cnpj;

        internal string RazaoSocial
        {
            get => _razao_social;
            set => _razao_social = value;
        }

        internal string NomeFantasia
        {
            get => _nome_fantasia;
            set => _nome_fantasia = value;
        }

        internal string Cnpj
        {
            get => _cnpj;
            set => _cnpj = value;
        }

        internal PessoaJuridica GetById(int id)
        {
            return id > 0 ? new PessoaJuridicaDAO().GetById(id) : null;
        }

        internal bool VerifyCnpj(string cnpj)
        {
            return !string.IsNullOrEmpty(cnpj) && new PessoaJuridicaDAO().CountCnpj(cnpj) > 0;
        }

        internal int Gravar()
        {
            if (Id == 0 && Tipo == 2 && !string.IsNullOrEmpty(Telefone) && !string.IsNullOrEmpty(Celular) &&
                !string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(_razao_social) &&
                !string.IsNullOrEmpty(_nome_fantasia) && !string.IsNullOrEmpty(_cnpj) && Endereco > 0)
            {
                return new PessoaJuridicaDAO().Gravar(this);
            }

            return -10;
        }

        internal int Alterar()
        {
            if (Id > 0 && Tipo == 2 && !string.IsNullOrEmpty(Telefone) && !string.IsNullOrEmpty(Celular) &&
                !string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(_razao_social) &&
                !string.IsNullOrEmpty(_nome_fantasia) && !string.IsNullOrEmpty(_cnpj) && Endereco > 0)
            {
                return new PessoaJuridicaDAO().Alterar(this);
            }

            return -10;
        }

        internal int Excluir(int id)
        {
            if (id > 0)
            {
                return new PessoaJuridicaDAO().Excluir(id);
            }

            return -1;
        }
    }
}
