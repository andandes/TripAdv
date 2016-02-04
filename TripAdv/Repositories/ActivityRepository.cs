using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TripAdv.Repositories;
using TripAdv.ViewModel;
using AutoMapper;

namespace TripAdv.Repositories
{
    public class ActivityRepository
    {
        public ICollection<ViewActivity> GetActivities(int id)
        {
            ICollection<ViewActivity> activities = new HashSet<ViewActivity>();
            using (OnATripEntities tr = new OnATripEntities())
            {
                var dbActivity = (from a in tr.Activities where a.TripID == id select a).ToList();
                Mapper.Map(dbActivity, activities);
            }
            return activities;
        }

        public ViewActivity UpdateActivity(ViewActivity viewActivity, int id)
        {

            using (OnATripEntities tr = new OnATripEntities())
            {
                Activity dbActivity = tr.Activities.Where(act => act.ActivityID == id).Select(act => act).FirstOrDefault();
                tr.ActivityActivityTypeMaps.RemoveRange(tr.ActivityActivityTypeMaps.Where(at => at.ActivityID == id));
                tr.SaveChanges();
                // odstranim z viewActivity objekty activityType, aby se znovu nepridaly do activityType
                foreach (var at in viewActivity.activityActivityTypeMaps)
                {
                    at.activityType = null;
                }

                Mapper.Map(viewActivity, dbActivity);
                dbActivity.ActivityID = id;
                var point = string.Format("POINT({1} {0})", viewActivity.gps.lat, viewActivity.gps.lon);
                dbActivity.GPS = System.Data.Entity.Spatial.DbGeography.FromText(point);
                tr.SaveChanges();
            }
            return viewActivity;
        }


        public void DeleteActivity(int id)
        {
            using (OnATripEntities tr = new OnATripEntities())
            {
                tr.Activities.RemoveRange(tr.Activities.Where(act => act.ActivityID == id));
                tr.SaveChanges();
            }
            
        }


        public ViewActivity CreateActivity(ViewActivity viewActivity,int id)
        {
            // nacteni spravneho tripID ktere se overi v controlleru ze uzivatel je
            // vlastnikem tohoto vyletu
            viewActivity.TripID = id;
            using (OnATripEntities tr = new OnATripEntities())
            {
                Activity dbActivity = tr.Activities.Add(Mapper.Map<Activity>(viewActivity));
                var point = string.Format("POINT({1} {0})", viewActivity.gps.lat, viewActivity.gps.lon);
                dbActivity.GPS = System.Data.Entity.Spatial.DbGeography.FromText(point);
                tr.SaveChanges();
                viewActivity.ActivityID = dbActivity.ActivityID;
            }
            return viewActivity;
        }


        public ViewActivity GetActivity(int activityId)
        {
            ViewActivity viewActivity = new ViewActivity();
            using (OnATripEntities tr = new OnATripEntities())
            {
                var dbActivity = (from a in tr.Activities where a.ActivityID == activityId select a).FirstOrDefault();
                Mapper.Map(dbActivity, viewActivity);
            }
            return viewActivity;
        }

        public void DeleteActivities(int id)
        {
            ICollection<ViewActivity> activities = new HashSet<ViewActivity>();
            using (OnATripEntities tr = new OnATripEntities())
            {
                tr.Activities.Remove(tr.Activities.Where(tam => tam.TripID == id).FirstOrDefault());
                tr.SaveChanges();
            }
        }

        public ViewActivity Update(ViewActivity activity)
        {
            ICollection<ViewActivity> activities = new HashSet<ViewActivity>();
            using (OnATripEntities tr = new OnATripEntities())
            {
                Activity activityDb = tr.Activities.Where(a => a.ActivityID == activity.ActivityID).Select(a => a).FirstOrDefault();
                Mapper.Map(activity, activityDb);
                //tr.Entry(u1).State = System.Data.Entity.EntityState.Modified;
                tr.SaveChanges();
            }
            return activity;
        }

        public ICollection<ViewActivityType> GetTypes()
        {
            ICollection<ViewActivityType> activityTypes = new HashSet<ViewActivityType>();
            using (OnATripEntities tr = new OnATripEntities())
            {
                var dbActivityTypes = from at in tr.ActivityTypes where at.ParentID == null select at;
                Mapper.Map(dbActivityTypes, activityTypes);
            }
            return activityTypes;
        }
        
    }

}