using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using scrweb.Models;

namespace scrweb.Controllers
{
    public class MotoristaController : Controller
    {
        private static List<Motorista> _motoristas;

        public MotoristaController()
        {
            _motoristas = new Motorista().GetAll();
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
        
        [HttpPost]
        public JsonResult Enviar(string id)
        {
            if (string.IsNullOrEmpty(id)) return Json("Parâmetro inválido...");
            HttpContext.Session.SetString("idcli", id);
            return Json("");
        }

        public JsonResult Obter()
        {
            if (_motoristas == null || _motoristas.Count == 0) return Json(new List<Motorista>());
            
            JArray array = new JArray();
            for (int i = 0; i < _motoristas.Count; i++)
            {
                array.Add(_motoristas[i].ToJObject());
            }
            
            return Json(array);
        }

        [HttpPost]
        public JsonResult ObterPorChave(string chave)
        {
            var filtrado = _motoristas.FindAll(cli => 
                cli.Pessoa.Nome.Contains(chave, StringComparison.CurrentCultureIgnoreCase) ||
                cli.Pessoa.Contato.Email.Contains(chave, StringComparison.CurrentCultureIgnoreCase)
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
            var filtrado = _motoristas.FindAll(cli => 
                cli.Cadastro.ToString("yyyy-MM-dd") == cad
            );
            
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
            var filtrado = _motoristas.FindAll(cli => 
                (
                    cli.Pessoa.Nome.Contains(chave, StringComparison.CurrentCultureIgnoreCase) ||
                    cli.Pessoa.Contato.Email.Contains(chave, StringComparison.CurrentCultureIgnoreCase)
                ) && cli.Cadastro.ToString("yyyy-MM-dd") == cad
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

            var cli = _motoristas.Find(c => c.Id == Convert.ToInt32(id));
            
            return Json(cli.ToJObject());
        }

        [HttpPost]
        public JsonResult Ordenar(string col)
        {
            var ord = new List<Motorista>();

            switch (col)
            {
                case "1":
                    ord = _motoristas.OrderBy(m => m.Id).ToList();
                    break;
                case "2":
                    ord = _motoristas.OrderByDescending(m => m.Id).ToList();
                    break;
                case "3":
                    ord = _motoristas.OrderBy(m => m.Pessoa.Nome).ToList();
                    break;
                case "4":
                    ord = _motoristas.OrderByDescending(m => m.Pessoa.Nome).ToList();
                    break;
                case "5":
                    ord = _motoristas.OrderBy(m => m.Pessoa.Cpf).ToList();
                    break;
                case "6":
                    ord = _motoristas.OrderByDescending(m => m.Pessoa.Cpf).ToList();
                    break;
                case "7":
                    ord = _motoristas.OrderBy(m => m.Cadastro).ToList();
                    break;
                case "8":
                    ord = _motoristas.OrderByDescending(m => m.Cadastro).ToList();
                    break;
                case "9":
                    ord = _motoristas.OrderBy(m => m.Pessoa.Contato.Email).ToList();
                    break;
                case "10":
                    ord = _motoristas.OrderByDescending(m => m.Pessoa.Contato.Email).ToList();
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
        public JsonResult Gravar(IFormCollection form)
        {
            var nome = form["nome"];
            var rg = form["rg"];
            var cpf = form["cpf"];
            var nasc = form["nasc"];
            var rua = form["rua"];
            var numero = form["numero"];
            var bairro = form["bairro"];
            var complemento = form["complemento"];
            var cep = form["cep"];
            var cidade = form["cidade"];
            var telefone = form["telefone"];
            var celular = form["celular"];
            var email = form["email"];

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

            int res3 = new PessoaFisica()
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

            if (res3 <= 0)
            {
                new Contato().Excluir(res2);
                new Endereco().Excluir(res1);
                return Json("Ocorreu um problema ao gravar a pessoa...");
            }
            
            int res4 = new Motorista()
            {
                Id = 0,
                Cadastro = DateTime.Now,
                Pessoa = new PessoaFisica()
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
                }
            }.Gravar();
            
            if (res4 <= 0)
            {
                new PessoaFisica().Excluir(res3);
                new Contato().Excluir(res2);
                new Endereco().Excluir(res1);
                return Json("Ocorreu um problema ao gravar o motorista...");
            }

            return Json("");
        }
        
        [HttpPost]
        public JsonResult Excluir(int id)
        {
            var res = 0;
            var mot = _motoristas.Find(m => m.Id == id);
            
            res = new Motorista().Excluir(id); 
            res = new PessoaFisica().Excluir(mot.Pessoa.Id);
            res = new Contato().Excluir(mot.Pessoa.Contato.Id);
            res = new Endereco().Excluir(mot.Pessoa.Contato.Endereco.Id);

            if (res > 0) _motoristas.Remove(mot);

            return Json(res);
        }
    }
}