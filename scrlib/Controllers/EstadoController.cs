using scrlib.Models;
using scrlib.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace scrlib.Controllers
{
    public class EstadoController
    {
        public EstadoViewModel GetById(int id)
        {
            EstadoViewModel evm = null;
            var estado = new Estado().GetById(id);
            if (estado != null)
            {
                evm = new EstadoViewModel()
                {
                    Id = estado.Id,
                    Nome = estado.Nome,
                    Sigla = estado.Sigla
                };
            }
            return evm;
        }

        public List<EstadoViewModel> Get()
        {
            List<EstadoViewModel> evms = null;
            var estados = new Estado().Get();
            if (estados != null && estados.Count > 0)
            {
                evms = new List<EstadoViewModel>();
                foreach (Estado estado in estados)
                {
                    evms.Add(new EstadoViewModel()
                    {
                        Id = estado.Id,
                        Nome = estado.Nome,
                        Sigla = estado.Sigla
                    });
                }
            }
            return evms;
        }
    }
}
