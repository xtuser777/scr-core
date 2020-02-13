using scrweb.Models;
using scrweb.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace scrweb.ModelControllers
{
    public class EnderecoModelController
    {
        public EnderecoViewModel GetById(int id)
        {
            var endereco = new Endereco().GetById(id);
            return new EnderecoViewModel()
            {
                Id = endereco.Id,
                Rua = endereco.Rua,
                Numero = endereco.Numero,
                Bairro = endereco.Bairro,
                Complemento = endereco.Complemento,
                Cep = endereco.Cep,
                Cidade = new CidadeModelController().GetById(endereco.Cidade)
            };
        }

        public List<EnderecoViewModel> Get()
        {
            List<EnderecoViewModel> evms = null;
            var enderecos = new Endereco().Get();
            if (enderecos != null && enderecos.Count > 0)
            {
                evms = new List<EnderecoViewModel>();
                foreach (Endereco endereco in enderecos)
                {
                    evms.Add(new EnderecoViewModel()
                    {
                        Id = endereco.Id,
                        Rua = endereco.Rua,
                        Numero = endereco.Numero,
                        Bairro = endereco.Bairro,
                        Complemento = endereco.Complemento,
                        Cep = endereco.Cep,
                        Cidade = new CidadeModelController().GetById(endereco.Cidade)
                    });
                }
            }
            return evms;
        }

        public int Gravar(EnderecoViewModel evm)
        {
            return new Endereco()
            {
                Id = evm.Id,
                Rua = evm.Rua,
                Numero = evm.Numero,
                Bairro = evm.Bairro,
                Complemento = evm.Complemento,
                Cep = evm.Cep,
                Cidade = evm.Cidade.Id
            }.Gravar();
        }

        public int Alterar(EnderecoViewModel evm)
        {
            return new Endereco()
            {
                Id = evm.Id,
                Rua = evm.Rua,
                Numero = evm.Numero,
                Bairro = evm.Bairro,
                Complemento = evm.Complemento,
                Cep = evm.Cep,
                Cidade = evm.Cidade.Id
            }.Alterar();
        }

        public int Excluir(int id)
        {
            return new Endereco().Excluir(id);
        }
    }
}
