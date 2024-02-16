using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMBListini.Models
{
    [Table("Controflange", Schema = "L6020_2")]
    public class L6020_2Controflange
    {
        [Key]
        public int ControflangeID { get; set; }
        public decimal ControflangeAlesaggioLength { get; set; }
        public decimal ControflangePrice { get; set; }


    }
}