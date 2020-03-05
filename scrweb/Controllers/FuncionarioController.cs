using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using scrweb.Filters;
using scrweb.ViewModels;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;
using scrweb.DAO;
using scrweb.Models;

namespace scrweb.Controllers
{
    [ValidarUsuario]
    public class FuncionarioController : Controller
    {
        private static List<Usuario> _funcs;

        public FuncionarioController()
        {
            _funcs = new Usuario().GetAll();
        }
        
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
        
        [HttpPost]
        public JsonResult Enviar(string id)
        {
            if (string.IsNullOrEmpty(id)) return Json("Parâmetro inválido...");
            HttpContext.Session.SetString("idfunc", id);
            return Json("");
        }

        public ActionResult Dados()
        {
            return View();
        }

        public JsonResult Obter()
        {
            if (_funcs == null || _funcs.Count == 0) return Json(new List<Usuario>());
            
            JArray array = new JArray();
            for (int i = 0; i < _funcs.Count; i++)
            {
                array.Add(_funcs[i].ToJObject());
            }
            
            return Json(array);
        }

        [HttpPost]
        public JsonResult ObterPorChave(string chave)
        {
            var filtrado = _funcs.FindAll(u =>
                u.Funcionario.Pessoa.Nome.Contains(chave, StringComparison.CurrentCultureIgnoreCase) ||
                u.Login.Contains(chave, StringComparison.CurrentCultureIgnoreCase) ||
                u.Funcionario.Pessoa.Contato.Email.Contains(chave, StringComparison.CurrentCultureIgnoreCase)
            );
            
            JArray array = new JArray();
            for (int i = 0; i < filtrado.Count; i++)
            {
                array.Add(filtrado[i].ToJObject());
            }
            
            return Json(array);
        }

        [HttpPost]
        public JsonResult ObterPorAdmissao(string adm)
        {
            var filtrado = _funcs.FindAll(u =>
                u.Funcionario.Admissao.ToString("yyyy-MM-dd") == adm
            );

            JArray array = new JArray();
            for (int i = 0; i < filtrado.Count; i++)
            {
                array.Add(filtrado[i].ToJObject());
            }
            
            return Json(array);
        }

        [HttpPost]
        public JsonResult ObterPorChaveAdm(string chave, string adm)
        {
            var filtrado = _funcs.FindAll(u =>
                (u.Funcionario.Pessoa.Nome.Contains(chave, StringComparison.CurrentCultureIgnoreCase) ||
                u.Login.Contains(chave, StringComparison.CurrentCultureIgnoreCase) ||
                u.Funcionario.Pessoa.Contato.Email.Contains(chave, StringComparison.CurrentCultureIgnoreCase)) &&
                u.Funcionario.Admissao.ToString("yyyy-MM-dd") == adm
            );
            
            JArray array = new JArray();
            for (int i = 0; i < filtrado.Count; i++)
            {
                array.Add(filtrado[i].ToJObject());
            }
            
            return Json(array);
        }

        public JsonResult ObterFuncInfo()
        {
            var id = HttpContext.Session.GetString("idfunc");
            
            return Json(_funcs.Find(u => u.Id == Convert.ToInt32(id)).ToJObject());
        }

        public JsonResult ObterFuncData()
        {
            var id = HttpContext.Session.GetString("id");
            
            return Json(new Usuario().GetById(Convert.ToInt32(id)).ToJObject());
        }

        [HttpGet]
        public JsonResult ObterNiveis()
        {
            return Json(new Nivel().GetAll());
        }

        [HttpPost]
        public JsonResult VerificaLogin(string login)
        {
            return Json(new Usuario().VerificarLogin(login) == true ? "true" : "false");
        }

