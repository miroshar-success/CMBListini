using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMBListini.Models
{
    [Table("Users")]
    public class User
    {
        [Key]
        public string UserID { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }
        public int OrganizationID { get; set; }
        public DateTime? ValidityDate { get; set; }
    }
}