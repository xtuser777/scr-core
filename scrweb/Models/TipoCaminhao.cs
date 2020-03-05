using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using scrweb.DAO;

namespace scrweb.Models
{
    public class TipoCaminhao
    {
        private int _id;
        private string _descricao;
        private int _eixos;
        private decimal _capacidade;

        public int Id { get => _id; set => _id = value; }
        public string Descricao { get => _descricao; set => _descricao = value; }
        public int Eixos { get => _eixos; set => _eixos = value; }
        public decimal Capacidade { get => _capacidade; set => _capacidade = value; }

        public TipoCaminhao GetById(int id)
        {
            return id > 0 ? new TipoCaminhaoDAO().GetById(id) : null;
        }

        public List<TipoCaminhao> GetAll()
        {
            return new TipoCaminhaoDAO().GetAll();
        }

        public int Gravar()
        {
            if (_id != 0 || string.IsNullOrEmpty(_descricao) || _eixos <= 0 || _capacidade <= 0) return -5;
            
            return new TipoCaminhaoDAO().Gravar(this);
        }

        public int Alterar()
        {
            if (_id <= 0 || string.IsNullOrEmpty(_descricao) || _eixos <= 0 || _capacidade <= 0) return -5;
            
            return new TipoCaminhaoDAO().Alterar(this);
        }

        public int Excluir(int id)
        {
            return id > 0 ? new TipoCaminhaoDAO().Excluir(id) : -5;
        }

        public JObject ToJObject()
        {
            JObject json = new JObject();
            json.Add("id", _id);
            json.Add("descricao", _descricao);
            json.Add("eixos", _eixos);
            json.Add("capacidade", _capacidade);

            return json;
        }
    }
}