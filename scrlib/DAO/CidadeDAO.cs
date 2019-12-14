using scrlib.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace scrlib.DAO
{
    internal class CidadeDAO : Banco
    {
        private Cidade GetObject(DataRow dr)
        {
            Cidade cidade = new Cidade()
            {
                Id = Convert.ToInt32(dr["id"]),
                Nome = dr["nome"].ToString(),
                Estado = Convert.ToInt32(dr["estado"])
            };
            return cidade;
        }

        private List<Cidade> GetList(DataTable dt)
        {
            List<Cidade> cidades = new List<Cidade>();
            foreach (DataRow row in dt.Rows)
            {
                cidades.Add(GetObject(row));
            }
            return cidades;
        }

        internal Cidade GetById(int id)
        {
            Cidade cidade = null;
            ComandoSQL.Parameters.Clear();
            ComandoSQL.CommandText = @"select id,nome,estado 
                                        from cidade 
                                        where id = @id;";
            ComandoSQL.Parameters.AddWithValue("@id", id);
            DataTable dt = ExecutaSelect();
            if (dt != null && dt.Rows.Count > 0)
            {
                cidade = GetObject(dt.Rows[0]);
            }
            return cidade;
        }

        internal List<Cidade> Get()
        {
            List<Cidade> cidades = null;
            ComandoSQL.Parameters.Clear();
            ComandoSQL.CommandText = @"select id,nome,estado 
                                        from cidade;";
            DataTable dt = ExecutaSelect();
            if (dt != null && dt.Rows.Count > 0)
            {
                cidades = GetList(dt);
            }
            return cidades;
        }

        internal List<Cidade> GetByEstado(int estado)
        {
            List<Cidade> cidades = null;
            ComandoSQL.Parameters.Clear();
            ComandoSQL.CommandText = @"select id,nome,estado 
                                        from cidade 
                                        where estado = @est;";
            ComandoSQL.Parameters.AddWithValue("@est", estado);
            DataTable dt = ExecutaSelect();
            if (dt != null && dt.Rows.Count > 0)
            {
                cidades = GetList(dt);
            }
            return cidades;
        }
    }
}
