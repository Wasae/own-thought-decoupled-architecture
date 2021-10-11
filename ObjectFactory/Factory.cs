using System;
using System.Collections;
using System.Collections.Generic;
using InterfaceDAL.Repository;
using InterfaceDAL.Service;
using AdoDotNetDAL.Template;
using Microsoft.Extensions.Configuration;

namespace ObjectFactory
{
    public class Factory
    {
        // Design Pattern : Lazy Loading,RIP(Replace If with Polymrphism), Simple Factory
        private static IConfiguration _configuration = null;
        public static Dictionary<string,object> objfactory = null;
        public Factory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public static object Create(string type)
        {
            if (objfactory == null)
            {
                objfactory = new Dictionary<string,object>();
                objfactory.Add("DAL",new SQLServerAdoDotNetDAL(Convert.ToString(_configuration
                                                                                    .GetSection("ConnectionStrings")
                                                                                    .GetSection("DbConnection")
                                                                                    .Value)));
            }
            return objfactory[type];
        }

        public static Dictionary<string,object> FillFactory()
        {
            return objfactory;
        }
    }
}
