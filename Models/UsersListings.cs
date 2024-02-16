using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMBListini.Models
{
    [Table("UsersListings")]
    public class UserListing
    {
        [Key]
        public int UsersListingsID { get; set; }
        public string UserID { get; set; }
        public int PriceListID { get; set; }
    }
}