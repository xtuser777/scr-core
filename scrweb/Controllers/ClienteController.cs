using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using scrweb.Filters;
using scrweb.Models;

namespace scrweb.Controllers
{
    [ValidarUsuario]
    public class ClienteController : Controller
    {
        private static List<Cliente> _clientes;

        public ClienteController()
        {
            _clientes = new Cliente().GetAll();
        }
        
        // GET
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Novo()
        {
            return View();
        }

        public IActionResult Detalhes(int id)
        {
            return View();
        }

        [HttpPost]
        public JsonResult Enviar(string id)
        {
            if (string.IsNullOrEmpty(id)) return Json("Parâmetro inválido...");
            HttpContext.Session.SetString("idcli", id);
            return Json("");
        }

        public JsonResult Obter()
        {
            if (_clientes == null || _clientes.Count == 0) return Json(new List<Cliente>());
            
            JArray array = new JArray();
            for (int i = 0; i < _clientes.Count; i++)
            {
                array.Add(_clientes[i].ToJObject());
            }
            
            return Json(array);
        }

        [HttpPost]
        public JsonResult ObterPorChave(string chave)
        {
            var filtrado = _clientes.FindAll(cli => cli.Tipo == 1
                ? (cli.PessoaFisica.Nome.Contains(chave, StringComparison.CurrentCultureIgnoreCase) ||
                  cli.PessoaFisica.Contato.Email.Contains(chave, StringComparison.CurrentCultureIgnoreCase))
                : (cli.PessoaJuridica.NomeFantasia.Contains(chave, StringComparison.CurrentCultureIgnoreCase) ||
                  cli.PessoaJuridica.Contato.Email.Contains(chave, StringComparison.CurrentCultureIgnoreCase))
            );
            
            JArray array = new JArray();
            for (int i = 0; i < filtrado.Count; i++)
            {
                array.Add(filtrado[i].ToJObject());
            }
            
            return Json(array);
        }

        [HttpPost]
        public JsonResult ObterPorCadastro(string cad)
        {
            var filtrado = _clientes.FindAll(cli => cli.Cadastro.ToString("yyyy-MM-dd") == cad);
            
            JArray array = new JArray();
            for (int i = 0; i < filtrado.Count; i++)
            {
                array.Add(filtrado[i].ToJObject());
            }
            
            return Json(array);
        }

        [HttpPost]
        public JsonResult ObterPorChaveCad(string chave, string cad)
        {
            var filtrado = _clientes.FindAll(cli => cli.Tipo == 1
                ? ((cli.PessoaFisica.Nome.Contains(chave, StringComparison.CurrentCultureIgnoreCase) ||
                  cli.PessoaFisica.Contato.Email.Contains(chave, StringComparison.CurrentCultureIgnoreCase)) &&
                  cli.Cadastro.ToString("yyyy-MM-dd") == cad)
                : ((cli.PessoaJuridica.NomeFantasia.Contains(chave, StringComparison.CurrentCultureIgnoreCase) ||
                  cli.PessoaJuridica.Contato.Email.Contains(chave, StringComparison.CurrentCultureIgnoreCase)) &&
                  cli.Cadastro.ToString("yyyy-MM-dd") == cad)
            );
            
            JArray array = new JArray();
            for (int i = 0; i < filtrado.Count; i++)
            {
                array.Add(filtrado[i].ToJObject());
            }
            
            return Json(array);
        }

        public JsonResult ObterDetalhes()
        {
            var id = HttpContext.Session.GetString("idcli");

            var cli = _clientes.Find(c => c.Id == Convert.ToInt32(id));
            
            return Json(cli.ToJObject());
        }

