using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using InterfaceDAL.Repository;
using Microsoft.Extensions.Configuration;
using ObjectFactory;

namespace CommonUIHelper.Repository
{
    public class AuthRepository
    {
        private IConfiguration _configuration;
        private IDAL _dal;
        public AuthRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _dal = (IDAL)Factory.Create(_configuration.GetSection("dbconnector").Value,_configuration);
        }

        async public Task<DataTable> login(string username, string password)
        {
            string spname ="USP_PartnerLogin";
            Dictionary<string,string> parameters = new Dictionary<string, string>();
            parameters.Add("@ip_username",username);
            parameters.Add("@ip_password",password);
            return await _dal.getTable(parameters,spname);
        }
    }
}