using scrweb.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace scrweb.DAO
{
    internal class PessoaJuridicaDAO : Banco
    {
        private PessoaJuridica GetObject(DataRow row)
        {
            return new PessoaJuridica()
            {
                Id = Convert.ToInt32(row["pes_id"]),
                RazaoSocial = row["pes_razao_social"].ToString(),
                NomeFantasia = row["pes_nome_fantasia"].ToString(),
                Cnpj = row["pes_cnpj"].ToString(),
                Contato = new Contato()
                {
                    Id = Convert.ToInt32(row["ctt_id"]),
                    Telefone = row["ctt_telefone"].ToString(),
                    Celular = row["ctt_celular"].ToString(),
                    Email = row["ctt_email"].ToString(),
                    Endereco = new Endereco()
                    {
                        Id = Convert.ToInt32(row["end_id"]),
                        Rua = row["end_rua"].ToString(),
                        Numero = row["end_numero"].ToString(),
                        Bairro = row["end_bairro"].ToString(),
                        Complemento = row["end_complemento"].ToString(),
                        Cep = row["end_cep"].ToString(),
                        Cidade = new Cidade()
                        {
                            Id = Convert.ToInt32(row["cid_id"]),
                            Nome = row["cid_nome"].ToString(),
                            Estado = new Estado()
                            {
                                Id = Convert.ToInt32(row["est_id"]),
                                Nome = row["est_nome"].ToString(),
                                Sigla = row["est_sigla"].ToString()
                            }
                        }
                    }
                }
            };
        }

        internal PessoaJuridica GetById(int id)
        {
            ComandoSQL.Parameters.Clear();
            ComandoSQL.CommandText = @"
                select e.id as est_id, e.nome as est_nome, e.sigla as est_sigla,
                       c.id as cid_id, c.nome as cid_nome, c.estado as cid_estado,
                       en.id as end_id, en.rua as end_rua, en.numero as end_numero, en.bairro as end_bairro, en.complemento as end_complemento, en.cep as end_cep, en.cidade as end_cidade,
                       ct.id as ctt_id, ct.telefone as ctt_telefone, ct.celular as ctt_celular, ct.email as ctt_email, ct.endereco as ctt_endereco,
                       p.id as pes_id, p.razao_social as pes_razao_social, p.nome_fantasia as pes_nome_fantasia, p.cnpj as pes_cnpj, p.contato as pes_contato
                from estado e, cidade c, endereco en, contato ct, pessoa_juridica p
                where p.id = @id
                and ct.id = p.contato
                and en.id = ct.endereco
                and c.id = en.cidade
                and e.id = c.estado;
            ";
            ComandoSQL.Parameters.AddWithValue("@id", id);
            
            DataTable dt = ExecutaSelect(); 
            
            return (dt != null && dt.Rows.Count > 0) ? GetObject(dt.Rows[0]) : null;
        }

        internal int CountCnpj(string cnpj)
        {
            ComandoSQL.Parameters.Clear();
            ComandoSQL.CommandText = @"select count(id) as cnt from pessoa_juridica where cnpj = @cnpj;";
            ComandoSQL.Parameters.AddWithValue("@cnpj", cnpj);

            var dt = ExecutaSelect();

            return dt != null && dt.Rows.Count > 0 ? Convert.ToInt32(dt.Rows[0]["cnt"]) : -10;
        }

        internal int Gravar(PessoaJuridica p)
        {
            ComandoSQL.Parameters.Clear();
            ComandoSQL.CommandText = @"
                insert into pessoa_juridica(razao_social,nome_fantasia,cnpj,contato) 
                values(@razao_social,@nome_fantasia,@cnpj,@contato) 
                returning id;
            ";
            ComandoSQL.Parameters.AddWithValue("@razao_social", p.RazaoSocial);
            ComandoSQL.Parameters.AddWithValue("@nome_fantasia", p.NomeFantasia);
            ComandoSQL.Parameters.AddWithValue("@cnpj", p.Cnpj);
            ComandoSQL.Parameters.AddWithValue("@contato", p.Contato.Id);
            
            DataTable dt = ExecutaSelect();
            
            return (dt != null && dt.Rows.Count > 0) ? Convert.ToInt32(dt.Rows[0]["id"]) : -10;
        }

        internal int Alterar(PessoaJuridica p)
        {
            ComandoSQL.Parameters.Clear();
            ComandoSQL.CommandText = @"
                update pessoa_juridica 
                set razao_social = @razao_social,
                nome_fantasia = @nome_fantasia,
                cnpj = @cnpj,
                contato = @contato
                where id = @id;
            ";
            ComandoSQL.Parameters.AddWithValue("@razao_social", p.RazaoSocial);
            ComandoSQL.Parameters.AddWithValue("@nome_fantasia", p.NomeFantasia);
            ComandoSQL.Parameters.AddWithValue("@cnpj", p.Cnpj);
            ComandoSQL.Parameters.AddWithValue("@contato", p.Contato.Id);
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
