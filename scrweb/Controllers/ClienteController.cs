using System;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using scrlib.ViewModels;
using scrweb.Filters;
using cl = scrlib.Controllers;

namespace scrweb.Controllers
{
    [ValidarUsuario]
    public class ClienteController : Controller
    {
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
            HttpContext.Session.SetInt32("idcli", id);

            return View();
        }

        public JsonResult Obter()
        {
            return Json(new cl.ClienteController().GetAll());
        }

        public JsonResult ObterPorChave(string chave)
        {
            return Json(new cl.ClienteController().GetByFilter(chave));
        }

        public JsonResult ObterPorCadastro(string cad)
        {
            return Json(new cl.ClienteController().GetByCad(Convert.ToDateTime(cad)));
        }

        public JsonResult ObterPorChaveCad(string chave, string cad)
        {
            return Json(new cl.ClienteController().GetByFilterAndCad(chave, Convert.ToDateTime(cad)));
        }

        public JsonResult ObterDetalhes()
        {
            var id = (int)HttpContext.Session.GetInt32("idcli");
            
            return Json(new cl.ClienteController().GetById(id));
        }

        public JsonResult ObterEstados()
        {
            return Json(new cl.EstadoController().Get());
        }

        [HttpPost]
        public JsonResult ObterCidades(IFormCollection form)
        {
            return Json(new cl.CidadeController().GetByEstado(Convert.ToInt32(form["estado"])));
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

            var res1 = new cl.EnderecoController().Gravar(new EnderecoViewModel()
            {
                Id = 0,
                Rua = rua,
                Numero = numero,
                Bairro = bairro,
                Complemento = complemento,
                Cep = cep,
                Cidade = new CidadeViewModel()
                {
                    Id = cidi,
                    Nome = "",
                    Estado = null
                }
            });

            if (res1 <= 0) return Json("Ocorreu um problema ao gravar o endereço...");

            var res2 = 0;
            if (tipoi == 1)
            {
                res2 = new cl.PessoaFisicaController().Gravar(new PessoaFisicaViewModel()
                {
                    Id = 0,
                    Nome = nome,
                    Rg = rg,
                    Cpf = cpf,
                    Nascimento = nascimento,
                    Telefone = telefone,
                    Celular = celular,
                    Email = email,
                    Tipo = tipoi,
                    Endereco = new EnderecoViewModel()
                    {
                        Id = res1,
                        Rua = rua,
                        Numero = numero,
                        Bairro = bairro,
                        Complemento = complemento,
                        Cep = cep,
                        Cidade = new CidadeViewModel()
                        {
                            Id = cidi,
                            Nome = "",
                            Estado = null
                        }
                    }
                });    
            }
            else
            {
                res2 = new cl.PessoaJuridicaController().Gravar(new PessoaJuridicaViewModel()
                {
                    Id = 0,
                    RazaoSocial = razaosocial,
                    NomeFantasia = nomefantasia,
                    Cnpj = cnpj,
                    Telefone = telefone,
                    Celular = celular,
                    Email = email,
                    Tipo = tipoi,
                    Endereco = new EnderecoViewModel()
                    {
                        Id = res1,
                        Rua = rua,
                        Numero = numero,
                        Bairro = bairro,
                        Complemento = complemento,
                        Cep = cep,
                        Cidade = new CidadeViewModel()
                        {
                            Id = cidi,
                            Nome = "",
                            Estado = null
                        }
                    }
                });
            }

            if (res2 <= 0)
            {
                new cl.EnderecoController().Excluir(res1);
                return Json("Ocorreu um problema ao gravar a pessoa...");
            }
                
            var res3 = new cl.ClienteController().Gravar(new ClienteViewModel()
            {
                Id = 0,
                Cadastro = DateTime.Now,
                Tipo = tipoi,
                Pessoa = new PessoaFisicaViewModel()
                {
                    Id = res2,
                    Nome = nome,
                    Rg = rg,
                    Cpf = cpf,
                    Nascimento = nascimento,
                    Telefone = telefone,
                    Celular = celular,
                    Email = email,
                    Tipo = tipoi,
                    Endereco = new EnderecoViewModel()
                    {
                        Id = res1,
                        Rua = rua,
                        Numero = numero,
                        Bairro = bairro,
                        Complemento = complemento,
                        Cep = cep,
                        Cidade = new CidadeViewModel()
                        {
                            Id = cidi,
                            Nome = "",
                            Estado = null
                        }
                    }
                }
            });

            if (res3 > 0) return Json("");
            
            if (tipoi == 1)
            {
                new cl.PessoaFisicaController().Excluir(res2);
            }
            else
            {
                new cl.PessoaJuridicaController().Excluir(res2);
            }

            new cl.EnderecoController().Excluir(res1);
                
            return Json("Ocorreu um problema ao gravar o cliente...");
        }

