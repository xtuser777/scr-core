using scrlib.DAO;
using System;
using System.Collections.Generic;
using System.Text;

namespace scrlib.Models
{
    internal class Usuario
    {
        private int _id;
        private string _login;
        private string _senha;
        private int _funcionario;
        private int _nivel;

        internal int Id { get => _id; set => _id = value; }

        internal string Login { get => _login; set => _login = value; }

        internal string Senha { get => _senha; set => _senha = value; }

        internal int Funcionario { get => _funcionario; set => _funcionario = value; }

        internal int Nivel { get => _nivel; set => _nivel = value; }

        internal Usuario Autenticar(string login, string senha)
        {
            Usuario usuario = null;
            if (!string.IsNullOrEmpty(login) && !string.IsNullOrEmpty(senha))
            {
                usuario = new UsuarioDAO().Autenticar(login, senha);
            }
            return usuario;
        }

        internal Usuario GetById(int id)
        {
            Usuario usuario = null;
            if (id > 0)
            {
                usuario = new UsuarioDAO().GetById(id);
            }
            return usuario;
        }

        internal List<Usuario> Get()
        {
            return new UsuarioDAO().Get();
        }

        internal int Gravar()
        {
            int res = -10;
            if (_id == 0) res = new UsuarioDAO().Gravar(this);
            return res;
        }

        internal int Alterar()
        {
            int res = -10;
            if (_id > 0) res = new UsuarioDAO().Alterar(this);
            return res;
        }

        internal bool VerificarLogin(string login)
        {
            bool res = false;
            if (!string.IsNullOrEmpty(login))
            {
                if (new UsuarioDAO().LoginCount(login) > 0)
                {
                    res = true;
                }
            }
            return res;
        }

        internal bool IsLastAdmin()
        {
            return (new UsuarioDAO().AdminCount() == 1);
        }
    }
}
