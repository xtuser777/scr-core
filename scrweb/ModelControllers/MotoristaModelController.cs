using System.Collections.Generic;
using System.Linq;
using scrweb.Models;
using scrweb.ViewModels;

namespace scrweb.ModelControllers
{
    public class MotoristaModelController
    {
        public static MotoristaViewModel GetById(int id)
        {
            var m = Motorista.GetById(id);

            return m != null
                ? new MotoristaViewModel() { Id = m.Id, Cadastro = m.Cadastro, Pessoa = new PessoaFisicaModelController().GetById(m.Pessoa) }
                : null;
        }

        public static List<MotoristaViewModel> GetAll()
        {
            var m = Motorista.GetAll();

            return m != null && m.Count > 0
                ? m.Select(o => new MotoristaViewModel()
                {
                    Id = o.Id,
                    Cadastro = o.Cadastro,
                    Pessoa = new PessoaFisicaModelController().GetById(o.Pessoa)
                }).ToList()
                : new List<MotoristaViewModel>();
        }

        public static int Gravar(MotoristaViewModel mvm)
        {
            return new Motorista()
            {
                Id = mvm.Id,
                Cadastro = mvm.Cadastro,
                Pessoa = mvm.Pessoa.Id
            }.Gravar();
        }

        public static int Alterar(MotoristaViewModel mvm)
        {
            return new Motorista()
            {
                Id = mvm.Id,
                Cadastro = mvm.Cadastro,
                Pessoa = mvm.Pessoa.Id
            }.Alterar();
        }

        public static int Excluir(int id)
        {
            return Motorista.Excluir(id);
        }
    }
}
