﻿using System;
using System.Collections.Generic;
using System.Text;

namespace scrweb.ViewModels
{
    public class UsuarioViewModel
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
        public bool Ativo { get; set; }
        public FuncionarioViewModel Funcionario { get; set; }
        public NivelViewModel Nivel { get; set; }
    }
}
