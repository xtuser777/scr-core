using System;
using System.Collections.Generic;
using System.Data;
using scrweb.Models;

namespace scrweb.DAO
{
    internal class FormaPagamentoDAO : Banco
    {
        private FormaPagamento GetObject(DataRow row) 
        {
            return new FormaPagamento()
            {
                Id = Convert.ToInt32(row["id"]),
                Descricao = row["descricao"].ToString(),
                Prazo = Convert.ToInt32(row["prazo"])
            };
        }

        private List<FormaPagamento> GetList(DataTable table)
        {
            List<FormaPagamento> list = new List<FormaPagamento>();

            for (int i = 0; i < table.Rows.Count; i++)
            {
                DataRow row = table.Rows[i];
                list.Add(GetObject(row));
            }

            return list;
        }

        internal FormaPagamento GetById(int id) 
        {
            ComandoSQL.Parameters.Clear();
            ComandoSQL.CommandText = @"
                select id,descricao,prazo 
                from forma_pagamento 
                where id = @id;
            ";
            ComandoSQL.Parameters.AddWithValue("@id", id);

            DataTable table = ExecutaSelect();

            return table != null && table.Rows.Count > 0 ? GetObject(table.Rows[0]) : null;
        }

        internal List<FormaPagamento> GetAll()
        {
            ComandoSQL.Parameters.Clear();
            ComandoSQL.CommandText = @"
                select id,descricao,prazo 
                from forma_pagamento;
            ";

            DataTable table = ExecutaSelect();

            return table != null && table.Rows.Count > 0 ? GetList(table) : null;
        }

        internal int Gravar(FormaPagamento fp)
        {
            ComandoSQL.Parameters.Clear();
            ComandoSQL.CommandText = @"
                insert into forma_pagamento(descricao,prazo) 
                values(@des,@pra) 
                returning id;
            ";
            ComandoSQL.Parameters.AddWithValue("@des", fp.Descricao);
            ComandoSQL.Parameters.AddWithValue("@pra", fp.Prazo);

            DataTable table = ExecutaSelect();

            return table != null && table.Rows.Count > 0 ? Convert.ToInt32(table.Rows[0]["id"]) : -10;
        }

        internal int Alterar(FormaPagamento fp)
        {
            ComandoSQL.Parameters.Clear();
            ComandoSQL.CommandText = @"
                update forma_pagamento
                set descricao = @des,
                prazo = @pra
                where id = @id;
            ";
            ComandoSQL.Parameters.AddWithValue("@des", fp.Descricao);
            ComandoSQL.Parameters.AddWithValue("@pra", fp.Prazo);
            ComandoSQL.Parameters.AddWithValue("@id", fp.Id);

            return ExecutaComando();
        }

        internal int Excluir(int id)
        {
            ComandoSQL.Parameters.Clear();
            ComandoSQL.CommandText = @"delete from forma_pagamento where id = @id;";
            ComandoSQL.Parameters.AddWithValue("@id", id);

            return ExecutaComando();
        }
    }
}