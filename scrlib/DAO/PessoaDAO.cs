using scrlib.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace scrlib.DAO
{
    internal class PessoaDAO : Banco
    {
        private PessoaFisica GetObjectPF(DataRow dr)
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

        private PessoaJuridica GetObjectPJ(DataRow dr)
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

        private List<Pessoa> GetList(DataTable dt)
        {
            List<Pessoa> pessoas = new List<Pessoa>();
            foreach (DataRow row in dt.Rows)
            {
                if (Convert.ToInt32(row["tipo"]) == 1)
                {
                    pessoas.Add(GetObjectPF(row));
                }
                else
                {
                    pessoas.Add(GetObjectPJ(row));
                }
            }

            return pessoas;
        }

        internal List<Pessoa> Get()
        {
            ComandoSQL.Parameters.Clear();
            ComandoSQL.CommandText = @"select * from pessoa;";
            DataTable dt = ExecutaSelect();
            if (dt != null && dt.Rows.Count > 0)
            {
                return GetList(dt);
            }

            return null;
        }
    }
}
