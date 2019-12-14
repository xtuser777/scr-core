using System;
using System.Collections.Generic;
using System.Text;

namespace scrlib.ViewModels
{
    public class FuncionarioViewModel
    {
        public int Id { get; set; }
        public int Tipo { get; set; }
        public DateTime Admissao { get; set; }
        public DateTime? Demissao { get; set; }
        public PessoaFisicaViewModel Pessoa { get; set; }
    }
}