        [HttpPost]
        public JsonResult Ordenar(string col)
        {
            var ord = new List<Usuario>();

            switch (col)
            {
                case "1":
                    ord = _funcs.OrderBy(u => u.Id).ToList();
                    break;
                case "2":
                    ord = _funcs.OrderByDescending(u => u.Id).ToList();
                    break;
                case "3":
                    ord = _funcs.OrderBy(u => u.Funcionario.Pessoa.Nome).ToList();
                    break;
                case "4":
                    ord = _funcs.OrderByDescending(u => u.Funcionario.Pessoa.Nome).ToList();
                    break;
                case "5":
                    ord = _funcs.OrderBy(u => u.Login).ToList();
                    break;
                case "6":
                    ord = _funcs.OrderByDescending(u => u.Login).ToList();
                    break;
                case "7":
                    ord = _funcs.OrderBy(u => u.Nivel.Id).ToList();
                    break;
                case "8":
                    ord = _funcs.OrderByDescending(u => u.Nivel.Id).ToList();
                    break;
                case "9":
                    ord = _funcs.OrderBy(u => u.Funcionario.Pessoa.Cpf).ToList();
                    break;
                case "10":
                    ord = _funcs.OrderByDescending(u => u.Funcionario.Pessoa.Cpf).ToList();
                    break;
                case "11":
                    ord = _funcs.OrderBy(u => u.Funcionario.Admissao).ToList();
                    break;
                case "12":
                    ord = _funcs.OrderByDescending(u => u.Funcionario.Admissao).ToList();
                    break;
                case "13":
                    ord = _funcs.OrderBy(u => u.Funcionario.Tipo).ToList();
                    break;
                case "14":
                    ord = _funcs.OrderByDescending(u => u.Funcionario.Tipo).ToList();
                    break;
                case "15":
                    ord = _funcs.OrderBy(u => u.Ativo).ToList();
                    break;
                case "16":
                    ord = _funcs.OrderByDescending(u => u.Ativo).ToList();
                    break;
                case "17":
                    ord = _funcs.OrderBy(u => u.Funcionario.Pessoa.Contato.Email).ToList();
                    break;
                case "18":
                    ord = _funcs.OrderByDescending(u => u.Funcionario.Pessoa.Contato.Email).ToList();
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
            string nome = form["nome"];
            string nasc = form["nasc"];
            string rg = form["rg"];
            string cpf = form["cpf"];
            string adm = form["adm"];
            string tipo = form["tipo"];
            string rua = form["rua"];
            string numero = form["numero"];
            string bairro = form["bairro"];
            string complemento = form["complemento"];
            string cep = form["cep"];
            string cidade = form["cidade"];
            string telefone = form["telefone"];
            string celular = form["celular"];
            string email = form["email"];
            string nivel = "";
            string login = "";
            string senha = "";
            if (tipo != "2")
            {
                nivel = form["nivel"];
                login = form["login"];
                senha = form["senha"];
            }

            int.TryParse(tipo, out int tipo1);
            int.TryParse(cidade, out int cidade1);
            int.TryParse(nivel, out int nivel1);
            DateTime.TryParse(nasc, out DateTime dataNasc);
            DateTime.TryParse(adm, out DateTime dataAdm);

            int res1 = new Endereco(){
                Id = 0,
                Rua = rua,
                Numero = numero,
                Bairro = bairro,
                Complemento = complemento,
                Cep = cep,
                Cidade = new Cidade()
                {
                    Id = cidade1
                }
            }.Gravar();

            if (res1 <= 0) return Json("Ocorreu um problema ao gravar o endereço.");
            
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
                return Json("Ocorreu um problema ao gravar o contato.");
            }

            int res3 = new PessoaFisica()
            {
                Id = 0,
                Nome = nome,
                Rg = rg,
                Cpf = cpf,
                Nascimento = dataNasc,
                Contato = new Contato()
                {
                    Id = res2
                }
            }.Gravar();
            
            if (res3 <= 0)
            {
                new Contato().Excluir(res2);
                new Endereco().Excluir(res1);
                return Json("Ocorreu um problema ao gravar a pessoa.");
            }
            
            int res4 = new Funcionario()
            {
                Id = 0,
                Tipo = tipo1,
                Admissao = dataAdm,
                Demissao = null,
                Pessoa = new PessoaFisica()
                {
                    Id = res3
                }
            }.Gravar();
            
            if (res4 <= 0)
            {
                new PessoaFisica().Excluir(res3);
                new Contato().Excluir(res2);
                new Endereco().Excluir(res1);
                return Json("Ocorreu um problema ao gravar o funcionário.");
            }

            int res5 = 0;
            if (tipo1 == 1)
            {
                res5 = new Usuario()
                {
                    Id = 0,
                    Login = login,
                    Senha = senha,
                    Ativo = true,
                    Funcionario = new Funcionario()
                    {
                        Id = res4
                    },
                    Nivel = new Nivel()
                    {
                        Id = nivel1
                    }
                }.Gravar();
            }
            else
            {
                res5 = new Usuario()
                {
                    Id = 0,
                    Login = "",
                    Senha = "",
                    Ativo = true,
                    Funcionario = new Funcionario()
                    {
                        Id = res4
                    },
                    Nivel = new Nivel()
                    {
                        Id = 3
                    }
                }.Gravar();
            }

            if (res5 <= 0)
            {
                new Funcionario().Excluir(res4);
                new PessoaFisica().Excluir(res3);
                new Contato().Excluir(res2);
                new Endereco().Excluir(res1);
                return Json("Ocorreu um problema ao gravar o usuário.");
            }

            return Json("");
        }
        
