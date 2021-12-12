using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using InterfaceDAL.Repository;
using Microsoft.Extensions.Configuration;
using ObjectFactory;

namespace CommonUIHelper.Repository
{
    public class PartnerRepository
    {
        private IConfiguration _configuration;
        private IDAL _dal;

        public PartnerRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _dal = (IDAL)Factory.Create(_configuration.GetSection("dbconnector").Value, _configuration);
        }

        public async Task<DataTable> GetUerProducts(int userid)
        {
            string spname = "USP_GetUserProducts";
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("@ip_userid", Convert.ToString(userid));
            return await _dal.getTable(parameters, spname);
        }

        public async Task<DataTable> GetUserCustomers(int userid)
        {
            string spname = "USP_GetUserCustomers";
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("@ip_userid", Convert.ToString(userid));
            return await _dal.getTable(parameters, spname);
        }

        public async Task<DataTable> GetUserRFQ(int userid)
        {
            string spname = "USP_GetRFQMappings";
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("i_supplierid", Convert.ToString(userid));
            return await _dal.getTable(parameters, spname);
        }

        public async Task<DataTable> GetUserRFQDetails(string rfqid,int userid)
        {
            string spname = "USP_GetRfqDetails";
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("i_rfqid", Convert.ToString(rfqid));
            parameters.Add("i_supplierid", Convert.ToString(userid));
            return await _dal.getTable(parameters, spname);
        }
    }
}