using System;
using System.Collections.Generic;
using System.Data;
using scrweb.Models;

namespace scrweb.DAO
{
    internal class ProdutoTipoCaminhaoDAO : Banco
    {
        private ProdutoTipoCaminhao GetObject(DataRow row)
        {
            return new ProdutoTipoCaminhao()
            {
                Produto = new Produto()
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
                },
                Tipo = new TipoCaminhao()
                {
                    Id = Convert.ToInt32(row["tip_id"]),
                    Descricao = row["tip_descricao"].ToString(),
                    Eixos = Convert.ToInt32(row["tip_eixos"]),
                    Capacidade = Convert.ToDecimal(row["tip_capacidade"])
                }
            };
        }

        private List<ProdutoTipoCaminhao> GetList(DataTable table)
        {
            List<ProdutoTipoCaminhao> list = new List<ProdutoTipoCaminhao>();

            for (int i = 0; i < table.Rows.Count; i++)
            {
                DataRow row = table.Rows[i];
                list.Add(GetObject(row));
            }

            return list;
        }

        internal ProdutoTipoCaminhao GetById(int produto, int tipo)
        {
            ComandoSQL.Parameters.Clear();
            ComandoSQL.CommandText = @"
                select e.id as est_id, e.nome as est_nome, e.sigla as est_sigla,
                       c.id as cid_id, c.nome as cid_nome, c.estado as cid_estado,
                       en.id as end_id, en.rua as end_rua, en.numero as end_numero, en.bairro as end_bairro, en.complemento as end_complemento, en.cep as end_cep, en.cidade as end_cidade,
                       ct.id as ctt_id, ct.telefone as ctt_telefone, ct.celular as ctt_celular, ct.email as ctt_email, ct.endereco as ctt_endereco,
                       pj.id as pes_id, pj.razao_social as pes_razao_social, pj.nome_fantasia as pes_nome_fantasia, pj.cnpj as pes_cnpj, pj.contato as pes_contato,
                       r.id as rep_id, r.cadastro as rep_cadastro, r.unidade as rep_unidade, r.pessoa as rep_pessoa,
                       p.id as pro_id, p.descricao as pro_descricao, p.medida as pro_medida, p.preco as pro_preco, p.preco_out as pro_preco_out, p.representacao as pro_representacao,
                       t.id as tip_id, t.descricao as tip_descricao, t.eixos as tip_eixos, t.capacidade as tip_capacidade,
                       ptc.produto as ptc_produto, ptc.tipo as ptc_tipo
                from estado e, cidade c, endereco en, contato ct, pessoa_juridica pj, representacao r, produto p, tipo_caminhao t, produto_tipo_caminhao ptc
                where ptc.produto = @p
                and ptc.tipo = @t
                and t.id = ptc.tipo
                and p.id = ptc.produto
                and r.id = p.representacao
                and pj.id = r.pessoa
                and ct.id = pj.contato
                and en.id = ct.endereco
                and c.id = en.cidade
                and e.id = c.estado;
            ";
            ComandoSQL.Parameters.AddWithValue("@p", produto);
            ComandoSQL.Parameters.AddWithValue("@t", tipo);

            DataTable table = ExecutaSelect();

            return table != null && table.Rows.Count > 0 ? GetObject(table.Rows[0]) : null;
        }

        internal List<ProdutoTipoCaminhao> GetAll()
        {
            ComandoSQL.Parameters.Clear();
            ComandoSQL.CommandText = @"
                select e.id as est_id, e.nome as est_nome, e.sigla as est_sigla,
                       c.id as cid_id, c.nome as cid_nome, c.estado as cid_estado,
                       en.id as end_id, en.rua as end_rua, en.numero as end_numero, en.bairro as end_bairro, en.complemento as end_complemento, en.cep as end_cep, en.cidade as end_cidade,
                       ct.id as ctt_id, ct.telefone as ctt_telefone, ct.celular as ctt_celular, ct.email as ctt_email, ct.endereco as ctt_endereco,
                       pj.id as pes_id, pj.razao_social as pes_razao_social, pj.nome_fantasia as pes_nome_fantasia, pj.cnpj as pes_cnpj, pj.contato as pes_contato,
                       r.id as rep_id, r.cadastro as rep_cadastro, r.unidade as rep_unidade, r.pessoa as rep_pessoa,
                       p.id as pro_id, p.descricao as pro_descricao, p.medida as pro_medida, p.preco as pro_preco, p.preco_out as pro_preco_out, p.representacao as pro_representacao,
                       t.id as tip_id, t.descricao as tip_descricao, t.eixos as tip_eixos, t.capacidade as tip_capacidade,
                       ptc.produto as ptc_produto, ptc.tipo as ptc_tipo
                from estado e, cidade c, endereco en, contato ct, pessoa_juridica pj, representacao r, produto p, tipo_caminhao t, produto_tipo_caminhao ptc
                where t.id = ptc.tipo
                and p.id = ptc.produto
                and r.id = p.representacao
                and pj.id = r.pessoa
                and ct.id = pj.contato
                and en.id = ct.endereco
                and c.id = en.cidade
                and e.id = c.estado;
            ";

            DataTable table = ExecutaSelect();

            return table != null && table.Rows.Count > 0 ? GetList(table) : null;
        }

