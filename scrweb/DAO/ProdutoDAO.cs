using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using scrweb.Models;

namespace scrweb.DAO
{
    internal class ProdutoDAO : Banco
    {
        private Produto GetObject(DataRow row)
        {
            return new Produto()
            {
                Id = Convert.ToInt32(row["id"]),
                Descricao = row["descricao"].ToString(),
                Medida = row["medida"].ToString(),
                Preco = Convert.ToDecimal(row["preco"]),
                PrecoOut = Convert.ToDecimal(row["preco_out"]),
                Representacao = Convert.ToInt32(row["representacao"])
            };
        }

        private List<Produto> GetList(DataTable table)
        {
            return (from DataRow row in table.Rows select GetObject(row)).ToList();
        }

        internal Produto GetById(int id)
        {
            ComandoSQL.Parameters.Clear();
            ComandoSQL.CommandText = @"
                select id,descricao,medida,preco,preco_out,representacao 
                from produto 
                where id = @id;
            ";
            ComandoSQL.Parameters.AddWithValue("@id", id);

            var table = ExecutaSelect();

            return table != null && table.Rows.Count > 0 ? GetObject(table.Rows[0]) : null;
        }

        internal List<Produto> GetAll()
        {
            ComandoSQL.Parameters.Clear();
            ComandoSQL.CommandText = @"
                select id,descricao,medida,preco,preco_out,representacao 
                from produto;
            ";

            var table = ExecutaSelect();

            return table != null && table.Rows.Count > 0 ? GetList(table) : null;
        }

        internal int Gravar(Produto p)
        {
            ComandoSQL.Parameters.Clear();
            ComandoSQL.CommandText = @"
                insert into produto(descricao,medida,preco,preco_out,representacao)
                values(@des,@med,@pre,@po,@rep) returning id;
            ";
            ComandoSQL.Parameters.AddWithValue("@des", p.Descricao);
            ComandoSQL.Parameters.AddWithValue("@med", p.Medida);
            ComandoSQL.Parameters.AddWithValue("@pre", p.Preco);
            ComandoSQL.Parameters.AddWithValue("@po", p.PrecoOut);
            ComandoSQL.Parameters.AddWithValue("@rep", p.Representacao);

            var table = ExecutaSelect();
            
            return table != null && table.Rows.Count > 0 ? Convert.ToInt32(table.Rows[0]["id"]) : -10;
        }

        internal int Alterar(Produto p)
        {
            ComandoSQL.Parameters.Clear();
            ComandoSQL.CommandText = @"
                update produto 
                set descricao = @des,
                medida = @med,
                preco = @pre,
                preco_out = @po,
                representacao = @rep
                where id = @id;
            ";
            ComandoSQL.Parameters.AddWithValue("@des", p.Descricao);
            ComandoSQL.Parameters.AddWithValue("@med", p.Medida);
            ComandoSQL.Parameters.AddWithValue("@pre", p.Preco);
            ComandoSQL.Parameters.AddWithValue("@po", p.PrecoOut);
            ComandoSQL.Parameters.AddWithValue("@rep", p.Representacao);
            ComandoSQL.Parameters.AddWithValue("@id", p.Id);

            return ExecutaComando();
        }

        internal int Excluir(int id)
        {
            ComandoSQL.Parameters.Clear();
            ComandoSQL.CommandText = @"
                delete from produto where id = @id;
            ";
            ComandoSQL.Parameters.AddWithValue("@id", id);

            return ExecutaComando();
        }
    }
}