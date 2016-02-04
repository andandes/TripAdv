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
    public class TripController : ApiController
    {
        TripAdv.Repositories.TripRepository tripRepo = new TripAdv.Repositories.TripRepository();

        [HttpGet]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "Trip", Operation = "GetTrips")]
        public IEnumerable<ViewTrip> GetTrips(int stateId = 0)
        {
            return tripRepo.GetTrips(stateId);
        }

        [HttpGet]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "Trip", Operation = "GetTrip")]
        public ViewTrip GetTrip(int id)
        {
            return tripRepo.GetTrip(id);
        }

        [HttpPost]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "Trip", Operation = "CreateTrip")]
        public ViewTrip CreateTrip(ViewTrip viewTrip)
        {
            return tripRepo.CreateTrip(viewTrip);
        }

        [HttpPut]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "Trip", Operation = "UpdateTrip")]
        public ViewTrip UpdateTrip(int id, ViewTrip viewTrip)
        {
            return tripRepo.UpdateTrip(viewTrip,id);
        }

        [HttpPost]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "Trip", Operation = "CloneTrip")]
        public IHttpActionResult CloneTrip(int id, ViewTrip trip)
        {
            int userId = 1; // uzivatel se pak vezme z application user - prihlaseneho uzivatele
            tripRepo.CloneTrip(id, userId, trip.startDate);
            return Ok();
        }

        [HttpDelete]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "Trip", Operation = "DeleteTrip")]
        public IHttpActionResult DeleteTrip(int id)
        {
            tripRepo.DeleteTrip(id);
            return Ok();
        }
    }
}
