using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMBListini.Models
{
    [Table("VerniciaturaPrice", Schema = "L6022_1")]
    public class L6022_1VerniciaturaPrice
    {
        [Key]
        public int VerniciaturaPriceID { get; set; }
        public decimal VerniciaturaBasePrice { get; set; }
        public decimal AlesaggioLength { get; set; }
        public decimal VerniciaturaAfterBasePrice { get; set; }
        public int VerniciaturaID { get; set; }


    }
}