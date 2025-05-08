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
            

        }



        [WebMethod]
        public static string BindDataSP()
        {
            var GetData = new DBData();
           

            return GetData.GetEmployeesAll();

        }
    }
}