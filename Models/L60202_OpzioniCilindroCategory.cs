using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMBListini.Models
{
    [Table("OpzioniCilindroCategory", Schema = "L6020_2")]
    public class L6020_2OpzioniCilindroCategory
    {
        [Key]
        public int OpzioniCilindroCategoryID { get; set; }
        public string OpzioniCilindroCategoryDesc { get; set; }
        public Boolean isStandard { get; set; }


    }
}