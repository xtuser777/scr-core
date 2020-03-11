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
    public class RepresentacaoController : Controller
    {
        private static List<Representacao> _representacoes;

        public RepresentacaoController()
        {
            _representacoes = new Representacao().GetAll();
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

        public IActionResult Detalhes()
        {
            return View();
        }

        public IActionResult AddUnidade()
        {
            return View();
        }

        public JsonResult Obter()
        {
            if (_representacoes == null || _representacoes.Count == 0) return Json(new List<Representacao>());
            
            JArray array = new JArray();
            for (int i = 0; i < _representacoes.Count; i++)
            {
                array.Add(_representacoes[i].ToJObject());
            }
            
            return Json(array);
        }
        
        [HttpPost]
        public JsonResult ObterPorChave(string chave)
        {
            var filtrado = _representacoes.FindAll(r => 
                r.Pessoa.NomeFantasia.Contains(chave, StringComparison.CurrentCultureIgnoreCase) ||
                r.Pessoa.Contato.Email.Contains(chave, StringComparison.CurrentCultureIgnoreCase)
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
            var filtrado = _representacoes.FindAll(r => r.Cadastro.ToString("yyyy-MM-dd") == cad);
            
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
            var filtrado = _representacoes.FindAll(r => 
                (r.Pessoa.NomeFantasia.Contains(chave, StringComparison.CurrentCultureIgnoreCase) || 
                 r.Pessoa.Contato.Email.Contains(chave, StringComparison.CurrentCultureIgnoreCase)) && 
                r.Cadastro.ToString("yyyy-MM-dd") == cad
            );
            
            JArray array = new JArray();
            for (int i = 0; i < filtrado.Count; i++)
            {
                array.Add(filtrado[i].ToJObject());
            }
            
            return Json(array);
        }

        [HttpPost]
        public JsonResult Enviar(string id)
        {
            HttpContext.Session.SetString("idrep", id);

            return Json(string.IsNullOrEmpty(id) ? "Parâmetro inválido" : "");
        }
        
        public JsonResult ObterDetalhes()
        {
            var id = HttpContext.Session.GetString("idrep");
            
            return Json(new Representacao().GetById(Convert.ToInt32(id)).ToJObject());
        }
        
        [HttpPost]
        public JsonResult Ordenar(string col)
        {
            var ord = new List<Representacao>();

            switch (col)
            {
                case "1":
                    ord = _representacoes.OrderBy(r => r.Id).ToList();
                    break;
                case "2":
                    ord = _representacoes.OrderByDescending(r => r.Id).ToList();
                    break;
                case "3":
                    ord = _representacoes.OrderBy(r => r.Pessoa.NomeFantasia).ToList();
                    break;
                case "4":
                    ord = _representacoes.OrderByDescending(r => r.Pessoa.NomeFantasia).ToList();
                    break;
                case "5":
                    ord = _representacoes.OrderBy(r => r.Pessoa.Cnpj).ToList();
                    break;
                case "6":
                    ord = _representacoes.OrderByDescending(r => r.Pessoa.Cnpj).ToList();
                    break;
                case "7":
                    ord = _representacoes.OrderBy(r => r.Cadastro).ToList();
                    break;
                case "8":
                    ord = _representacoes.OrderByDescending(r => r.Cadastro).ToList();
                    break;
                case "9":
                    ord = _representacoes.OrderBy(r => r.Unidade).ToList();
                    break;
                case "10":
                    ord = _representacoes.OrderByDescending(r => r.Unidade).ToList();
                    break;
                case "11":
                    ord = _representacoes.OrderBy(r => r.Pessoa.Contato.Email).ToList();
                    break;
                case "12":
                    ord = _representacoes.OrderByDescending(r => r.Pessoa.Contato.Email).ToList();
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
        public JsonResult VerificarCnpj(string cnpj)
        {
            return Json(new PessoaJuridica().VerifyCnpj(cnpj));
        }

        [HttpPost]
        public JsonResult Gravar(IFormCollection form)
        {
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

            int.TryParse(cidade, out var cid);

            var city = new Cidade().GetById(cid);
            
            var res1 = new Endereco()
            {
                Id = 0,
                Rua = rua,
                Numero = numero,
                Bairro = bairro,
                Complemento = complemento,
                Cep = cep,
                Cidade = city
            }.Gravar();

            if (res1 == -10) return Json("Ocorreu um problema na execução do comando SQL.");
            if (res1 == -5) return Json("Ocorreu um problema: um ou mais campos inválidos...");

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
            
            if (res2 == -10)
            {
                new Endereco().Excluir(res1);
                return Json("Ocorreu um problema na execução do comando SQL.");
            }

            if (res2 == -5)
            {
                new Endereco().Excluir(res1);
                return Json("Ocorreu um problema: um ou mais campos inválidos...");
            }
            
            var res3 = new PessoaJuridica()
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

            if (res3 == -10)
            {
                new Contato().Excluir(res2);
                new Endereco().Excluir(res1);
                return Json("Ocorreu um problema na execução do comando SQL.");
            }

            if (res3 == -5)
            {
                new Contato().Excluir(res2);
                new Endereco().Excluir(res1);
                return Json("Ocorreu um problema: um ou mais campos inválidos...");
            }
            
            var res4 = new Representacao()
            {
                Id = 0,
                Cadastro = DateTime.Now,
                Unidade = city.Nome+"/"+city.Estado.Sigla,
                Pessoa = new PessoaJuridica()
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
            
            if (res4 == -10)
            {
                new PessoaJuridica().Excluir(res3);
                new Contato().Excluir(res2);
                new Endereco().Excluir(res1);
                return Json("Ocorreu um problema na execução do comando SQL.");
            }

            if (res4 == -5)
            {
                new PessoaJuridica().Excluir(res3);
                new Contato().Excluir(res2);
                new Endereco().Excluir(res1);
                return Json("Ocorreu um problema: um ou mais campos inválidos...");
            }

            return Json("");
        }

        [HttpPost]
        public JsonResult Alterar(IFormCollection form)
        {
            var endereco = form["endereco"];
            var contato = form["contato"];
            var pessoa = form["pessoa"];
            var representacao = form["representacao"];
            
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

            int.TryParse(endereco, out var end);
            int.TryParse(contato, out int con);
            int.TryParse(pessoa, out var pes);
            int.TryParse(representacao, out var rep);
            int.TryParse(cidade, out var cid);

            var city = new Cidade().GetById(cid);
            
            var res1 = new Endereco()
            {
                Id = 0,
                Rua = rua,
                Numero = numero,
                Bairro = bairro,
                Complemento = complemento,
                Cep = cep,
                Cidade = city
            }.Alterar();

            if (res1 < 0) return Json("Ocorreu um problema na alteração do endereço...");

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
            }.Alterar();
            
            if (res2 < 0) return Json("Ocorreu um problema na alteração do contato...");
            
            var res3 = new PessoaJuridica()
            {
                Id = 0,
                RazaoSocial = razaosocial,
                NomeFantasia = nomefantasia,
                Cnpj = cnpj,
                Contato = new Contato()
                {
                    Id = res2
                }
            }.Alterar();

            if (res3 < 0) return Json("Ocorreu um problema na alteração da pessoa...");
            
            var res4 = new Representacao()
            {
                Id = 0,
                Cadastro = DateTime.Now,
                Unidade = city.Nome+"/"+city.Estado.Sigla,
                Pessoa = new PessoaJuridica()
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
            }.Alterar();
        
            return Json(res4 < 0 ? "Ocorreu um problema na alteração da representação..." : "");
        }

        [HttpPost]
        public JsonResult Excluir(int id)
        {
            var rep = _representacoes.Find(r => r.Id == id);
            
            var res = new Representacao().Excluir(id);
            if (res < 0) return Json("Ocorreu um problema na exclusão da representação...");
            
            res = new PessoaJuridica().Excluir(rep.Pessoa.Id);
            if (res < 0) return Json("Ocorreu um problema na exclusão da pessoa...");

            res = new Endereco().Excluir(rep.Pessoa.Contato.Endereco.Id);
            if (res < 0) return Json("Ocorreu um problema na exclusão do endereço...");
            
            res = new Contato().Excluir(rep.Pessoa.Contato.Id);
            if (res < 0) return Json("Ocorreu um problema na exclusão do contato...");

            _representacoes.Remove(rep);

            return Json("");
        }
    }
}