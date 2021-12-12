using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using InterfaceDAL.Repository;
using Microsoft.Extensions.Configuration;
using ObjectFactory;

namespace CommonUIHelper.Repository
{
    public class ProductRepository
    {
        private IConfiguration _configuration;
        private IDAL _dal;

        public ProductRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _dal = (IDAL)Factory.Create(_configuration.GetSection("dbconnector").Value, _configuration);
        }

        public async Task<DataTable> AddProduct(string userid,string sku,string productname,string price,string quantity)
        {
            string spname="USP_ADD_MAP_UserProduct";
            Dictionary<string,string> parameters= new Dictionary<string, string>();
            parameters.Add("@userid",userid);
            parameters.Add("@productsku",sku);
            parameters.Add("@productname",productname);
            parameters.Add("@quantity",quantity);
            parameters.Add("@price",price);
            return await _dal.getTable(parameters,spname);
        }
    }
}