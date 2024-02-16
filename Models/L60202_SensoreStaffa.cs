using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMBListini.Models
{
    [Table("SensoreStaffa", Schema = "L6020_2")]
    public class L6020_2SensoreStaffa
    {
        [Key]
        public int SensoreStaffaID { get; set; }
        public decimal SensorePrice { get; set; }
        public decimal StaffaPrice { get; set; }
        public Boolean onDiscount { get; set; }
        public Boolean inUse { get; set; }



    }
}