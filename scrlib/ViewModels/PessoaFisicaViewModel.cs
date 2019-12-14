using System;
using System.Collections.Generic;
using System.Text;

namespace scrlib.ViewModels
{
    public class PessoaFisicaViewModel : PessoaViewModel
    {
        public string Nome { get; set; }
        public string Rg { get; set; }
        public string Cpf { get; set; }
        public DateTime Nascimento { get; set; }
    }
}
