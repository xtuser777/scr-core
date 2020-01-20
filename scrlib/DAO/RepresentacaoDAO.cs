using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using scrlib.Models;

namespace scrlib.DAO
{
    internal class RepresentacaoDAO : Banco
    {
        private Representacao GetObject(DataRow row)
        {
            return new Representacao()
            {
                Id = Convert.ToInt32(row["id"]),
                Cadastro = Convert.ToDateTime(row["cadastro"]),
                Unidade = row["unidade"].ToString(),
                Pessoa = Convert.ToInt32(row["pessoa"])
            };
        }

        private List<Representacao> GetList(DataTable table)
        {
            return (from DataRow row in table.Rows select GetObject(row)).ToList();
        }

        internal Representacao GetById(int id)
        {
            ComandoSQL.Parameters.Clear();
            ComandoSQL.CommandText = @"select id,cadastro,unidade,pessoa from representacao where id = @id;";
            ComandoSQL.Parameters.AddWithValue("@id", id);

            var tables = ExecutaSelect();

            return tables != null && tables.Rows.Count > 0 ? GetObject(tables.Rows[0]) : null;
        }

        internal List<Representacao> GetAll()
        {
            ComandoSQL.Parameters.Clear();
            ComandoSQL.CommandText = @"select id,cadastro,unidade,pessoa from representacao order by id;";

            var tables = ExecutaSelect();

            return tables != null && tables.Rows.Count > 0 ? GetList(tables) : null;
        }

        internal int Gravar(Representacao r)
        {
            ComandoSQL.Parameters.Clear();
            ComandoSQL.CommandText = @"
            insert into representacao(cadastro,unidade,pessoa) 
            values(@cad,@uni,@pes) returning id;";
            ComandoSQL.Parameters.AddWithValue("@cad", r.Cadastro);
            ComandoSQL.Parameters.AddWithValue("@uni", r.Unidade);
            ComandoSQL.Parameters.AddWithValue("@pes", r.Pessoa);

            var table = ExecutaSelect();

            return table != null && table.Rows.Count > 0 ? Convert.ToInt32(table.Rows[0]["id"]) : -10;
        }

        internal int Alterar(Representacao r)
        {
            ComandoSQL.Parameters.Clear();
            ComandoSQL.CommandText = @"
            update representacao
            set cadastro = @cad,
            unidade = @uni,
            pessoa = @pes
            where id = @id;";
            ComandoSQL.Parameters.AddWithValue("@cad", r.Cadastro);
            ComandoSQL.Parameters.AddWithValue("@uni", r.Unidade);
            ComandoSQL.Parameters.AddWithValue("@pes", r.Pessoa);
            ComandoSQL.Parameters.AddWithValue("@id", r.Id);

            return ExecutaComando();
        }

        internal int Excluir(int id)
        {
            ComandoSQL.Parameters.Clear();
            ComandoSQL.CommandText = @"delete from representacao where id = @id;";
            ComandoSQL.Parameters.AddWithValue("@id", id);

            return ExecutaComando();
        }
    }
}