using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMBListini.Models
{
    [Table("TrasduttoreCategory", Schema = "L6022_1")]
    public class L6022_1TrasduttoreCategory
    {
        [Key]
        public int TrasduttoreCategoryID { get; set; }
        public string TrasduttoreCategoryDesc { get; set; }



    }
}