using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMBListini.Models
{
    [Table("AccessoriCategory", Schema = "L6022_1")]
    public class L6022_1AccessoriCategory
    {
        [Key]
        public int AccessoriCategoryID { get; set; }
        public string AccessoriCategoryCode { get; set; }
        public string AccessoriCategoryDesc { get; set; }
        public string AccessoriCategoryDesc2 { get; set; }
        public int AccessoriGroupID { get; set; }
        public decimal AccessoriCategoryPriceMultiplier { get; set; }
        public Boolean XOption { get; set; }
        public Boolean isActive { get; set; }



    }
}