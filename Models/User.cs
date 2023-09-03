using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace my_Ecommerce_App.Models
{
    public class User
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string? UserName { get; set; }

        [Required]
        public string? Email { get; set; }

        public string? Salt { get; set; }

        [Required]
        public string? HashedPassword { get; set; }

        [Required]
        public string? Phone { get; set; }

        [Required]
        public string? Address { get; set; }

        [Required]
        public string? Role { get; set; }

    }
}