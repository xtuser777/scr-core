using scrlib.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace scrlib.DAO
{
    internal class EnderecoDAO : Banco
    {
        private Endereco GetObject(DataRow dr)
        {
            return new Endereco()
            {
                Id = Convert.ToInt32(dr["id"]),
                Rua = dr["rua"].ToString(),
                Numero = dr["numero"].ToString(),
                Bairro = dr["bairro"].ToString(),
                Complemento = dr["complemento"].ToString(),
                Cep = dr["cep"].ToString(),
                Cidade = Convert.ToInt32(dr["cidade"])
            };
        }

        private List<Endereco> GetList(DataTable dt)
        {
            return (from DataRow row in dt.Rows select GetObject(row)).ToList();
        }

        internal Endereco GetById(int id)
        {
            ComandoSQL.Parameters.Clear();
            ComandoSQL.CommandText = @"select id,rua,numero,bairro,complemento,cep,cidade 
                                        from endereco 
                                        where id = @id;";
            ComandoSQL.Parameters.AddWithValue("@id", id);
            
            var dt = ExecutaSelect();
            
            return (dt != null && dt.Rows.Count > 0) ? GetObject(dt.Rows[0]) : null;
        }

        internal List<Endereco> Get()
        {
            ComandoSQL.Parameters.Clear();
            ComandoSQL.CommandText = @"select id,rua,numero,bairro,complemento,cep,cidade from endereco;";
            
            var dt = ExecutaSelect();
            
            return (dt != null && dt.Rows.Count > 0) ? GetList(dt) : null;
        }

        internal int Gravar(Endereco e)
        {
            ComandoSQL.Parameters.Clear();
            ComandoSQL.CommandText = @"insert into endereco(rua,numero,bairro,complemento,cep,cidade) 
                                        values(@rua,@numero,@bairro,@complemento,@cep,@cidade) returning id;";
            ComandoSQL.Parameters.AddWithValue("@rua", e.Rua);
            ComandoSQL.Parameters.AddWithValue("@numero", e.Numero);
            ComandoSQL.Parameters.AddWithValue("@bairro", e.Bairro);
            ComandoSQL.Parameters.AddWithValue("@complemento", e.Complemento);
            ComandoSQL.Parameters.AddWithValue("@cep", e.Cep);
            ComandoSQL.Parameters.AddWithValue("@cidade", e.Cidade);
            
            var dt = ExecutaSelect();
            
            return dt != null && dt.Rows.Count > 0 ? Convert.ToInt32(dt.Rows[0]["id"]) : -10;
        }

        internal int Alterar(Endereco e)
        {
            ComandoSQL.Parameters.Clear();
            ComandoSQL.CommandText = @"update endereco 
                                        set rua = @rua,
                                            numero = @numero,
                                            bairro = @bairro,
                                            complemento = @complemento,
                                            cep = @cep,
                                            cidade = @cidade
                                            where id = @id;";
            ComandoSQL.Parameters.AddWithValue("@rua", e.Rua);
            ComandoSQL.Parameters.AddWithValue("@numero", e.Numero);
            ComandoSQL.Parameters.AddWithValue("@bairro", e.Bairro);
            ComandoSQL.Parameters.AddWithValue("@complemento", e.Complemento);
            ComandoSQL.Parameters.AddWithValue("@cep", e.Cep);
            ComandoSQL.Parameters.AddWithValue("@cidade", e.Cidade);
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
