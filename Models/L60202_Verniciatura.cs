using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMBListini.Models
{
    [Table("Verniciatura", Schema = "L6020_2")]
    public class L6020_2Verniciatura
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