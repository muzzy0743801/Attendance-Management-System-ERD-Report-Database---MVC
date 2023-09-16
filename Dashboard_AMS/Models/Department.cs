using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace Dashboard_AMS.Models
{
    public class Department
    {
        //for department
        public int dept_id { get; set; }
        public string deptname { get; set; }

        public int Branch_id { get; set; }
        public string Branch_name { get; set; }

        SqlConnection sc = Connection.SQLCON();

        public bool Add_department()
        {
            try
            {
                sc.Open();
                string query = "insert into AMS_Departments values('"+ deptname +"')";
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


        public List<Department> ShowAll_department()
        {
            List<Department> ls = new List<Department>();

            try
            {
                sc.Open();
                string query = "Select * from AMS_Departments";
                SqlCommand cmd = new SqlCommand(query, sc);
                SqlDataReader sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    ls.Add(new Department()
                    {
                        dept_id = (int)sdr[0],
                        deptname = (string)sdr[1]
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

        public bool Add_Branch()
        {
            try
            {
                sc.Open();
                string query = "insert into AMS_Branches values('" + Branch_name + "')";
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


        public List<Department> ShowAll_Branches()
        {
            List<Department> ls = new List<Department>();

            try
            {
                sc.Open();
                string query = "Select * from AMS_Branches";
                SqlCommand cmd = new SqlCommand(query, sc);
                SqlDataReader sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    ls.Add(new Department()
                    {
                        Branch_id = (int)sdr[0],
                        Branch_name = (string)sdr[1]
                    });
                }
                sdr.Close();
                sc.Close();
                return ls;
            }
            catch (SqlException)
            {
                return ls;
            }

        }
        //search branch

        public Department Search()
        {
            Connection.SQLCON().Open();
            string a = "Select * From AMS_Branches Where Branch_ID='" + Branch_id + "'";
            SqlCommand sc = new SqlCommand(a, Connection.SQLCON());
            SqlDataReader sdr = sc.ExecuteReader();

            Department b = new Department();
            while (sdr.Read())
            {

                b.Branch_id = (int)sdr["Branch_ID"];
                b.Branch_name = (string)sdr["Branch_Name"];
             }
            sdr.Close();
            Connection.SQLCON().Close();
            return b;

        }
        //sort branches

        public List<Department> Sorting(string obj)
        {
            Connection.SQLCON().Open();
            //string a = "select * from AMS_Employees order by" + obj;
            SqlCommand sc = new SqlCommand("select * from AMS_Branches order by '" + obj + "'", Connection.SQLCON());
            SqlDataReader sdr = sc.ExecuteReader();
            List<Department> lst = new List<Department>();
            while (sdr.Read())
            {
                Department b = new Department()
                {
                Branch_id = (int)sdr["Branch_ID"],
                Branch_name = (string)sdr["Branch_Name"],
            };
                lst.Add(b);
            }
            sdr.Close();
            Connection.SQLCON().Close();
            return lst;

        }


        public bool Update_Dept()
        {
            try
            {
                sc.Open();
                SqlCommand cmd = new SqlCommand("Update AMS_Departments set Dept_Name ='" + deptname + "' where Department_ID ='" + dept_id + "'",sc);
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


        public bool Update_Branch()
        {
            try
            {
                sc.Open();
                SqlCommand cmd = new SqlCommand("Update AMS_Branches set Branch_Name ='" + Branch_name + "' where Branch_ID ='" + Branch_id + "'", sc);
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





        //search Depart

        public Department SearchDept()
        {
            sc.Open();
            string a = "Select * From AMS_Departments Where Department_ID='" + dept_id + "'";
            SqlCommand cmd = new SqlCommand(a, Connection.SQLCON());
            SqlDataReader sdr = cmd.ExecuteReader();

            Department b = new Department();
            while (sdr.Read())
            {

                b.dept_id = (int)sdr["Department_ID"];
                b.deptname = (string)sdr["Dept_Name"];
            }
            sdr.Close();
            sc.Close();
            return b;

        }
        //sort depar

        public List<Department> Sortingdept(string obj)
        {
            sc.Open();
            //string a = "select * from AMS_Employees order by" + obj;
            SqlCommand cmd = new SqlCommand("select * from AMS_Departments order by '" + obj + "'", Connection.SQLCON());
            SqlDataReader sdr = cmd.ExecuteReader();
            List<Department> lst = new List<Department>();
            while (sdr.Read())
            {
                Department b = new Department()
                {
                    dept_id = (int)sdr[0],
                deptname = (string)sdr[1],
            };
                lst.Add(b);
            }
            sdr.Close();
            sc.Close();
            return lst;

        }




    }
}