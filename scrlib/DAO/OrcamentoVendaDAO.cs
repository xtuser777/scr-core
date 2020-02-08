using System;
using System.Linq;
using System.Collections.Generic;
using System.Data;
using scrlib.Models;

namespace scrlib.DAO
{
    internal class OrcamentoVendaDAO : Banco
    {
        private OrcamentoVenda GetObject(DataRow row)
        {
            return new OrcamentoVenda()
            {
                Id = Convert.ToInt32(row["id"]),
                Descricao = row["descricao"].ToString(),
                Data = Convert.ToDateTime(row["data"]),
                NomeCliente = row["nome_cliente"].ToString(),
                DocumentoCliente = row["documento_cliente"].ToString(),
                TelefoneCliente = row["telefone_cliente"].ToString(),
                CelularCliente = row["celular_cliente"].ToString(),
                EmailCliente = row["email_cliente"].ToString(),
                Valor = Convert.ToDecimal(row["valor"]),
                Peso = Convert.ToDecimal(row["peso"]),
                Validade = Convert.ToDateTime(row["validade"]),
                Vendedor = Convert.ToInt32(row["vendedor"]),
                Destino = Convert.ToInt32(row["destino"]),
                TipoCaminhao = Convert.ToInt32(row["tipo_caminhao"]),
                Cliente = row["cliente"] == null ? 0 : Convert.ToInt32(row["cliente"]),
                Autor = Convert.ToInt32(row["autor"])
            };
        }

        private List<OrcamentoVenda> GetList(DataTable table)
        {
            return (from DataRow row in table.Rows select GetObject(row)).ToList();
        }

        internal OrcamentoVenda GetById(int id) 
        {
            ComandoSQL.Parameters.Clear();
            ComandoSQL.CommandText = @"
                select id,descricao,data,nome_cliente,documento_cliente,telefone_cliente,celular_cliente,email_cliente,valor,peso,validade,vendedor,destino,tipo_caminhao,cliente,autor 
                from orcamento_venda
                where id = @id;
            ";
            ComandoSQL.Parameters.AddWithValue("@id", id);

            var table = ExecutaSelect();

            return table != null && table.Rows.Count > 0 ? GetObject(table.Rows[0]) : null;
        }

        internal List<OrcamentoVenda> GetAll()
        {
            ComandoSQL.Parameters.Clear();
            ComandoSQL.CommandText = @"
                select id,descricao,data,nome_cliente,documento_cliente,telefone_cliente,celular_cliente,email_cliente,valor,peso,validade,vendedor,destino,tipo_caminhao,cliente,autor 
                from orcamento_venda;
            ";

            var table = ExecutaSelect();

            return table != null && table.Rows.Count > 0 ? GetList(table) : null;
        }

        internal int Gravar(OrcamentoVenda o) 
        {
            ComandoSQL.Parameters.Clear();
            ComandoSQL.CommandText = @"
                insert into orcamento_venda(descricao,data,nome_cliente,documento_cliente,telefone_cliente,celular_cliente,email_cliente,valor,peso,validade,vendedor,destino,tipo_caminhao,cliente,autor)
                values(@des,@dat,@nc,@dc,@tc,@cc,@ec,@val,@pes,@vdd,@vdr,@des,@tip,@cli,@aut) returning id;
            ";
            ComandoSQL.Parameters.AddWithValue("@des", o.Descricao);
            ComandoSQL.Parameters.AddWithValue("@dat", o.Data);
            ComandoSQL.Parameters.AddWithValue("@nc", o.NomeCliente);
            ComandoSQL.Parameters.AddWithValue("@dc", o.DocumentoCliente);
            ComandoSQL.Parameters.AddWithValue("@tc", o.TelefoneCliente);
            ComandoSQL.Parameters.AddWithValue("@cc", o.CelularCliente);
            ComandoSQL.Parameters.AddWithValue("@ec", o.EmailCliente);
            ComandoSQL.Parameters.AddWithValue("@val", o.Valor);
            ComandoSQL.Parameters.AddWithValue("@pes", o.Peso);
            ComandoSQL.Parameters.AddWithValue("@vdd", o.Validade);
            ComandoSQL.Parameters.AddWithValue("@vdr", o.Vendedor);
            ComandoSQL.Parameters.AddWithValue("@des", o.Destino);
            ComandoSQL.Parameters.AddWithValue("@tip", o.TipoCaminhao);
            ComandoSQL.Parameters.AddWithValue("@cli", o.Cliente);
            ComandoSQL.Parameters.AddWithValue("@aut", o.Autor);

            var table = ExecutaSelect();

            return table != null && table.Rows.Count > 0 ? Convert.ToInt32(table.Rows[0]["id"]) : -10;
        }

        internal int Alterar(OrcamentoVenda o) 
        {
            ComandoSQL.Parameters.Clear();
            ComandoSQL.CommandText = @"
                update orcamento_venda
                set descricao,
                data,
                nome_cliente,
                documento_cliente,
                telefone_cliente,
                celular_cliente,
                email_cliente,
                valor,
                peso,
                validade,
                vendedor,
                destino,
                tipo_caminhao,
                cliente,
                autor
                where id = @id;
            ";
            ComandoSQL.Parameters.AddWithValue("@des", o.Descricao);
            ComandoSQL.Parameters.AddWithValue("@dat", o.Data);
            ComandoSQL.Parameters.AddWithValue("@nc", o.NomeCliente);
            ComandoSQL.Parameters.AddWithValue("@dc", o.DocumentoCliente);
            ComandoSQL.Parameters.AddWithValue("@tc", o.TelefoneCliente);
            ComandoSQL.Parameters.AddWithValue("@cc", o.CelularCliente);
            ComandoSQL.Parameters.AddWithValue("@ec", o.EmailCliente);
            ComandoSQL.Parameters.AddWithValue("@val", o.Valor);
            ComandoSQL.Parameters.AddWithValue("@pes", o.Peso);
            ComandoSQL.Parameters.AddWithValue("@vdd", o.Validade);
            ComandoSQL.Parameters.AddWithValue("@vdr", o.Vendedor);
            ComandoSQL.Parameters.AddWithValue("@des", o.Destino);
            ComandoSQL.Parameters.AddWithValue("@tip", o.TipoCaminhao);
            ComandoSQL.Parameters.AddWithValue("@cli", o.Cliente);
            ComandoSQL.Parameters.AddWithValue("@aut", o.Autor);
            ComandoSQL.Parameters.AddWithValue("@id", o.Id);

            return ExecutaComando();
        }

        internal int Excluir(int id) 
        {
            ComandoSQL.Parameters.Clear();
            ComandoSQL.CommandText = @"delete from orcamento_venda where id = @id;";
            ComandoSQL.Parameters.AddWithValue("@id", id);

            return ExecutaComando();
        }
    }
}