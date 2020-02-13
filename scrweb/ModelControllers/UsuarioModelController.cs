using scrweb.Models;
using scrweb.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace scrweb.ModelControllers
{
    public class UsuarioModelController
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
                    Ativo = usuario.Ativo,
                    Funcionario = new FuncionarioModelController().GetById(usuario.Funcionario),
                    Nivel = new NivelModelController().GetById(usuario.Nivel)
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
                    Ativo = usuario.Ativo,
                    Funcionario = new FuncionarioModelController().GetById(usuario.Funcionario),
                    Nivel = new NivelModelController().GetById(usuario.Nivel)
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
                uvms = usuarios.Select(usuario => new UsuarioViewModel()
                {
                    Id = usuario.Id,
                    Login = usuario.Login,
                    Senha = usuario.Senha,
                    Ativo = usuario.Ativo,
                    Funcionario = new FuncionarioModelController().GetById(usuario.Funcionario),
                    Nivel = new NivelModelController().GetById(usuario.Nivel)
                }).ToList();
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
                Ativo = uvm.Ativo,
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
                Ativo = uvm.Ativo,
                Funcionario = uvm.Funcionario.Id,
                Nivel = uvm.Nivel.Id
            }.Alterar();
        }

        public bool VerificarLogin(string login)
        {
            return new Usuario().VerificarLogin(login);
        }

        public bool IsLastAdmin()
        {
            return new Usuario().IsLastAdmin();
        }

        public int Excluir(int id)
        {
            return new Usuario().Excluir(id);
        }
    }
}
