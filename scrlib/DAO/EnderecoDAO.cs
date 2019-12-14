using scrlib.Models;
using System;
using System.Collections.Generic;
using System.Data;
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
            List<Endereco> enderecos = new List<Endereco>();
            foreach (DataRow row in dt.Rows)
            {
                enderecos.Add(GetObject(row));
            }
            return enderecos;
        }

        internal Endereco GetById(int id)
        {
            Endereco endereco = null;
            ComandoSQL.Parameters.Clear();
            ComandoSQL.CommandText = @"select id,rua,numero,bairro,complemento,cep,cidade 
                                        from endereco 
                                        where id = @id;";
            ComandoSQL.Parameters.AddWithValue("@id", id);
            DataTable dt = ExecutaSelect();
            if (dt != null && dt.Rows.Count > 0)
            {
                endereco = GetObject(dt.Rows[0]);
            }
            return endereco;
        }

        internal List<Endereco> Get()
        {
            List<Endereco> enderecos = null;
            ComandoSQL.Parameters.Clear();
            ComandoSQL.CommandText = @"select id,rua,numero,bairro,complemento,cep,cidade 
                                        from endereco;";
            DataTable dt = ExecutaSelect();
            if (dt != null && dt.Rows.Count > 0)
            {
                enderecos = GetList(dt);
            }
            return enderecos;
        }

        internal int Gravar(Endereco e)
        {
            int res = -1;
            ComandoSQL.Parameters.Clear();
            ComandoSQL.CommandText = @"insert into endereco(id,rua,numero,bairro,complemento,cep,cidade) 
                                        values((select count(id)+1 from endereco),@rua,@numero,@bairro,@complemento,@cep,@cidade) returning id;";
            ComandoSQL.Parameters.AddWithValue("@rua", e.Rua);
            ComandoSQL.Parameters.AddWithValue("@numero", e.Numero);
            ComandoSQL.Parameters.AddWithValue("@bairro", e.Bairro);
            ComandoSQL.Parameters.AddWithValue("@complemento", e.Complemento);
            ComandoSQL.Parameters.AddWithValue("@cep", e.Cep);
            ComandoSQL.Parameters.AddWithValue("@cidade", e.Cidade);
            DataTable dt = ExecutaSelect();
            if (dt != null && dt.Rows.Count > 0)
            {
                res = Convert.ToInt32(dt.Rows[0]["id"]);
            }
            return res;
        }

        internal int Alterar(Endereco e)
        {
            int res = -1;
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
            res = ExecutaComando();
            return res;
        }

        internal int Excluir(int id)
        {
            int res = -1;
            ComandoSQL.Parameters.Clear();
            ComandoSQL.CommandText = @"delete from endereco where id = @id;";
            ComandoSQL.Parameters.AddWithValue("@id", id);
            res = ExecutaComando();
            return res;
        }
    }
}
