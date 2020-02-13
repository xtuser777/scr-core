using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using scrweb.Models;
using scrweb.ViewModels;

namespace scrweb.ModelControllers
{
    public class ClienteModelController
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
                    Pessoa = cliente.Tipo == 1 ? (PessoaViewModel) new PessoaFisicaModelController().GetById(cliente.Pessoa) : new PessoaJuridicaModelController().GetById(cliente.Pessoa)
                };
            }

            return null;
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
                    Pessoa = cli.Tipo == 1 ? (PessoaViewModel) new PessoaFisicaModelController().GetById(cli.Pessoa) : new PessoaJuridicaModelController().GetById(cli.Pessoa)
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