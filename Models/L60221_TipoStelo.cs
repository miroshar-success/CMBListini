using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMBListini.Models
{
    [Table("TipoStelo", Schema = "L6022_1")]
    public class L6022_1TipoStelo
    {
        [Key]
        public int TipoSteloID { get; set; }
        public string TipoSteloAcronym { get; set; }
        public string TipoSteloDesc { get; set; }
        public decimal TipoSteloPrice { get; set; }
        public Boolean TipoSteloPassante { get; set; }
        public Boolean TipoSteloAmmortizzato { get; set; }
        public Boolean isActive { get; set; }
        public int TipoSteloNAmmortizzatori { get; set; }

    }
}