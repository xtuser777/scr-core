using System;

namespace scrweb.ViewModels
{
    public class MotoristaViewModel
    {
        public int Id { get; set; }
        public DateTime Cadastro { get; set; }
        public PessoaFisicaViewModel Pessoa { get; set; }
    }
}