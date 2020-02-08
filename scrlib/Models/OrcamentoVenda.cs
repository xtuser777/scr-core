using System;
using System.Collections.Generic;
using scrlib.DAO;

namespace scrlib.Models
{
    internal class OrcamentoVenda
    {
        private int _id;
        private string _descricao;
        private DateTime _data;
        private string _nomeCliente;
        private string _documentoCliente;
        private string _telefoneCliente;
        private string _celularCliente;
        private string _emailCliente;
        private decimal _valor;
        private decimal _peso;
        private DateTime _validade;
        private int _vendedor;
        private int _destino;
        private int _tipoCaminhao;
        private int _autor;
        private int _cliente;

        internal int Cliente
        {
            get { return _cliente; }
            set { _cliente = value; }
        }
        
        internal int Autor
        {
            get { return _autor; }
            set { _autor = value; }
        }
        
        internal int TipoCaminhao
        {
            get { return _tipoCaminhao; }
            set { _tipoCaminhao = value; }
        }
        
        internal int Destino
        {
            get { return _destino; }
            set { _destino = value; }
        }
        
        internal int Vendedor
        {
            get { return _vendedor; }
            set { _vendedor = value; }
        }
        
        internal DateTime Validade
        {
            get { return _validade; }
            set { _validade = value; }
        }
        
        internal decimal Peso
        {
            get { return _peso; }
            set { _peso = value; }
        }
        
        internal decimal Valor
        {
            get { return _valor; }
            set { _valor = value; }
        }
        
        internal string EmailCliente
        {
            get { return _emailCliente; }
            set { _emailCliente = value; }
        }
        
        internal string CelularCliente
        {
            get { return _celularCliente; }
            set { _celularCliente = value; }
        }
        
        internal string TelefoneCliente
        {
            get { return _telefoneCliente; }
            set { _telefoneCliente = value; }
        }
        
        internal string DocumentoCliente
        {
            get { return _documentoCliente; }
            set { _documentoCliente = value; }
        }
        
        internal string NomeCliente
        {
            get { return _nomeCliente; }
            set { _nomeCliente = value; }
        }
        
        internal DateTime Data
        {
            get { return _data; }
            set { _data = value; }
        }
        
        internal string Descricao
        {
            get { return _descricao; }
            set { _descricao = value; }
        }
        
        internal int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        
        internal static OrcamentoVenda GetById(int id)
        {
            return id > 0 ? new OrcamentoVendaDAO().GetById(id) : null;
        }

        internal static List<OrcamentoVenda> GetAll()
        {
            return new OrcamentoVendaDAO().GetAll();
        }

        internal int Gravar()
        {
            return _id == 0 ? new OrcamentoVendaDAO().Gravar(this) : -5;
        }

        internal int Alterar()
        {
            return _id > 0 ? new OrcamentoVendaDAO().Gravar(this) : -5;
        }

        internal static int Excluir(int id)
        {
            return id > 0 ? new OrcamentoVendaDAO().Excluir(id) : -5;
        }
    }
}