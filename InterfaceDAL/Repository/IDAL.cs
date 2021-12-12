using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections;
using System.Data;
using System.Collections.Generic;

namespace InterfaceDAL.Repository
{
    public interface IDAL
    {
        void PushOrUpdate(Dictionary<string,string> parameters,string spname);
        Task<DataTable> getTable(Dictionary<string,string> parameters, string spname);
        Task<DataSet> getTables(Dictionary<string,string> parameters, string spname);
    }
}