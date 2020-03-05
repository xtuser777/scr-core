using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using scrweb.Filters;
using scrweb.Models;

namespace scrweb.Controllers
{
    [ValidarUsuario]
    public class ParametrizacaoController : Controller
    {
        private IHostingEnvironment _env;

        public ParametrizacaoController(IHostingEnvironment env)
        {
            _env = env;
        }

        // GET
        public IActionResult Index()
        {
            return View();
        }

        public JsonResult Obter()
        {
            var par = new Parametrizacao().Get();
            return Json(par?.ToJObject());
        }

        private string GravarLogotipo(ref bool err)
        {
            try
            {
                if (Request.Form.Files.Count > 0)
                {
                    var arquivo = Request.Form.Files[0];
                    if (arquivo != null && arquivo.Length > 0 && arquivo.Length <= 1048576) //Maximo 1MB
                    {
                        string extensaoArquivo = Path.GetExtension(arquivo.FileName).ToLower();
                        if (extensaoArquivo == ".png")
                        {
                            var nomeArquivo = "logo-personalizado.png";
                            var caminho = Path.Combine(_env.WebRootPath, "images");
                            caminho = Path.Combine(caminho, nomeArquivo);

                            //Gravar o arquivo no servidor
                            using (var stream = new FileStream(caminho, FileMode.Create))
                            {
                                arquivo.CopyTo(stream);
                            }

                            err = false;
                            return caminho + nomeArquivo;
                            //string base64 = "";
                            //var img = new MemoryStream();
                            //arquivo.CopyTo(img);
                            //Resize(img, 100, caminho, out base64);
                            //retorno.Add(new { Id = i, Dados = base64 });
                        }
                        else
                        {
                            err = true;
                            return "Formato inválido de arquivo.";
                        }
                    }
                    else
                    {
                        err = true;
                        return "Tamanho inválido de arquivo.";
                    }
                }
                else
                {
                    return "";
                }
            }
            catch (Exception e)
            {
                err = true;
                return e.Message;
            }
        }
        
        [HttpPost]
        public JsonResult Gravar(IFormCollection form)
        {
            string razaoSocial = form["razaosocial"];
            string nomeFantasia = form["nomefantasia"];
            string cnpj = form["cnpj"];
            string rua = form["rua"];
            string numero = form["numero"];
            string bairro = form["bairro"];
            string complemento = form["complemento"];
            string cep = form["cep"];
            string cidade = form["cidade"];
            string telefone = form["telefone"];
            string celular = form["celular"];
            string email = form["email"];

            bool err = false;
            string logo = GravarLogotipo(ref err);
            if (err == true)
                return Json(logo);
            
            int.TryParse(cidade, out var cidade1);

            int res1 = new Parametrizacao(){
                Id = 0,
                RazaoSocial = razaoSocial,
                NomeFantasia = nomeFantasia,
                Cnpj = cnpj,
                Rua = rua,
                Numero = numero,
                Bairro = bairro,
                Complemento = complemento,
                Cep = cep,
                Cidade = new Cidade().GetById(cidade1),
                Telefone = telefone,
                Celular = celular,
                Email = email,
                Logotipo = logo
            }.Gravar();

            return Json(res1 > 0 ? "" : "Ocorreu um problema ao gravar o Parametrização.");
        }
        
        [HttpPost]
        public JsonResult Alterar(IFormCollection form)
        {
            string razaoSocial = form["razaosocial"];
            string nomeFantasia = form["nomefantasia"];
            string cnpj = form["cnpj"];
            string rua = form["rua"];
            string numero = form["numero"];
            string bairro = form["bairro"];
            string complemento = form["complemento"];
            string cep = form["cep"];
            string cidade = form["cidade"];
            string telefone = form["telefone"];
            string celular = form["celular"];
            string email = form["email"];

            bool err = false;
            string logo = GravarLogotipo(ref err);
            if (err == true)
                return Json(logo);
            
            int.TryParse(cidade, out var cidade1);

            int res1 = new Parametrizacao(){
                Id = 1,
                RazaoSocial = razaoSocial,
                NomeFantasia = nomeFantasia,
                Cnpj = cnpj,
                Rua = rua,
                Numero = numero,
                Bairro = bairro,
                Complemento = complemento,
                Cep = cep,
                Cidade = new Cidade().GetById(cidade1),
                Telefone = telefone,
                Celular = celular,
                Email = email,
                Logotipo = logo
            }.Alterar();

            return Json(res1 > 0 ? "" : "Ocorreu um problema ao alterar a Parametrização.");
        }
    }
}