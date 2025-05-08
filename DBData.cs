using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Web;

using Newtonsoft.Json;

namespace Webapp_Demo
{
    public class DBData
    {
        //Had to use old Jet driver to work with Azure. Azure does not have the ACE driver(accdb extension)
        //I wanted to keep it simple with an MSAccess file to demonstrate the DB connection so I had to use the old mdb extension
        private const string connectionstr = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\DB.mdb";

        public string GetEmployeesAll()
        {
            var storedProcedureName = "spEmployeesAll";
            string jsonResult;

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