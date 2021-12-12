using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using CommonUIHelper.Repository;
using InterfaceBL.Repository;
using Microsoft.Extensions.Configuration;
using ObjectFactory;

namespace CommonUIHelper.Curator
{
    public class ProductCurator
    {
        private IConfiguration _configuration;
        private ProductRepository _repository;

        public ProductCurator(IConfiguration configuration)
        {
            _configuration = configuration;
            _repository = new ProductRepository(_configuration);
        }

        public async Task<bool> AddProduct(string userid, string sku, string productname, string price, string quantity)
        {
            var t = await _repository.AddProduct(userid, sku, productname, price, quantity);
            if (t != null && t.Rows.Count != 0 && Convert.ToInt32(t.Rows[0]["Id"]) == 1)
            {
                return true;
            }
            return false;
        }
    }
}