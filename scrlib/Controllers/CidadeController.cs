using scrlib.Models;
using scrlib.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace scrlib.Controllers
{
    public class CidadeController
    {
        public CidadeViewModel GetById(int id)
        {
            var cidade = new Cidade().GetById(id);
            if (cidade != null)
            {
                return new CidadeViewModel()
                {
                    Id = cidade.Id,
                    Nome = cidade.Nome,
                    Estado = new EstadoController().GetById(cidade.Estado)
                };
            }
            
            return null;
        }

        public List<CidadeViewModel> GetByEstado(int estado)
        {
            var cidades = new Cidade().GetByEstado(estado);
            if (cidades != null && cidades.Count > 0)
            {
                return cidades.Select(cidade => new CidadeViewModel() {Id = cidade.Id, Nome = cidade.Nome, Estado = null}).ToList();
            }
            
            return null;
        }
        
        public List<CidadeViewModel> GetByEstAndKey(int estado, string chave)
        {
            var cidades = new Cidade().GetByEstAndKey(estado, chave);
            if (cidades != null && cidades.Count > 0)
            {
                return cidades.Select(cidade => new CidadeViewModel() {Id = cidade.Id, Nome = cidade.Nome, Estado = null}).ToList();
            }
            
            return null;
        }

        public List<CidadeViewModel> Get()
        {
            var cidades = new Cidade().Get();
            if (cidades != null && cidades.Count > 0)
            {
                return cidades.Select(cidade => new CidadeViewModel() {Id = cidade.Id, Nome = cidade.Nome, Estado = new EstadoController().GetById(cidade.Estado)}).ToList();
            }
            
            return null;
        }
    }
}
