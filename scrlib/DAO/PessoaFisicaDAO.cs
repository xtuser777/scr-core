using scrlib.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace scrlib.DAO
{
    internal class PessoaFisicaDAO : Banco
    {
        private PessoaFisica GetObject(DataRow dr)
        {
            return new PessoaFisica()
            {
                Id = Convert.ToInt32(dr["id"]),
                Tipo = Convert.ToInt32(dr["tipo"]),
                Telefone = dr["telefone"].ToString(),
                Celular = dr["celular"].ToString(),
                Email = dr["email"].ToString(),
                Endereco = Convert.ToInt32(dr["endereco"]),
                Nome = dr["nome"].ToString(),
                Rg = dr["rg"].ToString(),
                Cpf = dr["cpf"].ToString(),
                Nascimento = Convert.ToDateTime(dr["nascimento"])
            };
        }

        internal PessoaFisica GetById(int id)
        {
            ComandoSQL.Parameters.Clear();
            ComandoSQL.CommandText = @"select * from pessoa_fisica where id = @id;";
            ComandoSQL.Parameters.AddWithValue("@id", id);
            
            var dt = ExecutaSelect();
            
            return (dt != null && dt.Rows.Count > 0) ? GetObject(dt.Rows[0]) : null;
        }

        internal int CountCpf(string cpf)
        {
            ComandoSQL.Parameters.Clear();
            ComandoSQL.CommandText = @"select count(id) as cnt from pessoa_fisica where cpf = @cpf;";
            ComandoSQL.Parameters.AddWithValue("@cpf", cpf);

            var dt = ExecutaSelect();

            return dt != null && dt.Rows.Count > 0 ? Convert.ToInt32(dt.Rows[0]["cnt"]) : -10;
        }

        internal int Gravar(PessoaFisica p)
        {
            ComandoSQL.Parameters.Clear();
            ComandoSQL.CommandText = @"insert into pessoa_fisica(nome,rg,cpf,nascimento,tipo,telefone,celular,email,endereco) 
            values (@nome,@rg,@cpf,@nascimento,@tipo,@telefone,@celular,@email,@endereco) returning id;";
            ComandoSQL.Parameters.AddWithValue("@nome", p.Nome);
            ComandoSQL.Parameters.AddWithValue("@rg", p.Rg);
            ComandoSQL.Parameters.AddWithValue("@cpf", p.Cpf);
            ComandoSQL.Parameters.AddWithValue("@nascimento", p.Nascimento);
            ComandoSQL.Parameters.AddWithValue("@tipo", p.Tipo);
            ComandoSQL.Parameters.AddWithValue("@telefone", p.Telefone);
            ComandoSQL.Parameters.AddWithValue("@celular", p.Celular);
            ComandoSQL.Parameters.AddWithValue("@email", p.Email);
            ComandoSQL.Parameters.AddWithValue("@endereco", p.Endereco);
            
            var dt = ExecutaSelect();
            
            return (dt != null && dt.Rows.Count > 0) ? Convert.ToInt32(dt.Rows[0]["id"]) : -1;
        }

        internal int Alterar(PessoaFisica p)
        {
            ComandoSQL.Parameters.Clear();
            ComandoSQL.CommandText = @"update pessoa_fisica 
                                        set nome = @nome,
                                            rg = @rg,
                                            cpf = @cpf,
                                            nascimento = @nascimento,
                                            tipo = @tipo,
                                            telefone = @telefone,
                                            celular = @celular,
                                            email = @email,
                                            endereco = @endereco
                                        where id = @id;";
            ComandoSQL.Parameters.AddWithValue("@nome", p.Nome);
            ComandoSQL.Parameters.AddWithValue("@rg", p.Rg);
            ComandoSQL.Parameters.AddWithValue("@cpf", p.Cpf);
            ComandoSQL.Parameters.AddWithValue("@nascimento", p.Nascimento);
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
            ComandoSQL.CommandText = @"delete from pessoa_fisica where id = @id;";
            ComandoSQL.Parameters.AddWithValue("@id", id);
            
            return ExecutaComando();
        }
    }
}
