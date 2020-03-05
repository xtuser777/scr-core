using System;
using System.Collections.Generic;
using System.Data;
using scrweb.Models;

namespace scrweb.DAO
{
    internal class ProdutoDAO : Banco
    {
        private Produto GetObject(DataRow row)
        {
            return new Produto()
            {
                Id = Convert.ToInt32(row["pro_id"]),
                Descricao = row["pro_descricao"].ToString(),
                Medida = row["pro_medida"].ToString(),
                Preco = Convert.ToDecimal(row["pro_preco"]),
                PrecoOut = Convert.ToDecimal(row["pro_preco_out"]),
                Representacao = new Representacao()
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
                }
            };
        }

        private List<Produto> GetList(DataTable table)
        {
            List<Produto> list = new List<Produto>();

            for (int i = 0; i < table.Rows.Count; i++)
            {
                DataRow row = table.Rows[i];
                list.Add(GetObject(row));
            }

            return list;
        }

        internal Produto GetById(int id)
        {
            ComandoSQL.Parameters.Clear();
            ComandoSQL.CommandText = @"
                select e.id as est_id, e.nome as est_nome, e.sigla as est_sigla,
                       c.id as cid_id, c.nome as cid_nome, c.estado as cid_estado,
                       en.id as end_id, en.rua as end_rua, en.numero as end_numero, en.bairro as end_bairro, en.complemento as end_complemento, en.cep as end_cep, en.cidade as end_cidade,
                       ct.id as ctt_id, ct.telefone as ctt_telefone, ct.celular as ctt_celular, ct.email as ctt_email, ct.endereco as ctt_endereco,
                       pj.id as pes_id, pj.razao_social as pes_razao_social, pj.nome_fantasia as pes_nome_fantasia, pj.cnpj as pes_cnpj, pj.contato as pes_contato,
                       r.id as rep_id, r.cadastro as rep_cadastro, r.unidade as rep_unidade, r.pessoa as rep_pessoa,
                       p.id as pro_id, p.descricao as pro_descricao, p.medida as pro_medida, p.preco as pro_preco, p.preco_out as pro_preco_out, p.representacao as pro_representacao
                       from estado e, cidade c, endereco en, contato ct, pessoa_juridica pj, representacao r, produto p
                where p.id = @id
                and r.id = p.representacao
                and pj.id = r.pessoa
                and ct.id = pj.contato
                and en.id = ct.endereco
                and c.id = en.cidade
                and e.id = c.estado;
            ";
            ComandoSQL.Parameters.AddWithValue("@id", id);

            DataTable table = ExecutaSelect();

            return table != null && table.Rows.Count > 0 ? GetObject(table.Rows[0]) : null;
        }

        internal List<Produto> GetAll()
        {
            ComandoSQL.Parameters.Clear();
            ComandoSQL.CommandText = @"
                select e.id as est_id, e.nome as est_nome, e.sigla as est_sigla,
                       c.id as cid_id, c.nome as cid_nome, c.estado as cid_estado,
                       en.id as end_id, en.rua as end_rua, en.numero as end_numero, en.bairro as end_bairro, en.complemento as end_complemento, en.cep as end_cep, en.cidade as end_cidade,
                       ct.id as ctt_id, ct.telefone as ctt_telefone, ct.celular as ctt_celular, ct.email as ctt_email, ct.endereco as ctt_endereco,
                       pj.id as pes_id, pj.razao_social as pes_razao_social, pj.nome_fantasia as pes_nome_fantasia, pj.cnpj as pes_cnpj, pj.contato as pes_contato,
                       r.id as rep_id, r.cadastro as rep_cadastro, r.unidade as rep_unidade, r.pessoa as rep_pessoa,
                       p.id as pro_id, p.descricao as pro_descricao, p.medida as pro_medida, p.preco as pro_preco, p.preco_out as pro_preco_out, p.representacao as pro_representacao
                       from estado e, cidade c, endereco en, contato ct, pessoa_juridica pj, representacao r, produto p
                where r.id = p.representacao
                and pj.id = r.pessoa
                and ct.id = pj.contato
                and en.id = ct.endereco
                and c.id = en.cidade
                and e.id = c.estado;
            ";

            DataTable table = ExecutaSelect();

            return table != null && table.Rows.Count > 0 ? GetList(table) : null;
        }

        internal int Gravar(Produto p)
        {
            ComandoSQL.Parameters.Clear();
            ComandoSQL.CommandText = @"
                insert into produto(descricao,medida,preco,preco_out,representacao)
                values(@des,@med,@pre,@po,@rep) 
                returning id;
            ";
            ComandoSQL.Parameters.AddWithValue("@des", p.Descricao);
            ComandoSQL.Parameters.AddWithValue("@med", p.Medida);
            ComandoSQL.Parameters.AddWithValue("@pre", p.Preco);
            ComandoSQL.Parameters.AddWithValue("@po", p.PrecoOut);
            ComandoSQL.Parameters.AddWithValue("@rep", p.Representacao.Id);

            DataTable table = ExecutaSelect();
            
            return table != null && table.Rows.Count > 0 ? Convert.ToInt32(table.Rows[0]["id"]) : -10;
        }

        internal int Alterar(Produto p)
        {
            ComandoSQL.Parameters.Clear();
            ComandoSQL.CommandText = @"
                update produto 
                set descricao = @des,
                medida = @med,
                preco = @pre,
                preco_out = @po,
                representacao = @rep
                where id = @id;
            ";
            ComandoSQL.Parameters.AddWithValue("@des", p.Descricao);
            ComandoSQL.Parameters.AddWithValue("@med", p.Medida);
            ComandoSQL.Parameters.AddWithValue("@pre", p.Preco);
            ComandoSQL.Parameters.AddWithValue("@po", p.PrecoOut);
            ComandoSQL.Parameters.AddWithValue("@rep", p.Representacao.Id);
            ComandoSQL.Parameters.AddWithValue("@id", p.Id);

            return ExecutaComando();
        }

        internal int Excluir(int id)
        {
            ComandoSQL.Parameters.Clear();
            ComandoSQL.CommandText = @"
                delete from produto where id = @id;
            ";
            ComandoSQL.Parameters.AddWithValue("@id", id);

            return ExecutaComando();
        }
    }
}