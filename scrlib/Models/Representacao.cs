using System;
using System.Collections.Generic;
using scrlib.DAO;

namespace scrlib.Models
{
    internal class Representacao
    {
        private int _id;
        private DateTime _cadastro;
        private string _unidade;
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

        internal string Unidade
        {
            get => _unidade;
            set => _unidade = value;
        }

        internal int Pessoa
        {
            get => _pessoa;
            set => _pessoa = value;
        }

        internal Representacao GetById(int id)
        {
            return id > 0 ? new RepresentacaoDAO().GetById(id) : null;
        }

        internal List<Representacao> GetAll()
        {
            return new RepresentacaoDAO().GetAll();
        }

        internal int Gravar()
        {
            return _id == 0 && !string.IsNullOrEmpty(_unidade) && _pessoa > 0
                ? new RepresentacaoDAO().Gravar(this)
                : -5;
        }

        internal int Alterar()
        {
            return _id > 0 && !string.IsNullOrEmpty(_unidade) && _pessoa > 0
                ? new RepresentacaoDAO().Alterar(this)
                : -5;
        }

        internal int Excluir(int id)
        {
            return id > 0 ? new RepresentacaoDAO().Excluir(id) : -5;
        }
    }
}