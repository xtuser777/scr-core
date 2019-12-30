using scrlib.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace scrlib.DAO
{
    internal class CidadeDAO : Banco
    {
        private Cidade GetObject(DataRow dr)
        {
            var cidade = new Cidade()
            {
                Id = Convert.ToInt32(dr["id"]),
                Nome = dr["nome"].ToString(),
                Estado = Convert.ToInt32(dr["estado"])
            };
            
            return cidade;
        }

        private List<Cidade> GetList(DataTable dt)
        {
            return (from DataRow row in dt.Rows select GetObject(row)).ToList();
        }

        internal Cidade GetById(int id)
        {
            ComandoSQL.Parameters.Clear();
            ComandoSQL.CommandText = @"select id,nome,estado 
                                        from cidade 
                                        where id = @id;";
            ComandoSQL.Parameters.AddWithValue("@id", id);
            var dt = ExecutaSelect();
            if (dt != null && dt.Rows.Count > 0)
            {
                return GetObject(dt.Rows[0]);
            }
            
            return null;
        }

        internal List<Cidade> Get()
        {
            ComandoSQL.Parameters.Clear();
            ComandoSQL.CommandText = @"select id,nome,estado 
                                        from cidade;";
            var dt = ExecutaSelect();
            if (dt != null && dt.Rows.Count > 0)
            {
                return GetList(dt);
            }
            
            return null;
        }

        internal List<Cidade> GetByEstado(int estado)
        {
            ComandoSQL.Parameters.Clear();
            ComandoSQL.CommandText = @"select id,nome,estado 
                                        from cidade 
                                        where estado = @est;";
            ComandoSQL.Parameters.AddWithValue("@est", estado);
            var dt = ExecutaSelect();
            if (dt != null && dt.Rows.Count > 0)
            {
                return GetList(dt);
            }
            
            return null;
        }
        
        internal List<Cidade> GetByEstAndKey(int estado, string chave)
        {
            ComandoSQL.Parameters.Clear();
            ComandoSQL.CommandText = @"select id,nome,estado 
                                        from cidade 
                                        where estado = @est 
                                        and nome like @chave;";
            ComandoSQL.Parameters.AddWithValue("@est", estado);
            ComandoSQL.Parameters.AddWithValue("@chave", "%" + chave + "%");
            var dt = ExecutaSelect();
            if (dt != null && dt.Rows.Count > 0)
            {
                return GetList(dt);
            }
            
            return null;
        }
    }
}
