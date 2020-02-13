using System;
using System.Collections.Generic;
using System.Linq;
using scrweb.Models;
using scrweb.ViewModels;

namespace scrweb.ModelControllers
{
    public class FormaPagamentoModelController
    {
        public static FormaPagamentoViewModel GetById(int id) 
        {
            var fp = FormaPagamento.GetById(id);
            
            return fp != null 
            ? new FormaPagamentoViewModel()
            {
                Id = fp.Id,
                Descricao = fp.Descricao,
                Prazo = fp.Prazo
            }
            : null;
        }

        public static List<FormaPagamentoViewModel> GetAll()
        {
            var fps = FormaPagamento.GetAll();

            return fps != null && fps.Count > 0
            ? fps.Select(fp => new FormaPagamentoViewModel()
            {
                Id = fp.Id,
                Descricao = fp.Descricao,
                Prazo = fp.Prazo
            }).ToList()
            : new List<FormaPagamentoViewModel>();
        }

        public static int Gravar(FormaPagamentoViewModel fpvm)
        {
            return new FormaPagamento()
            {
                Id = fpvm.Id,
                Descricao = fpvm.Descricao,
                Prazo = fpvm.Prazo
            }.Gravar();
        }

        public static int Alterar(FormaPagamentoViewModel fpvm) 
        {
            return new FormaPagamento()
            {
                Id = fpvm.Id,
                Descricao = fpvm.Descricao,
                Prazo = fpvm.Prazo
            }.Alterar();
        }

        public static int Excluir(int id) 
        {
            return FormaPagamento.Excluir(id);
        }
    }
}