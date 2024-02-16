using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMBListini.Models
{
    [Table("GuarnizionePriceCategory", Schema = "L6022_1")]
    public class L6022_1GuarnizionePriceCategory
    {
        [Key]
        public int GuarnizionePriceCategoryID { get; set; }
        public string GuarnizionePriceCategoryDesc { get; set; }
        public Boolean isStandard { get; set; }



    }
}