//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TripAdv.Repositories
{
    using System;
    using System.Collections.Generic;
    
    public partial class Activity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Activity()
        {
            this.ActivityActivityTypeMaps = new HashSet<ActivityActivityTypeMap>();
        }
    
        public int ActivityID { get; set; }
        public Nullable<int> CityID { get; set; }
        public Nullable<System.DateTimeOffset> SpendTime { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public System.Data.Entity.Spatial.DbGeography GPS { get; set; }
        public string URL { get; set; }
        public Nullable<byte> Priority { get; set; }
        public string Contact { get; set; }
        public Nullable<int> Valuation { get; set; }
        public Nullable<int> GoogleMapsPlaceID { get; set; }
        public int TripID { get; set; }
        public Nullable<System.DateTime> DiaryTime { get; set; }
    
        public virtual Trip Trip { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ActivityActivityTypeMap> ActivityActivityTypeMaps { get; set; }
    }
}
