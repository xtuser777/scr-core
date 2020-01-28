using scrlib.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace scrlib.DAO
{
    internal class FuncionarioDAO : Banco
    {
        private Funcionario GetObject(DataRow dr)
        {
            return new Funcionario()
            {
                Id = Convert.ToInt32(dr["id"]),
                Tipo = Convert.ToInt32(dr["tipo"]),
                Admissao = Convert.ToDateTime(dr["admissao"]),
                Demissao = (dr["demissao"] is DBNull) ? (DateTime?)null : Convert.ToDateTime(dr["demissao"]),
                Pessoa = Convert.ToInt32(dr["pessoa"])
            };
        }

        private List<Funcionario> GetList(DataTable dt)
        {
            return (from DataRow row in dt.Rows select GetObject(row)).ToList();
        }

        internal Funcionario GetById(int id)
        {
            ComandoSQL.Parameters.Clear();
            ComandoSQL.CommandText = @"select id,tipo,admissao,demissao,pessoa from funcionario where id = @id;";
            ComandoSQL.Parameters.AddWithValue("@id", id);
            
            var dt = ExecutaSelect();
            
            return (dt != null && dt.Rows.Count > 0) ? GetObject(dt.Rows[0]) : null;
        }

        internal List<Funcionario> Get()
        {
            ComandoSQL.Parameters.Clear();
            ComandoSQL.CommandText = @"select id,tipo,admissao,demissao,pessoa from funcionario;";
            
            var dt = ExecutaSelect();
            
            return (dt != null && dt.Rows.Count > 0) ? GetList(dt) : null;
        }

        internal Funcionario GetVendedorById(int id)
        {
            ComandoSQL.Parameters.Clear();
            ComandoSQL.CommandText = @"select id,tipo,admissao,demissao,pessoa from funcionario where id = @id and tipo = 2;";
            ComandoSQL.Parameters.AddWithValue("@id", id);
            
            var dt = ExecutaSelect();
            
            return (dt != null && dt.Rows.Count > 0) ? GetObject(dt.Rows[0]) : null;
        }

        internal List<Funcionario> GetVendedores()
        {
            ComandoSQL.Parameters.Clear();
            ComandoSQL.CommandText = @"select id,tipo,admissao,demissao,pessoa from funcionario where tipo = 2;";
            
            var dt = ExecutaSelect(); 
            
            return (dt != null && dt.Rows.Count > 0) ? GetList(dt) : null;
        }

        internal int Gravar(Funcionario f)
        {
            ComandoSQL.Parameters.Clear();
            ComandoSQL.CommandText = @"insert into funcionario(tipo,admissao,demissao,pessoa) 
            values(@tipo,@admissao,null,@pessoa) returning id;";
            ComandoSQL.Parameters.AddWithValue("@tipo", f.Tipo);
            ComandoSQL.Parameters.AddWithValue("@admissao", f.Admissao);
            ComandoSQL.Parameters.AddWithValue("@pessoa", f.Pessoa);
            
            var dt = ExecutaSelect();
            
            return (dt != null && dt.Rows.Count > 0) ? Convert.ToInt32(dt.Rows[0]["id"]) : -10;
        }

        internal int Alterar(Funcionario f)
        {
            ComandoSQL.Parameters.Clear();
            ComandoSQL.CommandText = @"update funcionario 
                                        set tipo = @tipo,
                                            admissao = @admissao,
                                            pessoa = @pessoa
                                            where id = @id;";
            ComandoSQL.Parameters.AddWithValue("@tipo", f.Tipo);
            ComandoSQL.Parameters.AddWithValue("@admissao", f.Admissao);
            ComandoSQL.Parameters.AddWithValue("@pessoa", f.Pessoa);
            ComandoSQL.Parameters.AddWithValue("@id", f.Id);

            return ExecutaComando();
        }

        internal int Excluir(int id)
        {
            ComandoSQL.Parameters.Clear();
            ComandoSQL.CommandText = @"delete from funcionario where id = @id;";
            ComandoSQL.Parameters.AddWithValue("@id", id);

            return ExecutaComando();
        }

        internal int Desativar(int id)
        {
            ComandoSQL.Parameters.Clear();
            ComandoSQL.CommandText = @"update funcionario set demissao = now() where id = @id;
                                       update usuario set ativo = false where id = @id;";
            ComandoSQL.Parameters.AddWithValue("@id", id);

            return ExecutaComando();
        }

        internal int Reativar(int id)
        {
            ComandoSQL.Parameters.Clear();
            ComandoSQL.CommandText = @"update funcionario set demissao = null where id = @id;
                                       update usuario set ativo = true where id = @id;";
            ComandoSQL.Parameters.AddWithValue("@id", id);

            return ExecutaComando();
        }
    }
}
