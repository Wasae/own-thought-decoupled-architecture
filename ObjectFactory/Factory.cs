using System;
using System.Collections;
using System.Collections.Generic;
using InterfaceDAL.Repository;
using InterfaceDAL.Service;
using AdoDotNetDAL.Template;

namespace ObjectFactory
{
    public class Factory
    {
        // Design Pattern : Lazy Loading,RIP(Replace If with Polymrphism), Simple Factory
        public static Dictionary<string,object> objfactory = null;
        public Factory()
        {
            
        }

        public static object Create(string type)
        {
            if (objfactory == null)
            {
                objfactory = new Dictionary<string,object>();
                objfactory.Add("DAL",new SQLServerAdoDotNetDAL("connection string"));
            }
            return objfactory[type];
        }

        public static Dictionary<string,object> FillFactory()
        {
            return objfactory;
        }
    }
}
