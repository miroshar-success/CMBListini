using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMBListini.Models
{
    [Table("PiastraCetopCategory", Schema = "L6022_1")]
    public class L6022_1PiastraCetopCategory
    {
        [Key]
        public int PiastraCetopCategoryID { get; set; }
        public string PiastraCetopCategoryDesc { get; set; }
        public Boolean isActive { get; set; }


    }
}