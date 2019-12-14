using scrlib.Models;
using scrlib.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace scrlib.Controllers
{
    public class UsuarioController
    {
        public UsuarioViewModel Autenticar(string login, string senha)
        {
            UsuarioViewModel uvm = null;
            var usuario = new Usuario().Autenticar(login, senha);
            if (usuario != null)
            {
                uvm = new UsuarioViewModel()
                {
                    Id = usuario.Id,
                    Login = usuario.Login,
                    Senha = usuario.Senha,
                    Funcionario = new FuncionarioController().GetById(usuario.Funcionario),
                    Nivel = new NivelController().GetById(usuario.Nivel)
                };
            }
            return uvm;
        }

        public UsuarioViewModel GetById(int id)
        {
            UsuarioViewModel uvm = null;
            var usuario = new Usuario().GetById(id);
            if (usuario != null)
            {
                uvm = new UsuarioViewModel()
                {
                    Id = usuario.Id,
                    Login = usuario.Login,
                    Senha = usuario.Senha,
                    Funcionario = new FuncionarioController().GetById(usuario.Funcionario),
                    Nivel = new NivelController().GetById(usuario.Nivel)
                };
            }
            return uvm;
        }

        public List<UsuarioViewModel> Get()
        {
            List<UsuarioViewModel> uvms = null;
            var usuarios = new Usuario().Get();
            if (usuarios != null && usuarios.Count > 0)
            {
                uvms = new List<UsuarioViewModel>();
                foreach (Usuario usuario in usuarios)
                {
                    uvms.Add(new UsuarioViewModel()
                    {
                        Id = usuario.Id,
                        Login = usuario.Login,
                        Senha = usuario.Senha,
                        Funcionario = new FuncionarioController().GetById(usuario.Funcionario),
                        Nivel = new NivelController().GetById(usuario.Nivel)
                    });
                }
            }
            return uvms;
        }

        public int Gravar(UsuarioViewModel uvm)
        {
            return new Usuario()
            {
                Id = uvm.Id,
                Login = uvm.Login,
                Senha = uvm.Senha,
                Funcionario = uvm.Funcionario.Id,
                Nivel = uvm.Nivel.Id
            }.Gravar();
        }

        public int Alterar(UsuarioViewModel uvm)
        {
            return new Usuario()
            {
                Id = uvm.Id,
                Login = uvm.Login,
                Senha = uvm.Senha,
                Funcionario = uvm.Funcionario.Id,
                Nivel = uvm.Nivel.Id
            }.Alterar();
        }

        public bool VerificarLogin(string login)
        {
            return new Usuario().VerificarLogin(login);
        }
    }
}
