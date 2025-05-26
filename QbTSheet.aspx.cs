using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using RestSharp;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Webapp_Demo
{
    public partial class QbTSheet : System.Web.UI.Page
    {

    

        protected void Page_Load(object sender, EventArgs e)
        {
           
          
        }

        [WebMethod]
        public static string TSDatatoJson(string dateStart, string dateEnd)
        {
            string TOKEN = "S.26__8767e49c9a94030c9185bc71252e42d0e6ad6a10";
           
            var client = new RestClient("https://rest.tsheets.com");
            var request = new RestRequest("/api/v1/timesheets?start_date=" + dateStart + "&end_date="+dateEnd+"&on_the_clock=both");
            request.AddHeader("Authorization", "Bearer " + TOKEN);
            var response = client.Execute(request);

            List<QbClass.TimesheetParameters> tmparameters = new List<QbClass.TimesheetParameters>();


            // Parse the JSON
           // JObject json = JObject.Parse(response.Content);
            var Jobj = JsonConvert.DeserializeObject<JObject>(response.Content);
            var timesheets = Jobj["results"]?["timesheets"] as JObject;
            var usersInfo = Jobj["supplemental_data"]?["users"] as JObject;

            QbClass.QbClassProperties TSInfo = JsonConvert.DeserializeObject<QbClass.QbClassProperties>(response.Content);


            if (TSInfo.results != null)
            {

                foreach (var item in TSInfo.results.timesheets)
                {
                    
                    tmparameters.Add(
                        new QbClass.TimesheetParameters()
                        {
                            id = item.Value.id,
                            user_id = item.Value.user_id,
                            jobcode_id = item.Value.jobcode_id,
                            start = item.Value.start,
                            end = item.Value.end,
                            date = item.Value.date,
                            duration = (item.Value.duration) / 3600,
                            fullname = TSInfo.supplemental_data.users.First(x => x.Value.id == item.Value.user_id).Value.first_name + " "+
                            TSInfo.supplemental_data.users.First(x => x.Value.id == item.Value.user_id).Value.last_name,
                        });
                }
            }

            string jsonResult = JsonConvert.SerializeObject(tmparameters);
            return jsonResult;
        }
    }
}
