using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRUDAjax.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Price { get; set; }
        public string ImageName { get; set; }
    }
}