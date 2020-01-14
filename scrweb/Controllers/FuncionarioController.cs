using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using scrweb.Filters;
using scrlib.Controllers;
using scrlib.ViewModels;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;

namespace scrweb.Controllers
{
    [SuppressMessage("ReSharper", "IdentifierTypo")]
    [ValidarUsuario]
    public class FuncionarioController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Novo()
        {
            return View();
        }

        /*[HttpGet("/Funcionario/Detalhes/{id}", Name = "User_Info")]*/
        public IActionResult Detalhes(int id)
        {
            //ViewData["id"] = id;
            HttpContext.Session.SetInt32("idfunc", id);
            return View("Detalhes");
        }

        public ActionResult Dados()
        {
            return View("Dados");
        }

        public JsonResult Obter()
        {
            var funcionarios = new UsuarioController().Get();
            return Json(funcionarios);
        }

        [HttpPost]
        public JsonResult ObterPorChave(string chave)
        {
            return Json(new UsuarioController().GetByKey(chave));
        }

        [HttpPost]
        public JsonResult ObterPorAdmissao(string adm)
        {
            DateTime.TryParse(adm, out var admissao);

            return Json(new UsuarioController().GetByAdm(admissao));
        }

        [HttpPost]
        public JsonResult ObterPorChaveAdm(string chave, string adm)
        {
            DateTime.TryParse(adm, out var admissao);

            return Json(new UsuarioController().GetByKeyAndAdm(chave,admissao));
        }

        public JsonResult ObterFuncInfo()
        {
            var id = HttpContext.Session.GetInt32("idfunc");
            return Json(new UsuarioController().GetById((int)id));
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
            int res = new scrlib.Controllers.FuncionarioController().Excluir(id);
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