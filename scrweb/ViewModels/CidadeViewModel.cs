using System;
using System.Collections.Generic;
using System.Text;

namespace scrweb.ViewModels
{
    public class CidadeViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public EstadoViewModel Estado { get; set; }
    }
}
