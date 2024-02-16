using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMBListini.Models
{
    [Table("GuarnizionePriceCategory", Schema = "L6020_2")]
    public class L6020_2GuarnizionePriceCategory
    {
        [Key]
        public int GuarnizionePriceCategoryID { get; set; }
        public string GuarnizionePriceCategoryDesc { get; set; }
        public Boolean isStandard { get; set; }



    }
}