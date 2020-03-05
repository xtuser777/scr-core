using System;
using System.Linq;
using System.Collections.Generic;
using System.Data;
using scrweb.Models;

namespace scrweb.DAO
{
    internal class CategoriaDAO : Banco
    {
        private Categoria GetObject(DataRow row) 
        {
            return new Categoria() 
            {
                Id = Convert.ToInt32(row["id"]),
                Descricao = row["descricao"].ToString()
            };
        }

        private List<Categoria> GetList(DataTable table)
        {
            return (from DataRow row in table.Rows select GetObject(row)).ToList();
        }

        internal Categoria GetById(int id)
        {
            ComandoSQL.Parameters.Clear();
            ComandoSQL.CommandText = @"
                select id,descricao 
                from categoria
                where id = @id;
            ";
            ComandoSQL.Parameters.AddWithValue("@id", id);

            var table = ExecutaSelect();

            return table != null && table.Rows.Count > 0 ? GetObject(table.Rows[0]) : null;
        }

        internal List<Categoria> GetAll()
        {
            ComandoSQL.Parameters.Clear();
            ComandoSQL.CommandText = @"
                select id,descricao 
                from categoria;
            ";

            var table = ExecutaSelect();

            return table != null && table.Rows.Count > 0 ? GetList(table) : null;
        }

        internal int Gravar(Categoria c)
        {
            ComandoSQL.Parameters.Clear();
            ComandoSQL.CommandText = @"
                insert into categoria(descricao) 
                values(@des) 
                returning id;
            ";
            ComandoSQL.Parameters.AddWithValue("@des", c.Descricao);

            var table = ExecutaSelect();

            return table != null && table.Rows.Count > 0 ? Convert.ToInt32(table.Rows[0]["id"]) : -10;
        }

        internal int Alterar(Categoria c)
        {
            ComandoSQL.Parameters.Clear();
            ComandoSQL.CommandText = @"
                update categoria 
                set descricao = @des
                where id = @id;
            ";
            ComandoSQL.Parameters.AddWithValue("@des", c.Descricao);
            ComandoSQL.Parameters.AddWithValue("@id", c.Id);

            return ExecutaComando();
        }

        internal int Excluir(int id)
        {
            ComandoSQL.Parameters.Clear();
            ComandoSQL.CommandText = @"delete from categoria where id = @id;";
            ComandoSQL.Parameters.AddWithValue("@id", id);

            return ExecutaComando();
        }
    }
}