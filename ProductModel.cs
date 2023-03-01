using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace MDpro.Models
{
    public class ProductModel
    {
        public int ProductId { get; set; }
       [DisplayName("Product Name")]
        public string ProductName { get; set; }

        public decimal Price { get; set; }

        public int Count { get; set; }
    }
}