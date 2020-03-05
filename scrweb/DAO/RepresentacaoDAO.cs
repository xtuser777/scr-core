using System;
using System.Collections.Generic;
using System.Data;
using scrweb.Models;

namespace scrweb.DAO
{
    internal class RepresentacaoDAO : Banco
    {
        private Representacao GetObject(DataRow row)
        {
            return new Representacao()
            {
                Id = Convert.ToInt32(row["rep_id"]),
                Cadastro = Convert.ToDateTime(row["rep_cadastro"]),
                Unidade = row["rep_unidade"].ToString(),
                Pessoa = new PessoaJuridica()
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
                }
            };
        }

        private List<Representacao> GetList(DataTable table)
        {
            List<Representacao> list = new List<Representacao>();

            for (int i = 0; i < table.Rows.Count; i++)
            {
                DataRow row = table.Rows[i];
                list.Add(GetObject(row));
            }

            return list;
        }

        internal Representacao GetById(int id)
        {
            ComandoSQL.Parameters.Clear();
            ComandoSQL.CommandText = @"
                select e.id as est_id, e.nome as est_nome, e.sigla as est_sigla,
                       c.id as cid_id, c.nome as cid_nome, c.estado as cid_estado,
                       en.id as end_id, en.rua as end_rua, en.numero as end_numero, en.bairro as end_bairro, en.complemento as end_complemento, en.cep as end_cep, en.cidade as end_cidade,
                       ct.id as ctt_id, ct.telefone as ctt_telefone, ct.celular as ctt_celular, ct.email as ctt_email, ct.endereco as ctt_endereco,
                       p.id as pes_id, p.razao_social as pes_razao_social, p.nome_fantasia as pes_nome_fantasia, p.cnpj as pes_cnpj, p.contato as pes_contato,
                       r.id as rep_id, r.cadastro as rep_cadastro, r.unidade as rep_unidade, r.pessoa as rep_pessoa
                       from estado e, cidade c, endereco en, contato ct, pessoa_juridica p, representacao r
                where r.id = @id
                and p.id = r.pessoa
                and ct.id = p.contato
                and en.id = ct.endereco
                and c.id = en.cidade
                and e.id = c.estado;
            ";
            ComandoSQL.Parameters.AddWithValue("@id", id);

            DataTable tables = ExecutaSelect();

            return tables != null && tables.Rows.Count > 0 ? GetObject(tables.Rows[0]) : null;
        }

        internal List<Representacao> GetAll()
        {
            ComandoSQL.Parameters.Clear();
            ComandoSQL.CommandText = @"
                select e.id as est_id, e.nome as est_nome, e.sigla as est_sigla,
                       c.id as cid_id, c.nome as cid_nome, c.estado as cid_estado,
                       en.id as end_id, en.rua as end_rua, en.numero as end_numero, en.bairro as end_bairro, en.complemento as end_complemento, en.cep as end_cep, en.cidade as end_cidade,
                       ct.id as ctt_id, ct.telefone as ctt_telefone, ct.celular as ctt_celular, ct.email as ctt_email, ct.endereco as ctt_endereco,
                       p.id as pes_id, p.razao_social as pes_razao_social, p.nome_fantasia as pes_nome_fantasia, p.cnpj as pes_cnpj, p.contato as pes_contato,
                       r.id as rep_id, r.cadastro as rep_cadastro, r.unidade as rep_unidade, r.pessoa as rep_pessoa
                       from estado e, cidade c, endereco en, contato ct, pessoa_juridica p, representacao r
                where p.id = r.pessoa
                and ct.id = p.contato
                and en.id = ct.endereco
                and c.id = en.cidade
                and e.id = c.estado;
            ";

            DataTable tables = ExecutaSelect();

            return tables != null && tables.Rows.Count > 0 ? GetList(tables) : null;
        }

        internal int Gravar(Representacao r)
        {
            ComandoSQL.Parameters.Clear();
            ComandoSQL.CommandText = @"
                insert into representacao(cadastro,unidade,pessoa) 
                values(@cad,@uni,@pes) 
                returning id;
            ";
            ComandoSQL.Parameters.AddWithValue("@cad", r.Cadastro);
            ComandoSQL.Parameters.AddWithValue("@uni", r.Unidade);
            ComandoSQL.Parameters.AddWithValue("@pes", r.Pessoa.Id);

            var table = ExecutaSelect();

            return table != null && table.Rows.Count > 0 ? Convert.ToInt32(table.Rows[0]["id"]) : -10;
        }

        internal int Alterar(Representacao r)
        {
            ComandoSQL.Parameters.Clear();
            ComandoSQL.CommandText = @"
                update representacao
                set cadastro = @cad,
                unidade = @uni,
                pessoa = @pes
                where id = @id;
            ";
            ComandoSQL.Parameters.AddWithValue("@cad", r.Cadastro);
            ComandoSQL.Parameters.AddWithValue("@uni", r.Unidade);
            ComandoSQL.Parameters.AddWithValue("@pes", r.Pessoa.Id);
            ComandoSQL.Parameters.AddWithValue("@id", r.Id);

            return ExecutaComando();
        }

        internal int Excluir(int id)
        {
            ComandoSQL.Parameters.Clear();
            ComandoSQL.CommandText = @"delete from representacao where id = @id;";
            ComandoSQL.Parameters.AddWithValue("@id", id);

            return ExecutaComando();
        }
    }
}