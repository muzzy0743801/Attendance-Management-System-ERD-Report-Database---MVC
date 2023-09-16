using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Web;

namespace Dashboard_AMS.Models
{
    public class Connection
    {
        public static SqlConnection sc;

        public static SqlConnection SQLCON()
            {
            if (sc == null)
            {
                sc = new SqlConnection(@"Data Source=AHMED\MYSQL2014;Initial Catalog=Attendance_db;Integrated Security=True");
            }
            return sc;
        }
    }
}