using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using InterfaceDAL.Repository;

namespace InterfaceDAL.Service
{
    public class DALService:IDAL
    {
        protected string _connectionstring = "";
        public DALService(string connectionstring)
        {
            _connectionstring = connectionstring;
        }

        public virtual void PushOrUpdate(Dictionary<string,string> parameters,string spname)
        {
            
        }

        public virtual Task<DataTable> getTable(Dictionary<string,string> parameters,string spname)
        {
            return null;
        }

        public virtual Task<DataSet> getTables(Dictionary<string,string> parameters,string spname)
        {
            return null;
        }
    }
}