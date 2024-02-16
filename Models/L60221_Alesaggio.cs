using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMBListini.Models
{
    [Table("Alesaggio", Schema = "L6022_1")]
    public class L6022_1Alesaggio
    {
        [Key]
        public int AlesaggioID { get; set; }
        public decimal AlesaggioLength { get; set; }
        public string AlesaggioSerieID { get; set; }
        public Boolean AlesaggioisActive { get; set; }



    }
}