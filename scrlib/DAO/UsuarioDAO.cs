﻿using scrlib.Models;
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
            List<Usuario> usuarios = new List<Usuario>();
            foreach (DataRow row in dt.Rows)
            {
                usuarios.Add(GetObject(row));
            }
            return usuarios;
        }

        internal Usuario GetById(int id)
        {
            Usuario usuario = null;
            ComandoSQL.Parameters.Clear();
            ComandoSQL.CommandText = @"select id,login,senha,funcionario,nivel,ativo 
                                        from usuario 
                                        where id = @id;";
            ComandoSQL.Parameters.AddWithValue("@id", id);
            DataTable dt = ExecutaSelect();
            if (dt != null && dt.Rows.Count > 0)
            {
                usuario = GetObject(dt.Rows[0]);
            }
            return usuario;
        }

        internal Usuario Autenticar(string login, string senha)
        {
            Usuario usuario = null;
            ComandoSQL.Parameters.Clear();
            ComandoSQL.CommandText = @"select id,login,senha,funcionario,nivel,ativo 
                                        from usuario 
                                        where login = @login 
                                        and senha = @senha 
                                        and ativo = true;";
            ComandoSQL.Parameters.AddWithValue("@login", login);
            ComandoSQL.Parameters.AddWithValue("@senha", senha);
            DataTable dt = ExecutaSelect();
            if (dt != null && dt.Rows.Count > 0)
            {
                usuario = GetObject(dt.Rows[0]);
            }
            return usuario;
        }

        internal List<Usuario> GetByKey(string chave)
        {
            ComandoSQL.Parameters.Clear();
            ComandoSQL.CommandText = @"select id,login,senha,funcionario,nivel,ativo 
                                        from usuario 
                                        inner join funcionario using (id)
                                        inner join pessoa_fisica using(id)
                                        where usuario.login like @chave 
                                        or pessoa_fisica.nome like @chave1 
                                        or pessoa_fisica.email like @chave2
                                        order by id;";
            ComandoSQL.Parameters.AddWithValue("@chave", "%"+chave+"%");
            ComandoSQL.Parameters.AddWithValue("@chave1", "%"+chave+"%");
            ComandoSQL.Parameters.AddWithValue("@chave2", "%"+chave+"%");
            DataTable dt = ExecutaSelect();
            if (dt != null && dt.Rows.Count > 0)
            {
                return GetList(dt);
            }

            return null;
        }
        
        internal List<Usuario> GetByAdm(DateTime admissao)
        {
            ComandoSQL.Parameters.Clear();
            ComandoSQL.CommandText = @"select id,login,senha,funcionario,nivel,ativo 
                                        from usuario 
                                        inner join funcionario using (id)
                                        where funcionario.admissao = @admissao
                                        order by id;";
            ComandoSQL.Parameters.AddWithValue("@admissao", admissao);
            DataTable dt = ExecutaSelect();
            if (dt != null && dt.Rows.Count > 0)
            {
                return GetList(dt);
            }

            return null;
        }
        
        internal List<Usuario> GetByKeyAndAdm(string chave, DateTime admissao)
        {
            ComandoSQL.Parameters.Clear();
            ComandoSQL.CommandText = @"select id,login,senha,funcionario,nivel,ativo 
                                        from usuario 
                                        inner join funcionario using (id)
                                        inner join pessoa_fisica using(id)
                                        where (usuario.login like @chave 
                                        or pessoa_fisica.nome like @chave1 
                                        or pessoa_fisica.email like @chave2)
                                        and funcionario.admissao = @admissao
                                        order by id;";
            ComandoSQL.Parameters.AddWithValue("@chave", "%"+chave+"%");
            ComandoSQL.Parameters.AddWithValue("@chave1", "%"+chave+"%");
            ComandoSQL.Parameters.AddWithValue("@chave2", "%"+chave+"%");
            ComandoSQL.Parameters.AddWithValue("@admissao", admissao);
            DataTable dt = ExecutaSelect();
            if (dt != null && dt.Rows.Count > 0)
            {
                return GetList(dt);
            }

            return null;
        }

        internal List<Usuario> Get()
        {
            List<Usuario> usuarios = null;
            ComandoSQL.Parameters.Clear();
            ComandoSQL.CommandText = @"select id,login,senha,funcionario,nivel,ativo 
                                        from usuario
                                        order by id;";
            DataTable dt = ExecutaSelect();
            if (dt != null && dt.Rows.Count > 0)
            {
                usuarios = GetList(dt);
            }
            return usuarios;
        }

        internal int Gravar(Usuario u)
        {
            int res = -1;
            ComandoSQL.Parameters.Clear();
            ComandoSQL.CommandText = @"insert into usuario(id,login,senha,funcionario,nivel,ativo) 
                                        values((select count(id)+1 from usuario),@login,@senha,@funcionario,@nivel,@ativo) returning id;";
            ComandoSQL.Parameters.AddWithValue("@login", u.Login);
            ComandoSQL.Parameters.AddWithValue("@senha", u.Senha);
            ComandoSQL.Parameters.AddWithValue("@funcionario", u.Funcionario);
            ComandoSQL.Parameters.AddWithValue("@nivel", u.Nivel);
            ComandoSQL.Parameters.AddWithValue("@ativo", u.Ativo);
            DataTable dt = ExecutaSelect();
            if (dt != null && dt.Rows.Count > 0)
            {
                res = Convert.ToInt32(dt.Rows[0]["id"]);
            }
            return res;
        }

        internal int Alterar(Usuario u)
        {
            int res = -1;
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
            res = ExecutaComando();
            return res;
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
    }
}
