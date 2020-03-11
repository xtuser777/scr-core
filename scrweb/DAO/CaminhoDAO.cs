using System;
using scrweb.Models;
using System.Data;
using System.Collections.Generic;

namespace scrweb.DAO 
{
    internal class CaminhaoDAO : Banco
    {
        private Caminhao GetObject(DataRow row) 
        {
            return new Caminhao() 
            {
                Id = Convert.ToInt32(row["cam_id"]),
                Placa = row["cam_placa"].ToString(),
                Marca = row["cam_marca"].ToString(),
                Modelo = row["cam_modelo"].ToString(),
                Ano = row["cam_ano"].ToString(),
                Tipo = new TipoCaminhao()
                {
                    Id = Convert.ToInt32(row["tip_id"]),
                    Descricao = row["tip_descricao"].ToString(),
                    Eixos = Convert.ToInt32(row["tip_eixos"]),
                    Capacidade = Convert.ToDecimal(row["tip_capacidade"])
                },
                Proprietario = new Motorista()
                {
                    Id = Convert.ToInt32(row["mot_id"]),
                    Cadastro = Convert.ToDateTime(row["mot_cadastro"]),
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
                }
            };
        }

        private List<Caminhao> GetList(DataTable table) 
        {
            List<Caminhao> list = new List<Caminhao>();
            
            for (int i = 0; i < table.Rows.Count; i++)
            {
                DataRow row = table.Rows[i];
                list.Add(GetObject(row));
            }

            return list;
        }

        internal Caminhao GetById(int id) 
        {
            ComandoSQL.Parameters.Clear();
            ComandoSQL.CommandText = @"
                select e.id as est_id, e.nome as est_nome, e.sigla as est_sigla,
                       c.id as cid_id, c.nome as cid_nome, c.estado as cid_estado,
                       en.id as end_id, en.rua as end_rua, en.numero as end_numero, en.bairro as end_bairro, en.complemento as end_complemento, en.cep as end_cep, en.cidade as end_cidade,
                       ct.id as ctt_id, ct.telefone as ctt_telefone, ct.celular as ctt_celular, ct.email as ctt_email, ct.endereco as ctt_endereco,
                       p.id as pes_id, p.nome as pes_nome, p.rg as pes_rg, p.cpf as pes_cpf, p.nascimento as pes_nascimento, p.contato as pes_contato,
                       m.id as mot_id, m.cadastro as mot_cadastro, m.pessoa as mot_pessoa,
                       t.id as tip_id, t.descricao as tip_descricao, t.eixos as tip_eixos, t.capacidade as tip_capacidade,
                       cm.id as cam_id, cm.placa as cam_placa, cm.marca as cam_marca, cm.modelo as cam_modelo, cm.ano as cam_ano, cm.tipo as cam_tipo, cm.proprietario as cam_proprietario
                from estado e, cidade c, endereco en, contato ct, pessoa_fisica p, motorista m, tipo_caminhao t, caminhao cm
                where cm.id = @id
                and t.id = cm.tipo
                and m.id = cm.proprietario
                and p.id = m.pessoa
                and ct.id = p.contato
                and en.id = ct.endereco
                and c.id = en.cidade
                and e.id = c.estado;
            ";
            ComandoSQL.Parameters.AddWithValue("@id", id);

            DataTable table = ExecutaSelect();

            return table != null && table.Rows.Count > 0 ? GetObject(table.Rows[0]) : null;    
        }

        internal List<Caminhao> GetAll()
        {
            ComandoSQL.Parameters.Clear();
            ComandoSQL.CommandText = @"
                select e.id as est_id, e.nome as est_nome, e.sigla as est_sigla,
                       c.id as cid_id, c.nome as cid_nome, c.estado as cid_estado,
                       en.id as end_id, en.rua as end_rua, en.numero as end_numero, en.bairro as end_bairro, en.complemento as end_complemento, en.cep as end_cep, en.cidade as end_cidade,
                       ct.id as ctt_id, ct.telefone as ctt_telefone, ct.celular as ctt_celular, ct.email as ctt_email, ct.endereco as ctt_endereco,
                       p.id as pes_id, p.nome as pes_nome, p.rg as pes_rg, p.cpf as pes_cpf, p.nascimento as pes_nascimento, p.contato as pes_contato,
                       m.id as mot_id, m.cadastro as mot_cadastro, m.pessoa as mot_pessoa,
                       t.id as tip_id, t.descricao as tip_descricao, t.eixos as tip_eixos, t.capacidade as tip_capacidade,
                       cm.id as cam_id, cm.placa as cam_placa, cm.marca as cam_marca, cm.modelo as cam_modelo, cm.ano as cam_ano, cm.tipo as cam_tipo, cm.proprietario as cam_proprietario
                from estado e, cidade c, endereco en, contato ct, pessoa_fisica p, motorista m, tipo_caminhao t, caminhao cm
                where t.id = cm.tipo
                and m.id = cm.proprietario
                and p.id = m.pessoa
                and ct.id = p.contato
                and en.id = ct.endereco
                and c.id = en.cidade
                and e.id = c.estado;
            ";

            DataTable table = ExecutaSelect();

            return table != null && table.Rows.Count > 0 ? GetList(table) : null;
        }

        internal int Gravar(Caminhao c) 
        {
            ComandoSQL.Parameters.Clear();
            ComandoSQL.CommandText = @"
                insert into caminhao(placa,marca,modelo,ano,tipo,proprietario) 
                values(@pla,@mar,@mod,@ano,@tip,@pro) 
                returning id;
            ";
            ComandoSQL.Parameters.AddWithValue("@pla", c.Placa);
            ComandoSQL.Parameters.AddWithValue("@mar", c.Marca);
            ComandoSQL.Parameters.AddWithValue("@mod", c.Modelo);
            ComandoSQL.Parameters.AddWithValue("@ano", c.Ano);
            ComandoSQL.Parameters.AddWithValue("@tip", c.Tipo.Id);
            ComandoSQL.Parameters.AddWithValue("@pro", c.Proprietario.Id);

            DataTable table = ExecutaSelect();

            return table != null && table.Rows.Count > 0 ? Convert.ToInt32(table.Rows[0]["id"]) : -10;
        }

        internal int Alterar(Caminhao c) 
        {
            ComandoSQL.Parameters.Clear();
            ComandoSQL.CommandText = @"
                update caminhao
                set placa = @pla,
                marca = @mar,
                modelo = @mod,
                ano = @ano,
                tipo = @tip,
                proprietario = @pro
                where id = @id;
            ";
            ComandoSQL.Parameters.AddWithValue("@pla", c.Placa);
            ComandoSQL.Parameters.AddWithValue("@mar", c.Marca);
            ComandoSQL.Parameters.AddWithValue("@mod", c.Modelo);
            ComandoSQL.Parameters.AddWithValue("@ano", c.Ano);
            ComandoSQL.Parameters.AddWithValue("@tip", c.Tipo.Id);
            ComandoSQL.Parameters.AddWithValue("@pro", c.Proprietario.Id);
            ComandoSQL.Parameters.AddWithValue("@id", c.Id);

            return ExecutaComando();
        }

        internal int Excluir(int id) 
        {
            ComandoSQL.Parameters.Clear();
            ComandoSQL.CommandText = @"delete from caminhao where id = @id;";
            ComandoSQL.Parameters.AddWithValue("@id", id);

            return ExecutaComando();
        }
    }
}