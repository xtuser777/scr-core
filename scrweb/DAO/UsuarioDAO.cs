using scrweb.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace scrweb.DAO
{
    internal class UsuarioDAO : Banco
    {
        private Usuario GetObject(DataRow row)
        {
            return new Usuario()
            {
                Id = Convert.ToInt32(row["usu_id"]),
                Login = row["usu_login"].ToString(),
                Senha = row["usu_senha"].ToString(),
                Ativo = Convert.ToBoolean(row["usu_ativo"]),
                Funcionario = new Funcionario()
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
                },
                Nivel = new Nivel()
                {
                    Id = Convert.ToInt32(row["niv_id"]),
                    Descricao = row["niv_descricao"].ToString()
                }
            };
        }

        private List<Usuario> GetList(DataTable dt)
        {
            List<Usuario> list = new List<Usuario>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow row = dt.Rows[i];
                list.Add(GetObject(row));
            }

            return list;
        }

        internal Usuario GetById(int id)
        {
            ComandoSQL.Parameters.Clear();
            ComandoSQL.CommandText = @"
                select e.id as est_id, e.nome as est_nome, e.sigla as est_sigla,
                       c.id as cid_id, c.nome as cid_nome, c.estado as cid_estado,
                       en.id as end_id, en.rua as end_rua, en.numero as end_numero, en.bairro as end_bairro, en.complemento as end_complemento, en.cep as end_cep, en.cidade as end_cidade,
                       ct.id as ctt_id, ct.telefone as ctt_telefone, ct.celular as ctt_celular, ct.email as ctt_email, ct.endereco as ctt_endereco,
                       p.id as pes_id, p.nome as pes_nome, p.rg as pes_rg, p.cpf as pes_cpf, p.nascimento as pes_nascimento, p.contato as pes_contato,
                       f.id as fun_id, f.tipo as fun_tipo, f.admissao as fun_admissao, f.demissao as fun_demissao, f.pessoa as fun_pessoa,
                       n.id as niv_id, n.descricao as niv_descricao,
                       u.id as usu_id, u.login as usu_login, u.senha as usu_senha, u.ativo as usu_ativo, u.funcionario as usu_funcionario, u.nivel as usu_nivel
                from estado e, cidade c, endereco en, contato ct, pessoa_fisica p, funcionario f, nivel n, usuario u
                where u.id = @id
                and n.id = u.nivel
                and f.id = u.funcionario
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

        internal Usuario Autenticar(string login, string senha)
        {
            ComandoSQL.Parameters.Clear();
            ComandoSQL.CommandText = @"
                select e.id as est_id, e.nome as est_nome, e.sigla as est_sigla,
                       c.id as cid_id, c.nome as cid_nome, c.estado as cid_estado,
                       en.id as end_id, en.rua as end_rua, en.numero as end_numero, en.bairro as end_bairro, en.complemento as end_complemento, en.cep as end_cep, en.cidade as end_cidade,
                       ct.id as ctt_id, ct.telefone as ctt_telefone, ct.celular as ctt_celular, ct.email as ctt_email, ct.endereco as ctt_endereco,
                       p.id as pes_id, p.nome as pes_nome, p.rg as pes_rg, p.cpf as pes_cpf, p.nascimento as pes_nascimento, p.contato as pes_contato,
                       f.id as fun_id, f.tipo as fun_tipo, f.admissao as fun_admissao, f.demissao as fun_demissao, f.pessoa as fun_pessoa,
                       n.id as niv_id, n.descricao as niv_descricao,
                       u.id as usu_id, u.login as usu_login, u.senha as usu_senha, u.ativo as usu_ativo, u.funcionario as usu_funcionario, u.nivel as usu_nivel
                from estado e, cidade c, endereco en, contato ct, pessoa_fisica p, funcionario f, nivel n, usuario u
                where u.login = @login
                and u.senha = @senha
                and u.ativo = true
                and n.id = u.nivel
                and f.id = u.funcionario
                and p.id = f.pessoa
                and ct.id = p.contato
                and en.id = ct.endereco
                and c.id = en.cidade
                and e.id = c.estado;
            ";
            ComandoSQL.Parameters.AddWithValue("@login", login);
            ComandoSQL.Parameters.AddWithValue("@senha", senha);
            
            DataTable dt = ExecutaSelect();
            
            return (dt != null && dt.Rows.Count > 0) ? GetObject(dt.Rows[0]) : null;
        }

        internal List<Usuario> GetAll()
        {
            ComandoSQL.Parameters.Clear();
            ComandoSQL.CommandText = @"
                select e.id as est_id, e.nome as est_nome, e.sigla as est_sigla,
                       c.id as cid_id, c.nome as cid_nome, c.estado as cid_estado,
                       en.id as end_id, en.rua as end_rua, en.numero as end_numero, en.bairro as end_bairro, en.complemento as end_complemento, en.cep as end_cep, en.cidade as end_cidade,
                       ct.id as ctt_id, ct.telefone as ctt_telefone, ct.celular as ctt_celular, ct.email as ctt_email, ct.endereco as ctt_endereco,
                       p.id as pes_id, p.nome as pes_nome, p.rg as pes_rg, p.cpf as pes_cpf, p.nascimento as pes_nascimento, p.contato as pes_contato,
                       f.id as fun_id, f.tipo as fun_tipo, f.admissao as fun_admissao, f.demissao as fun_demissao, f.pessoa as fun_pessoa,
                       n.id as niv_id, n.descricao as niv_descricao,
                       u.id as usu_id, u.login as usu_login, u.senha as usu_senha, u.ativo as usu_ativo, u.funcionario as usu_funcionario, u.nivel as usu_nivel
                from estado e, cidade c, endereco en, contato ct, pessoa_fisica p, funcionario f, nivel n, usuario u
                where n.id = u.nivel
                and f.id = u.funcionario
                and p.id = f.pessoa
                and ct.id = p.contato
                and en.id = ct.endereco
                and c.id = en.cidade
                and e.id = c.estado;
            ";
            
            DataTable dt = ExecutaSelect();
            
            return (dt != null && dt.Rows.Count > 0) ?  GetList(dt) : null;
        }

        internal int Gravar(Usuario u)
        {
            ComandoSQL.Parameters.Clear();
            ComandoSQL.CommandText = @"
                insert into usuario(login,senha,funcionario,nivel,ativo) 
                values(@login,@senha,@funcionario,@nivel,@ativo) 
                returning id;
            ";
            ComandoSQL.Parameters.AddWithValue("@login", u.Login);
            ComandoSQL.Parameters.AddWithValue("@senha", u.Senha);
            ComandoSQL.Parameters.AddWithValue("@funcionario", u.Funcionario.Id);
            ComandoSQL.Parameters.AddWithValue("@nivel", u.Nivel.Id);
            ComandoSQL.Parameters.AddWithValue("@ativo", u.Ativo);
            
            DataTable dt = ExecutaSelect();
            
            return (dt != null && dt.Rows.Count > 0) ? Convert.ToInt32(dt.Rows[0]["id"]) : -10;
        }

        internal int Alterar(Usuario u)
        {
            ComandoSQL.Parameters.Clear();
            ComandoSQL.CommandText = @"
                update usuario 
                set login = @login,
                senha = @senha,
                funcionario = @funcionario,
                nivel = @nivel,
                ativo = @ativo
                where id = @id;
            ";
            ComandoSQL.Parameters.AddWithValue("@login", u.Login);
            ComandoSQL.Parameters.AddWithValue("@senha", u.Senha);
            ComandoSQL.Parameters.AddWithValue("@funcionario", u.Funcionario.Id);
            ComandoSQL.Parameters.AddWithValue("@nivel", u.Nivel.Id);
            ComandoSQL.Parameters.AddWithValue("@ativo", u.Ativo);
            ComandoSQL.Parameters.AddWithValue("@id", u.Id);

            return ExecutaComando();
        }

        internal int LoginCount(string login)
        {
            int count = 0;
            ComandoSQL.Parameters.Clear();
            ComandoSQL.CommandText = @"select count(id) as logins from usuario where login = @login;";
            ComandoSQL.Parameters.AddWithValue("@login", login);
            DataTable dt = ExecutaSelect();
            if (dt != null && dt.Rows.Count > 0)
            {
                count = Convert.ToInt32(dt.Rows[0]["logins"]);
            }
            return count;
        }

        internal int AdminCount()
        {
            ComandoSQL.Parameters.Clear();
            ComandoSQL.CommandText = @"
                select count(usuario.id) as admins 
                from usuario 
                inner join funcionario on usuario.funcionario = funcionario.id
                where usuario.nivel = 1 
                and funcionario.demissao is null;
            ";
            
            DataTable dt = ExecutaSelect();
            
            if (dt != null && dt.Rows.Count > 0)
            {
                return Convert.ToInt32(dt.Rows[0]["admins"]);
            }

            return 0;
        }

        internal int Excluir(int id)
        {
            ComandoSQL.Parameters.Clear();
            ComandoSQL.CommandText = @"delete from usuario where id = @id;";
            ComandoSQL.Parameters.AddWithValue("@id", id);

            return ExecutaComando();
        }
    }
}
