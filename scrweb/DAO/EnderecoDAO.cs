using scrweb.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace scrweb.DAO
{
    internal class EnderecoDAO : Banco
    {
        private Endereco GetObject(DataRow dr)
        {
            return new Endereco()
            {
                Id = Convert.ToInt32(dr["end_id"]),
                Rua = dr["end_rua"].ToString(),
                Numero = dr["end_numero"].ToString(),
                Bairro = dr["end_bairro"].ToString(),
                Complemento = dr["end_complemento"].ToString(),
                Cep = dr["end_cep"].ToString(),
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

        private List<Endereco> GetList(DataTable dt)
        {
            List<Endereco> list = new List<Endereco>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow row = dt.Rows[i];
                list.Add(GetObject(row));
            }

            return list;
        }

        internal Endereco GetById(int id)
        {
            ComandoSQL.Parameters.Clear();
            ComandoSQL.CommandText = @"
                select e.id as est_id, e.nome as est_nome, e.sigla as est_sigla,
                       c.id as cli_id, c.nome as cid_nome, c.estado as cid_estado,
                       en.id as end_id, en.rua as end_rua, en.numero as end_numero, en.bairro as end_bairro, en.complemento as end_complemento, en.cep as end_cep, en.cidade as end_cidade
                from estado e, cidade c, endereco en
                where en.id = @id
                and c.id = en.cidade
                and e.id = c.estado;
            ";
            ComandoSQL.Parameters.AddWithValue("@id", id);
            
            DataTable dt = ExecutaSelect();
            
            return (dt != null && dt.Rows.Count > 0) ? GetObject(dt.Rows[0]) : null;
        }

        internal List<Endereco> GetAll()
        {
            ComandoSQL.Parameters.Clear();
            ComandoSQL.CommandText = @"
                select e.id as est_id, e.nome as est_nome, e.sigla as est_sigla,
                       c.id as cli_id, c.nome as cid_nome, c.estado as cid_estado,
                       en.id as end_id, en.rua as end_rua, en.numero as end_numero, en.bairro as end_bairro, en.complemento as end_complemento, en.cep as end_cep, en.cidade as end_cidade
                from estado e, cidade c, endereco en
                where c.id = en.cidade
                and e.id = c.estado
                order by en.id;
            ";
            
            DataTable dt = ExecutaSelect();
            
            return (dt != null && dt.Rows.Count > 0) ? GetList(dt) : null;
        }

        internal int Gravar(Endereco e)
        {
            ComandoSQL.Parameters.Clear();
            ComandoSQL.CommandText = @"
                insert into endereco(rua,numero,bairro,complemento,cep,cidade) 
                values(@rua,@numero,@bairro,@complemento,@cep,@cidade) returning id;
            ";
            ComandoSQL.Parameters.AddWithValue("@rua", e.Rua);
            ComandoSQL.Parameters.AddWithValue("@numero", e.Numero);
            ComandoSQL.Parameters.AddWithValue("@bairro", e.Bairro);
            ComandoSQL.Parameters.AddWithValue("@complemento", e.Complemento);
            ComandoSQL.Parameters.AddWithValue("@cep", e.Cep);
            ComandoSQL.Parameters.AddWithValue("@cidade", e.Cidade.Id);
            
            DataTable dt = ExecutaSelect();
            
            return dt != null && dt.Rows.Count > 0 ? Convert.ToInt32(dt.Rows[0]["id"]) : -10;
        }

        internal int Alterar(Endereco e)
        {
            ComandoSQL.Parameters.Clear();
            ComandoSQL.CommandText = @"
                update endereco 
                set rua = @rua,
                numero = @numero,
                bairro = @bairro,
                complemento = @complemento,
                cep = @cep,
                cidade = @cidade
                where id = @id;
            ";
            ComandoSQL.Parameters.AddWithValue("@rua", e.Rua);
            ComandoSQL.Parameters.AddWithValue("@numero", e.Numero);
            ComandoSQL.Parameters.AddWithValue("@bairro", e.Bairro);
            ComandoSQL.Parameters.AddWithValue("@complemento", e.Complemento);
            ComandoSQL.Parameters.AddWithValue("@cep", e.Cep);
            ComandoSQL.Parameters.AddWithValue("@cidade", e.Cidade.Id);
            ComandoSQL.Parameters.AddWithValue("@id", e.Id);

            return ExecutaComando();
        }

        internal int Excluir(int id)
        {
            ComandoSQL.Parameters.Clear();
            ComandoSQL.CommandText = @"delete from endereco where id = @id;";
            ComandoSQL.Parameters.AddWithValue("@id", id);

            return ExecutaComando();
        }
    }
}
