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
                //routes.MapRoute(name: "inicio_index1", template: "/inicio/index", defaults: new { controller="Inicio", action="Index" });
                //routes.MapRoute(name: "login_index", template: "/login/index", defaults: new { controller="Login", action="Index" });
                //routes.MapRoute(name: "login_autenticar", template: "/login/autenticar", defaults: new { controller = "Login", action = "Autenticar" });
                routes.MapRoute(name: "funcionario_index", template: "/gerenciar/funcionario/index", defaults: new { controller = "Funcionario", action = "Index" });
                routes.MapRoute(name: "funcionario_novo", template: "/gerenciar/funcionario/novo", defaults: new { controller = "Funcionario", action = "Novo" });
                routes.MapRoute(name: "funcionario_detalhes", template: "/gerenciar/funcionario/detalhes/{id}", defaults: new { controller = "Funcionario", action = "Detalhes" });
                routes.MapRoute(name: "funcionario_dados", template: "/gerenciar/funcionario/dados", defaults: new { controller = "Funcionario", action = "Dados" });
                routes.MapRoute(name: "parametrizacao", template: "/configuracao/parametrizacao", defaults: new { controller = "Parametrizacao", action = "Index" });
            });
        }
    }
}
