using System;
using InterfaceBL.Repository;

namespace BusinessLogic
{
    public class Product : IProduct
    {
        public int ID { get; set; }
        public string ProductSKU { get; set; }
        public string ProductName { get; set; }
        public string CreatedDate { get; set; }
        public string ModifiedDate { get; set; }
        public int IsActive { get; set; }
        public string Quantity { get; set; }
        public string Price { get; set; }
    }
}