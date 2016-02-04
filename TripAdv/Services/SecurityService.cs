using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.Configuration;
using System.Runtime.Caching;
using TripAdv.Models;

namespace TripAdv.Services
{
    public class SecurityService
    {
        private ObjectCache cache = MemoryCache.Default;

        public XmlDocument GetClaimsConfig()
        {
            // todo: || false je pouze pro ucely testovani, aby se nemusel vzdy resetovat IIS - odstranit or vyraz
            if (cache["XmlConfig"] == null || false)
            {

                string claimsConfigFile = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase) + @"\..\" + ConfigurationManager.AppSettings["ClaimsConfig"];
                if (!System.IO.File.Exists(claimsConfigFile.Replace(@"file:\", "")))
                {
                    throw new Exception("XML claims not found");
                }
                XmlDocument xmlClaims = new XmlDocument();
                xmlClaims.Load(claimsConfigFile);
                cache["XmlConfig"] = xmlClaims;
            }
            return (XmlDocument)cache["XmlConfig"];
        }

        public TripClaim GetRoles(string resource, string action, XmlDocument xmlClaims)
        {
            XmlNode nodeClaim = null; //xmlClaims.SelectSingleNode(@"Permissions/Permission[@Resource=""" + resource + @""" and @Action=""" + action + @"""]");

            foreach (XmlNode claim2 in xmlClaims.SelectNodes(@"Permissions/Permission[@Resource=""" + resource + @"""]"))
            {
                if (claim2.Attributes["Action"].Value.IndexOf(action) > -1)
                {
                    nodeClaim = claim2;
                }
            }

            if (nodeClaim == null)
            {
                throw new Exception("CheckAccess: nodeClaim in for resource: " + resource + ", action: " + action + "in claim config file not found.");
            }
            TripClaim claim = new TripClaim()
            {
                Roles = nodeClaim.SelectSingleNode("Roles").InnerText.Trim().Split(','),
                ItemID = (nodeClaim.SelectSingleNode("ItemID") != null) ? nodeClaim.SelectSingleNode("ItemID").InnerText.Trim() : string.Empty
            };
            // vrati pole roli
            return claim;
        }

        public bool CheckAuthorization(string resource, string action, string role, int userId, int id)
        {
            TripAdv.Repositories.SecurityRepository secRepo = new Repositories.SecurityRepository();
            return secRepo.CheckAuthorization(resource, action, role, userId, id);
        }
    }
}