using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using scrlib.Models;
using scrlib.ViewModels;

namespace scrlib.Controllers
{
    public class ClienteController
    {
        public ClienteViewModel GetById(int id)
        {
            var cliente = new Cliente().GetById(id);
            if (cliente != null)
            {
                return new ClienteViewModel()
                {
                    Id = cliente.Id,
                    Cadastro = cliente.Cadastro,
                    Tipo = cliente.Tipo,
                    Pessoa = cliente.Tipo == 1 ? (PessoaViewModel) new PessoaFisicaController().GetById(cliente.Pessoa) : new PessoaJuridicaController().GetById(cliente.Pessoa)
                };
            }

            return null;
        }

        public List<ClienteViewModel> GetByFilter(string chave)
        {
            var cvms = new List<ClienteViewModel>();
            var c = new Cliente().GetByFilter(chave);
            if (c == null || c.Count <= 0) return cvms;
            cvms.AddRange(c.Select(cli => 
                new ClienteViewModel()
                {
                    Id = cli.Id, 
                    Cadastro = cli.Cadastro, 
                    Tipo = cli.Tipo, 
                    Pessoa = cli.Tipo == 1 ? (PessoaViewModel) new PessoaFisicaController().GetById(cli.Pessoa) : new PessoaJuridicaController().GetById(cli.Pessoa)
                }
            ));

            return cvms;
        }

        public List<ClienteViewModel> GetByCad(DateTime cadastro)
        {
            var cvms = new List<ClienteViewModel>();
            var c = new Cliente().GetByCad(cadastro);
            if (c == null || c.Count <= 0) return cvms;
            cvms.AddRange(c.Select(cli => 
                new ClienteViewModel()
                {
                    Id = cli.Id, 
                    Cadastro = cli.Cadastro, 
                    Tipo = cli.Tipo, 
                    Pessoa = cli.Tipo == 1 ? (PessoaViewModel) new PessoaFisicaController().GetById(cli.Pessoa) : new PessoaJuridicaController().GetById(cli.Pessoa)
                }
            ));

            return cvms;
        }

        public List<ClienteViewModel> GetByFilterAndCad(string chave, DateTime cadastro)
        {
            var cvms = new List<ClienteViewModel>();
            var c = new Cliente().GetByFilterCad(chave, cadastro);
            if (c == null || c.Count <= 0) return cvms;
            cvms.AddRange(c.Select(cli => 
                new ClienteViewModel()
                {
                    Id = cli.Id, 
                    Cadastro = cli.Cadastro, 
                    Tipo = cli.Tipo, 
                    Pessoa = cli.Tipo == 1 ? (PessoaViewModel) new PessoaFisicaController().GetById(cli.Pessoa) : new PessoaJuridicaController().GetById(cli.Pessoa)
                }
            ));

            return cvms;
        }

        public List<ClienteViewModel> GetAll()
        {
            var cvms = new List<ClienteViewModel>();
            var c = new Cliente().GetAll();
            if (c == null || c.Count <= 0) return cvms;
            cvms.AddRange(c.Select(cli => 
                new ClienteViewModel()
                {
                    Id = cli.Id, 
                    Cadastro = cli.Cadastro, 
                    Tipo = cli.Tipo, 
                    Pessoa = cli.Tipo == 1 ? (PessoaViewModel) new PessoaFisicaController().GetById(cli.Pessoa) : new PessoaJuridicaController().GetById(cli.Pessoa)
                }
            ));

            return cvms;
        }

        public int Gravar(ClienteViewModel cvm)
        {
            return new Cliente()
            {
                Id = cvm.Id,
                Cadastro = cvm.Cadastro,
                Tipo = cvm.Tipo,
                Pessoa = cvm.Pessoa.Id
            }.Gravar();
        }

        public int Alterar(ClienteViewModel cvm)
        {
            return new Cliente()
            {
                Id = cvm.Id,
                Cadastro = cvm.Cadastro,
                Tipo = cvm.Tipo,
                Pessoa = cvm.Pessoa.Id
            }.Alterar();
        }

        public int Excluir(int id)
        {
            return new Cliente().Excluir(id);
        }
    }
}