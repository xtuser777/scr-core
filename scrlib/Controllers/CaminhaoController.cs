using System;
using System.Collections.Generic;
using System.Linq;
using scrlib.Models;
using scrlib.ViewModels;

namespace scrlib.Controllers 
{
    public class CaminhaoController
    {
        public static CaminhaoViewModel GetById(int id)
        {
            var c = Caminhao.GetById(id);

            return c != null 
            ? new CaminhaoViewModel() 
            {
                Id = c.Id,
                Placa = c.Placa,
                Marca = c.Marca,
                Modelo = c.Modelo,
                Ano = c.Ano,
                Tipo = new TipoCaminhaoController().GetById(c.Tipo),
                Proprietario = MotoristaController.GetById(c.Proprietario)
            }
            : null;
        }

        public static List<CaminhaoViewModel> GetAll()
        {
            var cs = Caminhao.GetAll();

            return cs != null && cs.Count > 0 
            ? cs.Select(c => new CaminhaoViewModel()
            {
                Id = c.Id,
                Placa = c.Placa,
                Marca = c.Marca,
                Modelo = c.Modelo,
                Ano = c.Ano,
                Tipo = new TipoCaminhaoController().GetById(c.Tipo),
                Proprietario = MotoristaController.GetById(c.Proprietario)
            }).ToList()
            : new List<CaminhaoViewModel>();
        }

        public static int Gravar(CaminhaoViewModel cvm) 
        {
            return new Caminhao() 
            {
                Id = cvm.Id,
                Placa = cvm.Placa,
                Marca = cvm.Marca,
                Modelo = cvm.Modelo,
                Ano = cvm.Ano,
                Tipo = cvm.Tipo.Id,
                Proprietario = cvm.Proprietario.Id
            }.Gravar();
        }

        public static int Alterar(CaminhaoViewModel cvm)
        {
            return new Caminhao() 
            {
                Id = cvm.Id,
                Placa = cvm.Placa,
                Marca = cvm.Marca,
                Modelo = cvm.Modelo,
                Ano = cvm.Ano,
                Tipo = cvm.Tipo.Id,
                Proprietario = cvm.Proprietario.Id
            }.Alterar();
        }

        public static int Excluir(int id) 
        {
            return Caminhao.Excluir(id);
        }
    }
}