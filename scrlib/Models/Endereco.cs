using scrlib.DAO;
using System;
using System.Collections.Generic;
using System.Text;

namespace scrlib.Models
{
    internal class Endereco
    {
        private int _id;
        private string _rua;
        private string _numero;
        private string _bairro;
        private string _complemento;
        private string _cep;
        private int _cidade;

        internal int Id
        {
            get => _id;
            set => _id = value;
        }

        internal string Rua
        {
            get => _rua;
            set => _rua = value;
        }

        internal string Numero
        {
            get => _numero;
            set => _numero = value;
        }

        internal string Bairro
        {
            get => _bairro;
            set => _bairro = value;
        }

        internal string Complemento
        {
            get => _complemento;
            set => _complemento = value;
        }

        internal string Cep
        {
            get => _cep;
            set => _cep = value;
        }

        internal int Cidade
        {
            get => _cidade;
            set => _cidade = value;
        }

        internal Endereco GetById(int id)
        {
            Endereco e = null;
            if (id > 0)
            {
                e = new EnderecoDAO().GetById(id);
            }
            return e;
        }

        internal List<Endereco> Get()
        {
            return new EnderecoDAO().Get();
        }

        internal int Gravar()
        {
            int res = -10;
            if (_id == 0 && !string.IsNullOrEmpty(_rua) && !string.IsNullOrEmpty(_numero) && !string.IsNullOrEmpty(_bairro) && !string.IsNullOrEmpty(_cep) && _cidade > 0)
            {
                res = new EnderecoDAO().Gravar(this);
            }
            return res;
        }

        internal int Alterar()
        {
            int res = -10;
            if (_id > 0 && !string.IsNullOrEmpty(_rua) && !string.IsNullOrEmpty(_numero) && !string.IsNullOrEmpty(_bairro) && !string.IsNullOrEmpty(_cep) && _cidade > 0)
            {
                res = new EnderecoDAO().Alterar(this);
            }
            return res;
        }

        internal int Excluir(int id)
        {
            int res = -1;
            if (id > 0)
            {
                res = new EnderecoDAO().Excluir(id);
            }
            return res;
        }
    }
}
