using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Security.Principal;
using System.Diagnostics;

namespace TripAdv.Filters
{
    public class CustomAuthenticationFilterAttribute : ActionFilterAttribute
    {

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            string token = "";
            if (actionContext.Request.Headers.Where(i => i.Key == "Authorization").Count() > 0)
            {
                token = actionContext.Request.Headers.GetValues("Authorization").First();
            }
            else
            {
                token = actionContext.Request.GetQueryNameValuePairs().Where(k => k.Key == "Authorization").Select(v => v.Value).FirstOrDefault();
            }
             
            TripAdv.ViewModel.ViewTripUser user = TripAdv.Repositories.UserRepository.GetUserFromToken(token);
            if (user != null)
            {
                TripAdv.Models.appUser.token = token;
                TripAdv.Models.appUser.userName = user.UserName;
                TripAdv.Models.appUser.userId = user.UserId;
            }
            else
            {
                if (actionContext.Request.RequestUri.LocalPath.IndexOf("login") == -1 && actionContext.Request.RequestUri.LocalPath != "/api/users") throw new System.Security.SecurityException("Token is not valid.");
            }
            
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {

        }
        
    }
}
