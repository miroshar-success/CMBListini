using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMBListini.Models
{
    [Table("vwUserListingIntro")]
    public class vwUserListingIntro
    {
        [Key, Column("UserID", Order = 0)]
        public string UserID { get; set; }
        public string PriceListDesc { get; set; }
        public string LandingPage { get; set; }
        [Key, Column("PriceListID", Order = 1)]
        public int PriceListID { get; set; }
        public Boolean isActive { get; set; }
    }
}