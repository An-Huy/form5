using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace form5
{
    internal class DBconnection
    {
        SqlConnection sqlConnection;
        public void Connect()
        {
            sqlConnection = new SqlConnection("");
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }
        }
        public void closeConnect()
        {
            if (sqlConnection.State != ConnectionState.Closed)
            {
                sqlConnection.Close();
            }
        }
        public DataTable table(string query)
        {
            DataTable table = new DataTable();
            Connect();
            SqlDataAdapter adapter = new SqlDataAdapter(query, sqlConnection);
            adapter.Fill(table);
            closeConnect();
            table.Dispose();
            return table;
        }
        public void excute(string query)
        {
            Connect();
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            sqlCommand.ExecuteNonQuery();
            closeConnect();
        }
    }
}

