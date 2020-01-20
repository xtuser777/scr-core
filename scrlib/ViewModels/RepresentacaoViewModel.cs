using System;

namespace scrlib.ViewModels
{
    public class RepresentacaoViewModel
    {
        public int Id { get; set; }
        public DateTime Cadastro { get; set; }
        public string Unidade { get; set; }
        public PessoaJuridicaViewModel Pessoa { get; set; }
    }
}