using scrlib.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace scrlib.DAO
{
    internal class EstadoDAO : Banco
    {
        private Estado GetObject(DataRow dr)
        {
            Estado e = new Estado()
            {
                Id = Convert.ToInt32(dr["id"]),
                Nome = dr["nome"].ToString(),
                Sigla = dr["sigla"].ToString()
            };
            return e;
        }

        private List<Estado> GetList(DataTable dt)
        {
            List<Estado> estados = new List<Estado>();
            foreach (DataRow row in dt.Rows)
            {
                estados.Add(GetObject(row));
            }
            return estados;
        }

        internal Estado GetById(int id)
        {
            Estado e = null;
            ComandoSQL.Parameters.Clear();
            ComandoSQL.CommandText = @"select * from estado where id = @id;";
            ComandoSQL.Parameters.AddWithValue("@id", id);
            DataTable dt = ExecutaSelect();
            if (dt != null && dt.Rows.Count > 0)
            {
                e = GetObject(dt.Rows[0]);
            }
            return e;
        }

        internal List<Estado> Get()
        {
            List<Estado> estados = null;
            ComandoSQL.Parameters.Clear();
            ComandoSQL.CommandText = @"select * from estado;";
            DataTable dt = ExecutaSelect();
            if (dt != null && dt.Rows.Count > 0)
            {
                estados = GetList(dt);
            }
            return estados;
        }
    }
}
