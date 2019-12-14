using System;
using System.Collections.Generic;
using System.Text;

namespace scrlib.ViewModels
{
    public class PessoaJuridicaViewModel : PessoaViewModel
    {
        public string RazaoSocial { get; set; }
        public string NomeFantasia { get; set; }
        public string Cnpj { get; set; }
    }
}
