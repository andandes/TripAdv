using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace TripAdv.ViewModel
{

    public class ViewGPS
    {
        public double? lon { get; set; }
        public double? lat { get; set; }
    }

    public class ViewActivityTypeMap
    {
        public int ActivityActivityTypeMapID { get; set; }
        public int ActivityID { get; set; }
        public int ActivityTypeID { get; set; }
        public ViewActivityType activityType { get; set; }
    }
    
    public class ViewActivity
    {
    
        public string Title { get; set; }
        public int ActivityID { get; set; }
        public int TripID { get; set; }
        public string description { get; set; }
        public DateTimeOffset? SpendTime { get; set; }
        public DateTime? DiaryTime { get; set; }
        public ViewGPS gps { get; set; }
        public Int64 googleMapsPlaceID { get; set; }
        public string contact { get; set; }
        public int priority { get; set; }
        public ICollection<ViewActivityTypeMap> activityActivityTypeMaps { get; set; }
    }

 

    public class ViewTripActivityMap
    {
        public int tripId { get; set; }
        public int activityId { get; set; }
        public DateTime start { get; set; }
        public int durationMinutes { get;  set;}
    }

    public class ViewTripUser
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Language { get; set; }
        public string Token { get; set; }
    }

    public class ViewActivityType
    {
        public int ActivityTypeID { get; set; }
        public string Name { get; set; }
        public string description { get; set; }
        public string IconURL { get; set; }
        public int? ParentID { get; set; }
        public string GoogleCode { get; set; }
        public ICollection<ViewActivityType> types { get; set; }
    }

    public class ViewMember
    {
        public int tripId { get; set; }
        public int userId { get; set; }
        public string role { get; set; }
    }

    public class ViewCountry
    {
        public int countryId { get; set; }
        public string name { get; set; }
        public string phone { get; set;}
        public string continent { get; set; }
        public string capital { get; set; }
        public string currency { get; set;}
        public string languges { get; set; }
        //public ViewGPS gps { get; set; }
        public string WikiURL { get; set; }
    }

    public class ViewTrip
    {
        public ViewTrip()
        {
            activity = new HashSet<ViewActivity>();
            members = new HashSet<ViewMember>();
        }

        public int TripId { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<ViewActivity> activity {get; set;}
        public DateTime startDate { get; set; }
        public ICollection<ViewMember> members { get; set; }
    }
}