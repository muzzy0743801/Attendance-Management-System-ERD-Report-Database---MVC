using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Dashboard_AMS.Models
{
    public class Reporting
    {
        public string datefrom { get; set; }
        public string dateto { get; set; }
        public int empID { get; set; }
        public string fname { get; set; }
        public string lname { get; set; }
        public string job_title { get; set; }
        public string dob { get; set; }
        public long cnic { get; set; }
        public int salary { get; set; }
        public string joindate { get; set; }
        public int jobid { get; set; }
        public int branchid { get; set; }
        public int deptid { get; set; }
        public object obj { get; set; }

        public string date { get; set; }
        public string timein { get; set; }
        public string timeout { get; set; }

        //for leaves
        public int leaveid { get; set; }
        public int Leave_Type { get; set; }
        public string From_Date { get; set; }
        public string To_Date { get; set; }
        public string Description { get; set; }
        public int status { get; set; }
        SqlConnection sc = Connection.SQLCON();

        public DataTable ShowAllAttendance()
        {
           // DataTable dt = null;
            DataSet ds = new DataSet();
            sc.Open();
            // string command = "select * from AMS_Attendance where Date between '" + datefrom + "' AND '" + dateto + "'";
            string command = "Show_AllAttendance_Report";
            //string command = "select Emp_ID , Date , CONVERT(char(8),Time_IN,108) as Time_IN , CONVERT(char(8),Time_Out,108) as Time_Out , st.Status from AMS_Attendance at Inner JOIN AMS_Status st on at.Status = st.Status_ID where at.Date between '" + datefrom + "' AND '" + dateto + "'";
            SqlCommand cmd = new SqlCommand(command, sc);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@datefrom", Convert.ToString(datefrom));
            cmd.Parameters.AddWithValue("@dateto", Convert.ToString(dateto));
            SqlDataAdapter sda = new SqlDataAdapter(command, sc);
            sda.SelectCommand = cmd;
            sda.Fill(ds);
            SqlDataReader sdr = cmd.ExecuteReader();
            sdr.Close();
            sc.Close();
            return ds.Tables[0];
        }

        public DataTable ShowAllEmp()
        {
            DataSet ds = new DataSet();
            sc.Open();
            string command = "show_all_emp";
            SqlCommand cmd = new SqlCommand(command, sc);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sda = new SqlDataAdapter(command, sc);
            sda.SelectCommand = cmd;
            sda.Fill(ds);
            sc.Close();
            return ds.Tables[0];
        }

        public DataTable ShowAllLeaves()
        {

            DataSet ds = new DataSet();
            sc.Open();
            string command = "show_all_leaves_report";
            SqlCommand cmd = new SqlCommand(command, sc);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@fromdate", Convert.ToDateTime(datefrom));
            cmd.Parameters.AddWithValue("@todate", Convert.ToDateTime(dateto));
            SqlDataAdapter sda = new SqlDataAdapter(command, sc);
            sda.SelectCommand = cmd;

            sda.Fill(ds);



            sc.Close();
            return ds.Tables[0];
        }
    }
}