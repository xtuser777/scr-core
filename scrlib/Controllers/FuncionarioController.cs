using scrlib.Models;
using scrlib.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace scrlib.Controllers
{
    public class FuncionarioController
    {
        public FuncionarioViewModel GetById(int id)
        {
            var funcionario = new Funcionario().GetById(id);
            return new FuncionarioViewModel()
            {
                Id = funcionario.Id,
                Tipo = funcionario.Tipo,
                Admissao = funcionario.Admissao,
                Demissao = funcionario.Demissao,
                Pessoa = new PessoaFisicaController().GetById(funcionario.Pessoa)
            };
        }

        public List<FuncionarioViewModel> Get()
        {
            List<FuncionarioViewModel> fvms = null;
            var funcs = new Funcionario().Get();
            if (funcs != null && funcs.Count > 0)
            {
                fvms = new List<FuncionarioViewModel>();
                foreach(Funcionario f in funcs)
                {
                    fvms.Add(new FuncionarioViewModel()
                    {
                        Id = f.Id,
                        Tipo = f.Tipo,
                        Admissao = f.Admissao,
                        Demissao = f.Demissao,
                        Pessoa = new PessoaFisicaController().GetById(f.Pessoa)
                    });
                }
            }
            return fvms;
        }

        public FuncionarioViewModel GetVendedorById(int id)
        {
            var vend = new Funcionario().GetVendedorById(id);
            return new FuncionarioViewModel()
            {
                Id = vend.Id,
                Tipo = vend.Tipo,
                Admissao = vend.Admissao,
                Demissao = vend.Demissao,
                Pessoa = new PessoaFisicaController().GetById(vend.Pessoa)
            };
        }

        public List<FuncionarioViewModel> GetVendedores()
        {
            List<FuncionarioViewModel> fvms = null;
            var vends = new Funcionario().GetVendedores();
            if (vends != null && vends.Count > 0)
            {
                fvms = new List<FuncionarioViewModel>();
                foreach (Funcionario f in vends)
                {
                    fvms.Add(new FuncionarioViewModel()
                    {
                        Id = f.Id,
                        Tipo = f.Tipo,
                        Admissao = f.Admissao,
                        Demissao = f.Demissao,
                        Pessoa = new PessoaFisicaController().GetById(f.Pessoa)
                    });
                }
            }
            return fvms;
        }

        public int Gravar(FuncionarioViewModel fvm)
        {
            return new Funcionario()
            {
                Id = fvm.Id,
                Tipo = fvm.Tipo,
                Admissao = fvm.Admissao,
                Demissao = fvm.Demissao,
                Pessoa = fvm.Pessoa.Id
            }.Gravar();
        }

        public int Alterar(FuncionarioViewModel fvm)
        {
            return new Funcionario()
            {
                Id = fvm.Id,
                Tipo = fvm.Tipo,
                Admissao = fvm.Admissao,
                Demissao = fvm.Demissao,
                Pessoa = fvm.Pessoa.Id
            }.Alterar();
        }

        public int Excluir(int id)
        {
            return new Funcionario().Excluir(id);
        }
    }
}