        internal List<ProdutoTipoCaminhao> GetPorProduto(int produto)
        {
            ComandoSQL.Parameters.Clear();
            ComandoSQL.CommandText = @"
                select e.id as est_id, e.nome as est_nome, e.sigla as est_sigla,
                       c.id as cid_id, c.nome as cid_nome, c.estado as cid_estado,
                       en.id as end_id, en.rua as end_rua, en.numero as end_numero, en.bairro as end_bairro, en.complemento as end_complemento, en.cep as end_cep, en.cidade as end_cidade,
                       ct.id as ctt_id, ct.telefone as ctt_telefone, ct.celular as ctt_celular, ct.email as ctt_email, ct.endereco as ctt_endereco,
                       pj.id as pes_id, pj.razao_social as pes_razao_social, pj.nome_fantasia as pes_nome_fantasia, pj.cnpj as pes_cnpj, pj.contato as pes_contato,
                       r.id as rep_id, r.cadastro as rep_cadastro, r.unidade as rep_unidade, r.pessoa as rep_pessoa,
                       p.id as pro_id, p.descricao as pro_descricao, p.medida as pro_medida, p.preco as pro_preco, p.preco_out as pro_preco_out, p.representacao as pro_representacao,
                       t.id as tip_id, t.descricao as tip_descricao, t.eixos as tip_eixos, t.capacidade as tip_capacidade,
                       ptc.produto as ptc_produto, ptc.tipo as ptc_tipo
                from estado e, cidade c, endereco en, contato ct, pessoa_juridica pj, representacao r, produto p, tipo_caminhao t, produto_tipo_caminhao ptc
                where ptc.produto = @p
                and t.id = ptc.tipo
                and p.id = ptc.produto
                and r.id = p.representacao
                and pj.id = r.pessoa
                and ct.id = pj.contato
                and en.id = ct.endereco
                and c.id = en.cidade
                and e.id = c.estado;
            ";
            ComandoSQL.Parameters.AddWithValue("@p", produto);

            DataTable table = ExecutaSelect();
            
            return table != null && table.Rows.Count > 0 ? GetList(table) : null;
        }

        internal int Gravar(ProdutoTipoCaminhao ptc)
        {
            ComandoSQL.Parameters.Clear();
            ComandoSQL.CommandText = @"
                insert into produto_tipo_caminhao(produto,tipo) 
                values(@p,@t);
            ";
            ComandoSQL.Parameters.AddWithValue("@p", ptc.Produto.Id);
            ComandoSQL.Parameters.AddWithValue("t", ptc.Tipo.Id);

            return ExecutaComando();
        }

        internal int Excluir(int produto, int tipo)
        {
            ComandoSQL.Parameters.Clear();
            ComandoSQL.CommandText = @"
                delete from produto_tipo_caminhao 
                where produto = @p 
                and tipo = @t;
            ";
            ComandoSQL.Parameters.AddWithValue("@p", produto);
            ComandoSQL.Parameters.AddWithValue("@t", tipo);

            return ExecutaComando();
        }

        internal int ExcluirPorProduto(int produto)
        {
            ComandoSQL.Parameters.Clear();
            ComandoSQL.CommandText = @"
                delete from produto_tipo_caminhao 
                where produto = @p;
            ";
            ComandoSQL.Parameters.AddWithValue("@p", produto);

            return ExecutaComando();
        }

        internal int ExcluirPorTipo(int tipo)
        {
            ComandoSQL.Parameters.Clear();
            ComandoSQL.CommandText = @"
                delete from produto_tipo_caminhao 
                where tipo = @t;
            ";
            ComandoSQL.Parameters.AddWithValue("@t", tipo);

            return ExecutaComando();
        }
    }
}