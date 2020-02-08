using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using scrlib.Models;

namespace scrlib.DAO
{
    internal class MotoristaDAO : Banco
    {
        private Motorista GetObject(DataRow row)
        {
            return new Motorista()
            {
                Id = Convert.ToInt32(row["id"]),
                Cadastro = Convert.ToDateTime(row["cadastro"]),
                Pessoa = Convert.ToInt32(row["pessoa"])
            };
        }

        private List<Motorista> GetList(DataTable table)
        {
            return (from DataRow row in table.Rows select GetObject(row)).ToList();
        }

        internal Motorista GetById(int id)
        {
            ComandoSQL.Parameters.Clear();
            ComandoSQL.CommandText = @"
                select id,cadastro,pessoa 
                from motorista 
                where id = @id;
            ";
            ComandoSQL.Parameters.AddWithValue("@id", id);

            var table = ExecutaSelect();

            return table != null && table.Rows.Count > 0 ? GetObject(table.Rows[0]) : null;
        }

        internal List<Motorista> GetAll()
        {
            ComandoSQL.Parameters.Clear();
            ComandoSQL.CommandText = @"
                select id,cadastro,pessoa 
                from motorista;
            ";

            var table = ExecutaSelect();

            return table != null && table.Rows.Count > 0 ? GetList(table) : null;
        }

        internal int Gravar(Motorista m)
        {
            ComandoSQL.Parameters.Clear();
            ComandoSQL.CommandText = @"
                insert into motorista(cadastro,pessoa)
                values(@cad,@pes) returning id;
            ";
            ComandoSQL.Parameters.AddWithValue("@cad", m.Cadastro);
            ComandoSQL.Parameters.AddWithValue("@pes", m.Pessoa);

            var table = ExecutaSelect();

            return table != null && table.Rows.Count > 0 ? Convert.ToInt32(table.Rows[0]["id"]) : -10;
        }

        internal int Alterar(Motorista m)
        {
            ComandoSQL.Parameters.Clear();
            ComandoSQL.CommandText = @"
                update motorista 
                set cadastro = @cad,
                pessoa = @pes
                where id = @id;
            ";
            ComandoSQL.Parameters.AddWithValue("@cad", m.Cadastro);
            ComandoSQL.Parameters.AddWithValue("@pes", m.Pessoa);
            ComandoSQL.Parameters.AddWithValue("@id", m.Id);

            return ExecutaComando();
        }

        internal int Excluir(int id)
        {
            ComandoSQL.Parameters.Clear();
            ComandoSQL.CommandText = @"
                delete from motorista where id = @id;
            ";
            ComandoSQL.Parameters.AddWithValue("@id", id);

            return ExecutaComando();
        }
    }
}