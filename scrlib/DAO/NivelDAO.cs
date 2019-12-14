using scrlib.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace scrlib.DAO
{
    internal class NivelDAO : Banco
    {
        private Nivel GetObject(DataRow dr)
        {
            Nivel nivel = new Nivel()
            {
                Id = Convert.ToInt32(dr["id"]),
                Descricao = dr["descricao"].ToString()
            };
            return nivel;
        }

        private List<Nivel> GetList(DataTable dt)
        {
            List<Nivel> niveis = new List<Nivel>();
            foreach (DataRow row in dt.Rows)
            {
                niveis.Add(GetObject(row));
            }
            return niveis;
        }

        internal Nivel GetById(int id)
        {
            Nivel nivel = null;
            ComandoSQL.Parameters.Clear();
            ComandoSQL.CommandText = @"select id,descricao 
                                        from nivel 
                                        where id = @id;";
            ComandoSQL.Parameters.AddWithValue("@id", id);
            DataTable dt = ExecutaSelect();
            if (dt != null && dt.Rows.Count > 0)
            {
                nivel = GetObject(dt.Rows[0]);
            }
            return nivel;
        }

        internal List<Nivel> Get()
        {
            List<Nivel> niveis = null;
            ComandoSQL.Parameters.Clear();
            ComandoSQL.CommandText = @"select id,descricao 
                                        from nivel;";
            DataTable dt = ExecutaSelect();
            if (dt != null && dt.Rows.Count > 0)
            {
                niveis = GetList(dt);
            }
            return niveis;
        }
    }
}
