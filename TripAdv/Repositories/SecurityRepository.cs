using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TripAdv.Repositories
{
    public class SecurityRepository
    {
        public bool CheckAuthorization(string resource, string action, string role, int userId, int id)
        {
            System.Data.Entity.Core.Objects.ObjectResult<int?> result;
            using (OnATripEntities tr = new OnATripEntities())
            {
                result = tr.CheckAuthorization(resource, action, role, userId, id);
                return (result.SingleOrDefault() >= 1);
            }

           
        }
    }
}