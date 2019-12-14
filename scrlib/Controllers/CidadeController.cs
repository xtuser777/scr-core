using scrlib.Models;
using scrlib.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace scrlib.Controllers
{
    public class CidadeController
    {
        public CidadeViewModel GetById(int id)
        {
            CidadeViewModel cvm = null;
            var cidade = new Cidade().GetById(id);
            if (cidade != null)
            {
                cvm = new CidadeViewModel()
                {
                    Id = cidade.Id,
                    Nome = cidade.Nome,
                    Estado = new EstadoController().GetById(cidade.Estado)
                };
            }
            return cvm;
        }

        public List<CidadeViewModel> GetByEstado(int estado)
        {
            List<CidadeViewModel> cvms = null;
            var cidades = new Cidade().GetByEstado(estado);
            if (cidades != null && cidades.Count > 0)
            {
                cvms = new List<CidadeViewModel>();
                foreach (Cidade cidade in cidades)
                {
                    cvms.Add(new CidadeViewModel()
                    {
                        Id = cidade.Id,
                        Nome = cidade.Nome,
                        Estado = null
                    });
                }
            }
            return cvms;
        }

        public List<CidadeViewModel> Get()
        {
            List<CidadeViewModel> cvms = null;
            var cidades = new Cidade().Get();
            if (cidades != null && cidades.Count > 0)
            {
                cvms = new List<CidadeViewModel>();
                foreach (Cidade cidade in cidades)
                {
                    cvms.Add(new CidadeViewModel()
                    {
                        Id = cidade.Id,
                        Nome = cidade.Nome,
                        Estado = new EstadoController().GetById(cidade.Estado)
                    });
                }
            }
            return cvms;
        }
    }
}
