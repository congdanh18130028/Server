using Microsoft.EntityFrameworkCore;
using Shop.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Models
{
    [Index(nameof(Email), IsUnique = true)]
    public class Customer
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Column(TypeName ="nvarchar(255)")]
        public string Name { get; set; }
        [Required]
        [Column(TypeName = "varchar(25)")]
        public string Phone { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(255)")]
        public string Address { get; set; }

        [Required]
        [Column(TypeName = "varchar(10)")]
        public string Role { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(255)")]
        public string Email { get; set; }
        [Required]
        [Column(TypeName = "varchar(100)")]
        public string Password { get; set; }
    }
}
