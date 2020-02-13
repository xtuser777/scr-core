using System;
using System.Collections.Generic;
using System.Linq;
using scrweb.Models;
using scrweb.ViewModels;

namespace scrweb.ModelControllers
{
    public class CategoriaModelController
    {
        public static CategoriaViewModel GetById(int id)
        {
            var c = Categoria.GetById(id);

            return c != null
            ? new CategoriaViewModel()
            {
                Id = c.Id,
                Descricao = c.Descricao
            }
            : null;
        }

        public static List<CategoriaViewModel> GetAll()
        {
            var cs = Categoria.GetAll();

            return cs != null && cs.Count > 0
            ? cs.Select(c => new CategoriaViewModel()
            {
                Id = c.Id,
                Descricao = c.Descricao
            }).ToList()
            : new List<CategoriaViewModel>();
        }

        public static int Gravar(CategoriaViewModel cvm)
        {
            return new Categoria()
            {
                Id = cvm.Id,
                Descricao = cvm.Descricao
            }.Gravar();
        }

        public static int Alterar(CategoriaViewModel cvm)
        {
            return new Categoria()
            {
                Id = cvm.Id,
                Descricao = cvm.Descricao
            }.Alterar();
        }

        public static int Excluir(int id)
        {
            return Categoria.Excluir(id);
        }
    }
}