using scrweb.Models;
using scrweb.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace scrweb.ModelControllers
{
    public class PessoaModelController
    {
        public List<PessoaViewModel> Get()
        {
            List<PessoaViewModel> pvms = null;
            var pessoas = new Pessoa().Get();
            if (pessoas != null && pessoas.Count > 0)
            {
                pvms = new List<PessoaViewModel>();
                foreach(Pessoa pessoa in pessoas)
                {
                    if (pessoa.Tipo == 1)
                    {
                        pvms.Add(new PessoaFisicaViewModel()
                        {
                            Id = ((PessoaFisica)pessoa).Id,
                            Tipo = ((PessoaFisica)pessoa).Tipo,
                            Nome = ((PessoaFisica)pessoa).Nome,
                            Rg = ((PessoaFisica)pessoa).Rg,
                            Cpf = ((PessoaFisica)pessoa).Cpf,
                            Nascimento = ((PessoaFisica)pessoa).Nascimento,
                            Telefone = ((PessoaFisica)pessoa).Telefone,
                            Celular = ((PessoaFisica)pessoa).Celular,
                            Email = ((PessoaFisica)pessoa).Email,
                            Endereco = new EnderecoModelController().GetById(pessoa.Endereco)
                        });
                    }
                    else
                    {
                        pvms.Add(new PessoaJuridicaViewModel()
                        {
                            Id = pessoa.Id,
                            Tipo = pessoa.Tipo,
                            Telefone = pessoa.Telefone,
                            Celular = pessoa.Celular,
                            Email = pessoa.Email,
                            Endereco = new EnderecoModelController().GetById(pessoa.Endereco),
                            RazaoSocial = ((PessoaJuridica)pessoa).RazaoSocial,
                            NomeFantasia = ((PessoaJuridica)pessoa).NomeFantasia,
                            Cnpj = ((PessoaJuridica)pessoa).Cnpj
                        });
                    }
                }
            }
            return pvms;
        }
    }
}
