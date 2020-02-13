using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using scrweb.ViewModels;
using scrweb.Filters;
using scrweb.ModelControllers;

namespace scrweb.Controllers
{
    [ValidarUsuario]
    public class RepresentacaoController : Controller
    {
        private static List<RepresentacaoViewModel> _representacoes;
        
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
            _representacoes = new RepresentacaoModelController().GetAll();
            return Json(_representacoes);
        }
        
        [HttpPost]
        public JsonResult ObterPorChave(string chave)
        {
            var filtrado = _representacoes.FindAll(r => 
                r.Pessoa.NomeFantasia.Contains(chave, StringComparison.CurrentCultureIgnoreCase) ||
                r.Pessoa.Email.Contains(chave, StringComparison.CurrentCultureIgnoreCase)
            );
            
            return Json(filtrado);
        }

        [HttpPost]
        public JsonResult ObterPorCadastro(string cad)
        {
            var filtrado = _representacoes.FindAll(r => r.Cadastro.ToString("yyyy-MM-dd") == cad);
            
            return Json(filtrado);
        }

        [HttpPost]
        public JsonResult ObterPorChaveCad(string chave, string cad)
        {
            var filtrado = _representacoes.FindAll(r => 
                (r.Pessoa.NomeFantasia.Contains(chave, StringComparison.CurrentCultureIgnoreCase) || 
                 r.Pessoa.Email.Contains(chave, StringComparison.CurrentCultureIgnoreCase)) && 
                r.Cadastro.ToString("yyyy-MM-dd") == cad
            );
            
            return Json(filtrado);
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
            
            return Json(new RepresentacaoModelController().GetById(Convert.ToInt32(id)));
        }
        
        public JsonResult ObterEstados()
        {
            return Json(new EstadoModelController().Get());
        }

        [HttpPost]
        public JsonResult ObterCidades(IFormCollection form)
        {
            return Json(new CidadeModelController().GetByEstado(Convert.ToInt32(form["estado"])));
        }
        
        [HttpPost]
        public JsonResult Ordenar(string col)
        {
            var ord = new List<RepresentacaoViewModel>();

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
                    ord = _representacoes.OrderBy(r => r.Pessoa.Email).ToList();
                    break;
                case "12":
                    ord = _representacoes.OrderByDescending(r => r.Pessoa.Email).ToList();
                    break;
            }

