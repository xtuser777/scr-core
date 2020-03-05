using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using scrweb.Models;

namespace scrweb.DAO
{
    internal class MotoristaDAO : Banco
    {
        private Motorista GetObject(DataRow row)
        {
            return new Motorista()
            {
                Id = Convert.ToInt32(row["mot_id"]),
                Cadastro = Convert.ToDateTime(row["mot_cadastro"]),
                Pessoa = new PessoaFisica()
                {
                    Id = Convert.ToInt32(row["pes_id"]),
                    Nome = row["pes_nome"].ToString(),
                    Rg = row["pes_rg"].ToString(),
                    Cpf = row["pes_cpf"].ToString(),
                    Nascimento = Convert.ToDateTime(row["pes_nascimento"]),
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

        private List<Motorista> GetList(DataTable table)
        {
            return (from DataRow row in table.Rows select GetObject(row)).ToList();
        }

        internal Motorista GetById(int id)
        {
            ComandoSQL.Parameters.Clear();
            ComandoSQL.CommandText = @"
                select e.id as est_id, e.nome as est_nome, e.sigla as est_sigla,
                       c.id as cid_id, c.nome as cid_nome, c.estado as cid_estado,
                       en.id as end_id, en.rua as end_rua, en.numero as end_numero, en.bairro as end_bairro, en.complemento as end_complemento, en.cep as end_cep, en.cidade as end_cidade,
                       ct.id as ctt_id, ct.telefone as ctt_telefone, ct.celular as ctt_celular, ct.email as ctt_email, ct.endereco as ctt_endereco,
                       p.id as pes_id, p.nome as pes_nome, p.rg as pes_rg, p.cpf as pes_cpf, p.nascimento as pes_nascimento, p.contato as pes_contato,
                       m.id as mot_id, m.cadastro as mot_cadastro, m.pessoa as mot_pessoa
                from estado e, cidade c, endereco en, contato ct, pessoa_fisica p, motorista m
                where m.id = @id
                and p.id = m.pessoa
                and ct.id = p.contato
                and en.id = ct.endereco
                and c.id = en.cidade
                and e.id = c.estado;
            ";
            ComandoSQL.Parameters.AddWithValue("@id", id);

            DataTable table = ExecutaSelect();

            return table != null && table.Rows.Count > 0 ? GetObject(table.Rows[0]) : null;
        }

        internal List<Motorista> GetAll()
        {
            ComandoSQL.Parameters.Clear();
            ComandoSQL.CommandText = @"
                select e.id as est_id, e.nome as est_nome, e.sigla as est_sigla,
                       c.id as cid_id, c.nome as cid_nome, c.estado as cid_estado,
                       en.id as end_id, en.rua as end_rua, en.numero as end_numero, en.bairro as end_bairro, en.complemento as end_complemento, en.cep as end_cep, en.cidade as end_cidade,
                       ct.id as ctt_id, ct.telefone as ctt_telefone, ct.celular as ctt_celular, ct.email as ctt_email, ct.endereco as ctt_endereco,
                       p.id as pes_id, p.nome as pes_nome, p.rg as pes_rg, p.cpf as pes_cpf, p.nascimento as pes_nascimento, p.contato as pes_contato,
                       m.id as mot_id, m.cadastro as mot_cadastro, m.pessoa as mot_pessoa
                from estado e, cidade c, endereco en, contato ct, pessoa_fisica p, motorista m
                where p.id = m.pessoa
                and ct.id = p.contato
                and en.id = ct.endereco
                and c.id = en.cidade
                and e.id = c.estado;
            ";

            DataTable table = ExecutaSelect();

            return table != null && table.Rows.Count > 0 ? GetList(table) : null;
        }

        internal int Gravar(Motorista m)
        {
            ComandoSQL.Parameters.Clear();
            ComandoSQL.CommandText = @"
                insert into motorista(cadastro,pessoa)
                values(@cad,@pes) 
                returning id;
            ";
            ComandoSQL.Parameters.AddWithValue("@cad", m.Cadastro);
            ComandoSQL.Parameters.AddWithValue("@pes", m.Pessoa.Id);

            DataTable table = ExecutaSelect();

            return table != null && table.Rows.Count > 0 ? Convert.ToInt32(table.Rows[0]["id"]) : -10;
        }

        internal int Alterar(Motorista m)
        {
            ComandoSQL.Parameters.Clear();
            ComandoSQL.CommandText = @"
                update motorista 
                set cadastro = @cad,
                pessoa = @pes
                where id = @id;
            ";
            ComandoSQL.Parameters.AddWithValue("@cad", m.Cadastro);
            ComandoSQL.Parameters.AddWithValue("@pes", m.Pessoa.Id);
            ComandoSQL.Parameters.AddWithValue("@id", m.Id);

            return ExecutaComando();
        }

        internal int Excluir(int id)
        {
            ComandoSQL.Parameters.Clear();
            ComandoSQL.CommandText = @"
                delete from motorista where id = @id;
            ";
            ComandoSQL.Parameters.AddWithValue("@id", id);

            return ExecutaComando();
        }
    }
}