        [HttpPost]
        public JsonResult Alterar(IFormCollection form)
        {
            string idendereco = form["idendereco"];
            string idcontato = form["idcontato"];
            string idpessoa = form["idpessoa"];
            string idfuncionario = form["idfuncionario"];
            string idusuario = form["idusuario"];
            
            string ativo = form["ativo"];
            
            string nome = form["nome"];
            string nasc = form["nasc"];
            string rg = form["rg"];
            string cpf = form["cpf"];
            string adm = form["adm"];
            string tipo = form["tipo"];
            string rua = form["rua"];
            string numero = form["numero"];
            string bairro = form["bairro"];
            string complemento = form["complemento"];
            string cep = form["cep"];
            string cidade = form["cidade"];
            string telefone = form["telefone"];
            string celular = form["celular"];
            string email = form["email"];
            string nivel = "";
            string login = "";
            string senha = "";
            
            if (tipo != "2")
            {
                nivel = form["nivel"];
                login = form["login"];
                senha = form["senha"];
            }

            int.TryParse(idendereco, out int endereco);
            int.TryParse(idcontato, out int contato);
            int.TryParse(idpessoa, out int pessoa);
            int.TryParse(idfuncionario, out int func);
            int.TryParse(idusuario, out int usu);
            int.TryParse(tipo, out int tipo1);
            int.TryParse(cidade, out int cidade1);
            int.TryParse(nivel, out int nivel1);
            DateTime.TryParse(nasc, out DateTime dataNasc);
            DateTime.TryParse(adm, out DateTime dataAdm);
            bool.TryParse(ativo, out var ativo1);

            int res1 = new Endereco(){
                Id = endereco,
                Rua = rua,
                Numero = numero,
                Bairro = bairro,
                Complemento = complemento,
                Cep = cep,
                Cidade = new Cidade()
                {
                    Id = cidade1
                }
            }.Alterar();

            if (res1 <= 0) return Json("Ocorreu um problema ao alterar o endereço.");
            
            int res2 = new Contato()
            {
                Id = contato,
                Telefone = telefone,
                Celular = celular,
                Email = email,
                Endereco = new Endereco()
                {
                    Id = endereco
                }
            }.Alterar();

            if (res2 <= 0) return Json("Ocorreu um problema ao alterar o contato.");

            int res3 = new PessoaFisica()
            {
                Id = pessoa,
                Nome = nome,
                Rg = rg,
                Cpf = cpf,
                Nascimento = dataNasc,
                Contato = new Contato()
                {
                    Id = contato
                }
            }.Alterar();
            
            if (res3 <= 0) return Json("Ocorreu um problema ao alterar a pessoa.");
            
            int res4 = new Funcionario()
            {
                Id = func,
                Tipo = tipo1,
                Admissao = dataAdm,
                Demissao = null,
                Pessoa = new PessoaFisica()
                {
                    Id = pessoa
                }
            }.Alterar();
            
            if (res4 <= 0) return Json("Ocorreu um problema ao alterar o funcionário.");

            int res5 = 0;
            if (tipo1 == 1)
            {
                res5 = new Usuario()
                {
                    Id = usu,
                    Login = login,
                    Senha = senha,
                    Ativo = ativo1,
                    Funcionario = new Funcionario()
                    {
                        Id = func
                    },
                    Nivel = new Nivel()
                    {
                        Id = nivel1
                    }
                }.Alterar();
            }
            else
            {
                res5 = new Usuario()
                {
                    Id = usu,
                    Login = "",
                    Senha = "",
                    Ativo = false,
                    Funcionario = new Funcionario()
                    {
                        Id = func
                    },
                    Nivel = new Nivel()
                    {
                        Id = 3
                    }
                }.Alterar();
            }

            return Json(res5 <= 0 ? "Ocorreu um problema ao alterar o usuário." : "");
        }

        public JsonResult IsLastAdmin()
        {
            return Json(new Usuario().IsLastAdmin());
        }
        
        [HttpPost]
        public JsonResult Excluir(int id)
        {
            var res = 0;
            var func = _funcs.Find(u => u.Id == id);
            
            res = new Usuario().Excluir(id);
            if (res <= 0) return Json("Ocorreu um problema ao excluir o usuário...");
            
            res = new Funcionario().Excluir(id);
            if (res <= 0) return Json("Ocorreu um problema ao excluir o funcionário...");
            
            res = new PessoaFisica().Excluir(func.Funcionario.Pessoa.Id);
            if (res <= 0) return Json("Ocorreu um problema ao excluir a pessoa...");
            
            res = new Contato().Excluir(func.Funcionario.Pessoa.Contato.Id);
            if (res <= 0) return Json("Ocorreu um problema ao excluir o contato...");
            
            res = new Endereco().Excluir(func.Funcionario.Pessoa.Contato.Endereco.Id);
            if (res <= 0) return Json("Ocorreu um problema ao excluir o endereço...");

            _funcs.Remove(func);

            return Json("");
        }

        [HttpPost]
        public JsonResult Desativar(int id)
        {
            return Json(new Funcionario().Desativar(id));
        }

        [HttpPost]
        public JsonResult Reativar(int id)
        {
            return Json(new Funcionario().Reativar(id));
        }
    }
}