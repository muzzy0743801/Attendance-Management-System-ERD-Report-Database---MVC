using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace Dashboard_AMS.Models
{
    public class Attendance
    {
        public string date { get; set; }
        public string timeIN { get; set; }
        public string timeOUT { get; set; }
        public int Emp_ID { get; set; }
        public string status { get; set; }

        SqlConnection sc = Connection.SQLCON();

        public List<Attendance> Show_Attendance()
        {
            List<Attendance> lst = new List<Attendance>();

            try
            {
                sc.Open();
                SqlCommand cmd = new SqlCommand("Show_Attendance", sc);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@empid", Authentication.empid);
                SqlDataReader sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    if (sdr[2].ToString() != string.Empty)
                    {
                        timeIN = Convert.ToString(Convert.ToDateTime(sdr[2]).ToShortTimeString());
                    }
                    else
                    {
                        timeIN = "-";
                    }

                    if (sdr[3].ToString() != string.Empty)
                    {
                        timeOUT = Convert.ToString((Convert.ToDateTime(sdr[3]).ToShortTimeString()));
                    }
                    else
                    {
                        timeOUT = "-";
                    }

                    lst.Add(new Attendance()
                    {

                        date = Convert.ToString(Convert.ToDateTime(sdr[1]).ToShortDateString()),
                        timeIN = timeIN,
                        timeOUT = timeOUT, //Convert.ToString((DateTime)sdr[3])
                        status = (string)sdr[4]
                    });
                }
                sc.Close();
                sdr.Close();
                return lst;
            }
            catch (SqlException)
            {
                return lst;
            }

        }



        


        
    }
}