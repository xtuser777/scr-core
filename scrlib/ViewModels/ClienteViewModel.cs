using System;

namespace scrlib.ViewModels
{
    public class ClienteViewModel
    {
        public int Id { get; set; }
        public DateTime Cadastro { get; set; }
        public int Tipo { get; set; }
        public PessoaViewModel Pessoa { get; set; }
    }
}