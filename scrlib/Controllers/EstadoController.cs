using scrlib.Models;
using scrlib.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
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
                evms = estados.Select(estado => new EstadoViewModel() {Id = estado.Id, Nome = estado.Nome, Sigla = estado.Sigla}).ToList();
            }
            return evms;
        }
        
        public List<EstadoViewModel> GetByFilter(string chave)
        {
            List<EstadoViewModel> evms = null;
            var estados = new Estado().GetByFilter(chave);
            if (estados != null && estados.Count > 0)
            {
                evms = estados.Select(estado => new EstadoViewModel() {Id = estado.Id, Nome = estado.Nome, Sigla = estado.Sigla}).ToList();
            }
            return evms;
        }
    }
}
