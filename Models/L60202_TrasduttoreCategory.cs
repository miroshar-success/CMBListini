using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMBListini.Models
{
    [Table("TrasduttoreCategory", Schema = "L6020_2")]
    public class L6020_2TrasduttoreCategory
    {
        [Key]
        public int TrasduttoreCategoryID { get; set; }
        public string TrasduttoreCategoryDesc { get; set; }



    }
}