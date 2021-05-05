using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Models
{
    public class FilePath
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string Path { get; set; }
        [Required]
        public int ProductId { get; set; }

    
    }
}
