using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMBListini.Models
{
    [Table("GuarnizionePrice", Schema = "L6020_2")]
    public class L6020_2GuarnizionePrice
    {
        [Key]
        public int GuarnizionePriceID { get; set; }
        public decimal GuarnizionePrice { get; set; }
        public int GuarnizionePriceCategoryID { get; set; }
        public decimal GuarnizionePriceAlesaggioLength { get; set; }
        public decimal GuarnizionePriceSteloValue { get; set; }
        public Boolean isTrasduttore { get; set; }



    }
}