using System;
using System.Data;
using scrweb.Models;

namespace scrweb.DAO
{
    internal class ParametrizacaoDAO : Banco
    {
        private Parametrizacao GetObject(DataRow dr)
        {
            return new Parametrizacao()
            {
                Id = Convert.ToInt32(dr["par_id"]),
                RazaoSocial = dr["par_razao_social"].ToString(),
                NomeFantasia = dr["par_nome_fantasia"].ToString(),
                Cnpj = dr["par_cnpj"].ToString(),
                Rua = dr["par_rua"].ToString(),
                Numero = dr["par_numero"].ToString(),
                Bairro = dr["par_bairro"].ToString(),
                Complemento = dr["par_complemento"].ToString(),
                Cep = dr["par_cep"].ToString(),
                Telefone = dr["par_telefone"].ToString(),
                Celular = dr["par_celular"].ToString(),
                Email = dr["par_email"].ToString(),
                Logotipo = dr["par_logotipo"].ToString(),
                Cidade = new Cidade()
                {
                    Id = Convert.ToInt32(dr["cid_id"]),
                    Nome = dr["cid_nome"].ToString(),
                    Estado = new Estado()
                    {
                        Id = Convert.ToInt32(dr["est_id"]),
                        Nome = dr["est_nome"].ToString(),
                        Sigla = dr["est_sigla"].ToString()
                    }
                }
            };
        }

        internal Parametrizacao Get()
        {
            ComandoSQL.Parameters.Clear();
            ComandoSQL.CommandText = @"
                select e.id as est_id, e.nome as est_nome, e.sigla as est_sigla,
                       c.id as cid_id, c.nome as cid_nome, c.estado as cid_estado,
                       p.id as par_id, p.razao_social as par_razao_social, p.nome_fantasia as par_nome_fantasia, p.cnpj as par_cnpj, p.rua as par_rua, p.numero as par_numero, p.bairro as par_bairro, p.complemento as par_complemento, p.cep as par_cep, p.cidade as par_cidade, p.telefone as par_telefone, p.celular as par_celular, p.email as par_email, p.logotipo as par_logotipo
                from estado e, cidade c, parametrizacao p
                where p.id = 1
                and c.id = p.cidade
                and e.id = c.estado;
            ";
            
            DataTable dt = ExecutaSelect();
            
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
                insert into parametrizacao (id,razao_social,nome_fantasia,cnpj,rua,numero,bairro,complemento,cep,cidade,
                                            telefone,celular,email,logotipo) 
                values (1,@rs,@nf,@cnpj,@rua,@num,@bairro,@comp,@cep,@cidade,@tel,@cel,@email,@logo);
            ";
            ComandoSQL.Parameters.AddWithValue("@rs", p.RazaoSocial);
            ComandoSQL.Parameters.AddWithValue("@nf", p.NomeFantasia);
            ComandoSQL.Parameters.AddWithValue("@cnpj", p.Cnpj);
            ComandoSQL.Parameters.AddWithValue("@rua", p.Rua);
            ComandoSQL.Parameters.AddWithValue("@num", p.Numero);
            ComandoSQL.Parameters.AddWithValue("@bairro", p.Bairro);
            ComandoSQL.Parameters.AddWithValue("@comp", p.Complemento);
            ComandoSQL.Parameters.AddWithValue("@cep", p.Cep);
            ComandoSQL.Parameters.AddWithValue("@cidade", p.Cidade.Id);
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
            ComandoSQL.Parameters.AddWithValue("@cidade", p.Cidade.Id);
            ComandoSQL.Parameters.AddWithValue("@tel", p.Telefone);
            ComandoSQL.Parameters.AddWithValue("@cel", p.Celular);
            ComandoSQL.Parameters.AddWithValue("@email", p.Email);
            ComandoSQL.Parameters.AddWithValue("@logo", p.Logotipo);
            ComandoSQL.Parameters.AddWithValue("@id", p.Id);

            return ExecutaComando();
        }
    }
}