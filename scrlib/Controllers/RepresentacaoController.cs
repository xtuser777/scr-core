using System;
using System.Collections.Generic;
using System.Linq;
using scrlib.Models;
using scrlib.ViewModels;

namespace scrlib.Controllers
{
    public class RepresentacaoController
    {
        public RepresentacaoViewModel GetById(int id)
        {
            var r = new Representacao().GetById(id);
            return r != null ? new RepresentacaoViewModel()
            {
                Id = r.Id,
                Cadastro = r.Cadastro,
                Unidade = r.Unidade,
                Pessoa = new PessoaJuridicaController().GetById(r.Pessoa)
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
                    Pessoa = new PessoaJuridicaController().GetById(r.Pessoa)
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