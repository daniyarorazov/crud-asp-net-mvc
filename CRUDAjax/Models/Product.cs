using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRUDAjax.Models
{
    // Creating model of Product
    public class Product
    {
        // Product property of type int and string with get and set accessors
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Price { get; set; }
        public string ImageName { get; set; }
    }
}