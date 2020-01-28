using System;
using System.Collections.Generic;
using System.Linq;
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
        private static List<ClienteViewModel> _clientes;

        public ClienteController()
        {
            _clientes = new cl.ClienteController().GetAll();
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
            return Json(_clientes);
        }

        [HttpPost]
        public JsonResult ObterPorChave(string chave)
        {
            var filtrado = _clientes.FindAll(cli => cli.Tipo == 1
                ? ((PessoaFisicaViewModel) cli.Pessoa).Nome.Contains(chave, StringComparison.CurrentCultureIgnoreCase) ||
                  cli.Pessoa.Email.Contains(chave, StringComparison.CurrentCultureIgnoreCase)
                : ((PessoaJuridicaViewModel) cli.Pessoa).NomeFantasia.Contains(chave, StringComparison.CurrentCultureIgnoreCase) ||
                  cli.Pessoa.Email.Contains(chave, StringComparison.CurrentCultureIgnoreCase)
            );
            
            return Json(filtrado);
        }

        [HttpPost]
        public JsonResult ObterPorCadastro(string cad)
        {
            var filtrado = _clientes.FindAll(cli => cli.Cadastro.ToString("yyyy-MM-dd") == cad);
            
            return Json(filtrado);
        }

        [HttpPost]
        public JsonResult ObterPorChaveCad(string chave, string cad)
        {
            var filtrado = _clientes.FindAll(cli => cli.Tipo == 1
                ? (((PessoaFisicaViewModel) cli.Pessoa).Nome.Contains(chave, StringComparison.CurrentCultureIgnoreCase) ||
                  cli.Pessoa.Email.Contains(chave, StringComparison.CurrentCultureIgnoreCase)) &&
                  cli.Cadastro.ToString("yyyy-MM-dd") == cad
                : (((PessoaJuridicaViewModel) cli.Pessoa).NomeFantasia.Contains(chave, StringComparison.CurrentCultureIgnoreCase) ||
                  cli.Pessoa.Email.Contains(chave, StringComparison.CurrentCultureIgnoreCase)) &&
                  cli.Cadastro.ToString("yyyy-MM-dd") == cad
            );
            
            return Json(filtrado);
        }

        public JsonResult ObterDetalhes()
        {
            var id = HttpContext.Session.GetString("idcli");

            var cli = _clientes.Find(c => c.Id == Convert.ToInt32(id));
            
            return Json(cli);
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
        public JsonResult Ordenar(string col)
        {
            var ord = new List<ClienteViewModel>();

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
                            ? ((PessoaFisicaViewModel) c.Pessoa).Nome
                            : ((PessoaJuridicaViewModel) c.Pessoa).NomeFantasia).ToList();
                    break;
                case "4":
                    ord = _clientes.OrderByDescending(c =>
                        c.Tipo == 1
                            ? ((PessoaFisicaViewModel) c.Pessoa).Nome
                            : ((PessoaJuridicaViewModel) c.Pessoa).NomeFantasia).ToList();
                    break;
                case "5":
                    ord = _clientes.OrderBy(c =>
                        c.Tipo == 1
                            ? ((PessoaFisicaViewModel) c.Pessoa).Cpf
                            : ((PessoaJuridicaViewModel) c.Pessoa).Cnpj).ToList();
                    break;
                case "6":
                    ord = _clientes.OrderByDescending(c =>
                        c.Tipo == 1
                            ? ((PessoaFisicaViewModel) c.Pessoa).Cpf
                            : ((PessoaJuridicaViewModel) c.Pessoa).Cnpj).ToList();
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
                    ord = _clientes.OrderBy(c => c.Pessoa.Email).ToList();
                    break;
                case "12":
                    ord = _clientes.OrderByDescending(c => c.Pessoa.Email).ToList();
                    break;
            }

            return Json(ord);
        }

        [HttpPost]
        public JsonResult VerificarCpf(string cpf)
        {
            return Json(new cl.PessoaFisicaController().VerifyCpf(cpf));
        }

        [HttpPost]
        public JsonResult VerificarCnpj(string cnpj)
        {
            return Json(new cl.PessoaJuridicaController().VerifyCnpj(cnpj));
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
            var res = 0;
            var cli = _clientes.Find(c => c.Id == id);
            
            res = new cl.ClienteController().Excluir(id);
            res = cli.Tipo == 1
                ? new cl.PessoaFisicaController().Excluir(cli.Pessoa.Id)
                : new cl.PessoaJuridicaController().Excluir(cli.Pessoa.Id);
            res = new cl.EnderecoController().Excluir(cli.Pessoa.Endereco.Id);
            
            return Json(res);
        }
    }
}