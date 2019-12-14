using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using scrweb.Filters;
using scrlib.Controllers;
using scrlib.ViewModels;
using Microsoft.AspNetCore.Http;

namespace scrweb.Controllers
{
    public class FuncionarioController : Controller
    {
        [ValidarUsuario]
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [ValidarUsuario]
        [HttpGet]
        public IActionResult Novo()
        {
            return View();
        }

        [ValidarUsuario]
        [HttpGet]
        public JsonResult Obter()
        {
            var funcionarios = new UsuarioController().Get();
            return Json(funcionarios);
        }

        [ValidarUsuario]
        [HttpGet]
        public JsonResult ObterEstados()
        {
            var estados = new EstadoController().Get();
            return Json(estados);
        }

        [ValidarUsuario]
        [HttpPost]
        public JsonResult ObterCidadesPorEstado(string estado)
        {
            var cidades = new CidadeController().GetByEstado(Convert.ToInt32(estado));
            return Json(cidades);
        }

        [ValidarUsuario]
        [HttpGet]
        public JsonResult ObterNiveis()
        {
            var niveis = new NivelController().Get();
            return Json(niveis);
        }

        [ValidarUsuario]
        [HttpPost]
        public JsonResult VerificaLogin(string login)
        {
            if (new UsuarioController().VerificarLogin(login) == true)
            {
                return Json("true");
            }
            else
            {
                return Json("false");
            }
        }

        [ValidarUsuario]
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
                            Funcionario = new scrlib.Controllers.FuncionarioController().GetById(res3),
                            Nivel = new NivelController().GetById(nivel1)
                        });

                        if (res4 > 0)
                        {
                            return Json("");
                        }
                        else
                        {
                            return Json("Ocorreu um problema ao gravar o usuário.");
                        }
                    }
                    else
                    if (res3 > 0 && tipo1 == 2)
                    {
                        return Json("");
                    }
                    else
                    {
                        return Json("Ocorreu um problema ao gravar o funcionário.");
                    }
                }
                else
                {
                    return Json("Ocorreu um problema ao gravar a pessoa.");
                }
            }
            else
            {
                return Json("Ocorreu um problema ao gravar o Endereço.");
            }
        }

        [ValidarUsuario]
        [HttpPost]
        public JsonResult Excluir(int id)
        {
            int res = new scrlib.Controllers.FuncionarioController().Excluir(id);
            return Json(res);
        }
    }
}