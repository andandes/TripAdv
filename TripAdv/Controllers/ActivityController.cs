using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TripAdv.ViewModel;
using TripAdv.Repositories;
using System.IdentityModel.Services;
using System.Security.Permissions;

namespace TripAdv.Controllers
{
    public class ActivityController : ApiController
    {

        ActivityRepository activityRepo = new ActivityRepository();
        CountryRepository countryRepo = new CountryRepository();

        [HttpGet]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "Activity", Operation = "GetActivity")]
        public ViewActivity GetActivity(int id)
        {
            return activityRepo.GetActivity(id);
        }

        [HttpPost]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "Activity", Operation = "CreateActivity")]
        public ViewActivity CreateActivity(ViewActivity activity, int id)
        {
            // id = tripId
            return activityRepo.CreateActivity(activity,id);
        }

        [HttpPut]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "Activity", Operation = "UpdateActivity")]
        public ViewActivity UpdateActivity(ViewActivity activity, int id)
        {
            // id = activityId
            return activityRepo.UpdateActivity(activity, id);
        }

        [HttpDelete]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "Activity", Operation = "DeleteActivity")]
        public IHttpActionResult DeleteActivity(int id)
        {
            activityRepo.DeleteActivity(id);
            return Ok();
        }

        [HttpGet]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "Activity", Operation = "GetActivities")]
        public IEnumerable<ViewActivity> GetActivities(int id)
        {
            return activityRepo.GetActivities(id);
        }

        [HttpGet]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "Activity", Operation = "GetTypes")]
        public ICollection<ViewActivityType> GetTypes()
        {
            return activityRepo.GetTypes();
        }

        [HttpGet]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "Activity", Operation = "GetCountries")]
        public ICollection<ViewCountry> GetCountries()
        {
            return countryRepo.GetCountries();
        }

        


    }
}
