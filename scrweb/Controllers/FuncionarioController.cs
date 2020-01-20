using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using scrweb.Filters;
using scrlib.Controllers;
using scrlib.ViewModels;
using Microsoft.AspNetCore.Http;

namespace scrweb.Controllers
{
    [ValidarUsuario]
    public class FuncionarioController : Controller
    {
        private static List<UsuarioViewModel> _funcs;
        
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
            HttpContext.Session.SetString("idfunc", id.ToString());
            
            return View();
        }

        public ActionResult Dados()
        {
            return View();
        }

        public JsonResult Obter()
        {
            _funcs = new UsuarioController().Get();
            return Json(_funcs);
        }

        [HttpPost]
        public JsonResult ObterPorChave(string chave)
        {
            var filtrado = _funcs.FindAll(u =>
                u.Funcionario.Pessoa.Nome.Contains(chave, StringComparison.CurrentCultureIgnoreCase) ||
                u.Login.Contains(chave, StringComparison.CurrentCultureIgnoreCase) ||
                u.Funcionario.Pessoa.Email.Contains(chave, StringComparison.CurrentCultureIgnoreCase)
            );
            
            return Json(filtrado);
        }

        [HttpPost]
        public JsonResult ObterPorAdmissao(string adm)
        {
            var filtrado = _funcs.FindAll(u =>
                u.Funcionario.Admissao.ToString("yyyy-MM-dd") == adm
            );

            return Json(filtrado);
        }

        [HttpPost]
        public JsonResult ObterPorChaveAdm(string chave, string adm)
        {
            var filtrado = _funcs.FindAll(u =>
                (u.Funcionario.Pessoa.Nome.Contains(chave, StringComparison.CurrentCultureIgnoreCase) ||
                u.Login.Contains(chave, StringComparison.CurrentCultureIgnoreCase) ||
                u.Funcionario.Pessoa.Email.Contains(chave, StringComparison.CurrentCultureIgnoreCase)) &&
                u.Funcionario.Admissao.ToString("yyyy-MM-dd") == adm
            );
            
            return Json(filtrado);
        }

        public JsonResult ObterFuncInfo()
        {
            var id = HttpContext.Session.GetString("idfunc");
            
            return Json(_funcs.Find(u => u.Id == Convert.ToInt32(id)));
        }

        public JsonResult ObterFuncData()
        {
            var id = HttpContext.Session.GetString("id");
            
            return Json(new UsuarioController().GetById(Convert.ToInt32(id)));
        }

        public JsonResult ObterEstados()
        {
            var estados = new EstadoController().Get();
            return Json(estados);
        }

        [HttpPost]
        public JsonResult ObterCidades(IFormCollection form)
        {
            var cidades = new CidadeController().GetByEstado(Convert.ToInt32(form["estado"]));
            return Json(cidades);
        }

        [HttpGet]
        public JsonResult ObterNiveis()
        {
            var niveis = new NivelController().Get();
            return Json(niveis);
        }

        [HttpPost]
        public JsonResult VerificaLogin(string login)
        {
            return Json(new UsuarioController().VerificarLogin(login) == true ? "true" : "false");
        }

        [HttpPost]
        public JsonResult Ordenar(string col)
        {
            var ord = new List<UsuarioViewModel>();

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
                    ord = _funcs.OrderBy(u => u.Funcionario.Pessoa.Email).ToList();
                    break;
                case "18":
                    ord = _funcs.OrderByDescending(u => u.Funcionario.Pessoa.Email).ToList();
                    break;
            }

