using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TripAdv.ViewModel;
using AutoMapper;


namespace TripAdv.Repositories
{
    public class CountryRepository
    {
        public ICollection<ViewCountry> GetCountries()
        {
            ICollection<ViewCountry> states = new HashSet<ViewCountry>();
            using (OnATripEntities tr = new OnATripEntities())
            {
                var dbStates = from s in tr.countries orderby s.name select s;
                Mapper.Map(dbStates, states);
            }
            return states;
        }

        public ICollection<ViewCountry> ImportCountries(ICollection<ViewCountry> countries)
        {
            using (OnATripEntities tr = new OnATripEntities())
            {
                tr.countries.AddRange(Mapper.Map<ICollection<country>>(countries));
                tr.SaveChanges();
            }
                return countries;
        }
    }
}