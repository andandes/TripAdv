using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Xml;
using System.Configuration;

namespace TripAdv
{
    public class TripClaimsManager: ClaimsAuthorizationManager
    {
        /// <summary>
        /// Overeni uzivatelskeho pristupu k prostredku
        /// </summary>
        /// <param name="context">AuthorizationContext (action, resource)</param>
        /// <returns></returns>
        /// 
        public override bool CheckAccess(AuthorizationContext context)
        {
            TripAdv.Services.SecurityService securityService = new Services.SecurityService();
            // pokud neni uzivatel prihlaseny, pak se vraci false - not authorized
            if (TripAdv.Models.appUser.userId == null) return false;

            var action = context.Action.FirstOrDefault().Value;
            var resource = context.Resource.FirstOrDefault().Value;
            bool isAuthorized = false;
            var Request = System.Web.HttpContext.Current.Request;
            TripAdv.Models.TripClaim claim = securityService.GetRoles(resource, action, securityService.GetClaimsConfig());
            int id = (Request.RequestContext.RouteData.Values[claim.ItemID] != null) ? System.Convert.ToInt32(Request.RequestContext.RouteData.Values[claim.ItemID]) : 0;
            
            foreach (string role in claim.Roles)
            {
                // staci alespon jedna role a uzivatel je autorizovany
                if (role == "everyone") return true;
                isAuthorized = (securityService.CheckAuthorization(resource,action,role, TripAdv.Models.appUser.userId, id)) ? true : isAuthorized;
            }

            return isAuthorized;
        }





    }
}



