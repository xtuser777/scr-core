using System;
using System.Collections.Generic;
using System.Data;
using scrweb.Models;

namespace scrweb.DAO
{
    internal class ContatoDAO : Banco
    {
        private Contato GetObject(DataRow row)
        {
            return new Contato()
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
            };
        }

        private List<Contato> GetList(DataTable table)
        {
            List<Contato> list = new List<Contato>();

            for (int i = 0; i < table.Rows.Count; i++)
            {
                DataRow row = table.Rows[i];
                list.Add(GetObject(row));
            }

            return list;
        }

        internal Contato GetById(int id)
        {
            ComandoSQL.Parameters.Clear();
            ComandoSQL.CommandText = @"
                select e.id as est_id, e.nome as est_nome, e.sigla as est_sigla,
                       c.id as cli_id, c.nome as cid_nome, c.estado as cid_estado,
                       en.id as end_id, en.rua as end_rua, en.numero as end_numero, en.bairro as end_bairro, en.complemento as end_complemento, en.cep as end_cep, en.cidade as end_cidade,
                       ct.id as ctt_id, ct.telefone as ctt_telefone, ct.celular as ctt_celular, ct.email as ctt_email, ct.endereco as ctt_endereco
                from estado e, cidade c, endereco en, contato ct
                where ct.id = @1
                and en.id = ct.endereco
                and c.id = en.cidade
                and e.id = c.estado;
            ";
            ComandoSQL.Parameters.AddWithValue("@1", id);

            DataTable table = ExecutaSelect();

            if (table == null || table.Rows.Count <= 0) return null;

            DataRow row = table.Rows[0];

            return GetObject(row);
        }

        internal int Gravar(Contato c)
        {
            ComandoSQL.Parameters.Clear();
            ComandoSQL.CommandText = @"
                insert into contato(telefone, celular, email, endereco)
                values (@1, @2, @3, @4)
                returning id;
            ";
            ComandoSQL.Parameters.AddWithValue("@1", c.Telefone);
            ComandoSQL.Parameters.AddWithValue("@2", c.Celular);
            ComandoSQL.Parameters.AddWithValue("@3", c.Email);
            ComandoSQL.Parameters.AddWithValue("@4", c.Endereco.Id);

            DataTable table = ExecutaSelect();
            
            if (table == null || table.Rows.Count <= 0) return -10;

            DataRow row = table.Rows[0];

            return Convert.ToInt32(row["id"]);
        }

        internal int Alterar(Contato c)
        {
            ComandoSQL.Parameters.Clear();
            ComandoSQL.CommandText = @"
                update contato 
                set telefone = @1,
                celular = @2,
                email = @3,
                endereco = @4
                where id = @5;
            ";
            ComandoSQL.Parameters.AddWithValue("@1", c.Telefone);
            ComandoSQL.Parameters.AddWithValue("@2", c.Celular);
            ComandoSQL.Parameters.AddWithValue("@3", c.Email);
            ComandoSQL.Parameters.AddWithValue("@4", c.Endereco.Id);
            ComandoSQL.Parameters.AddWithValue("@5", c.Id);

            return ExecutaComando();
        }

        internal int Excluir(int id)
        {
            ComandoSQL.Parameters.Clear();
            ComandoSQL.CommandText = "delete from contato where id = @1";
            ComandoSQL.Parameters.AddWithValue("@1", id);

            return ExecutaComando();
        }
    }
}