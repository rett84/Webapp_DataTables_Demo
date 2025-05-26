using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Web;

using Newtonsoft.Json;

using Webapp_Demo.Properties;

namespace Webapp_Demo
{
    public class DBData
    {
        public string GetEmployeesAll()
        {
            var storedProcedureName = "spEmployeesAll";
            string jsonResult;
            string connectionstr = Settings.Default.DBConnString;

            // establish connection to DB, define command to execute stored procedure
            using (OleDbConnection conn = new OleDbConnection(connectionstr))
            using (OleDbCommand cmd = new OleDbCommand(storedProcedureName, conn))
            {
                try
                {
                    // set type of command to stored procedure
                    cmd.CommandType = CommandType.StoredProcedure;

                    conn.Open();
                    Console.WriteLine("successful connection");

                    DataSet ds = new DataSet();

                    using (OleDbDataAdapter adapter = new OleDbDataAdapter(cmd))
                    {
                        adapter.Fill(ds, "table");
                        // Access the DataTable
                        var table = ds.Tables["table"];


                        jsonResult = JsonConvert.SerializeObject(table);

                    }
                    cmd.Dispose();
                    //return serialized Json
                    return jsonResult;
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex);
                    return null;
                }
            }
        }
    }
}