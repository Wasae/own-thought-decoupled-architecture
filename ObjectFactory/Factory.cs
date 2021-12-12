using System;
using Microsoft.Extensions.Configuration;
using AdoDotNetDAL.Template;
using BusinessLogic;
using InterfaceBL.Repository;

namespace ObjectFactory
{
    public class Factory
    {
        // Design Pattern : Lazy Loading,RIP(Replace If with Polymrphism), Simple Factory
        public static object Create(string type,IConfiguration _configuration)
        {
            if (type == "MySqlDAL")
            {
                return new MySQLAdoDotNetDAL(Convert.ToString(_configuration
                                                              .GetSection("ConnectionStrings")
                                                              .GetSection("DbConnection")
                                                              .Value));
            }
            else if(type == "Sql"){
                return new SQLServerAdoDotNetDAL(Convert.ToString(_configuration
                                                              .GetSection("ConnectionStrings")
                                                              .GetSection("DbConnection")
                                                              .Value));
            }
            else if (type == "Products")
            {
                return new Product();
            }
            else if (type == "Users")
            {
                return new User();
            }
            else if (type=="RFQ")
            {
                return new RFQ();
            }
            return null;
        }

    }
}
