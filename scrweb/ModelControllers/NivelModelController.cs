using scrweb.Models;
using scrweb.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace scrweb.ModelControllers
{
    public class NivelModelController
    {
        public NivelViewModel GetById(int id)
        {
            NivelViewModel nvm = null;
            var nivel = new Nivel().GetById(id);
            if (nivel != null)
            {
                nvm = new NivelViewModel()
                {
                    Id = nivel.Id,
                    Descricao = nivel.Descricao
                };
            }
            return nvm;
        }

        public List<NivelViewModel> Get()
        {
            List<NivelViewModel> nvms = null;
            var niveis = new Nivel().Get();
            if (niveis != null && niveis.Count > 0)
            {
                nvms = new List<NivelViewModel>();
                foreach (Nivel nivel in niveis)
                {
                    nvms.Add(new NivelViewModel()
                    {
                        Id = nivel.Id,
                        Descricao = nivel.Descricao
                    });
                }
            }
            return nvms;
        }
    }
}
