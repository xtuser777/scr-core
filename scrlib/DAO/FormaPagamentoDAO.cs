using System;
using System.Linq;
using System.Collections.Generic;
using System.Data;
using scrlib.Models;

namespace scrlib.DAO
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
            return (from DataRow row in table.Rows select GetObject(row)).ToList();
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

            var table = ExecutaSelect();

            return table != null && table.Rows.Count > 0 ? GetObject(table.Rows[0]) : null;
        }

        internal List<FormaPagamento> GetAll()
        {
            ComandoSQL.Parameters.Clear();
            ComandoSQL.CommandText = @"
                select id,descricao,prazo 
                from forma_pagamento;
            ";

            var table = ExecutaSelect();

            return table != null && table.Rows.Count > 0 ? GetList(table) : null;
        }

        internal int Gravar(FormaPagamento fp)
        {
            ComandoSQL.Parameters.Clear();
            ComandoSQL.CommandText = @"
                insert into forma_pagamento(descricao,prazo) 
                values(@des,@pra) returning id;
            ";
            ComandoSQL.Parameters.AddWithValue("@des", fp.Descricao);
            ComandoSQL.Parameters.AddWithValue("@pra", fp.Prazo);

            var table = ExecutaSelect();

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