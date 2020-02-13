using scrweb.Models;
using scrweb.ViewModels;

namespace scrweb.ModelControllers
{
    public class ParametrizacaoModelController
    {
        public ParametrizacaoViewModel Get()
        {
            var p = new Parametrizacao().Get();
            
            if (p != null)
            {
                return new ParametrizacaoViewModel()
                {
                    Id = p.Id,
                    RazaoSocial = p.RazaoSocial,
                    NomeFantasia = p.NomeFantasia,
                    Cnpj = p.Cnpj,
                    Rua = p.Rua,
                    Numero = p.Numero,
                    Bairro = p.Bairro,
                    Complemento = p.Complemento,
                    Cep = p.Cep,
                    Cidade = new CidadeModelController().GetById(p.Cidade),
                    Telefone = p.Telefone,
                    Celular = p.Celular,
                    Email = p.Email,
                    Logotipo = p.Logotipo
                };
            }

            return null;
        }

        public int Gravar(ParametrizacaoViewModel pvm)
        {
            return new Parametrizacao()
            {
                Id = pvm.Id,
                RazaoSocial = pvm.RazaoSocial,
                NomeFantasia = pvm.NomeFantasia,
                Cnpj = pvm.Cnpj,
                Rua = pvm.Rua,
                Numero = pvm.Numero,
                Bairro = pvm.Bairro,
                Complemento = pvm.Complemento,
                Cep = pvm.Cep,
                Cidade = pvm.Cidade.Id,
                Telefone = pvm.Telefone,
                Celular = pvm.Celular,
                Email = pvm.Email,
                Logotipo = pvm.Logotipo
            }.Gravar();
        }

        public int Alterar(ParametrizacaoViewModel pvm)
        {
            return new Parametrizacao()
            {
                Id = pvm.Id,
                RazaoSocial = pvm.RazaoSocial,
                NomeFantasia = pvm.NomeFantasia,
                Cnpj = pvm.Cnpj,
                Rua = pvm.Rua,
                Numero = pvm.Numero,
                Bairro = pvm.Bairro,
                Complemento = pvm.Complemento,
                Cep = pvm.Cep,
                Cidade = pvm.Cidade.Id,
                Telefone = pvm.Telefone,
                Celular = pvm.Celular,
                Email = pvm.Email,
                Logotipo = pvm.Logotipo
            }.Alterar();
        }
    }
}