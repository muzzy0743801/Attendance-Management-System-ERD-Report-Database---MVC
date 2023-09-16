using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;


namespace Dashboard_AMS.Models
{
    public class Jobs
    {
        public int job_id { get; set; }
        public string job_title { get; set; }

        SqlConnection sc = Connection.SQLCON();

        public bool Add_Job()
        {
            try
            {
                sc.Open();
                string query = "insert into AMS_Jobs values('" + job_title + "')";
                SqlCommand cmd = new SqlCommand(query, sc);
                cmd.ExecuteNonQuery();
                sc.Close();
                return true;
            }
            catch (SqlException)
            {
                return false;
            }

        }


        public List<Jobs> ShowAll_Jobs()
        {
            List<Jobs> ls = new List<Jobs>();

            try
            {
                sc.Open();
                string query = "Select * from AMS_Jobs";
                SqlCommand cmd = new SqlCommand(query, sc);
                SqlDataReader sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    ls.Add(new Jobs()
                    {
                        job_id = (int)sdr[0],
                        job_title = (string)sdr[1]
                    });
                }
                sc.Close();
                sdr.Close();
                return ls;
            }
            catch (SqlException)
            {
                return ls;
            }

        }




        public Jobs SearchJob()
        {
            Connection.SQLCON().Open();
            string a = "Select * From AMS_Jobs Where Job_ID='" + job_id + "'";
            SqlCommand sc = new SqlCommand(a, Connection.SQLCON());
            SqlDataReader sdr = sc.ExecuteReader();

            Jobs b = new Jobs();
            while (sdr.Read())
            {

                b.job_id = (int)sdr[0];
                b.job_title = (string)sdr[1];
            }
            sdr.Close();
            Connection.SQLCON().Close();
            return b;

        }
        //sort depar

        public List<Jobs> Sort(string obj)
        {
            Connection.SQLCON().Open();
            //string a = "select * from AMS_Employees order by" + obj;
            SqlCommand sc = new SqlCommand("select * from AMS_Jobs order by '" + obj + "'", Connection.SQLCON());
            SqlDataReader sdr = sc.ExecuteReader();
            List<Jobs> lst = new List<Jobs>();
            while (sdr.Read())
            {
                Jobs b = new Jobs()
                {
                    job_id = (int)sdr[0],
                    job_title = (string)sdr[1],
                };
                lst.Add(b);
            }
            sdr.Close();
            Connection.SQLCON().Close();
            return lst;

        }


        public bool Update_Job()
        {
            try
            {
                sc.Open();
                SqlCommand cmd = new SqlCommand("Update AMS_Jobs set Job_Title ='" + job_title + "' where Job_ID ='" + job_id + "'", sc);
                cmd.ExecuteNonQuery();
                sc.Close();
                return true;
            }
            catch (Exception ex)
            {
                string err = ex.ToString();
                sc.Close();
                return false;
            }
        }


    }
}