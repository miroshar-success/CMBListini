using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMBListini.Models
{
    [Table("ProtezioneTrasduttore", Schema = "L6022_1")]
    public class L6022_1ProtezioneTrasduttore
    {
        [Key]
        public int ProtezioneTrasduttoreID { get; set; }
        public decimal ProtezioneTrasduttorePrice { get; set; }
        public decimal ProtezioneTrasduttoreMultiplier { get; set; }
        public Boolean inUse { get; set; }
        public Boolean onDiscount { get; set; }


    }
}