            return Json(ord);
        }
        
        [HttpPost]
        public JsonResult VerificarCnpj(string cnpj)
        {
            return Json(new PessoaJuridicaModelController().VerifyCnpj(cnpj));
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

            var city = new CidadeModelController().GetById(cid);
            
            var res1 = new EnderecoModelController().Gravar(new EnderecoViewModel()
            {
                Id = 0,
                Rua = rua,
                Numero = numero,
                Bairro = bairro,
                Complemento = complemento,
                Cep = cep,
                Cidade = city
            });

            if (res1 == -10) return Json("Ocorreu um problema na execução do comando SQL.");
            if (res1 == -5) return Json("Ocorreu um problema: um ou mais campos inválidos...");
            
            var res2 = new PessoaJuridicaModelController().Gravar(new PessoaJuridicaViewModel()
            {
                Id = 0,
                RazaoSocial = razaosocial,
                NomeFantasia = nomefantasia,
                Cnpj = cnpj,
                Tipo = 2,
                Telefone = telefone,
                Celular = celular,
                Email = email,
                Endereco = new EnderecoViewModel()
                {
                    Id = res1,
                    Rua = rua,
                    Numero = numero,
                    Bairro = bairro,
                    Complemento = complemento,
                    Cep = cep,
                    Cidade = city
                }
            });

            if (res2 == -10)
            {
                new EnderecoModelController().Excluir(res1);
                return Json("Ocorreu um problema na execução do comando SQL.");
            }

            if (res2 == -5)
            {
                new EnderecoModelController().Excluir(res1);
                return Json("Ocorreu um problema: um ou mais campos inválidos...");
            }
            
            var res3 = new RepresentacaoModelController().Gravar(new RepresentacaoViewModel()
            {
                Id = 0,
                Cadastro = DateTime.Now,
                Unidade = city.Nome+"/"+city.Estado.Sigla,
                Pessoa = new PessoaJuridicaViewModel()
                {
                    Id = res2,
                    RazaoSocial = razaosocial,
                    NomeFantasia = nomefantasia,
                    Cnpj = cnpj,
                    Tipo = 2,
                    Telefone = telefone,
                    Celular = celular,
                    Email = email,
                    Endereco = new EnderecoViewModel()
                    {
                        Id = res1,
                        Rua = rua,
                        Numero = numero,
                        Bairro = bairro,
                        Complemento = complemento,
                        Cep = cep,
                        Cidade = city
                    }
                }
            });
            
            if (res3 == -10)
            {
                new PessoaJuridicaModelController().Excluir(res2);
                new EnderecoModelController().Excluir(res1);
                return Json("Ocorreu um problema na execução do comando SQL.");
            }

            if (res3 == -5)
            {
                new PessoaJuridicaModelController().Excluir(res2);
                new EnderecoModelController().Excluir(res1);
                return Json("Ocorreu um problema: um ou mais campos inválidos...");
            }

            return Json("");
        }

        [HttpPost]
        public JsonResult Alterar(IFormCollection form)
        {
            var endereco = form["endereco"];
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
            int.TryParse(pessoa, out var pes);
            int.TryParse(representacao, out var rep);
            int.TryParse(cidade, out var cid);

            var city = new CidadeModelController().GetById(cid);
            
            var res1 = new EnderecoModelController().Alterar(new EnderecoViewModel()
            {
                Id = end,
                Rua = rua,
                Numero = numero,
                Bairro = bairro,
                Complemento = complemento,
                Cep = cep,
                Cidade = city
            });

            if (res1 < 0) return Json("Ocorreu um problema na alteração do endereço...");
            
            var res2 = new PessoaJuridicaModelController().Alterar(new PessoaJuridicaViewModel()
            {
                Id = pes,
                RazaoSocial = razaosocial,
                NomeFantasia = nomefantasia,
                Cnpj = cnpj,
                Tipo = 2,
                Telefone = telefone,
                Celular = celular,
                Email = email,
                Endereco = new EnderecoViewModel()
                {
                    Id = end,
                    Rua = rua,
                    Numero = numero,
                    Bairro = bairro,
                    Complemento = complemento,
                    Cep = cep,
                    Cidade = city
                }
            });
            
            if (res2 < 0) return Json("Ocorreu um problema na alteração da pessoa...");
            
            var res3 = new RepresentacaoModelController().Alterar(new RepresentacaoViewModel()
            {
                Id = rep,
                Cadastro = DateTime.Now,
                Unidade = city.Nome+"/"+city.Estado.Sigla,
                Pessoa = new PessoaJuridicaViewModel()
                {
                    Id = pes,
                    RazaoSocial = razaosocial,
                    NomeFantasia = nomefantasia,
                    Cnpj = cnpj,
                    Tipo = 2,
                    Telefone = telefone,
                    Celular = celular,
                    Email = email,
                    Endereco = new EnderecoViewModel()
                    {
                        Id = end,
                        Rua = rua,
                        Numero = numero,
                        Bairro = bairro,
                        Complemento = complemento,
                        Cep = cep,
                        Cidade = city
                    }
                }
            });
        
            return Json(res3 < 0 ? "Ocorreu um problema na alteração da representação..." : "");
        }

        [HttpPost]
        public JsonResult Excluir(int id)
        {
            var res = new RepresentacaoModelController().Excluir(id);
            if (res < 0) return Json("Ocorreu um problema na exclusão da representação...");
            
            res = new PessoaJuridicaModelController().Excluir(_representacoes.Find(r => r.Id == id).Pessoa.Id);
            if (res < 0) return Json("Ocorreu um problema na exclusão da pessoa...");

            res = new EnderecoModelController().Excluir(_representacoes.Find(r => r.Id == id).Pessoa.Endereco.Id);
            return Json(res < 0 ? "Ocorreu um problema na exclusão do endereço..." : "");
        }
    }
}