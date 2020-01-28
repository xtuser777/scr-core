using System.Collections.Generic;
using scrlib.DAO;

namespace scrlib.Models
{
    internal class TipoCaminhao
    {
        private int _id;
        private string _descricao;
        private int _eixos;
        private decimal _capacidade;

        internal int Id
        {
            get => _id;
            set => _id = value;
        }

        internal string Descricao
        {
            get => _descricao;
            set => _descricao = value;
        }

        internal int Eixos
        {
            get => _eixos;
            set => _eixos = value;
        }

        internal decimal Capacidade
        {
            get => _capacidade;
            set => _capacidade = value;
        }

        internal TipoCaminhao GetById(int id)
        {
            return id > 0 ? new TipoCaminhaoDAO().GetById(id) : null;
        }

        internal List<TipoCaminhao> GetAll()
        {
            return new TipoCaminhaoDAO().GetAll();
        }

        internal int Gravar()
        {
            return _id == 0 && !string.IsNullOrEmpty(_descricao) && _eixos > 0 && _capacidade > 0
                ? new TipoCaminhaoDAO().Gravar(this)
                : -5;
        }

        internal int Alterar()
        {
            return _id > 0 && !string.IsNullOrEmpty(_descricao) && _eixos > 0 && _capacidade > 0
                ? new TipoCaminhaoDAO().Alterar(this)
                : -5;
        }

        internal int Excluir(int id)
        {
            return id > 0 ? new TipoCaminhaoDAO().Excluir(id) : -5;
        }
    }
}