        [HttpPost]
        public JsonResult Alterar(IFormCollection form)
        {
            var endereco = form["endereco"];
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
            int.TryParse(pessoa, out var idpessoa);
            int.TryParse(cliente, out var idcliente);

            DateTime.TryParse(nasc, out var nascimento);

            var res1 = new cl.EnderecoController().Alterar(new EnderecoViewModel()
            {
                Id = idendereco,
                Rua = rua,
                Numero = numero,
                Bairro = bairro,
                Complemento = complemento,
                Cep = cep,
                Cidade = new CidadeViewModel()
                {
                    Id = cidi,
                    Nome = "",
                    Estado = null
                }
            });

            if (res1 <= 0) return Json("Ocorreu um problema ao alterar o endereço...");

            var res2 = 0;
            if (tipoi == 1)
            {
                res2 = new cl.PessoaFisicaController().Alterar(new PessoaFisicaViewModel()
                {
                    Id = idpessoa,
                    Nome = nome,
                    Rg = rg,
                    Cpf = cpf,
                    Nascimento = nascimento,
                    Telefone = telefone,
                    Celular = celular,
                    Email = email,
                    Tipo = tipoi,
                    Endereco = new EnderecoViewModel()
                    {
                        Id = idendereco,
                        Rua = rua,
                        Numero = numero,
                        Bairro = bairro,
                        Complemento = complemento,
                        Cep = cep,
                        Cidade = new CidadeViewModel()
                        {
                            Id = cidi,
                            Nome = "",
                            Estado = null
                        }
                    }
                });    
            }
            else
            {
                res2 = new cl.PessoaJuridicaController().Alterar(new PessoaJuridicaViewModel()
                {
                    Id = idpessoa,
                    RazaoSocial = razaosocial,
                    NomeFantasia = nomefantasia,
                    Cnpj = cnpj,
                    Telefone = telefone,
                    Celular = celular,
                    Email = email,
                    Tipo = tipoi,
                    Endereco = new EnderecoViewModel()
                    {
                        Id = idendereco,
                        Rua = rua,
                        Numero = numero,
                        Bairro = bairro,
                        Complemento = complemento,
                        Cep = cep,
                        Cidade = new CidadeViewModel()
                        {
                            Id = cidi,
                            Nome = "",
                            Estado = null
                        }
                    }
                });
            }
            
            if (res2 <= 0) return Json("Ocorreu um problema ao alterar a pessoa...");
                
            var res3 = new cl.ClienteController().Alterar(new ClienteViewModel()
            {
                Id = idcliente,
                Cadastro = DateTime.Now,
                Tipo = tipoi,
                Pessoa = new PessoaFisicaViewModel()
                {
                    Id = idpessoa,
                    Nome = nome,
                    Rg = rg,
                    Cpf = cpf,
                    Nascimento = nascimento,
                    Telefone = telefone,
                    Celular = celular,
                    Email = email,
                    Tipo = tipoi,
                    Endereco = new EnderecoViewModel()
                    {
                        Id = idendereco,
                        Rua = rua,
                        Numero = numero,
                        Bairro = bairro,
                        Complemento = complemento,
                        Cep = cep,
                        Cidade = new CidadeViewModel()
                        {
                            Id = cidi,
                            Nome = "",
                            Estado = null
                        }
                    }
                }
            });

            return Json(res3 > 0 ? "" : "Ocorreu um problema ao alterar o cliente...");
        }

        [HttpPost]
        public JsonResult Excluir(int id)
        {
            var res = new cl.ClienteController().Excluir(id);
            
            if (res == -10) return Json("Ocorreu um problema na comunicação com o banco de dados...");
            if (res == -5) return Json("Valor passado por parâmetro está inválido...");
            if (res < 0) return Json("Ocorreu um problema ao excluir o cliente...");
            
            return Json("Cliente excluído com êxito!");
        }
    }
}