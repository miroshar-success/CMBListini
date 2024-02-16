using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMBListini.Models
{
    [Table("VerniciaturaPrice", Schema = "L6020_2")]
    public class L6020_2VerniciaturaPrice
    {
        [Key]
        public int VerniciaturaPriceID { get; set; }
        public decimal VerniciaturaBasePrice { get; set; }
        public decimal AlesaggioLength { get; set; }
        public decimal VerniciaturaAfterBasePrice { get; set; }
        public int VerniciaturaID { get; set; }


    }
}