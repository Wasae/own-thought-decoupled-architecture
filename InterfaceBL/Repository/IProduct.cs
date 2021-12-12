using System;

namespace InterfaceBL.Repository
{
    public interface IProduct
    {
        public int ID { get; set; }
        public string ProductSKU { get; set; }
        public string ProductName { get; set; }
        public string CreatedDate { get; set; }
        public string ModifiedDate { get; set; }
        public string Quantity { get; set; }
        public string Price { get; set; }
        public int IsActive { get; set; }
    }
}