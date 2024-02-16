using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMBListini.Models
{
    [Table("FissaggioPriceCategory", Schema = "L6020_2")]
    public class L6020_2FissaggioPriceCategory
    {
        [Key]
        public int FissaggioPriceCategoryID { get; set; }
        public string FissaggioPriceCategoryDesc { get; set; }
        public Boolean isStandard { get; set; }
        public Boolean isAmmortizzatori { get; set; }
        public Boolean isSnodo { get; set; }



    }
}