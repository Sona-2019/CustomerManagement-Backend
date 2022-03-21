using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerAPI.Database.Models
{
    public class CustomerDetails
    {
        [Key]
        public int CustomerId { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public string Username { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string Password { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public string FirstName { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public string LastName { get; set; }

        [Required]
        [Column(TypeName = "varchar(200)")]
        public string Address { get; set; }

        [Required]
        [Column(TypeName = "varchar(320)")]
        public string EmailID { get; set; }

        [Required]
        [Column(TypeName = "varchar(20)")]
        public string PhoneNumber { get; set; }
    }
}
