using System;
using InterfaceDAL.Service;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Threading.Tasks;

namespace AdoDotNetDAL.Template
{
    public class MySQLAdoDotNetDAL:DALService
    {
        private MySqlConnection _connection = null;
        private MySqlCommand _command = null;
        public MySQLAdoDotNetDAL(string connectionstring):base(connectionstring)
        {}

        private void Open()
        {
            _connection = new MySqlConnection(_connectionstring);
            _connection.Open();
            _command = new MySqlCommand();
            _command.Connection = _connection;
            _command.CommandType = CommandType.StoredProcedure;
        }

        private void Close()
        {
            _connection.Close();
        }

        private void ExecuteNonQuery(Dictionary<string,string> parameters,string spname)
        {
            Open();
            if (parameters != null && parameters.Count != 0)
            {
                foreach (KeyValuePair<string,string> parameter in parameters)
                {
                    _command.Parameters.AddWithValue(parameter.Key,parameter.Value);
                }
            }
            if (!String.IsNullOrEmpty(spname))
            {
                _command.CommandText = spname;
                _command.ExecuteNonQuery();
            }
            Close();
        }

        private DataTable ExecuteDataTable(Dictionary<string,string> parameters,string spname)
        {
            DataTable dt = new DataTable();
            Open();
            if (parameters != null && parameters.Count != 0)
            {
                foreach (KeyValuePair<string,string> parameter in parameters)
                {
                    _command.Parameters.AddWithValue(parameter.Key,parameter.Value);
                }
            }
            if (!String.IsNullOrEmpty(spname))
            {
                _command.CommandText = spname;
                MySqlDataAdapter da = new MySqlDataAdapter(_command);
                da.Fill(dt);
            }
            Close();
            return dt;
        }

        private DataSet ExecuteDataSet(Dictionary<string,string> parameters,string spname)
        {
            DataSet ds = new DataSet();
            Open();
            if (parameters != null && parameters.Count != 0)
            {
                foreach (KeyValuePair<string,string> parameter in parameters)
                {
                    _command.Parameters.AddWithValue(parameter.Key,parameter.Value);
                }
            }
            if (!String.IsNullOrEmpty(spname))
            {
                _command.CommandText = spname;
                MySqlDataAdapter da = new MySqlDataAdapter(_command);
                da.Fill(ds);
            }
            Close();
            return ds;
        }

        public override void PushOrUpdate(Dictionary<string,string> parameters,string spname)
        {
           ExecuteNonQuery(parameters,spname);
        }

        async public override Task<DataTable> getTable(Dictionary<string,string> parameters,string spname)
        {
            return ExecuteDataTable(parameters,spname);
        }

        async public override Task<DataSet> getTables(Dictionary<string,string> parameters,string spname)
        {
            return ExecuteDataSet(parameters,spname);
        }
    }
}