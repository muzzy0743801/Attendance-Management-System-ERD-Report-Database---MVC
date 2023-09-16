using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

namespace Dashboard_AMS.Models
{
    public class Employees
    {
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
        public string branch_name { get; set; }
        public string dept_name { get; set; }
        public int deptid { get; set; }
        public object obj { get; set; }

        public string Username { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public int roleid { get; set; }
        public string role { get; set; }

        public DateTime gethours { get; set;  }

        SqlConnection sc = Connection.SQLCON();

        public List<Employees> ShowAll_Roles()
        {
            List<Employees> ls = new List<Employees>();

            try
            {
                sc.Open();
                string query = "Select * from AMS_Roles";
                SqlCommand cmd = new SqlCommand(query, sc);
                SqlDataReader sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    ls.Add(new Employees()
                    {
                        roleid = (int)sdr[0],
                        role = (string)sdr[1]
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

        public bool Add_user()
        {
            try
            {
                sc.Open();
                SqlCommand cmd = new SqlCommand("Add_Users", sc);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@username", Username);
                cmd.Parameters.AddWithValue("@password", password);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@Role", roleid);
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

        //insert emp

        public bool AddEmployeeData()
        {
            try
            {
                sc.Open();
                SqlCommand cmd = new SqlCommand("Add_Employee", sc);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@fname", fname);
                cmd.Parameters.AddWithValue("@lname", lname);
                cmd.Parameters.AddWithValue("@dob", Convert.ToDateTime(dob).Date.ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@CNIC", cnic);
                cmd.Parameters.AddWithValue("@jobid", jobid);
                cmd.Parameters.AddWithValue("@sal", salary);
                cmd.Parameters.AddWithValue("@deptid", deptid);
                cmd.Parameters.AddWithValue("@branchid", branchid);
                cmd.Parameters.AddWithValue("@datejoin", Convert.ToDateTime(joindate).Date.ToString("yyyy-MM-dd"));
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

        public List<Employees> ShowAll_Employees()
        {
            List<Employees> ls = new List<Employees>();

            try
            {
                sc.Open();
                SqlCommand cmd = new SqlCommand("Show_AllEmployees", sc);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                SqlDataReader sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    ls.Add(new Employees()
                    {
                        empID = (int)sdr[0],
                        fname = (string)sdr[1],
                        lname = (string)sdr[2],
                        dob = Convert.ToString((DateTime)sdr[3]),
                        cnic = Convert.ToInt64(sdr[4]),
                        job_title = (string)sdr[5],
                        salary = (int)sdr[6],
                        dept_name = (string)sdr[7],
                        branch_name = (string)sdr[8],
                        joindate = Convert.ToString((DateTime)sdr[9])
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

        //search employees

        public Employees Search()
        {
            Connection.SQLCON().Open();
            string a = "select Emp_ID,First_Name,Last_Name,DOB,CNIC,j.Job_Title,Salary,d.Dept_Name,br.Branch_Name,Date_Join from AMS_Employees E INNER JOIN AMS_Branches Br on e.Branch_ID = br.Branch_ID " +
    "INNER JOIN AMS_Jobs J on e.JOB_ID = j.Job_ID INNER JOIN AMS_Departments d on e.Dept_ID = d.Department_ID INNER JOIN AMS_Users usr on e.Emp_ID = usr.[User_ID] where usr.Status = 0 and Emp_ID='" + empID + "'";
            SqlCommand sc = new SqlCommand(a, Connection.SQLCON());
            SqlDataReader sdr = sc.ExecuteReader();

            Employees b = new Employees();
            while (sdr.Read())
            {
                b.empID = (int)sdr[0];
                b.fname = (string)sdr[1];
                b.lname = (string)sdr[2];
                b.dob = Convert.ToString((DateTime)sdr[3]);
                b.cnic = Convert.ToInt64(sdr[4]);
                b.job_title = (string)sdr[5];
                b.salary = (int)sdr[6];
                b.dept_name = (string)sdr[7];
                b.branch_name = (string)sdr[8];
                b.joindate = Convert.ToString((DateTime)sdr[9]);
            }
            sdr.Close();
            Connection.SQLCON().Close();
            return b;

        }


        public Employees Search(int ID)
        {
            Connection.SQLCON().Open();
            string a = "	select Emp_ID,First_Name,Last_Name,DOB,CNIC,j.Job_ID,Salary,d.Department_ID,br.Branch_ID,Date_Join, usr.Username, usr.User_Role , usr.Password , usr.Email from AMS_Employees E INNER JOIN AMS_Branches Br on e.Branch_ID = br.Branch_ID " +
    "INNER JOIN AMS_Jobs J on e.JOB_ID = j.Job_ID INNER JOIN AMS_Departments d on e.Dept_ID = d.Department_ID INNER JOIN AMS_Users usr on e.Emp_ID = usr.[User_ID] where usr.Status = 0 and Emp_ID='" + ID + "'";
            SqlCommand sc = new SqlCommand(a, Connection.SQLCON());
            SqlDataReader sdr = sc.ExecuteReader();

            Employees b = new Employees();
            while (sdr.Read())
            {
                b.empID = (int)sdr[0];
                b.fname = (string)sdr[1];
                b.lname = (string)sdr[2];
                b.dob = Convert.ToString((DateTime)sdr[3]);
                b.cnic = Convert.ToInt64(sdr[4]);
                b.jobid = (int)sdr[5];
                b.salary = (int)sdr[6];
                b.deptid = (int)sdr[7];
                b.branchid = (int)sdr[8];
                b.joindate = Convert.ToString((DateTime)sdr[9]);
                b.Username = (string)sdr[10];
                b.password = (string)sdr[12];
                b.roleid = (int)sdr[11];
                b.email = (string)sdr[13];
            }
            sdr.Close();
            Connection.SQLCON().Close();
            return b;

        }

        //sort employess

        public List<Employees> Sorting(string obj)
        {
            Connection.SQLCON().Open();
            //string a = "select * from AMS_Employees order by" + obj;
            SqlCommand sc = new SqlCommand("select Emp_ID,First_Name,Last_Name,DOB,CNIC,j.Job_Title,Salary,d.Dept_Name,br.Branch_Name,Date_Join from AMS_Employees E INNER JOIN AMS_Branches Br on e.Branch_ID = br.Branch_ID " +
    "INNER JOIN AMS_Jobs J on e.JOB_ID = j.Job_ID INNER JOIN AMS_Departments d on e.Dept_ID = d.Department_ID INNER JOIN AMS_Users usr on e.Emp_ID = usr.[User_ID] where usr.Status = 0  order by '" + obj + "'", Connection.SQLCON());
            SqlDataReader sdr = sc.ExecuteReader();

            List<Employees> lst = new List<Employees>();
            while (sdr.Read())
            {
                Employees b = new Employees()
                {
                    empID = (int)sdr[0],
                    fname = (string)sdr[1],
                    lname = (string)sdr[2],
                    dob = Convert.ToString((DateTime)sdr[3]),
                    cnic = Convert.ToInt64(sdr[4]),
                    job_title = (string)sdr[5],
                    salary = (int)sdr[6],
                    dept_name = (string)sdr[7],
                    branch_name = (string)sdr[8],
                    joindate = Convert.ToString((DateTime)sdr[9])
                };
                lst.Add(b);
            }
            sdr.Close();
            Connection.SQLCON().Close();
            return lst;

        }


        public bool Mark_Attendance()
        {

            try
            {
                sc.Open();
                string getofficehour = "select top 1 Office_hours from AMS_Schedule";
                SqlCommand cmd1 = new SqlCommand(getofficehour, sc);
                SqlDataReader sdr = cmd1.ExecuteReader();
                sdr.Read();
                gethours = Convert.ToDateTime(sdr[0]);
                DateTime today = DateTime.Now;
                int cmp = TimeSpan.Compare(today.TimeOfDay, gethours.TimeOfDay);
                sdr.Close();
                if (cmp == -1 || cmp == 0)
                {
                    string query = "insert into AMS_Attendance values('" + Authentication.empid + "','" + DateTime.Now.ToShortDateString() + "','" + DateTime.Now + "',NULL,7) ";
                    SqlCommand cmd = new SqlCommand(query, sc);
                    cmd.ExecuteNonQuery();
                    sc.Close();
                    return true;
                }
                else
                {
                    string query = "insert into AMS_Attendance values('" + Authentication.empid + "','" + DateTime.Now.ToShortDateString() + "','" + DateTime.Now + "',NULL ,6) ";
                    SqlCommand cmd = new SqlCommand(query, sc);
                    cmd.ExecuteNonQuery();
                    sc.Close();
                    return true;
                }
            }
            catch (SqlException)
            {
                sc.Close();
                return false;

            }
        }


        public bool Mark_Attendance_out()
        {
            try
            {
                sc.Open();
                string query = "Update AMS_Attendance set Time_Out = '" + DateTime.Now  + "' where Emp_ID = '"+ Authentication.empid + "' and Date = '"+ DateTime.Now.ToShortDateString() + "'";
                SqlCommand cmd = new SqlCommand(query, sc);
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



        public bool UpdateEmployeeData(int id)
        {
            empID = id;

            if (Update_user() == true)
            {
                try
                {

                    sc.Open();
                    SqlCommand cmd = new SqlCommand("Update_EmployeeDetail", sc);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@empid", empID);
                    cmd.Parameters.AddWithValue("@fname", fname);
                    cmd.Parameters.AddWithValue("@lname", lname);
                    cmd.Parameters.AddWithValue("@dob", Convert.ToDateTime(dob).Date.ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("@CNIC", cnic);
                    cmd.Parameters.AddWithValue("@jobid", jobid);
                    cmd.Parameters.AddWithValue("@sal", salary);
                    cmd.Parameters.AddWithValue("@deptid", deptid);
                    cmd.Parameters.AddWithValue("@branchid", branchid);
                    cmd.Parameters.AddWithValue("@datejoin", Convert.ToDateTime(joindate).Date.ToString("yyyy-MM-dd"));
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
            else
            {
                return false;
            }
        }

        public bool Update_user()
        {
            try
            {
                sc.Open();
                SqlCommand cmd = new SqlCommand("Update_user", sc);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@userid", empID);
                cmd.Parameters.AddWithValue("@username", Username);
                cmd.Parameters.AddWithValue("@password", password);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@Role", roleid);
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

        public bool Delete_User()
        {
            try
            {
                sc.Open();
                SqlCommand cmd = new SqlCommand("Delete_Employee", sc);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@empid", empID);
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


       public List<Employees> RecentEmployees()
        {
            List<Employees> ls = new List<Employees>();

            try
            {
                sc.Open();
                SqlCommand cmd = new SqlCommand("select top 5 * from AMS_Employees order by Date_Join DESC ", sc);
                SqlDataReader sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    ls.Add(new Employees()
                    {
                        empID = (int)sdr[0],
                        fname = (string)sdr[1],
                        lname = (string)sdr[2],
                        joindate = Convert.ToDateTime(sdr[9]).Date.ToString("yyyy-MM-dd")
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
    }
}