using System;
using System.Collections.Generic;
using System.Text;

namespace scrlib.ViewModels
{
    public class EnderecoViewModel
    {
        public int Id { get; set; }
        public string Rua { get; set; }
        public string Numero { get; set; }
        public string Bairro { get; set; }
        public string Complemento { get; set; }
        public string Cep { get; set; }
        public CidadeViewModel Cidade { get; set; }
    }
}
