using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Webapp_Demo
{
    public class QbClass
    {

        public class Results
        {
            public Dictionary<string, Timesheets> timesheets { get; set; }
        }

        public class QbClassProperties
        {
            public Results results { get; set; }
            public bool more { get; set; }
            public SupplementalData supplemental_data { get; set; }
        }

        public class SupplementalData
        {
            public Dictionary<string, Users> users { get; set; }
        }

        public class Users
        {
            public int id { get; set; }
            public string username { get; set; }
            public string last_name { get; set; }
            public string first_name { get; set; }
            public string employeeNumber { get; set; }
        }

        public class Timesheets
        {
            public int id { get; set; }
            public int user_id { get; set; }
            public int jobcode_id { get; set; }
            public string start { get; set; }
            public string end { get; set; }
            public int duration { get; set; }
            public string date { get; set; }
            public string fullname { get; set; }
        }

        public class TimesheetParameters
        {
            public int id { get; set; }
            public int user_id { get; set; }
            public int jobcode_id { get; set; }
            public string start { get; set; }
            public string end { get; set; }
            public string date { get; set; }
            public int duration { get; set; }

            public string fullname { get; set; }
        }

        public class SupplementDataParameters
        {
            public int id { get; set; }
            public string username { get; set; }
            public string firstName { get; set; }
            public string lastName { get; set; }
            public string employeeNumber { get; set; }
        }

    }
}
