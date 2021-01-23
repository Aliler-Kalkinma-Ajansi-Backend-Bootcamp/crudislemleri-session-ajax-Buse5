using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proje.Filters
{
    public class LoginAtribute:ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var isim = context.HttpContext.Session.GetString("Id");
            if (isim ==null || Int32.Parse(isim)<=0) 
            {
                context.Result = new RedirectResult("~/Home/Login");
            }
            base.OnActionExecuting(context);
        }
    }
}