        [HttpPost]
        public JsonResult Ordenar(string col)
        {
            var ord = new List<Cliente>();

            switch (col)
            {
                case "1":
                    ord = _clientes.OrderBy(c => c.Id).ToList();
                    break;
                case "2":
                    ord = _clientes.OrderByDescending(c => c.Id).ToList();
                    break;
                case "3":
                    ord = _clientes.OrderBy(c =>
                        c.Tipo == 1
                            ? c.PessoaFisica.Nome
                            : c.PessoaJuridica.NomeFantasia).ToList();
                    break;
                case "4":
                    ord = _clientes.OrderByDescending(c =>
                        c.Tipo == 1
                            ? c.PessoaFisica.Nome
                            : c.PessoaJuridica.NomeFantasia).ToList();
                    break;
                case "5":
                    ord = _clientes.OrderBy(c =>
                        c.Tipo == 1
                            ? c.PessoaFisica.Cpf
                            : c.PessoaJuridica.Cnpj).ToList();
                    break;
                case "6":
                    ord = _clientes.OrderByDescending(c =>
                        c.Tipo == 1
                            ? c.PessoaFisica.Cpf
                            : c.PessoaJuridica.Cnpj).ToList();
                    break;
                case "7":
                    ord = _clientes.OrderBy(c => c.Cadastro).ToList();
                    break;
                case "8":
                    ord = _clientes.OrderByDescending(c => c.Cadastro).ToList();
                    break;
                case "9":
                    ord = _clientes.OrderBy(c => c.Tipo).ToList();
                    break;
                case "10":
                    ord = _clientes.OrderByDescending(c => c.Tipo).ToList();
                    break;
                case "11":
                    ord = _clientes.OrderBy(c => c.PessoaFisica.Contato.Email).ToList();
                    break;
                case "12":
                    ord = _clientes.OrderByDescending(c => c.PessoaFisica.Contato.Email).ToList();
                    break;
            }

            JArray array = new JArray();
            for (int i = 0; i < ord.Count; i++)
            {
                array.Add(ord[i].ToJObject());
            }
            
            return Json(array);
        }

        [HttpPost]
        public JsonResult VerificarCpf(string cpf)
        {
            return Json(new PessoaFisica().VerifyCpf(cpf));
        }

        [HttpPost]
        public JsonResult VerificarCnpj(string cnpj)
        {
            return Json(new PessoaJuridica().VerifyCnpj(cnpj));
        }

        [HttpPost]
        public JsonResult Gravar(IFormCollection form)
        {
            var nome = form["nome"];
            var rg = form["rg"];
            var cpf = form["cpf"];
            var nasc = form["nasc"];
            var razaosocial = form["razaosocial"];
            var nomefantasia = form["nomefantasia"];
            var cnpj = form["cnpj"];
            var rua = form["rua"];
            var numero = form["numero"];
            var bairro = form["bairro"];
            var complemento = form["complemento"];
            var cep = form["cep"];
            var cidade = form["cidade"];
            var telefone = form["telefone"];
            var celular = form["celular"];
            var email = form["email"];
            var tipo = form["tipo"];

            int.TryParse(tipo, out var tipoi);
            int.TryParse(cidade, out var cidi);

            DateTime.TryParse(nasc, out var nascimento);

            int res1 = new Endereco(){
                Id = 0,
                Rua = rua,
                Numero = numero,
                Bairro = bairro,
                Complemento = complemento,
                Cep = cep,
                Cidade = new Cidade()
                {
                    Id = cidi
                }
            }.Gravar();

            if (res1 <= 0) return Json("Ocorreu um problema ao gravar o endereço...");
            
            int res2 = new Contato()
            {
                Id = 0,
                Telefone = telefone,
                Celular = celular,
                Email = email,
                Endereco = new Endereco()
                {
                    Id = res1
                }
            }.Gravar();

            if (res2 <= 0)
            {
                new Endereco().Excluir(res1);
                return Json("Ocorreu um problema ao gravar o contato...");
            }

            int res3 = 0;
            if (tipoi == 1)
            {
                res3 = new PessoaFisica()
                {
                    Id = 0,
                    Nome = nome,
                    Rg = rg,
                    Cpf = cpf,
                    Nascimento = nascimento,
                    Contato = new Contato()
                    {
                        Id = res2
                    }
                }.Gravar();    
            }
            else
            {
                res3 = new PessoaJuridica()
                {
                    Id = 0,
                    RazaoSocial = razaosocial,
                    NomeFantasia = nomefantasia,
                    Cnpj = cnpj,
                    Contato = new Contato()
                    {
                        Id = res2
                    }
                }.Gravar();
            }

            if (res3 <= 0)
            {
                new Contato().Excluir(res2);
                new Endereco().Excluir(res1);
                return Json("Ocorreu um problema ao gravar a pessoa...");
            }
            
            int res4 = new Cliente()
            {
                Id = 0,
                Cadastro = DateTime.Now,
                Tipo = tipoi,
                PessoaFisica = tipoi == 2 ? null : new PessoaFisica()
                {
                    Id = res3,
                    Nome = nome,
                    Rg = rg,
                    Cpf = cpf,
                    Nascimento = nascimento,
                    Contato = new Contato()
                    {
                        Id = res2
                    }
                },
                PessoaJuridica = tipoi == 1 ? null : new PessoaJuridica()
                {
                    Id = res3,
                    RazaoSocial = razaosocial,
                    NomeFantasia = nomefantasia,
                    Cnpj = cnpj,
                    Contato = new Contato()
                    {
                        Id = res2
                    }
                }
            }.Gravar();
            
            if (res4 <= 0)
            {
                if (tipoi == 1) new PessoaFisica().Excluir(res3);
                else new PessoaJuridica().Excluir(res3);
                new Contato().Excluir(res2);
                new Endereco().Excluir(res1);
                return Json("Ocorreu um problema ao gravar o cliente...");
            }

            return Json("");
        }

