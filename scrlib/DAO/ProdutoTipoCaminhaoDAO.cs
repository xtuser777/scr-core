using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using scrlib.Models;
using scrlib.ViewModels;

namespace scrlib.DAO
{
    internal class ProdutoTipoCaminhaoDAO : Banco
    {
        private ProdutoTipoCaminhao GetObject(DataRow row)
        {
            return new ProdutoTipoCaminhao()
            {
                Produto = Convert.ToInt32(row["produto"]),
                Tipo = Convert.ToInt32(row["tipo"])
            };
        }

        private List<ProdutoTipoCaminhao> GetList(DataTable table)
        {
            return (from DataRow row in table.Rows select GetObject(row)).ToList();
        }

        internal ProdutoTipoCaminhao GetById(int produto, int tipo)
        {
            ComandoSQL.Parameters.Clear();
            ComandoSQL.CommandText = @"
                select produto,tipo 
                from produto_tipo_caminhao 
                where produto = @p 
                and tipo = @t;
            ";
            ComandoSQL.Parameters.AddWithValue("@p", produto);
            ComandoSQL.Parameters.AddWithValue("@t", tipo);

            var table = ExecutaSelect();

            return table != null && table.Rows.Count > 0 ? GetObject(table.Rows[0]) : null;
        }

        internal List<ProdutoTipoCaminhao> GetAll()
        {
            ComandoSQL.Parameters.Clear();
            ComandoSQL.CommandText = @"
                select produto,tipo 
                from produto_tipo_caminhao;
            ";

            var table = ExecutaSelect();

            return table != null && table.Rows.Count > 0 ? GetList(table) : null;
        }

        internal List<ProdutoTipoCaminhao> GetPorProduto(int produto)
        {
            ComandoSQL.Parameters.Clear();
            ComandoSQL.CommandText = @"
                select produto,tipo 
                from produto_tipo_caminhao
                where produto = @p;
            ";
            ComandoSQL.Parameters.AddWithValue("@p", produto);

            var table = ExecutaSelect();
            
            return table != null && table.Rows.Count > 0 ? GetList(table) : null;
        }

        internal int Gravar(ProdutoTipoCaminhao ptc)
        {
            ComandoSQL.Parameters.Clear();
            ComandoSQL.CommandText = @"
                insert into produto_tipo_caminhao(produto,tipo) 
                values(@p,@t);
            ";
            ComandoSQL.Parameters.AddWithValue("@p", ptc.Produto);
            ComandoSQL.Parameters.AddWithValue("t", ptc.Tipo);

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