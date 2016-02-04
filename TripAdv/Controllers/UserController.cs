using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TripAdv.ViewModel;
using System.IdentityModel.Services;
using System.Security.Permissions;

namespace TripAdv.Controllers
{
    public class UserController : ApiController
    {
        Repositories.UserRepository ur = new Repositories.UserRepository();

        [HttpPost]
        public ViewTripUser Login(ViewTripUser login)
        {
            return ur.Login(login);
        }

        [HttpPost]
        public ViewTripUser Create(ViewTripUser user)
        {
            return ur.Create(user);
        }

        [HttpPut]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "User", Operation = "Update")]
        public ViewTripUser Update(ViewTripUser user)
        {
            return ur.Update(user);
        }

        [HttpDelete]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "User", Operation = "Delete")]
        public IHttpActionResult Delete(int userId)
        {
            ur.Delete(userId);
            return Ok();
        }


        // PUT: api/User/5
        public void Put(int id, [FromBody]string value)
        {
        }

    }
}
