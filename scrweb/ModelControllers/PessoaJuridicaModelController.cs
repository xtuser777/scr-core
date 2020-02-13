using scrweb.Models;
using scrweb.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace scrweb.ModelControllers
{
    public class PessoaJuridicaModelController
    {
        public PessoaJuridicaViewModel GetById(int id)
        {
            var pessoa = new PessoaJuridica().GetById(id);
            return new PessoaJuridicaViewModel()
            {
                Id = pessoa.Id,
                Tipo = pessoa.Tipo,
                RazaoSocial = pessoa.RazaoSocial,
                NomeFantasia = pessoa.NomeFantasia,
                Cnpj = pessoa.Cnpj,
                Telefone = pessoa.Telefone,
                Celular = pessoa.Celular,
                Email = pessoa.Email,
                Endereco = new EnderecoModelController().GetById(pessoa.Endereco)
            };
        }

        public bool VerifyCnpj(string cnpj)
        {
            return new PessoaJuridica().VerifyCnpj(cnpj);
        }

        public int Gravar(PessoaJuridicaViewModel pvm)
        {
            return new PessoaJuridica()
            {
                Id = pvm.Id,
                Tipo = pvm.Tipo,
                RazaoSocial = pvm.RazaoSocial,
                NomeFantasia = pvm.NomeFantasia,
                Cnpj = pvm.Cnpj,
                Telefone = pvm.Telefone,
                Celular = pvm.Celular,
                Email = pvm.Email,
                Endereco = pvm.Endereco.Id
            }.Gravar();
        }

        public int Alterar(PessoaJuridicaViewModel pvm)
        {
            return new PessoaJuridica()
            {
                Id = pvm.Id,
                Tipo = pvm.Tipo,
                RazaoSocial = pvm.RazaoSocial,
                NomeFantasia = pvm.NomeFantasia,
                Cnpj = pvm.Cnpj,
                Telefone = pvm.Telefone,
                Celular = pvm.Celular,
                Email = pvm.Email,
                Endereco = pvm.Endereco.Id
            }.Alterar();
        }

        public int Excluir(int id)
        {
            return new PessoaJuridica().Excluir(id); 
        }
    }
}
