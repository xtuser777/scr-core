using scrweb.DAO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Newtonsoft.Json.Linq;

namespace scrweb.Models
{
    public class Usuario
    {
        private int _id;
        private string _login;
        private string _senha;
        private bool _ativo;
        private Funcionario _funcionario;
        private Nivel _nivel;

        public int Id { get => _id; set => _id = value; }
        public string Login { get => _login; set => _login = value; }
        public string Senha { get => _senha; set => _senha = value; }
        public bool Ativo { get => _ativo; set => _ativo = value; }
        public Funcionario Funcionario { get => _funcionario; set => _funcionario = value; }
        public Nivel Nivel { get => _nivel; set => _nivel = value; }

        public Usuario Autenticar(string login, string senha)
        {
            return !string.IsNullOrEmpty(login) && !string.IsNullOrEmpty(senha)
                ? new UsuarioDAO().Autenticar(login, senha)
                : null;
        }

        public Usuario GetById(int id)
        {
            return id > 0 ? new UsuarioDAO().GetById(id) : null;
        }

        public List<Usuario> GetAll()
        {
            return new UsuarioDAO().GetAll();
        }

        public int Gravar()
        {
            if (_id != 0 || string.IsNullOrEmpty(_login) || string.IsNullOrEmpty(_senha) || _funcionario == null ||
                _nivel == null
            ) return -5;
            
            return new UsuarioDAO().Gravar(this);
        }

        public int Alterar()
        {
            if (_id <= 0 || string.IsNullOrEmpty(_login) || string.IsNullOrEmpty(_senha) || _funcionario == null ||
                _nivel == null
            ) return -5;
            
            return new UsuarioDAO().Alterar(this);
        }

        public int Excluir(int id)
        {
            return id > 0 ? new UsuarioDAO().Excluir(id) : -5;
        }
        
        public bool VerificarLogin(string login)
        {
            var res = false;
            if (!string.IsNullOrEmpty(login))
            {
                if (new UsuarioDAO().LoginCount(login) > 0)
                {
                    res = true;
                }
            }
            return res;
        }

        public bool IsLastAdmin()
        {
            return (new UsuarioDAO().AdminCount() == 1);
        }

        public JObject ToJObject()
        {
            JObject json = new JObject();
            json.Add("id", _id);
            json.Add("login", _login);
            json.Add("senha", _senha);
            json.Add("ativo", _ativo);
            json.Add("funcionario", _funcionario.ToJObject());
            json.Add("nivel", _nivel.ToJObject());

            return json;
        }
    }
}
