using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMBListini.Models
{
    [Table("Serie", Schema = "L6020_2")]
    public class L6020_2Serie
    {
        [Key]
        public string SerieID { get; set; }
        public string SerieDesc { get; set; }
        public Boolean SerieisActive { get; set; }
        public decimal SeriePrice { get; set; }
        public Boolean SerieTransducer { get; set; }
        public Boolean SerieMagnets { get; set; }
        public Boolean SerieInox { get; set; }
        public Boolean isStandard { get; set; }
        public decimal SerieTransducerMultiplier { get; set; }


    }
}