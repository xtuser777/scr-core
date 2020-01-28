using scrlib.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace scrlib.DAO
{
    internal class UsuarioDAO : Banco
    {
        private Usuario GetObject(DataRow dr)
        {
            return new Usuario()
            {
                Id = Convert.ToInt32(dr["id"]),
                Login = dr["login"].ToString(),
                Senha = dr["senha"].ToString(),
                Ativo = Convert.ToBoolean(dr["ativo"]),
                Funcionario = Convert.ToInt32(dr["funcionario"]),
                Nivel = Convert.ToInt32(dr["nivel"])
            };
        }

        private List<Usuario> GetList(DataTable dt)
        {
            return (from DataRow row in dt.Rows select GetObject(row)).ToList();
        }

        internal Usuario GetById(int id)
        {
            ComandoSQL.Parameters.Clear();
            ComandoSQL.CommandText = @"select id,login,senha,funcionario,nivel,ativo from usuario  where id = @id;";
            ComandoSQL.Parameters.AddWithValue("@id", id);
            
            var dt = ExecutaSelect();
            
            return (dt != null && dt.Rows.Count > 0) ? GetObject(dt.Rows[0]) : null;
        }

        internal Usuario Autenticar(string login, string senha)
        {
            ComandoSQL.Parameters.Clear();
            ComandoSQL.CommandText = @"select id,login,senha,funcionario,nivel,ativo 
                                        from usuario 
                                        where login = @login 
                                        and senha = @senha 
                                        and ativo = true;";
            ComandoSQL.Parameters.AddWithValue("@login", login);
            ComandoSQL.Parameters.AddWithValue("@senha", senha);
            
            var dt = ExecutaSelect();
            
            return (dt != null && dt.Rows.Count > 0) ? GetObject(dt.Rows[0]) : null;
        }

        internal List<Usuario> Get()
        {
            ComandoSQL.Parameters.Clear();
            ComandoSQL.CommandText = @"select id,login,senha,funcionario,nivel,ativo 
                                        from usuario
                                        order by id;";
            
            var dt = ExecutaSelect();
            
            return (dt != null && dt.Rows.Count > 0) ?  GetList(dt) : null;
        }

        internal int Gravar(Usuario u)
        {
            ComandoSQL.Parameters.Clear();
            ComandoSQL.CommandText = @"insert into usuario(login,senha,funcionario,nivel,ativo) 
                                        values(@login,@senha,@funcionario,@nivel,@ativo) returning id;";
            ComandoSQL.Parameters.AddWithValue("@login", u.Login);
            ComandoSQL.Parameters.AddWithValue("@senha", u.Senha);
            ComandoSQL.Parameters.AddWithValue("@funcionario", u.Funcionario);
            ComandoSQL.Parameters.AddWithValue("@nivel", u.Nivel);
            ComandoSQL.Parameters.AddWithValue("@ativo", u.Ativo);
            
            var dt = ExecutaSelect();
            
            return (dt != null && dt.Rows.Count > 0) ? Convert.ToInt32(dt.Rows[0]["id"]) : -10;
        }

        internal int Alterar(Usuario u)
        {
            ComandoSQL.Parameters.Clear();
            ComandoSQL.CommandText = @"update usuario 
                                        set login = @login,
                                        senha = @senha,
                                        funcionario = @funcionario,
                                        nivel = @nivel,
                                        ativo = @ativo
                                        where id = @id;";
            ComandoSQL.Parameters.AddWithValue("@login", u.Login);
            ComandoSQL.Parameters.AddWithValue("@senha", u.Senha);
            ComandoSQL.Parameters.AddWithValue("@funcionario", u.Funcionario);
            ComandoSQL.Parameters.AddWithValue("@nivel", u.Nivel);
            ComandoSQL.Parameters.AddWithValue("@ativo", u.Ativo);
            ComandoSQL.Parameters.AddWithValue("@id", u.Id);

            return ExecutaComando();
        }

        internal int LoginCount(string login)
        {
            int count = 0;
            ComandoSQL.Parameters.Clear();
            ComandoSQL.CommandText = @"select count(id) as logins from usuario where login = @login;";
            ComandoSQL.Parameters.AddWithValue("@login", login);
            DataTable dt = ExecutaSelect();
            if (dt != null && dt.Rows.Count > 0)
            {
                count = Convert.ToInt32(dt.Rows[0]["logins"]);
            }
            return count;
        }

        internal int AdminCount()
        {
            ComandoSQL.Parameters.Clear();
            ComandoSQL.CommandText = @"select count(id) as admins 
                                        from usuario 
                                        inner join funcionario using(id) 
                                        where nivel = 1 
                                        and funcionario.demissao is null;";
            DataTable dt = ExecutaSelect();
            if (dt != null && dt.Rows.Count > 0)
            {
                return Convert.ToInt32(dt.Rows[0]["admins"]);
            }

            return 0;
        }

        internal int Excluir(int id)
        {
            ComandoSQL.Parameters.Clear();
            ComandoSQL.CommandText = @"delete from usuario where id = @id;";
            ComandoSQL.Parameters.AddWithValue("@id", id);

            return ExecutaComando();
        }
    }
}
