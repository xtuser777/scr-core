using System.Collections.Generic;
using scrweb.Models;
using scrweb.ViewModels;

namespace scrweb.ModelControllers
{
    public class OrcamentoVendaModelController
    {
        public static OrcamentoVendaViewModel GetById(int id)
        {
            var ov = OrcamentoVenda.GetById(id);

            return ov != null
                ? new OrcamentoVendaViewModel()
                {
                    Id = ov.Id,
                    Descricao = ov.Descricao,
                    Data = ov.Data,
                    NomeCliente = ov.NomeCliente,
                    DocumentoCliente = ov.DocumentoCliente,
                    TelefoneCliente = ov.TelefoneCliente,
                    CelularCliente = ov.CelularCliente,
                    EmailCliente = ov.EmailCliente,
                    Valor = ov.Valor,
                    Peso = ov.Peso,
                    Validade = ov.Validade,
                    Vendedor = new FuncionarioModelController().GetVendedorById(ov.Vendedor),
                    Destino = new CidadeModelController().GetById(ov.Destino),
                    TipoCaminhao = new TipoCaminhaoModelController().GetById(ov.TipoCaminhao),
                    Autor = new UsuarioModelController().GetById(ov.Autor),
                    Cliente = new ClienteModelController().GetById(ov.Cliente)
                }
                : null;
        }

        public static List<OrcamentoVendaViewModel> GetAll()
        {
            var ovs = OrcamentoVenda.GetAll();
            var ovms = new List<OrcamentoVendaViewModel>();
            for (var i = 0; ovs != null && i < ovs.Count; i++)
            {
                var ov = ovs[i];
                ovms.Add(new OrcamentoVendaViewModel()
                {
                    Id = ov.Id,
                    Descricao = ov.Descricao,
                    Data = ov.Data,
                    NomeCliente = ov.NomeCliente,
                    DocumentoCliente = ov.DocumentoCliente,
                    TelefoneCliente = ov.TelefoneCliente,
                    CelularCliente = ov.CelularCliente,
                    EmailCliente = ov.EmailCliente,
                    Valor = ov.Valor,
                    Peso = ov.Peso,
                    Validade = ov.Validade,
                    Vendedor = new FuncionarioModelController().GetVendedorById(ov.Vendedor),
                    Destino = new CidadeModelController().GetById(ov.Destino),
                    TipoCaminhao = new TipoCaminhaoModelController().GetById(ov.TipoCaminhao),
                    Autor = new UsuarioModelController().GetById(ov.Autor),
                    Cliente = new ClienteModelController().GetById(ov.Cliente)
                });
            }

            return ovms;
        }

        public static int Gravar(OrcamentoVendaViewModel ovvm)
        {
            return new OrcamentoVenda()
            {
                Id = ovvm.Id,
                Descricao = ovvm.Descricao,
                Data = ovvm.Data,
                NomeCliente = ovvm.NomeCliente,
                DocumentoCliente = ovvm.DocumentoCliente,
                TelefoneCliente = ovvm.TelefoneCliente,
                CelularCliente = ovvm.CelularCliente,
                EmailCliente = ovvm.EmailCliente,
                Valor = ovvm.Valor,
                Peso = ovvm.Peso,
                Validade = ovvm.Validade,
                Vendedor = ovvm.Vendedor.Id,
                Destino = ovvm.Destino.Id,
                TipoCaminhao = ovvm.TipoCaminhao.Id,
                Autor = ovvm.Autor.Id,
                Cliente = ovvm.Cliente.Id
            }.Gravar();
        }
        
        public static int Alterar(OrcamentoVendaViewModel ovvm)
        {
            return new OrcamentoVenda()
            {
                Id = ovvm.Id,
                Descricao = ovvm.Descricao,
                Data = ovvm.Data,
                NomeCliente = ovvm.NomeCliente,
                DocumentoCliente = ovvm.DocumentoCliente,
                TelefoneCliente = ovvm.TelefoneCliente,
                CelularCliente = ovvm.CelularCliente,
                EmailCliente = ovvm.EmailCliente,
                Valor = ovvm.Valor,
                Peso = ovvm.Peso,
                Validade = ovvm.Validade,
                Vendedor = ovvm.Vendedor.Id,
                Destino = ovvm.Destino.Id,
                TipoCaminhao = ovvm.TipoCaminhao.Id,
                Autor = ovvm.Autor.Id,
                Cliente = ovvm.Cliente.Id
            }.Alterar();
        }

        public static int Excluir(int id)
        {
            return OrcamentoVenda.Excluir(id);
        }
    }
}