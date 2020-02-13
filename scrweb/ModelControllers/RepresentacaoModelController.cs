using System;
using System.Collections.Generic;
using System.Linq;
using scrweb.Models;
using scrweb.ViewModels;

namespace scrweb.ModelControllers
{
    public class RepresentacaoModelController
    {
        public RepresentacaoViewModel GetById(int id)
        {
            var r = new Representacao().GetById(id);
            return r != null ? new RepresentacaoViewModel()
            {
                Id = r.Id,
                Cadastro = r.Cadastro,
                Unidade = r.Unidade,
                Pessoa = new PessoaJuridicaModelController().GetById(r.Pessoa)
            } : null;
        }

        public List<RepresentacaoViewModel> GetAll()
        {
            var rs = new Representacao().GetAll();

            return rs != null && rs.Count > 0
                ? rs.Select(r => new RepresentacaoViewModel()
                {
                    Id = r.Id,
                    Cadastro = r.Cadastro,
                    Unidade = r.Unidade,
                    Pessoa = new PessoaJuridicaModelController().GetById(r.Pessoa)
                }).ToList()
                : new List<RepresentacaoViewModel>();
        }

        public int Gravar(RepresentacaoViewModel rvm)
        {
            return new Representacao()
            {
                Id = rvm.Id,
                Cadastro = rvm.Cadastro,
                Unidade = rvm.Unidade,
                Pessoa = rvm.Pessoa.Id
            }.Gravar();
        }

        public int Alterar(RepresentacaoViewModel rvm)
        {
            return new Representacao()
            {
                Id = rvm.Id,
                Cadastro = rvm.Cadastro,
                Unidade = rvm.Unidade,
                Pessoa = rvm.Pessoa.Id
            }.Alterar();
        }

        public int Excluir(int id)
        {
            return new Representacao().Excluir(id);
        }
    }
}