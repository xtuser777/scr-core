using System;
using System.Collections.Generic;
using System.Data;
using scrweb.Models;

namespace scrweb.DAO
{
    internal class TipoCaminhaoDAO : Banco
    {
        private TipoCaminhao GetObject(DataRow row)
        {
            return new TipoCaminhao()
            {
                Id = Convert.ToInt32(row["id"]),
                Descricao = row["descricao"].ToString(),
                Eixos = Convert.ToInt32(row["eixos"]),
                Capacidade = Convert.ToDecimal(row["capacidade"])
            };
        }

        private List<TipoCaminhao> GetList(DataTable table)
        {
            List<TipoCaminhao> list = new List<TipoCaminhao>();

            for (int i = 0; i < table.Rows.Count; i++)
            {
                DataRow row = table.Rows[i];
                list.Add(GetObject(row));
            }

            return list;
        }

        internal TipoCaminhao GetById(int id)
        {
            ComandoSQL.Parameters.Clear();
            ComandoSQL.CommandText = @"
                select id,descricao,capacidade,eixos 
                from tipo_caminhao 
                where id = @id;
            ";
            ComandoSQL.Parameters.AddWithValue("@id", id);

            DataTable table = ExecutaSelect();

            return table != null && table.Rows.Count > 0 ? GetObject(table.Rows[0]) : null;
        }

        internal List<TipoCaminhao> GetAll()
        {
            ComandoSQL.Parameters.Clear();
            ComandoSQL.CommandText = @"
                select id,descricao,capacidade,eixos 
                from tipo_caminhao;
            ";

            DataTable table = ExecutaSelect();
            
            return table != null && table.Rows.Count > 0 ? GetList(table) : null;
        }

        internal int Gravar(TipoCaminhao tc)
        {
            ComandoSQL.Parameters.Clear();
            ComandoSQL.CommandText = @"
                insert into tipo_caminhao(descricao,eixos,capacidade)
                values(@des,@eix,@cap) 
                returning id;
            ";
            ComandoSQL.Parameters.AddWithValue("@des", tc.Descricao);
            ComandoSQL.Parameters.AddWithValue("@eix", tc.Eixos);
            ComandoSQL.Parameters.AddWithValue("@cap", tc.Capacidade);

            DataTable table = ExecutaSelect();

            return table != null && table.Rows.Count > 0 ? Convert.ToInt32(table.Rows[0]["id"]) : -10;
        }

        internal int Alterar(TipoCaminhao tc)
        {
            ComandoSQL.Parameters.Clear();
            ComandoSQL.CommandText = @"
                update tipo_caminhao 
                set descricao = @des,
                eixos = @eix,
                capacidade = @cap
                where id = @id;
            ";
            ComandoSQL.Parameters.AddWithValue("@des", tc.Descricao);
            ComandoSQL.Parameters.AddWithValue("@eix", tc.Eixos);
            ComandoSQL.Parameters.AddWithValue("@cap", tc.Capacidade);
            ComandoSQL.Parameters.AddWithValue("@id", tc.Id);

            return ExecutaComando();
        }

        internal int Excluir(int id)
        {
            ComandoSQL.Parameters.Clear();
            ComandoSQL.CommandText = @"delete from tipo_caminhao where id = @id;";
            ComandoSQL.Parameters.AddWithValue("@id", id);

            return ExecutaComando();
        }
    }
}