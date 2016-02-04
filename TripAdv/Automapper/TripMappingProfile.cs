using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using TripAdv.Repositories;
using TripAdv.ViewModel;

namespace TripAdv.Automapper
{
    public class TripMappingProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<ActivityActivityTypeMap, ViewActivityTypeMap>().ReverseMap();
            Mapper.CreateMap<User, ViewTripUser>().ReverseMap();
            Mapper.CreateMap<Trip, ViewTrip>().ReverseMap();
            Mapper.CreateMap<country, ViewCountry>().ReverseMap();
            Mapper.CreateMap<System.Data.Entity.Spatial.DbGeography, ViewGPS>()
                .ForMember(vgps => vgps.lon, dbgeo => dbgeo.MapFrom(d => d.Longitude))
                .ForMember(vgps => vgps.lat, dbgeo => dbgeo.MapFrom(d => d.Latitude));
            Mapper.CreateMap<ViewActivity, Activity>()
                .ForMember(act => act.GPS, opt => opt.Ignore());
            Mapper.CreateMap<Activity, ViewActivity>()
                .ForMember(va => va.gps,dbAct => dbAct.MapFrom(d => d.GPS));
            Mapper.CreateMap<ActivityType, ViewActivityType>()
                .ForMember(vat => vat.types, at => at.MapFrom(a => a.ActivityType1)).ReverseMap();
        }

    }
}