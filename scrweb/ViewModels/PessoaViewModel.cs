using System;
using System.Collections.Generic;
using System.Text;

namespace scrweb.ViewModels
{
    public class PessoaViewModel
    {
        public int Id { get; set; }
        public int Tipo { get; set; }
        public string Telefone { get; set; }
        public string Celular { get; set; }
        public string Email { get; set; }
        public EnderecoViewModel Endereco { get; set; }
    }
}
