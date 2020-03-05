using scrweb.Models;
using System;
using System.Collections.Generic;
using System.Data;

namespace scrweb.DAO
{
    internal class CidadeDAO : Banco
    {
        private Cidade GetObject(DataRow dr)
        {
            Cidade cidade = new Cidade()
            {
                Id = Convert.ToInt32(dr["cid_id"]),
                Nome = dr["cid_nome"].ToString(),
                Estado = new Estado()
                {
                    Id = Convert.ToInt32(dr["est_id"]),
                    Nome = dr["est_nome"].ToString(),
                    Sigla = dr["est_sigla"].ToString()
                }
            };
            
            return cidade;
        }

        private List<Cidade> GetList(DataTable dt)
        {
            List<Cidade> list = new List<Cidade>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow row = dt.Rows[i];
                list.Add(GetObject(row));
            }

            return list;
        }

        internal Cidade GetById(int id)
        {
            ComandoSQL.Parameters.Clear();
            ComandoSQL.CommandText = @"
                select e.id as est_id, e.nome as est_nome, e.sigla as est_sigla,
                       c.id as cid_id, c.nome as cid_nome, c.estado as cid_estado 
                from estado e, cidade c 
                where c.id = @id
                and e.id = c.estado;
            ";
            ComandoSQL.Parameters.AddWithValue("@id", id);
            
            DataTable dt = ExecutaSelect();
            
            if (dt != null && dt.Rows.Count > 0)
            {
                return GetObject(dt.Rows[0]);
            }
            
            return null;
        }

        internal List<Cidade> GetAll()
        {
            ComandoSQL.Parameters.Clear();
            ComandoSQL.CommandText = @"
                select e.id as est_id, e.nome as est_nome, e.sigla as est_sigla,
                       c.id as cid_id, c.nome as cid_nome, c.estado as cid_estado 
                from estado e, cidade c 
                where e.id = c.estado
                order by c.id;
            ";
            
            DataTable dt = ExecutaSelect();
            
            if (dt != null && dt.Rows.Count > 0)
            {
                return GetList(dt);
            }
            
            return null;
        }

        internal List<Cidade> GetByEstado(int estado)
        {
            ComandoSQL.Parameters.Clear();
            ComandoSQL.CommandText = @"
                select e.id as est_id, e.nome as est_nome, e.sigla as est_sigla,
                       c.id as cid_id, c.nome as cid_nome, c.estado as cid_estado 
                from estado e, cidade c 
                where c.estado = @est
                and e.id = c.estado
                order by c.id;
            ";
            ComandoSQL.Parameters.AddWithValue("@est", estado);
            var dt = ExecutaSelect();
            if (dt != null && dt.Rows.Count > 0)
            {
                return GetList(dt);
            }
            
            return null;
        }
    }
}
