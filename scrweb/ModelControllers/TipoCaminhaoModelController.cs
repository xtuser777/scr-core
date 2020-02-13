using System.Collections.Generic;
using System.Linq;
using scrweb.Models;
using scrweb.ViewModels;

namespace scrweb.ModelControllers
{
    public class TipoCaminhaoModelController
    {
        public TipoCaminhaoViewModel GetById(int id)
        {
            var t = new TipoCaminhao().GetById(id);
            return t != null 
                ? new TipoCaminhaoViewModel()
                {
                    Id = t.Id,
                    Descricao = t.Descricao,
                    Eixos = t.Eixos,
                    Capacidade = t.Capacidade
                } 
                : null;
        }

        public List<TipoCaminhaoViewModel> GetAll()
        {
            var t = new TipoCaminhao().GetAll();
            return t != null && t.Count > 0
                ? t.Select(tc => new TipoCaminhaoViewModel()
                {
                    Id = tc.Id,
                    Descricao = tc.Descricao,
                    Eixos = tc.Eixos,
                    Capacidade = tc.Capacidade
                }).ToList()
                : new List<TipoCaminhaoViewModel>();
        }

        public int Gravar(TipoCaminhaoViewModel tcvm)
        {
            return new TipoCaminhao()
            {
                Id = tcvm.Id,
                Descricao = tcvm.Descricao,
                Eixos = tcvm.Eixos,
                Capacidade = tcvm.Capacidade
            }.Gravar();
        }

        public int Alterar(TipoCaminhaoViewModel tcvm)
        {
            return new TipoCaminhao()
            {
                Id = tcvm.Id,
                Descricao = tcvm.Descricao,
                Eixos = tcvm.Eixos,
                Capacidade = tcvm.Capacidade
            }.Alterar();
        }

        public int Exlcuir(int id)
        {
            return new TipoCaminhao().Excluir(id);
        }
    }
}