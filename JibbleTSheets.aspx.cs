using System;
using System.Collections.Generic;

using System.Threading.Tasks;
using System.Web.Services;
using System.Xml;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using RestSharp;

namespace Webapp_Demo
{
    public partial class JibbleTSheets : System.Web.UI.Page
    {

        public static string TOKEN;


        protected void Page_Load(object sender, EventArgs e)
        {
            TOKEN = GetAccessToken();
            GetOrganization(TOKEN);
          //  GetTSEntries(TOKEN, "", "");
     //      GetTEntriesRaw(TOKEN, "1900-01-01", "2025-06-01");

        }

        private static string GetAccessToken()
        {
            string apiID = "b0bc1eaf-a708-48e3-b339-b9dab851df4d";
            string apiSecret = "pPva8oWWkTtrk5vv5XrcYk9ZYXKsRy2MrCIW3Ru8PEgmdCCq";


            var options = new RestClientOptions("https://identity.prod.jibble.io");
            var client = new RestClient(options);
            var request = new RestRequest("/connect/token", Method.Post);
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddParameter("grant_type", "client_credentials");
            request.AddParameter("client_id", apiID);
            request.AddParameter("client_secret", apiSecret);
            RestResponse response = client.Execute(request);
            Console.WriteLine(response.Content);

            var json = JObject.Parse(response.Content);

            return (string)json["access_token"];
        }

        private string GetOrganization(string token)
        {
            var options = new RestClientOptions("https://workspace.prod.jibble.io");

            var client = new RestClient(options);
            var request = new RestRequest("/v1/Organizations", Method.Get);
            request.AddHeader("Authorization", "Bearer " + token);
            RestResponse response = client.Execute(request);
            Console.WriteLine(response.Content);

            var json = JsonConvert.DeserializeObject<JObject>(response.Content);
            var orgInfo = json["value"];

            foreach (var item in orgInfo)
            {
                var name = (string)(item["name"] ?? "");
            }

            return (string)(orgInfo[0]["name"] ?? "");

        }


        [WebMethod]
        public static string GetTSEntries(string dateStart, string dateEnd = "")
        {
            var token  = GetAccessToken();

            var options = new RestClientOptions("https://time-attendance.prod.jibble.io");

            var client = new RestClient(options);
            var request = new RestRequest("/v1/TimesheetsSummary?period=Custom&date=" + dateStart + 
                "&endDate="+ dateEnd, Method.Get);
            request.AddHeader("Authorization", "Bearer " + token);
            RestResponse response = client.Execute(request);
            Console.WriteLine(response.Content);

            List<JibbleClass.TimesheetParameters> tmparameters = new List<JibbleClass.TimesheetParameters>();

            var json = JsonConvert.DeserializeObject<JObject>(response.Content);
            JibbleClass.JibbleProperties TSInfo = JsonConvert.DeserializeObject<JibbleClass.JibbleProperties>(response.Content);

            if (TSInfo.value != null)
            {
                foreach (var employee in TSInfo.value)
                {

                    foreach (var day in employee.daily)
                    {
                        var ts = XmlConvert.ToTimeSpan(day.tracked);
                        if (ts.TotalSeconds > 0)
                        {
                            tmparameters.Add(
                             new JibbleClass.TimesheetParameters()
                             {
                                 id = employee.personId,
                                 jobcode_id = employee.project.code ?? "",
                                 date = day.date,
                                 duration = Convert.ToString(decimal.Round((decimal)
                                    (ts.TotalSeconds / 3600),2)),
                                 fullname = employee.person.fullName
                             });
                        }
                    }
                }
            }

            string jsonResult = JsonConvert.SerializeObject(tmparameters);
            return jsonResult;
        }

        [WebMethod]
        public static string GetTEntriesRaw(string dateStart = "1900-01-01", string dateEnd = "1900-01-01")
        {
            var token = GetAccessToken();

            var options = new RestClientOptions("https://time-tracking.prod.jibble.io");

            var client = new RestClient(options);

            var request = new RestRequest("/v1/HourEntries?$expand=activity,project($select=id,name,code)" +
                ",person($select=id,groupId,fullname)" +
                "&$filter=(date ge " + dateStart +
                " and date le "+ dateEnd + ")&$orderby=createdAt asc&" +
                "$select=personId,date,duration,activityId," +
                "projectId&$count=true", Method.Get);
            request.AddHeader("Authorization", "Bearer " + token);
            RestResponse response = client.Execute(request);

            List<JibbleClass.TimesheetParameters> tmparameters = new List<JibbleClass.TimesheetParameters>();

            var json = JsonConvert.DeserializeObject<JObject>(response.Content);
            JibbleClass.JibbleProperties TSInfo = JsonConvert.DeserializeObject<JibbleClass.JibbleProperties>(response.Content);

            if (TSInfo.value != null)
            {
                foreach (var employee in TSInfo.value)
                {
                   
                        var ts = XmlConvert.ToTimeSpan(employee.duration);
                        if (ts.TotalSeconds > 0)
                        {
                            tmparameters.Add(
                             new JibbleClass.TimesheetParameters()
                             {
                                 id = employee.personId,
                                 jobcode_id = employee.project?.code.ToString() ?? "",
                                 date = employee.date,
                                 duration = Convert.ToString(decimal.Round((decimal)
                                    (ts.TotalSeconds / 3600), 2)),
                                 fullname = employee.person.fullName
                             });
                        }                  
                }
            }

            string jsonResult = JsonConvert.SerializeObject(tmparameters);
            return jsonResult;
            //  lblOutput.Text = jsonResult;

        }
    }

}
