using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using scrweb.Models;

namespace scrweb.DAO
{
    internal class ClienteDAO : Banco
    {
        private Cliente GetObject(DataRow row)
        {
            return new Cliente()
            {
                Id = Convert.ToInt32(row["cli_id"]),
                Cadastro = Convert.ToDateTime(row["cli_cadastro"]),
                Tipo = Convert.ToInt32(row["cli_tipo"]),
                PessoaFisica = Convert.ToInt32(row["cli_tipo"]) == 2 ? null : new PessoaFisica().GetById(Convert.ToInt32(row["cli_pessoa_fisica"])),
                PessoaJuridica = Convert.ToInt32(row["cli_tipo"]) == 1 ? null : new PessoaJuridica().GetById(Convert.ToInt32(row["cli_pessoa_juridica"]))
            };
        }

        private List<Cliente> GetList(DataTable dt)
        {
            return (from DataRow row in dt.Rows select GetObject(row)).ToList();
        }

        internal Cliente GetById(int id)
        {
            ComandoSQL.Parameters.Clear();
            ComandoSQL.CommandText = @"
                select 
                    cl.id as cli_id, cl.cadastro as cli_cadastro, cl.tipo as cli_tipo, cl.pessoa_fisica as cli_pessoa_fisica, cl.pessoa_juridica as cli_pessoa_juridica
                from
                    cliente cl
                where 
                    cl.id = @id;
            ";
            ComandoSQL.Parameters.AddWithValue("@id", id);
            
            DataTable dt = ExecutaSelect();

            return dt != null && dt.Rows.Count > 0 ? GetObject(dt.Rows[0]) : null;
        }

        internal List<Cliente> GetAll()
        {
            ComandoSQL.Parameters.Clear();
            ComandoSQL.CommandText = @"
                select 
                    cl.id as cli_id, cl.cadastro as cli_cadastro, cl.tipo as cli_tipo, cl.pessoa_fisica as cli_pessoa_fisica, cl.pessoa_juridica as cli_pessoa_juridica
                from 
                    cliente cl;
            ";

            DataTable dt = ExecutaSelect();
            
            return dt != null && dt.Rows.Count > 0 ? GetList(dt) : null;
        }

        internal int Gravar(Cliente c)
        {
            ComandoSQL.Parameters.Clear();
            if (c.Tipo == 1) ComandoSQL.CommandText = @"
                insert into cliente(cadastro,tipo,pessoa_fisica,pessoa_juridica) 
                values(@cad,@tipo,@pf,0) 
                returning id;
            ";
            else ComandoSQL.CommandText = @"
                insert into cliente(cadastro,tipo,pessoa_fisica,pessoa_juridica) 
                values(@cad,@tipo,0,@pj) 
                returning id;
            ";
            ComandoSQL.Parameters.AddWithValue("@cad", c.Cadastro);
            ComandoSQL.Parameters.AddWithValue("@tipo", c.Tipo);
            if (c.Tipo == 1) ComandoSQL.Parameters.AddWithValue("@pf", c.PessoaFisica.Id);
            else ComandoSQL.Parameters.AddWithValue("@pj", c.PessoaJuridica.Id);

            DataTable dt = ExecutaSelect();

            return dt != null && dt.Rows.Count > 0 ? Convert.ToInt32(dt.Rows[0]["id"]) : -10;
        }

        internal int Alterar(Cliente c)
        {
            ComandoSQL.Parameters.Clear();
            ComandoSQL.CommandText = @"
                update cliente 
                set cadastro = @cad, 
                    tipo = @tipo, 
                    pessoa_fisica = @pf,
                    pessoa_juridica = @pj
                where id = @id;
            ";
            ComandoSQL.Parameters.AddWithValue("@cad", c.Cadastro);
            ComandoSQL.Parameters.AddWithValue("@tipo", c.Tipo);
            if (c.Tipo == 1)
            {
                ComandoSQL.Parameters.AddWithValue("@pf", c.PessoaFisica.Id);
                ComandoSQL.Parameters.AddWithValue("@pj", 0);
            }
            else
            {
                ComandoSQL.Parameters.AddWithValue("@pf", 0);
                ComandoSQL.Parameters.AddWithValue("@pj", c.PessoaJuridica.Id);
            }
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