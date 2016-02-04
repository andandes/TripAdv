using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TripAdv.Models
{
    public static class appUser
    {
        public static string token {get; set;}
        public static string userName { get; set; }
        public static string userRole { get; set; }
        public static int userId { get; set; }
    }
}