using System;

namespace scrweb.Models
{
    public class FormaPagamento
    {
        private int id;
        private string descricao;
        private int prazo;

        public int Prazo { get { return prazo; } set { prazo = value; }}
        public string Descricao { get { return descricao; } set { descricao = value; } }
        public int Id { get { return id; } set { id = value; } }
    }
}