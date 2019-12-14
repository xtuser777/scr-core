using scrlib.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace scrlib.DAO
{
    internal class PessoaJuridicaDAO : Banco
    {
        private PessoaJuridica GetObject(DataRow dr)
        {
            return new PessoaJuridica()
            {
                Id = Convert.ToInt32(dr["id"]),
                RazaoSocial = dr["razao_social"].ToString(),
                NomeFantasia = dr["nome_fantasia"].ToString(),
                Cnpj = dr["cnpj"].ToString(),
                Tipo = Convert.ToInt32(dr["tipo"]),
                Telefone = dr["telefone"].ToString(),
                Celular = dr["celular"].ToString(),
                Email = dr["email"].ToString(),
                Endereco = Convert.ToInt32(dr["endereco"])
            };
        }

        internal PessoaJuridica GetById(int id)
        {
            ComandoSQL.Parameters.Clear();
            ComandoSQL.CommandText = @"select * from pessoa_juridica where id = @id;";
            ComandoSQL.Parameters.AddWithValue("@id", id);
            DataTable dt = ExecutaSelect();
            if (dt != null && dt.Rows.Count > 0)
            {
                return GetObject(dt.Rows[0]);
            }

            return null;
        }

        internal int Gravar(PessoaJuridica p)
        {
            ComandoSQL.Parameters.Clear();
            ComandoSQL.CommandText = @"insert into pessoa_juridica(id,razao_social,nome_fantasia,cnpj,tipo,telefone,celular,email,endereco) 
            values((select count(id)+1 from pessoa),@razao_social,@nome_fantasia,@cnpj,@tipo,@telefone,@celular,@email,@endereco) returning id;";
            ComandoSQL.Parameters.AddWithValue("@razao_social", p.RazaoSocial);
            ComandoSQL.Parameters.AddWithValue("@nome_fantasia", p.NomeFantasia);
            ComandoSQL.Parameters.AddWithValue("@cnpj", p.Cnpj);
            ComandoSQL.Parameters.AddWithValue("@tipo", p.Tipo);
            ComandoSQL.Parameters.AddWithValue("@telefone", p.Telefone);
            ComandoSQL.Parameters.AddWithValue("@celular", p.Celular);
            ComandoSQL.Parameters.AddWithValue("@email", p.Email);
            ComandoSQL.Parameters.AddWithValue("@endereco", p.Endereco);
            DataTable dt = ExecutaSelect();
            if (dt != null && dt.Rows.Count > 0)
            {
                return Convert.ToInt32(dt.Rows[0]["id"]);
            }

            return -1;
        }

        internal int Alterar(PessoaJuridica p)
        {
            ComandoSQL.Parameters.Clear();
            ComandoSQL.CommandText = @"update pessoa_juridica 
                                        set razao_social = @razao_social,
                                            nome_fantasia = @nome_fantasia,
                                            cnpj = @cnpj,
                                            tipo = @tipo,
                                            telefone = @telefone,
                                            celular = @celular,
                                            email = @email,
                                            endereco = @endereco
                                        where id = @id;";
            ComandoSQL.Parameters.AddWithValue("@razao_social", p.RazaoSocial);
            ComandoSQL.Parameters.AddWithValue("@nome_fantasia", p.NomeFantasia);
            ComandoSQL.Parameters.AddWithValue("@cnpj", p.Cnpj);
            ComandoSQL.Parameters.AddWithValue("@tipo", p.Tipo);
            ComandoSQL.Parameters.AddWithValue("@telefone", p.Telefone);
            ComandoSQL.Parameters.AddWithValue("@celular", p.Celular);
            ComandoSQL.Parameters.AddWithValue("@email", p.Email);
            ComandoSQL.Parameters.AddWithValue("@endereco", p.Endereco);
            ComandoSQL.Parameters.AddWithValue("@id", p.Id);

            return ExecutaComando();
        }

        internal int Excluir(int id)
        {
            ComandoSQL.Parameters.Clear();
            ComandoSQL.CommandText = @"delete from pessoa_juridica where id = @id;";
            ComandoSQL.Parameters.AddWithValue("@id", id);

            return ExecutaComando();
        }
    }
}