        [HttpPost]
        public JsonResult Alterar(IFormCollection form)
        {
            var endereco = form["endereco"];
            var contato = form["contato"];
            var pessoa = form["pessoa"];
            var cliente = form["cliente"];
            
            var nome = form["nome"];
            var rg = form["rg"];
            var cpf = form["cpf"];
            var nasc = form["nasc"];
            var razaosocial = form["razaosocial"];
            var nomefantasia = form["nomefantasia"];
            var cnpj = form["cnpj"];
            var rua = form["rua"];
            var numero = form["numero"];
            var bairro = form["bairro"];
            var complemento = form["complemento"];
            var cep = form["cep"];
            var cidade = form["cidade"];
            var telefone = form["telefone"];
            var celular = form["celular"];
            var email = form["email"];
            var tipo = form["tipo"];

            int.TryParse(tipo, out var tipoi);
            int.TryParse(cidade, out var cidi);
            int.TryParse(endereco, out var idendereco);
            int.TryParse(contato, out int idcontato);
            int.TryParse(pessoa, out var idpessoa);
            int.TryParse(cliente, out var idcliente);

            DateTime.TryParse(nasc, out var nascimento);

            int res1 = new Endereco(){
                Id = idendereco,
                Rua = rua,
                Numero = numero,
                Bairro = bairro,
                Complemento = complemento,
                Cep = cep,
                Cidade = new Cidade()
                {
                    Id = cidi
                }
            }.Alterar();

            if (res1 <= 0) return Json("Ocorreu um problema ao alterar o endereço...");
            
            int res2 = new Contato()
            {
                Id = idcontato,
                Telefone = telefone,
                Celular = celular,
                Email = email,
                Endereco = new Endereco()
                {
                    Id = idendereco
                }
            }.Alterar();

            if (res2 <= 0) return Json("Ocorreu um problema ao alterar o contato...");

            int res3 = 0;
            if (tipoi == 1)
            {
                res3 = new PessoaFisica()
                {
                    Id = idpessoa,
                    Nome = nome,
                    Rg = rg,
                    Cpf = cpf,
                    Nascimento = nascimento,
                    Contato = new Contato()
                    {
                        Id = idcontato
                    }
                }.Alterar();    
            }
            else
            {
                res3 = new PessoaJuridica()
                {
                    Id = idpessoa,
                    RazaoSocial = razaosocial,
                    NomeFantasia = nomefantasia,
                    Cnpj = cnpj,
                    Contato = new Contato()
                    {
                        Id = idcontato
                    }
                }.Alterar();
            }

            if (res3 <= 0) return Json("Ocorreu um problema ao alterar a pessoa...");
            
            int res4 = new Cliente()
            {
                Id = idcliente,
                Cadastro = DateTime.Now,
                Tipo = tipoi,
                PessoaFisica = tipoi == 2 ? null : new PessoaFisica()
                {
                    Id = idpessoa,
                    Nome = nome,
                    Rg = rg,
                    Cpf = cpf,
                    Nascimento = nascimento,
                    Contato = new Contato()
                    {
                        Id = idcontato
                    }
                },
                PessoaJuridica = tipoi == 1 ? null : new PessoaJuridica()
                {
                    Id = idpessoa,
                    RazaoSocial = razaosocial,
                    NomeFantasia = nomefantasia,
                    Cnpj = cnpj,
                    Contato = new Contato()
                    {
                        Id = idcontato
                    }
                }
            }.Alterar();

            return Json(res4 <= 0 ? "Ocorreu um problema ao alterar o cliente..." : "");
        }

        [HttpPost]
        public JsonResult Excluir(int id)
        {
            var res = 0;
            var cli = _clientes.Find(c => c.Id == id);
            
            res = new Cliente().Excluir(id);
            if (cli.Tipo == 1)
            {
                res = new PessoaFisica().Excluir(cli.PessoaFisica.Id);
                res = new Endereco().Excluir(cli.PessoaFisica.Contato.Endereco.Id);
            }
            else
            {
                res = new PessoaJuridica().Excluir(cli.PessoaJuridica.Id);
                res = res = new Endereco().Excluir(cli.PessoaJuridica.Contato.Endereco.Id);
            }

            if (res > 0) _clientes.Remove(cli);

            return Json(res);
        }
    }
}