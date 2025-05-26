using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Newtonsoft.Json;

namespace Webapp_Demo
{
    public class JibbleClass
    {
        public class Project
        {
            public string organizationId { get; set; }
            public string clientId { get; set; }
            public object locationId { get; set; }
            public string name { get; set; }
            public string code { get; set; }
            public object description { get; set; }
            public string status { get; set; }
            public string id { get; set; }
        }

        public class Daily
        {
            public string date { get; set; }
            public object firstIn { get; set; }
            public object lastOut { get; set; }
            public string paidBreak { get; set; }
            public string unpaidBreak { get; set; }
            public string tracked { get; set; }
            public string regular { get; set; }
            public string payrollHours { get; set; }
            public string overtime { get; set; }
            public string dailyOvertime { get; set; }
            public string dailyDoubleOvertime { get; set; }
            public string restDayOvertime { get; set; }
            public string publicHolidayOvertime { get; set; }
        }

        public class Activity
        {
            public string organizationId { get; set; }
            public string name { get; set; }
            public string code { get; set; }
            public object description { get; set; }
            public string color { get; set; }
            public string status { get; set; }
            public string id { get; set; }
        }

        public class Person
        {
            public string fullName { get; set; }
            public object pictureUrl { get; set; }
            public string timeZone { get; set; }
            public string code { get; set; }
            public string status { get; set; }
            public object groupId { get; set; }
        }

        public class JibbleProperties
        {
            [JsonProperty("@odata.context")]
            public string odatacontext { get; set; }
            [JsonProperty("@odata.count")]
            public int odatacount { get; set; }
            public List<Value> value { get; set; }
        }

        public class Value
        {
            public string personId { get; set; }
            public string total { get; set; }
            public Person person { get; set; }
            public List<Daily> daily { get; set; }
            public string date { get; set; }
            public string duration { get; set; }
            public string activityId { get; set; }
            public string projectId { get; set; }
            public Activity activity { get; set; }
            public Project project { get; set; }

        }



        public class TimesheetParameters
        {
            public string id { get; set; }
            public int user_id { get; set; }
            public string jobcode_id { get; set; } = string.Empty;

            public string start { get; set; }
            public string end { get; set; }
            public string date { get; set; }
            public string duration { get; set; }

            public string fullname { get; set; }
        }
    }
}