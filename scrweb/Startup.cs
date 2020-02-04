using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace scrweb
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromDays(1);
            });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseSession();
            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute( name: "default", template: "{controller=Inicio}/{action=Index}/{id?}");
                routes.MapRoute(name: "funcionario_index", template: "/gerenciar/funcionario/index", defaults: new { controller = "Funcionario", action = "Index" });
                routes.MapRoute(name: "funcionario_novo", template: "/gerenciar/funcionario/novo", defaults: new { controller = "Funcionario", action = "Novo" });
                routes.MapRoute(name: "funcionario_detalhes", template: "/gerenciar/funcionario/detalhes", defaults: new { controller = "Funcionario", action = "Detalhes" });
                routes.MapRoute(name: "funcionario_dados", template: "/gerenciar/funcionario/dados", defaults: new { controller = "Funcionario", action = "Dados" });
                
                routes.MapRoute(name: "parametrizacao", template: "/configuracao/parametrizacao", defaults: new { controller = "Parametrizacao", action = "Index" });
                
                routes.MapRoute(name: "cliente", template: "/gerenciar/cliente/index", defaults: new { controller = "Cliente", action = "Index" });
                routes.MapRoute(name: "cliente_novo", template: "/gerenciar/cliente/novo", defaults: new { controller = "Cliente", action = "Novo" });
                routes.MapRoute(name: "cliente_detalhes", template: "/gerenciar/cliente/detalhes", defaults: new { controller = "Cliente", action = "Detalhes" });
                
                routes.MapRoute(name: "representacao", template: "/gerenciar/representacao/index", defaults: new { controller = "Representacao", action = "Index" });
                routes.MapRoute(name: "representacao_novo", template: "/gerenciar/representacao/novo", defaults: new { controller = "Representacao", action = "Novo" });
                routes.MapRoute(name: "representacao_detalhes", template: "/gerenciar/representacao/detalhes", defaults: new { controller = "Representacao", action = "Detalhes" });
                routes.MapRoute(name: "representacao_addunidade", template: "/gerenciar/representacao/addunidade", defaults: new { controller = "Representacao", action = "AddUnidade" });

                routes.MapRoute(name: "tipocaminhao_index", template: "/gerenciar/tipocaminhao/index", defaults: new { controller = "TipoCaminhao", action = "Index" });
                routes.MapRoute(name: "tipocaminhao_novo", template: "/gerenciar/tipocaminhao/novo", defaults: new { controller = "TipoCaminhao", action = "Novo" });
                routes.MapRoute(name: "tipocaminhao_detalhes", template: "/gerenciar/tipocaminhao/detalhes", defaults: new { controller = "TipoCaminhao", action = "Detalhes" });
                
                routes.MapRoute(name: "produto_index", template: "/gerenciar/produto/index", defaults: new { controller = "Produto", action = "Index" });
                routes.MapRoute(name: "produto_novo", template: "/gerenciar/produto/novo", defaults: new { controller = "Produto", action = "Novo"});
                routes.MapRoute(name: "produto_detalhes", template: "/gerenciar/produto/detalhes", defaults: new { controller = "Produto", action = "Detalhes" });
                
                //routes.MapRoute(name: "", template: "", defaults: new { });
            });
        }
    }
}
