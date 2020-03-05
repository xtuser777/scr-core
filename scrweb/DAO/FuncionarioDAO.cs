using scrweb.Models;
using System;
using System.Collections.Generic;
using System.Data;

namespace scrweb.DAO
{
    internal class FuncionarioDAO : Banco
    {
        private Funcionario GetObject(DataRow row)
        {
            return new Funcionario()
            {
                Id = Convert.ToInt32(row["fun_id"]),
                Tipo = Convert.ToInt32(row["fun_tipo"]),
                Admissao = Convert.ToDateTime(row["fun_admissao"]),
                Demissao = (row["fun_demissao"] is DBNull) ? (DateTime?)null : Convert.ToDateTime(row["fun_demissao"]),
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

        private List<Funcionario> GetList(DataTable dt)
        {
            List<Funcionario> list = new List<Funcionario>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow row = dt.Rows[i];
                list.Add(GetObject(row));
            }

            return list;
        }

        internal Funcionario GetById(int id)
        {
            ComandoSQL.Parameters.Clear();
            ComandoSQL.CommandText = @"
                select e.id as est_id, e.nome as est_nome, e.sigla as est_sigla,
                       c.id as cli_id, c.nome as cid_nome, c.estado as cid_estado,
                       en.id as end_id, en.rua as end_rua, en.numero as end_numero, en.bairro as end_bairro, en.complemento as end_complemento, en.cep as end_cep, en.cidade as end_cidade,
                       ct.id as ctt_id, ct.telefone as ctt_telefone, ct.celular as ctt_celular, ct.email as ctt_email, ct.endereco as ctt_endereco,
                       p.id as pes_id, p.nome as pes_nome, p.rg as pes_rg, p.cpf as pes_cpf, p.nascimento as pes_nascimento, p.contato as pes_contato,
                       f.id as fun_id, f.tipo as fun_tipo, f.admissao as fun_admissao, f.demissao as fun_demissao, f.pessoa as fun_pessoa
                from estado e, cidade c, endereco en, contato ct, pessoa_fisica p, funcionario f
                where f.id = @id
                and p.id = f.pessoa
                and ct.id = p.contato
                and en.id = ct.endereco
                and c.id = en.cidade
                and e.id = c.estado;
            ";
            ComandoSQL.Parameters.AddWithValue("@id", id);
            
            DataTable dt = ExecutaSelect();
            
            return (dt != null && dt.Rows.Count > 0) ? GetObject(dt.Rows[0]) : null;
        }

        internal List<Funcionario> GetAll()
        {
            ComandoSQL.Parameters.Clear();
            ComandoSQL.CommandText = @"
                select e.id as est_id, e.nome as est_nome, e.sigla as est_sigla,
                       c.id as cli_id, c.nome as cid_nome, c.estado as cid_estado,
                       en.id as end_id, en.rua as end_rua, en.numero as end_numero, en.bairro as end_bairro, en.complemento as end_complemento, en.cep as end_cep, en.cidade as end_cidade,
                       ct.id as ctt_id, ct.telefone as ctt_telefone, ct.celular as ctt_celular, ct.email as ctt_email, ct.endereco as ctt_endereco,
                       p.id as pes_id, p.nome as pes_nome, p.rg as pes_rg, p.cpf as pes_cpf, p.nascimento as pes_nascimento, p.contato as pes_contato,
                       f.id as fun_id, f.tipo as fun_tipo, f.admissao as fun_admissao, f.demissao as fun_demissao, f.pessoa as fun_pessoa
                from estado e, cidade c, endereco en, contato ct, pessoa_fisica p, funcionario f
                where p.id = f.pessoa
                and ct.id = p.contato
                and en.id = ct.endereco
                and c.id = en.cidade
                and e.id = c.estado;
            ";
            
            var dt = ExecutaSelect();
            
            return (dt != null && dt.Rows.Count > 0) ? GetList(dt) : null;
        }

        internal Funcionario GetVendedorById(int id)
        {
            ComandoSQL.Parameters.Clear();
            ComandoSQL.CommandText = @"
                select e.id as est_id, e.nome as est_nome, e.sigla as est_sigla,
                       c.id as cli_id, c.nome as cid_nome, c.estado as cid_estado,
                       en.id as end_id, en.rua as end_rua, en.numero as end_numero, en.bairro as end_bairro, en.complemento as end_complemento, en.cep as end_cep, en.cidade as end_cidade,
                       ct.id as ctt_id, ct.telefone as ctt_telefone, ct.celular as ctt_celular, ct.email as ctt_email, ct.endereco as ctt_endereco,
                       p.id as pes_id, p.nome as pes_nome, p.rg as pes_rg, p.cpf as pes_cpf, p.nascimento as pes_nascimento, p.contato as pes_contato,
                       f.id as fun_id, f.tipo as fun_tipo, f.admissao as fun_admissao, f.demissao as fun_demissao, f.pessoa as fun_pessoa
                from estado e, cidade c, endereco en, contato ct, pessoa_fisica p, funcionario f
                where f.id = @id
                and f.tipo = 2
                and p.id = f.pessoa
                and ct.id = p.contato
                and en.id = ct.endereco
                and c.id = en.cidade
                and e.id = c.estado;
            ";
            ComandoSQL.Parameters.AddWithValue("@id", id);
            
            DataTable dt = ExecutaSelect();
            
            return (dt != null && dt.Rows.Count > 0) ? GetObject(dt.Rows[0]) : null;
        }

        internal List<Funcionario> GetVendedores()
        {
            ComandoSQL.Parameters.Clear();
            ComandoSQL.CommandText = @"
                select e.id as est_id, e.nome as est_nome, e.sigla as est_sigla,
                       c.id as cli_id, c.nome as cid_nome, c.estado as cid_estado,
                       en.id as end_id, en.rua as end_rua, en.numero as end_numero, en.bairro as end_bairro, en.complemento as end_complemento, en.cep as end_cep, en.cidade as end_cidade,
                       ct.id as ctt_id, ct.telefone as ctt_telefone, ct.celular as ctt_celular, ct.email as ctt_email, ct.endereco as ctt_endereco,
                       p.id as pes_id, p.nome as pes_nome, p.rg as pes_rg, p.cpf as pes_cpf, p.nascimento as pes_nascimento, p.contato as pes_contato,
                       f.id as fun_id, f.tipo as fun_tipo, f.admissao as fun_admissao, f.demissao as fun_demissao, f.pessoa as fun_pessoa
                from estado e, cidade c, endereco en, contato ct, pessoa_fisica p, funcionario f
                where f.tipo = 2
                and p.id = f.pessoa
                and ct.id = p.contato
                and en.id = ct.endereco
                and c.id = en.cidade
                and e.id = c.estado;
            ";
            
            DataTable dt = ExecutaSelect(); 
            
            return (dt != null && dt.Rows.Count > 0) ? GetList(dt) : null;
        }

        internal int Gravar(Funcionario f)
        {
            ComandoSQL.Parameters.Clear();
            ComandoSQL.CommandText = @"
                insert into funcionario(tipo,admissao,demissao,pessoa) 
                values(@tipo,@admissao,null,@pessoa) 
                returning id;
            ";
            ComandoSQL.Parameters.AddWithValue("@tipo", f.Tipo);
            ComandoSQL.Parameters.AddWithValue("@admissao", f.Admissao);
            ComandoSQL.Parameters.AddWithValue("@pessoa", f.Pessoa.Id);
            
            DataTable dt = ExecutaSelect();
            
            return (dt != null && dt.Rows.Count > 0) ? Convert.ToInt32(dt.Rows[0]["id"]) : -10;
        }

        internal int Alterar(Funcionario f)
        {
            ComandoSQL.Parameters.Clear();
            ComandoSQL.CommandText = @"
                update funcionario 
                set tipo = @tipo,
                admissao = @admissao,
                pessoa = @pessoa
                where id = @id;
            ";
            ComandoSQL.Parameters.AddWithValue("@tipo", f.Tipo);
            ComandoSQL.Parameters.AddWithValue("@admissao", f.Admissao);
            ComandoSQL.Parameters.AddWithValue("@pessoa", f.Pessoa.Id);
            ComandoSQL.Parameters.AddWithValue("@id", f.Id);

            return ExecutaComando();
        }

        internal int Excluir(int id)
        {
            ComandoSQL.Parameters.Clear();
            ComandoSQL.CommandText = @"delete from funcionario where id = @id;";
            ComandoSQL.Parameters.AddWithValue("@id", id);

            return ExecutaComando();
        }

        internal int Desativar(int id)
        {
            ComandoSQL.Parameters.Clear();
            ComandoSQL.CommandText = @"update funcionario set demissao = now() where id = @id;
                                       update usuario set ativo = false where id = @id;";
            ComandoSQL.Parameters.AddWithValue("@id", id);

            return ExecutaComando();
        }

        internal int Reativar(int id)
        {
            ComandoSQL.Parameters.Clear();
            ComandoSQL.CommandText = @"update funcionario set demissao = null where id = @id;
                                       update usuario set ativo = true where id = @id;";
            ComandoSQL.Parameters.AddWithValue("@id", id);

            return ExecutaComando();
        }
    }
}
