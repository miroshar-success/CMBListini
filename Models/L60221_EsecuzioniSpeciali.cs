using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMBListini.Models
{
    [Table("EsecuzioniSpeciali", Schema = "L6022_1")]
    public class L6022_1EsecuzioniSpeciali
    {
        [Key]
        public int EsecuzioniSpecialiID { get; set; }
        public string EsecuzioniSpecialiAcronym { get; set; }
        public string EsecuzioniSpecialiDesc { get; set; }
        public decimal EsecuzioniSpecialiPrice { get; set; }
        public Boolean isActive { get; set; }

    }
}