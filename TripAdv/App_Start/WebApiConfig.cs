using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;
using System.Web.Http.Routing;
using System.Web.Http.Cors;

namespace TripAdv
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.EnableCors(new EnableCorsAttribute("*", "*", "*"));
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));
            config.Filters.Add(new tripExceptions());
            // Users
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(name: "LoginRoute", routeTemplate: "api/users/login", defaults: new { controller = "User", action = "Login" }, constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Post, HttpMethod.Options) });
            config.Routes.MapHttpRoute(name: "CreateUserRoute", routeTemplate: "api/users", defaults: new { controller = "User", action = "Create" }, constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Post, HttpMethod.Options) });
            config.Routes.MapHttpRoute(name: "DeleteUserRoute", routeTemplate: "api/users/{userId}", defaults: new { controller = "User", action = "Delete" }, constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Delete, HttpMethod.Options) });
            config.Routes.MapHttpRoute(name: "UpdateUserRoute", routeTemplate: "api/users", defaults: new { controller = "User", action = "Update" }, constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Put, HttpMethod.Options) });

            // Trips
            config.Routes.MapHttpRoute(name: "GetTripsRoute", routeTemplate: "api/trips", defaults: new { controller = "Trip", action = "GetTrips", stateId = 0 }, constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Get, HttpMethod.Options) });
            config.Routes.MapHttpRoute(name: "GetTripRoute", routeTemplate: "api/trips/{id}", defaults: new { controller = "Trip", action = "GetTrip" }, constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Get, HttpMethod.Options) });
            config.Routes.MapHttpRoute(name: "CreateTripRoute", routeTemplate: "api/trips", defaults: new { controller = "Trip", action = "CreateTrip" }, constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Post, HttpMethod.Options) });
            config.Routes.MapHttpRoute(name: "UpdateTripRoute", routeTemplate: "api/trips/{id}", defaults: new { controller = "Trip", action = "UpdateTrip" }, constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Put, HttpMethod.Options) });
            config.Routes.MapHttpRoute(name: "DeleteTripRoute", routeTemplate: "api/trips/{id}", defaults: new { controller = "Trip", action = "DeleteTrip" }, constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Delete, HttpMethod.Options) });
            config.Routes.MapHttpRoute(name: "CloneTripRoute", routeTemplate: "api/trips/{id}/clone", defaults: new { controller = "Trip", action = "CloneTrip" }, constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Post, HttpMethod.Options) });
            
            // Activity
            config.Routes.MapHttpRoute(name: "GetActivityRoute", routeTemplate: "api/activities/{id}", defaults: new { controller = "Activity", action = "GetActivity"}, constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Get, HttpMethod.Options) });
            config.Routes.MapHttpRoute(name: "CreateActivitiesRoute", routeTemplate: "api/activities/{id}", defaults: new { controller = "Activity", action = "CreateActivity" }, constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Post, HttpMethod.Options) });
            config.Routes.MapHttpRoute(name: "UpdateActivitiesRoute", routeTemplate: "api/activities/{id}", defaults: new { controller = "Activity", action = "UpdateActivity" }, constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Put, HttpMethod.Options) });
            config.Routes.MapHttpRoute(name: "DeleteActivitiesRoute", routeTemplate: "api/activities/{id}", defaults: new { controller = "Activity", action = "DeleteActivity" }, constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Delete, HttpMethod.Options) });

            // Types
            config.Routes.MapHttpRoute(name: "GetActivityTypes", routeTemplate: "api/types", defaults: new { controller = "Activity", action = "GetTypes"}, constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Get, HttpMethod.Options) });

            // States
            config.Routes.MapHttpRoute(name: "GetCountries", routeTemplate: "api/countries", defaults: new { controller = "Activity", action = "GetCountries" }, constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Get, HttpMethod.Options) });
            config.Routes.MapHttpRoute(name: "ImportCountries", routeTemplate: "api/countries", defaults: new { controller = "Activity", action = "ImportCountries" }, constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Post, HttpMethod.Options) });
            



            config.Routes.MapHttpRoute(name: "DefaultApi",routeTemplate: "api/{controller}/{id}",defaults: new { id = RouteParameter.Optional });
            GlobalConfiguration.Configuration.Filters.Add(new TripAdv.Filters.CustomAuthenticationFilterAttribute());
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.MediaTypeMappings.Add(new RequestHeaderMapping("Accept", "text/html", StringComparison.InvariantCultureIgnoreCase, true, "application/json"));
        }

        


    }
}
