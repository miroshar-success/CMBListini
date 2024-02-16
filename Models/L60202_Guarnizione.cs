using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMBListini.Models
{
    [Table("Guarnizione", Schema = "L6020_2")]
    public class L6020_2Guarnizione
    {
        [Key]
        public int GuarnizioneID { get; set; }
        public string GuarnizioneDesc { get; set; }
        public string GuarnizioneAcronym { get; set; }
        public int GuarnizionePriceCategoryID { get; set; }
        public decimal GuarnizioneMultiplier { get; set; }
        public decimal GuarnizioneKitMultiplier { get; set; }
        public Boolean isActive { get; set; }
        public Boolean isMagg { get; set; }
        public Boolean OutDiscount { get; set; }


    }
}