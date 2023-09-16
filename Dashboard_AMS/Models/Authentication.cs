using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Web;

namespace Dashboard_AMS.Models
{
    public class Authentication
    {
        public static string ename { get; set; }
        public static int empid { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public static bool ischeck { get; set; }
        public static int roleid { get; set; }
        
        SqlConnection sc = Connection.SQLCON();

        public bool Login()
        {
            sc.Open();

            SqlCommand cmd = new SqlCommand("select * from AMS_Users where Email='"+ email  +"' and Password='"+ password +"'",sc);
            SqlDataReader sdr = cmd.ExecuteReader();

            if (sdr.HasRows)
            {
                ischeck = true;
                sdr.Read();
                empid = (int)sdr[0];
                ename = (string)sdr[1];
                roleid = (int)sdr[4];
                sc.Close(); return true;
            }
            else
            {
                sc.Close(); return false;
            }

        }


        public bool Logout()
        {
            try
            {
                ischeck = false;
                return true;
            }
            catch (Exception)
            {

                return false;
            }
            
            
        }

        public bool Check_TimeIn()
        {
            sc.Open();

            SqlCommand cmd = new SqlCommand("select * from AMS_Attendance where Emp_ID='" + Authentication.empid + "' and Date='" + DateTime.Now.ToShortDateString() + "'", sc);
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.HasRows)
            {
                sc.Close(); return true;
            }
            else
            {
                sc.Close(); return false;
            }

        }

        public bool Check_TimeOut()
        {
            sc.Open();

            SqlCommand cmd = new SqlCommand("select * from AMS_Attendance where Emp_ID='" + Authentication.empid + "' and Date='" + DateTime.Now.ToShortDateString() + "' and Time_Out IS NOT NULL", sc);
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.HasRows)
            {
                sc.Close();
                return true;
            }
            else
            {
                sc.Close();
                return false;
            }

        }
    }
}