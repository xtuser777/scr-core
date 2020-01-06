using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace scrweb.Filters
{
    public class ValidarUsuario : Attribute, IActionFilter, IOrderedFilter
    {
        public int Order { get; set; }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            //throw new NotImplementedException();
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var id = context.HttpContext.Session.GetString("id");
            var nivel = context.HttpContext.Session.GetString("nivel");
            var controller = (string)context.RouteData.Values["controller"];
            var action = (string)context.RouteData.Values["action"];
            //var ipCliente = context.HttpContext.Connection.RemoteIpAddress;
            //var browser = context.HttpContext.Request.Headers["User-Agent"].ToString();
            //var urlReferrer = context.HttpContext.Request.Headers["Referer"].ToString();

            if (string.IsNullOrEmpty(id))
            {
                context.Result = new RedirectResult("/Login/Logout");
            }
            else
            {
                if ((nivel == "2" || nivel == "3") && 
                    new[] { "funcionario", "parametrizacao" }.Contains(controller.ToLower()) && 
                    new[] { "index", "detalhes", "novo" }.Contains(action.ToLower()))
                {
                    context.Result = new RedirectResult("/Home/Denied");
                }
            }
        }
    }
}
