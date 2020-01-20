using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using scrlib.Models;

namespace scrlib.DAO
{
    internal class ClienteDAO : Banco
    {
        private Cliente GetObject(DataRow dr)
        {
            return new Cliente()
            {
                Id = Convert.ToInt32(dr["id"]),
                Cadastro = Convert.ToDateTime(dr["cadastro"]),
                Tipo = Convert.ToInt32(dr["tipo"]),
                Pessoa = Convert.ToInt32(dr["tipo"]) == 1 ? Convert.ToInt32(dr["pessoa_fisica"]) : Convert.ToInt32(dr["pessoa_juridica"])
            };
        }

        private List<Cliente> GetList(DataTable dt)
        {
            return (from DataRow row in dt.Rows select GetObject(row)).ToList();
        }

        internal Cliente GetById(int id)
        {
            ComandoSQL.Parameters.Clear();
            ComandoSQL.CommandText = @"select id,cadastro,tipo,pessoa_fisica,pessoa_juridica from cliente where id = @id;";
            ComandoSQL.Parameters.AddWithValue("@id", id);
            
            var dt = ExecutaSelect();

            return dt != null && dt.Rows.Count > 0 ? GetObject(dt.Rows[0]) : null;
        }

        internal List<Cliente> GetByFilter(string chave)
        {
            ComandoSQL.Parameters.Clear();
            ComandoSQL.CommandText = @"select c.id,c.cadastro,c.tipo,c.pessoa_fisica,c.pessoa_juridica 
                                        from cliente c
                                        inner join pessoa using (id)
                                        inner join pessoa_fisica using (id)
                                        inner join pessoa_juridica using (id)
                                        where pessoa_fisica.nome like @chave 
                                        or pessoa_juridica.nome_fantasia like @chave
                                        or pessoa.email like @chave;";
            ComandoSQL.Parameters.AddWithValue("@chave", "%"+chave+"%");

            var dt = ExecutaSelect();

            return dt != null && dt.Rows.Count > 0 ? GetList(dt) : null;
        }

        internal List<Cliente> GetByCad(DateTime cadastro)
        {
            ComandoSQL.Parameters.Clear();
            ComandoSQL.CommandText = @"select id,cadastro,tipo,pessoa_fisica,pessoa_juridica 
                                        from cliente 
                                        where cliente.cadastro = @cad;";
            ComandoSQL.Parameters.AddWithValue("@cad", cadastro);

            var dt = ExecutaSelect();

            return dt != null && dt.Rows.Count > 0 ? GetList(dt) : null;
        }

        internal List<Cliente> GetByFilterAndCad(string chave, DateTime cadastro)
        {
            ComandoSQL.Parameters.Clear();
            ComandoSQL.CommandText = @"select id,cadastro,tipo,pessoa_fisica,pessoa_juridica 
                                        from cliente 
                                        inner join pessoa using (id)
                                        inner join pessoa_fisica using (id)
                                        inner join pessoa_juridica using (id)
                                        where (pessoa_fisica.nome like @chave 
                                        or pessoa_juridica.nome_fantasia like @chave
                                        or pessoa.email like @chave)
                                        and cliente.cadastro = @cad;";
            ComandoSQL.Parameters.AddWithValue("@chave", "%"+chave+"%");
            ComandoSQL.Parameters.AddWithValue("@cad", cadastro);

            var dt = ExecutaSelect();

            return dt != null && dt.Rows.Count > 0 ? GetList(dt) : null;
        }

        internal List<Cliente> GetAll()
        {
            ComandoSQL.Parameters.Clear();
            ComandoSQL.CommandText = @"select id,cadastro,tipo,pessoa_fisica,pessoa_juridica from cliente;";

            var dt = ExecutaSelect();
            
            return dt != null && dt.Rows.Count > 0 ? GetList(dt) : null;
        }

        internal int Gravar(Cliente c)
        {
            ComandoSQL.Parameters.Clear();
            if (c.Tipo == 1) ComandoSQL.CommandText = @"insert into cliente(cadastro,tipo,pessoa_fisica) 
            values(@cad,@tipo,@pessoa) returning id;";
            else ComandoSQL.CommandText = @"insert into cliente(cadastro,tipo,pessoa_juridica) 
            values(@cad,@tipo,@pessoa) returning id;" ;
            ComandoSQL.Parameters.AddWithValue("@cad", c.Cadastro);
            ComandoSQL.Parameters.AddWithValue("@tipo", c.Tipo);
            ComandoSQL.Parameters.AddWithValue("@pessoa", c.Pessoa);

            var dt = ExecutaSelect();

            return dt != null && dt.Rows.Count > 0 ? Convert.ToInt32(dt.Rows[0]["id"]) : -10;
        }

        internal int Alterar(Cliente c)
        {
            ComandoSQL.Parameters.Clear();
            if (c.Tipo == 1) ComandoSQL.CommandText = @"update cliente 
            set cadastro = @cad, tipo = @tipo, pessoa_fisica = @pessoa where id = @id;";
            else ComandoSQL.CommandText = @"update cliente 
            set cadastro = @cad, tipo = @tipo, pessoa_juridica = @pessoa where id = @id;";
            ComandoSQL.Parameters.AddWithValue("@cad", c.Cadastro);
            ComandoSQL.Parameters.AddWithValue("@tipo", c.Tipo);
            ComandoSQL.Parameters.AddWithValue("@pessoa", c.Pessoa);
            ComandoSQL.Parameters.AddWithValue("@id", c.Id);

            return ExecutaComando();
        }

        internal int Excluir(int id)
        {
            ComandoSQL.Parameters.Clear();
            ComandoSQL.CommandText = @"delete from cliente where id = @id;";
            ComandoSQL.Parameters.AddWithValue("@id", id);

            return ExecutaComando();
        }
    }
}