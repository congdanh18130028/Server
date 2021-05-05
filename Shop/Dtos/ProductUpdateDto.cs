using Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Dtos
{
    public class ProductUpdateDto
    {
        public string Name { get; set; }

        public string Category { get; set; }

        public List<FilePath> ImgPath { get; set; }

        public string Description { get; set; }

        public int Quantity { get; set; }

        public int Price { get; set; }
    }
}