            return Json(ord);
        }

        [HttpPost]
        public JsonResult VerificarCpf(string cpf)
        {
            return Json(new scrlib.Controllers.PessoaFisicaController().VerifyCpf(cpf));
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

            int res1 = new EnderecoController().Gravar(new EnderecoViewModel()
            {
                Id = 0,
                Rua = rua,
                Numero = numero,
                Bairro = bairro,
                Complemento = complemento,
                Cep = cep,
                Cidade = new CidadeController().GetById(cidade1)
            });

            if (res1 > 0)
            {
                int res2 = new PessoaFisicaController().Gravar(new PessoaFisicaViewModel()
                {
                    Id = 0,
                    Nome = nome,
                    Rg = rg,
                    Cpf = cpf,
                    Nascimento = dataNasc,
                    Tipo = 1,
                    Telefone = telefone,
                    Celular = celular,
                    Email = email,
                    Endereco = new EnderecoController().GetById(res1)
                });

                if (res2 > 0)
                {
                    int res3 = new scrlib.Controllers.FuncionarioController().Gravar(new FuncionarioViewModel()
                    {
                        Id = 0,
                        Tipo = tipo1,
                        Admissao = dataAdm,
                        Demissao = null,
                        Pessoa = new PessoaFisicaController().GetById(res2)
                    });

                    if (res3 > 0 && tipo1 == 1)
                    {
                        int res4 = new UsuarioController().Gravar(new UsuarioViewModel()
                        {
                            Id = 0,
                            Login = login,
                            Senha = senha,
                            Ativo = true,
                            Funcionario = new scrlib.Controllers.FuncionarioController().GetById(res3),
                            Nivel = new NivelController().GetById(nivel1)
                        });

                        if (res4 <= 0)
                        {
                            new scrlib.Controllers.FuncionarioController().Excluir(res3);
                            new PessoaFisicaController().Excluir(res2);
                            new EnderecoController().Excluir(res1);
                            return Json("Ocorreu um problema ao gravar o usuário.");
                        }
                        return Json("");
                    }
                    
                    if (res3 > 0 && tipo1 == 2)
                    {
                        int res4 = new UsuarioController().Gravar(new UsuarioViewModel()
                        {
                            Id = 0,
                            Login = "",
                            Senha = "",
                            Ativo = true,
                            Funcionario = new scrlib.Controllers.FuncionarioController().GetById(res3),
                            Nivel = new NivelController().GetById(3)
                        });

                        if (res4 <= 0)
                        {
                            new scrlib.Controllers.FuncionarioController().Excluir(res3);
                            new PessoaFisicaController().Excluir(res2);
                            new EnderecoController().Excluir(res1);
                            return Json("Ocorreu um problema ao gravar o usuário.");
                        }
                        return Json("");
                    }
                    
                    new PessoaFisicaController().Excluir(res2);
                    new EnderecoController().Excluir(res1);
                    return Json("Ocorreu um problema ao gravar o funcionário.");
                }
                
                new EnderecoController().Excluir(res1);
                return Json("Ocorreu um problema ao gravar a pessoa.");
            }
            
            return Json("Ocorreu um problema ao gravar o Endereço.");
        }
        
        [HttpPost]
        public JsonResult Alterar(IFormCollection form)
        {
            string idendereco = form["idendereco"];
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
            int.TryParse(idpessoa, out int pessoa);
            int.TryParse(idfuncionario, out int func);
            int.TryParse(idusuario, out int usu);
            int.TryParse(tipo, out int tipo1);
            int.TryParse(cidade, out int cidade1);
            int.TryParse(nivel, out int nivel1);
            DateTime.TryParse(nasc, out DateTime dataNasc);
            DateTime.TryParse(adm, out DateTime dataAdm);
            bool.TryParse(ativo, out var ativo1);

            int res1 = new EnderecoController().Alterar(new EnderecoViewModel()
            {
                Id = endereco,
                Rua = rua,
                Numero = numero,
                Bairro = bairro,
                Complemento = complemento,
                Cep = cep,
                Cidade = new CidadeController().GetById(cidade1)
            });

            if (res1 > 0)
            {
                int res2 = new PessoaFisicaController().Alterar(new PessoaFisicaViewModel()
                {
                    Id = pessoa,
                    Nome = nome,
                    Rg = rg,
                    Cpf = cpf,
                    Nascimento = dataNasc,
                    Tipo = 1,
                    Telefone = telefone,
                    Celular = celular,
                    Email = email,
                    Endereco = new EnderecoController().GetById(endereco)
                });

                if (res2 > 0)
                {
                    int res3 = new scrlib.Controllers.FuncionarioController().Alterar(new FuncionarioViewModel()
                    {
                        Id = func,
                        Tipo = tipo1,
                        Admissao = dataAdm,
                        Demissao = null,
                        Pessoa = new PessoaFisicaController().GetById(pessoa)
                    });

                    if (res3 > 0 && tipo1 == 1)
                    {
                        int res4 = new UsuarioController().Alterar(new UsuarioViewModel()
                        {
                            Id = usu,
                            Login = login,
                            Senha = senha,
                            Ativo = ativo1,
                            Funcionario = new scrlib.Controllers.FuncionarioController().GetById(func),
                            Nivel = new NivelController().GetById(nivel1)
                        });

                        return Json(res4 > 0 ? "" : "Ocorreu um problema ao gravar o usuário.");
                    }
                    
                    if (res3 > 0 && tipo1 == 2)
                    {
                        int res4 = new UsuarioController().Alterar(new UsuarioViewModel()
                        {
                            Id = usu,
                            Login = "",
                            Senha = "",
                            Ativo = ativo1,
                            Funcionario = new scrlib.Controllers.FuncionarioController().GetById(func),
                            Nivel = new NivelController().GetById(3)
                        });

                        return Json(res4 > 0 ? "" : "Ocorreu um problema ao gravar o usuário.");
                    }
                    
                    return Json("Ocorreu um problema ao gravar o funcionário.");
                }
                
                return Json("Ocorreu um problema ao gravar a pessoa.");
            }
            
            return Json("Ocorreu um problema ao gravar o Endereço.");
        }

        public JsonResult IsLastAdmin()
        {
            return Json(new UsuarioController().IsLastAdmin());
        }
        
        [HttpPost]
        public JsonResult Excluir(int id)
        {
            var res = 0;
            res = new scrlib.Controllers.UsuarioController().Excluir(id);
            res = new scrlib.Controllers.FuncionarioController().Excluir(id);
            res = new scrlib.Controllers.PessoaFisicaController().Excluir(_funcs.Find(u => u.Id == id).Funcionario.Pessoa.Id);
            res = new scrlib.Controllers.EnderecoController().Excluir(_funcs.Find(u => u.Id == id).Funcionario.Pessoa.Endereco.Id);
            
            return Json(res);
        }

        [HttpPost]
        public JsonResult Desativar(int id)
        {
            return Json(new scrlib.Controllers.FuncionarioController().Desativar(id));
        }

        [HttpPost]
        public JsonResult Reativar(int id)
        {
            return Json(new scrlib.Controllers.FuncionarioController().Reativar(id));
        }
    }
}