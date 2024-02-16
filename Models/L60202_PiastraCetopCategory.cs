using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMBListini.Models
{
    [Table("PiastraCetopCategory", Schema = "L6020_2")]
    public class L6020_2PiastraCetopCategory
    {
        [Key]
        public int PiastraCetopCategoryID { get; set; }
        public string PiastraCetopCategoryDesc { get; set; }
        public Boolean isActive { get; set; }


    }
}