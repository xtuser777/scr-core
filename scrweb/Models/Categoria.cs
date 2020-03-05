using System;

namespace scrweb.Models
{
    public class Categoria
    {
        private int _id;
        private string _descricao;

        public string Descricao { get { return _descricao; } set { _descricao = value; } }
        public int Id { get { return _id; } set { _id = value; } }
    }
}