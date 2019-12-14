using scrlib.Models;
using scrlib.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace scrlib.Controllers
{
    public class PessoaFisicaController
    {
        public PessoaFisicaViewModel GetById(int id)
        {
            var pessoa = new PessoaFisica().GetById(id);
            return new PessoaFisicaViewModel()
            {
                Id = pessoa.Id,
                Tipo = pessoa.Tipo,
                Telefone = pessoa.Telefone,
                Celular = pessoa.Celular,
                Email = pessoa.Email,
                Endereco = new EnderecoController().GetById(pessoa.Endereco),
                Nome = pessoa.Nome,
                Rg = pessoa.Rg,
                Cpf = pessoa.Cpf,
                Nascimento = pessoa.Nascimento
            };
        }

        public int Gravar(PessoaFisicaViewModel pvm)
        {
            return new PessoaFisica()
            {
                Id = pvm.Id,
                Nome = pvm.Nome,
                Rg = pvm.Rg,
                Cpf = pvm.Cpf,
                Nascimento = pvm.Nascimento,
                Tipo = pvm.Tipo,
                Telefone = pvm.Telefone,
                Celular = pvm.Celular,
                Email = pvm.Email,
                Endereco = pvm.Endereco.Id
            }.Gravar();
        }

        public int Alterar(PessoaFisicaViewModel pvm)
        {
            return new PessoaFisica()
            {
                Id = pvm.Id,
                Nome = pvm.Nome,
                Rg = pvm.Rg,
                Cpf = pvm.Cpf,
                Nascimento = pvm.Nascimento,
                Tipo = pvm.Tipo,
                Telefone = pvm.Telefone,
                Celular = pvm.Celular,
                Email = pvm.Email,
                Endereco = pvm.Endereco.Id
            }.Alterar();
        }

        public int Excluir(int id)
        {
            return new PessoaFisica().Excluir(id);
        }
    }
}
