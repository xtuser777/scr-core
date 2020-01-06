using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using scrlib.Models;

namespace scrlib.DAO
{
    internal class ParametrizacaoDAO : Banco
    {
        private Parametrizacao GetObject(DataRow dr)
        {
            return new Parametrizacao()
            {
                Id = Convert.ToInt32(dr["id"]),
                RazaoSocial = dr["razao_social"].ToString(),
                NomeFantasia = dr["nome_fantasia"].ToString(),
                Cnpj = dr["cnpj"].ToString(),
                Rua = dr["rua"].ToString(),
                Numero = dr["numero"].ToString(),
                Bairro = dr["bairro"].ToString(),
                Complemento = dr["complemento"].ToString(),
                Cep = dr["cep"].ToString(),
                Cidade = Convert.ToInt32(dr["cidade"]),
                Telefone = dr["telefone"].ToString(),
                Celular = dr["celular"].ToString(),
                Email = dr["email"].ToString(),
                Logotipo = dr["logotipo"].ToString()
            };
        }

        internal Parametrizacao Get()
        {
            ComandoSQL.Parameters.Clear();
            ComandoSQL.CommandText = @"select * from parametrizacao;";
            
            var dt = ExecutaSelect();
            if (dt != null && dt.Rows.Count > 0)
            {
                return GetObject(dt.Rows[0]);
            }

            return null;
        }

        internal int Gravar(Parametrizacao p)
        {
            ComandoSQL.Parameters.Clear();
            ComandoSQL.CommandText = @"
            insert into parametrizacao (id,razao_social,nome_fantasia,cnpj,rua,numero,bairro,complemento,cep,cidade,telefone,celular,email,logotipo) 
            values (1,@rs,@nf,@cnpj,@rua,@num,@bairro,@comp,@cep,@cidade,@tel,@cel,@email,@logo);";
            ComandoSQL.Parameters.AddWithValue("@rs", p.RazaoSocial);
            ComandoSQL.Parameters.AddWithValue("@nf", p.NomeFantasia);
            ComandoSQL.Parameters.AddWithValue("@cnpj", p.Cnpj);
            ComandoSQL.Parameters.AddWithValue("@rua", p.Rua);
            ComandoSQL.Parameters.AddWithValue("@num", p.Numero);
            ComandoSQL.Parameters.AddWithValue("@bairro", p.Bairro);
            ComandoSQL.Parameters.AddWithValue("@comp", p.Complemento);
            ComandoSQL.Parameters.AddWithValue("@cep", p.Cep);
            ComandoSQL.Parameters.AddWithValue("@cidade", p.Cidade);
            ComandoSQL.Parameters.AddWithValue("@tel", p.Telefone);
            ComandoSQL.Parameters.AddWithValue("@cel", p.Celular);
            ComandoSQL.Parameters.AddWithValue("@email", p.Email);
            ComandoSQL.Parameters.AddWithValue("@logo", p.Logotipo);

            return ExecutaComando();
        }

        internal int Alterar(Parametrizacao p)
        {
            ComandoSQL.Parameters.Clear();
            ComandoSQL.CommandText = @"
            update parametrizacao 
            set razao_social = @rs,
            nome_fantasia = @nf,
            cnpj = @cnpj,
            rua = @rua,
            numero = @num,
            bairro = @bairro,
            complemento = @comp,
            cep = @cep,
            cidade = @cidade,
            telefone = @tel,
            celular = @cel,
            email = @email,
            logotipo = @logo
            where id = @id;";
            ComandoSQL.Parameters.AddWithValue("@rs", p.RazaoSocial);
            ComandoSQL.Parameters.AddWithValue("@nf", p.NomeFantasia);
            ComandoSQL.Parameters.AddWithValue("@cnpj", p.Cnpj);
            ComandoSQL.Parameters.AddWithValue("@rua", p.Rua);
            ComandoSQL.Parameters.AddWithValue("@num", p.Numero);
            ComandoSQL.Parameters.AddWithValue("@bairro", p.Bairro);
            ComandoSQL.Parameters.AddWithValue("@comp", p.Complemento);
            ComandoSQL.Parameters.AddWithValue("@cep", p.Cep);
            ComandoSQL.Parameters.AddWithValue("@cidade", p.Cidade);
            ComandoSQL.Parameters.AddWithValue("@tel", p.Telefone);
            ComandoSQL.Parameters.AddWithValue("@cel", p.Celular);
            ComandoSQL.Parameters.AddWithValue("@email", p.Email);
            ComandoSQL.Parameters.AddWithValue("@logo", p.Logotipo);
            ComandoSQL.Parameters.AddWithValue("@id", p.Id);

            return ExecutaComando();
        }
    }
}