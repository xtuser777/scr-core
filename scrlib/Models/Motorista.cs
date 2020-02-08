using System;
using System.Collections.Generic;
using scrlib.DAO;

namespace scrlib.Models
{
    internal class Motorista
    {
        private static int _id;
        private static DateTime _cadastro;
        private static int _pessoa;

        internal int Id { get => _id; set => _id = value; }

        internal DateTime Cadastro { get => _cadastro; set => _cadastro = value; }

        internal int Pessoa { get => _pessoa; set => _pessoa = value; }

        internal static Motorista GetById(int id)
        {
            return id > 0 ? new MotoristaDAO().GetById(id) : null;
        }

        internal static List<Motorista> GetAll()
        {
            return new MotoristaDAO().GetAll();
        }

        internal int Gravar()
        {
            return _id == 0 && _pessoa > 0 
                ? new MotoristaDAO().Gravar(this) 
                : -5;
        }

        internal int Alterar()
        {
            return _id > 0 && _pessoa > 0 
                ? new MotoristaDAO().Alterar(this) 
                : -5;
        }

        internal static int Excluir(int id)
        {
            return id > 0 ? new MotoristaDAO().Excluir(id) : -5;
        }
    }
}