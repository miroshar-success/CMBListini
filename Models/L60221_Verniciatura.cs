using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMBListini.Models
{
    [Table("Verniciatura", Schema = "L6022_1")]
    public class L6022_1Verniciatura
    {
        [Key]
        public int VerniciaturaID { get; set; }
        public string VerniciaturaDesc { get; set; }
        public decimal VerniciaturaBaseLength { get; set; }
        public decimal VerniciaturaAfterBaseLength { get; set; }
        public Boolean isActive { get; set; }
        public Boolean onDiscount { get; set; }


    }
}