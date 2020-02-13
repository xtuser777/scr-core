using System;
using System.Linq;
using scrweb.Models;
using System.Data;
using System.Collections.Generic;

namespace scrweb.DAO 
{
    internal class CaminhaoDAO : Banco
    {
        private Caminhao GetObject(DataRow row) 
        {
            return new Caminhao() 
            {
                Id = Convert.ToInt32(row["id"]),
                Placa = row["placa"].ToString(),
                Marca = row["marca"].ToString(),
                Modelo = row["modelo"].ToString(),
                Ano = row["ano"].ToString(),
                Tipo = Convert.ToInt32(row["tipo"]),
                Proprietario = Convert.ToInt32(row["proprietario"])
            };
        }

        private List<Caminhao> GetList(DataTable table) 
        {
            return (from DataRow row in table.Rows select GetObject(row)).ToList();
        }

        internal Caminhao GetById(int id) 
        {
            ComandoSQL.Parameters.Clear();
            ComandoSQL.CommandText = @"
                select id,placa,marca,modelo,ano,tipo,proprietario 
                from caminhao
                where id = @id;
            ";
            ComandoSQL.Parameters.AddWithValue("@id", id);

            var table = ExecutaSelect();

            return table != null && table.Rows.Count > 0 ? GetObject(table.Rows[0]) : null;    
        }

        internal List<Caminhao> GetAll()
        {
            ComandoSQL.Parameters.Clear();
            ComandoSQL.CommandText = @"
                select id,placa,marca,modelo,ano,tipo,proprietario 
                from caminhao;
            ";

            var table = ExecutaSelect();

            return table != null && table.Rows.Count > 0 ? GetList(table) : null;
        }

        internal int Gravar(Caminhao c) 
        {
            ComandoSQL.Parameters.Clear();
            ComandoSQL.CommandText = @"
                insert into caminhao(placa,marca,modelo,ano,tipo,proprietario) 
                values(@pla,@mar,@mod,@ano,@tip,@pro) returning id;
            ";
            ComandoSQL.Parameters.AddWithValue("@pla", c.Placa);
            ComandoSQL.Parameters.AddWithValue("@mar", c.Marca);
            ComandoSQL.Parameters.AddWithValue("@mod", c.Modelo);
            ComandoSQL.Parameters.AddWithValue("@ano", c.Ano);
            ComandoSQL.Parameters.AddWithValue("@tip", c.Tipo);
            ComandoSQL.Parameters.AddWithValue("@pro", c.Proprietario);

            var table = ExecutaSelect();

            return table != null && table.Rows.Count > 0 ? Convert.ToInt32(table.Rows[0]["id"]) : -10;
        }

        internal int Alterar(Caminhao c) 
        {
            ComandoSQL.Parameters.Clear();
            ComandoSQL.CommandText = @"
                update caminhao
                set placa = @pla,
                marca = @mar,
                modelo = @mod,
                ano = @ano,
                tipo = @tip,
                proprietario = @pro
                where id = @id;
            ";
            ComandoSQL.Parameters.AddWithValue("@pla", c.Placa);
            ComandoSQL.Parameters.AddWithValue("@mar", c.Marca);
            ComandoSQL.Parameters.AddWithValue("@mod", c.Modelo);
            ComandoSQL.Parameters.AddWithValue("@ano", c.Ano);
            ComandoSQL.Parameters.AddWithValue("@tip", c.Tipo);
            ComandoSQL.Parameters.AddWithValue("@pro", c.Proprietario);
            ComandoSQL.Parameters.AddWithValue("@id", c.Id);

            return ExecutaComando();
        }

        internal int Excluir(int id) 
        {
            ComandoSQL.Parameters.Clear();
            ComandoSQL.CommandText = @"delete from caminhao where id = @id;";
            ComandoSQL.Parameters.AddWithValue("@id", id);

            return ExecutaComando();
        }
    }
}