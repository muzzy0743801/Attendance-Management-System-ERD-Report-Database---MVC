using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace Dashboard_AMS.Models
{
    public class Leaves:Employees
    {
        public int leave_ID { get; set; }
        public int type_Id { get; set; }
        public string leave_type { get; set; }
        public string description { get; set; }
        public string fromdate { get; set; }
        public string todate { get; set; }
        public string status { get; set; }
        public int status_id { get; set; }
        public int Emp_ID { get; set; }


        SqlConnection sc = Connection.SQLCON();


        public List<Leaves> Show_LeaveType()
        {
            List<Leaves> ls = new List<Leaves>();

            try
            {
                sc.Open();
                string query = "Select * from AMS_Leavetypes";
                SqlCommand cmd = new SqlCommand(query, sc);
                SqlDataReader sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    ls.Add(new Leaves()
                    {
                        type_Id = (int)sdr[0],
                        leave_type = (string)sdr[1]
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

        public bool Apply_leave()
        {
            try
            {
                sc.Open();
                SqlCommand cmd = new SqlCommand("Apply_leave", sc);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@empid", Authentication.empid);
                cmd.Parameters.AddWithValue("@leavetype", type_Id);
                cmd.Parameters.AddWithValue("@from", Convert.ToDateTime(fromdate).Date.ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@to", Convert.ToDateTime(todate).Date.ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@description", description);
                cmd.ExecuteNonQuery();
                sc.Close();
                return true;
            }
            catch (SqlException)
            {
                sc.Close();
                return false;

            }
        }


        public List<Leaves> Show_history()
        {
            List<Leaves> ls = new List<Leaves>();

            try
            {
                sc.Open();
                SqlCommand cmd = new SqlCommand("Show_AllLeaves", sc);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@empid", Authentication.empid);
                SqlDataReader sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    ls.Add(new Leaves()
                    {
                        leave_ID = (int)sdr[0],
                        leave_type = (string)sdr[4],
                        fromdate = Convert.ToString((DateTime)sdr[2]),
                        todate = Convert.ToString((DateTime)sdr[3]),
                        description = (string)sdr[1],
                        status = (string)sdr[5],
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



        public List<Leaves> ShowAll_Leaves()
        {
            List<Leaves> ls = new List<Leaves>();

            try
            {
                sc.Open();
                SqlCommand cmd = new SqlCommand("Show_Leaves", sc);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                SqlDataReader sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    ls.Add(new Leaves()
                    {
                        empID = (int)sdr[0],
                        fname = (string)sdr[1],
                        leave_ID = (int)sdr[2],
                        leave_type = (string)sdr[6],
                        fromdate = Convert.ToString((DateTime)sdr[4]),
                        todate = Convert.ToString((DateTime)sdr[5]),
                        description = (string)sdr[3],
                        status = (string)sdr[7],
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


        public bool ApproveLeave()
        {
            try
            {
                sc.Open();
                SqlCommand cmd = new SqlCommand("Update AMS_Leaves set Status = 2 where ID =" + leave_ID , sc);
                cmd.ExecuteNonQuery();
                sc.Close();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        public bool RejectLeave()
        {
            try
            {
                sc.Open();
                SqlCommand cmd = new SqlCommand("Update AMS_Leaves set Status = 3 where ID =" + leave_ID, sc);
                cmd.ExecuteNonQuery();
                sc.Close();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool CancelLeave()
        {
            try
            {
                sc.Open();
                SqlCommand cmd = new SqlCommand("Update AMS_Leaves set Status = 5 where ID =" + leave_ID, sc);
                cmd.ExecuteNonQuery();
                sc.Close();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        public List<Leaves> Searchs()
        {


            try
            {
                sc.Open();
                SqlCommand cmd = new SqlCommand("Show_Leaves_id", sc);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", empID);
                SqlDataReader sdr = cmd.ExecuteReader();
                List<Leaves> b = new List<Leaves>();
                while (sdr.Read())
                {
                    b.Add(new Leaves()
                    {
                        empID = (int)sdr[0],
                        fname = (string)sdr[1],
                        leave_ID = (int)sdr[2],
                        leave_type = (string)sdr[6],
                        fromdate = Convert.ToString((DateTime)sdr[4]),
                        todate = Convert.ToString((DateTime)sdr[5]),
                        description = (string)sdr[3],
                        status = (string)sdr[7],
                    });
                }
                sc.Close();
                sdr.Close();
                return b;
              
            }
            catch (SqlException)
            {
                throw;
            }

        }

    //    public List<Leaves> Sorting(string obj)
    //    {
    //        Connection.SQLCON().Open();
    //        //string a = "select * from AMS_Employees order by" + obj;
    //        SqlCommand sc = new SqlCommand("select Emp_ID,First_Name,Last_Name,DOB,CNIC,j.Job_Title,Salary,d.Dept_Name,br.Branch_Name,Date_Join from AMS_Employees E INNER JOIN AMS_Branches Br on e.Branch_ID = br.Branch_ID " +
    //"INNER JOIN AMS_Jobs J on e.JOB_ID = j.Job_ID INNER JOIN AMS_Departments d on e.Dept_ID = d.Department_ID INNER JOIN AMS_Users usr on e.Emp_ID = usr.[User_ID] where usr.Status = 0  order by '" + obj + "'", Connection.SQLCON());
    //        SqlDataReader sdr = sc.ExecuteReader();

    //        List<Employees> lst = new List<Employees>();
    //        while (sdr.Read())
    //        {
    //            Employees b = new Employees()
    //            {
    //                empID = (int)sdr[0],
    //                fname = (string)sdr[1],
    //                lname = (string)sdr[2],
    //                dob = Convert.ToString((DateTime)sdr[3]),
    //                cnic = Convert.ToInt64(sdr[4]),
    //                job_title = (string)sdr[5],
    //                salary = (int)sdr[6],
    //                dept_name = (string)sdr[7],
    //                branch_name = (string)sdr[8],
    //                joindate = Convert.ToString((DateTime)sdr[9])
    //            };
    //            lst.Add(b);
    //        }
    //        sdr.Close();
    //        Connection.SQLCON().Close();
    //        return lst;

    //    }

    }
}