using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TripAdv.ViewModel;
using AutoMapper;
using TripAdv.Repositories;

namespace TripAdv.Repositories
{
    public class UserRepository
    {
        public ViewTripUser Create(ViewTripUser user)
        {
            using (OnATripEntities tr = new OnATripEntities())
            {
                User repositoryUser = Mapper.Map<User>(user);
                tr.Users.Add(repositoryUser);
                tr.SaveChanges();
                user.UserId = repositoryUser.UserId;
            }

            return user;
        }

        public ViewTripUser Update(ViewTripUser user)
        {
            using (OnATripEntities tr = new OnATripEntities())
            {
                User u1 = tr.Users.Where (u => u.UserId == user.UserId).Select(u => u).FirstOrDefault();
                Mapper.Map(user, u1);
                //tr.Entry(u1).State = System.Data.Entity.EntityState.Modified;
                tr.SaveChanges();
            }

            return user;
        }


        public void Delete(int userId)
        {
            using (OnATripEntities tr = new OnATripEntities())
            {
                tr.Logins.Remove(tr.Logins.Where(l => l.UserId == userId).FirstOrDefault());
                tr.SaveChanges();
                var user = tr.Users.Where(u => u.UserId == userId).FirstOrDefault();
                if (user != null)
                { 
                tr.Users.Remove(user);
                tr.SaveChanges();
                }
            }
        }

        public static ViewTripUser GetUserFromToken(string token)
        {
            using (OnATripEntities tr = new OnATripEntities())
            {
                var dbUser = (from u in tr.Users from l in tr.Logins where u.UserId == l.UserId && l.Token == token select u).FirstOrDefault();
                if (dbUser != null)
                {
                    ViewTripUser user = new ViewTripUser();
                    Mapper.Map(dbUser, user);
                    user.Token = token;
                    return user;

                }
                return null;
            }
        }

        public ViewTripUser Login(ViewTripUser vuser)
        {
            ViewTripUser user = new ViewTripUser();
            string token = "";
            using (OnATripEntities tr = new OnATripEntities())
            {
                token = (from l in tr.Logins from u in tr.Users where u.UserId == l.UserId && u.UserName == vuser.UserName && u.Password == vuser.Password select l.Token).FirstOrDefault();
                var dbUser = (from u in tr.Users where u.UserName == vuser.UserName && u.Password == vuser.Password select u).FirstOrDefault();
                // spatne prihlaseni - vyvola vyjimku securityException
                if (dbUser == null)
                {
                    throw new System.Security.SecurityException("Login or password is incorrect.");
                }
                    if (string.IsNullOrEmpty(token))
                    {
                        token = Guid.NewGuid().ToString();
                        tr.Logins.Add(new Repositories.Login() { Token = token, UserId = dbUser.UserId, LastHit = DateTime.UtcNow });
                        tr.SaveChanges();
                    }
                Mapper.Map(dbUser, user);
                user.Token = token;
            }

            return user;
        }
    }
}