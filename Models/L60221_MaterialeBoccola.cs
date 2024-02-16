using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMBListini.Models
{
    [Table("MaterialeBoccola", Schema = "L6022_1")]
    public class L6022_1MaterialeBoccola
    {
        [Key]
        public int MaterialeBoccolaID { get; set; }
        public string MaterialeBoccolaDesc { get; set; }
        public decimal MaterialeBoccolaMultiplier { get; set; }
        public decimal MaterialeBoccolaStPMultiplier { get; set; }
        public Boolean isActive { get; set; }
        public Boolean onDiscount { get; set; }


    }
}