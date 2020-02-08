using System;

namespace scrlib.ViewModels
{
    public class OrcamentoVendaViewModel
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public DateTime Data { get; set; }
        public string NomeCliente { get; set; }
        public string DocumentoCliente { get; set; }
        public string TelefoneCliente { get; set; }
        public string CelularCliente { get; set; }
        public string EmailCliente { get; set; }
        public decimal Peso { get; set; }
        public decimal Valor { get; set; }
        public DateTime Validade { get; set; }
        public FuncionarioViewModel Vendedor { get; set; }
        public CidadeViewModel Destino { get; set; }
        public TipoCaminhaoViewModel TipoCaminhao { get; set; }
        public UsuarioViewModel Autor { get; set; }
        public ClienteViewModel Cliente { get; set; }
    }
}