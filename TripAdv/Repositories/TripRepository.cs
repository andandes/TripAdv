using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TripAdv.ViewModel;
using AutoMapper;

namespace TripAdv.Repositories
{
    public class TripRepository
    {
        public ICollection<ViewTrip> GetTrips(int stateId)
        {
            ICollection<ViewTrip> trips = new HashSet<ViewTrip>();
            using (OnATripEntities tr = new OnATripEntities())
            {
                ICollection<Trip> dbTrips = tr.Trips.Where(trp => trp.IsPublic).Select(trp => trp).ToList<Trip>();
                trips = Mapper.Map<ICollection<ViewTrip>>(dbTrips);
            }
            return trips;
        }

        public ViewTrip GetTrip(int tripId)
        {
            ViewTrip trip = new ViewTrip();
            using (OnATripEntities tr = new OnATripEntities())
            {
                Trip dbTrip = tr.Trips.Where(trp => trp.TripId == tripId).Select(trp => trp).FirstOrDefault();
                Mapper.Map(dbTrip, trip);
                ICollection<Activity> dbActivity = (from a in tr.Activities where a.TripID == tripId select a).ToList();
                trip.activity = Mapper.Map <ICollection<ViewActivity>>(dbActivity);
            }
            return trip;
        }

        public void CloneTrip(int TripId, int UserId, DateTime startDate)
        {
            using (OnATripEntities tr = new OnATripEntities())
            {
                tr.spCloneTrip(UserId, TripId, startDate);
            }
        }

        public ViewTrip CreateTrip(ViewTrip viewTrip)
        {
            using (OnATripEntities tr = new OnATripEntities())
            {
                Trip dbTrip = tr.Trips.Add(Mapper.Map<Trip>(viewTrip));
                tr.SaveChanges();
                viewTrip.TripId = dbTrip.TripId;
            }
            return viewTrip;
        }

        public ViewTrip UpdateTrip(ViewTrip viewTrip, int id)
        {
            using (OnATripEntities tr = new OnATripEntities())
            {
                Trip dbTrip = tr.Trips.Where(trp => trp.TripId == viewTrip.TripId).Select(trp => trp).FirstOrDefault();
                Mapper.Map(viewTrip, dbTrip);
                tr.SaveChanges();
                viewTrip.TripId = dbTrip.TripId;
            }
            return viewTrip;
        }

        public void DeleteTrip(int id)
        {
            using (OnATripEntities tr = new OnATripEntities())
            {
                var x = tr.Trips.Where(trp => trp.TripId == id).FirstOrDefault();
                if (x != null)
                { 
                    tr.Trips.Remove(x);
                }
                tr.SaveChanges();
            }

        }
    }
}