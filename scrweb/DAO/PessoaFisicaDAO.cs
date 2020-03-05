using scrweb.Models;
using System;
using System.Data;

namespace scrweb.DAO
{
    internal class PessoaFisicaDAO : Banco
    {
        private PessoaFisica GetObject(DataRow row)
        {
            return new PessoaFisica()
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
            };
        }

        internal PessoaFisica GetById(int id)
        {
            ComandoSQL.Parameters.Clear();
            ComandoSQL.CommandText = @"
                select e.id as est_id, e.nome as est_nome, e.sigla as est_sigla,
                       c.id as cid_id, c.nome as cid_nome, c.estado as cid_estado,
                       en.id as end_id, en.rua as end_rua, en.numero as end_numero, en.bairro as end_bairro, en.complemento as end_complemento, en.cep as end_cep, en.cidade as end_cidade,
                       ct.id as ctt_id, ct.telefone as ctt_telefone, ct.celular as ctt_celular, ct.email as ctt_email, ct.endereco as ctt_endereco,
                       p.id as pes_id, p.nome as pes_nome, p.rg as pes_rg, p.cpf as pes_cpf, p.nascimento as pes_nascimento, p.contato as pes_contato
                from estado e, cidade c, endereco en, contato ct, pessoa_fisica p
                where p.id = @id
                and ct.id = p.contato
                and en.id = ct.endereco
                and c.id = en.cidade
                and e.id = c.estado;
            ";
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
            ComandoSQL.CommandText = @"
                insert into pessoa_fisica(nome,rg,cpf,nascimento,contato) 
                values (@nome,@rg,@cpf,@nascimento,@contato) 
                returning id;
            ";
            ComandoSQL.Parameters.AddWithValue("@nome", p.Nome);
            ComandoSQL.Parameters.AddWithValue("@rg", p.Rg);
            ComandoSQL.Parameters.AddWithValue("@cpf", p.Cpf);
            ComandoSQL.Parameters.AddWithValue("@nascimento", p.Nascimento);
            ComandoSQL.Parameters.AddWithValue("@contato", p.Contato.Id);
            
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
                                            contato = @contato
                                        where id = @id;";
            ComandoSQL.Parameters.AddWithValue("@nome", p.Nome);
            ComandoSQL.Parameters.AddWithValue("@rg", p.Rg);
            ComandoSQL.Parameters.AddWithValue("@cpf", p.Cpf);
            ComandoSQL.Parameters.AddWithValue("@nascimento", p.Nascimento);
            ComandoSQL.Parameters.AddWithValue("@contato", p.Contato.Id);
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
