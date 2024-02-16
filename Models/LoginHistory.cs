using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMBListini.Models
{
    [Table("LoginHistory")]
    public class LoginHistory
    {
        [Key]
        public string LogUsername { get; set; }
        public string LogIPAddress { get; set; }
        public string LogUserAgent { get; set; }
        public Boolean LoginSuccessful { get; set; }
    }
}