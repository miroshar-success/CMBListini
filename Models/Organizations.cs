using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMBListini.Models
{
    [Table("Organizations")]
    public class Organization
    {
        [Key]
        public int OrganizationID { get; set; }
        public string OrganizationName { get; set; }
        public string OrganizationDiscount { get; set; }
    }
}