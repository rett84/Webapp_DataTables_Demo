using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Webapp_Demo;

using Newtonsoft.Json;

namespace Webapp_Demo
{
    public partial class DataTable : System.Web.UI.Page
    {
        public DataTable()
        {
           
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            



           // BindDataold();
        }

        public void BindData()
        {
            var connectionstr = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\DB.accdb");


            var SQL = "SELECT * FROM [Employees]";

            OleDbDataAdapter adapter = new OleDbDataAdapter(SQL, connectionstr);
            DataSet dataSet = new DataSet();

            adapter.Fill(dataSet, "table");

            // Access the DataTable
            var table = dataSet.Tables["table"];


            var jsonResult = JsonConvert.SerializeObject(table);

            Response.Write(jsonResult);
        }

        private void BindDataold()
        {
            var conexao = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\DB.mdb");

            string SQL = "SELECT * FROM [Employees]";

            DataSet ds = new DataSet();
            OleDbDataAdapter da = new OleDbDataAdapter();
            OleDbCommand comando;
            conexao.Open();
            comando = new OleDbCommand(SQL, conexao);
            da.SelectCommand = comando;
            comando.ExecuteNonQuery();
            da.Fill(ds, "tabela");
            var table = ds.Tables["tabela"];

            conexao.Close();
           


            var jsonResult = JsonConvert.SerializeObject(table);

            Response.Write(jsonResult);
        }


        [WebMethod]
        public static string BindDataSP()
        {
            var GetData = new DBData();
           

            return GetData.GetEmployeesAll();

        }
    }
}