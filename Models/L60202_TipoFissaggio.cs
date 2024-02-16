using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMBListini.Models
{
    [Table("TipoFissaggio", Schema = "L6020_2")]
    public class L6020_2TipoFissaggio
    {
        [Key]
        public int TipoFissaggioID { get; set; }
        public string TipoFissaggioDesc { get; set; }
        public string TipoFissaggioAcronym { get; set; }
        public decimal TipoFissaggioPrice { get; set; }
        public int TirantiMultiplier { get; set; }
        public decimal TipoFissaggioControflangeMultiplier { get; set; }
        public Boolean TipoFissaggioControflangeSpecial { get; set; }
        public Boolean isActive { get; set; }
        public Boolean OutDiscount { get; set; }
        public int FissaggioPriceCategoryID { get; set; }


    }
}