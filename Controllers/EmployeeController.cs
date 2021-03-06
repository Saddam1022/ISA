using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Proj.Controllers
{
    public class EmployeeController : Controller
    {
        public string Email = "";
        public IActionResult Employeeportal()
        {
            Email = ViewData["Message"].ToString();

            return View();
        }
        public IActionResult Employee_details(string name, string fname, string cnic, string email, string paddress, string praddress, string city, string province, string zip)
        {
            SqlConnection con = new SqlConnection("data source=desktop-humtumm\\sadi976;initial catalog=hostel_management;integrated security=true");
            con.Open();

            SqlCommand cmd = new SqlCommand("select name, fname, cnic,p_address,pr_address, city,province, zip, email from e_table where email = @abc", con);

            cmd.Parameters.AddWithValue("@abc", email);
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                name = dr.GetValue(0).ToString();
                fname = dr.GetValue(1).ToString();
                cnic = dr.GetValue(2).ToString();
                paddress = dr.GetValue(3).ToString();
                praddress = dr.GetValue(4).ToString();
                city = dr.GetValue(5).ToString();
                province = dr.GetValue(6).ToString();
                zip = dr.GetValue(7).ToString();
                email = dr.GetValue(8).ToString();


                ViewData["name"] = name;
                ViewData["fname"] = fname;
                ViewData["cnic"] = cnic;
                ViewData["paddress"] = paddress;
                ViewData["praddress"] = praddress;
                ViewData["city"] = city;
                ViewData["province"] = province;
                ViewData["zip"] = zip;
                ViewData["Message"] = email;
            }
            con.Close();
            return View();
        }
        public IActionResult Employee_Salary(string name,string fathername,string salary,string emcategory,string email)
        {
            SqlConnection con = new SqlConnection("data source=desktop-humtumm\\sadi976;initial catalog=hostel_management;integrated security=true");
            con.Open();

            SqlCommand cmd = new SqlCommand("select name, fname, email,salary,Employee_category from e_table where email = @abc", con);

            cmd.Parameters.AddWithValue("@abc", email);
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                name = dr.GetValue(0).ToString();
                fathername = dr.GetValue(1).ToString();
                email = dr.GetValue(2).ToString();
                salary = dr.GetValue(3).ToString();
                emcategory = dr.GetValue(4).ToString();
                ViewData["name"] = name;
                ViewData["fathername"] = fathername;
                ViewData["Message"] = email;
                ViewData["salary"] = salary;
                ViewData["emcategory"] = emcategory;

            }
            con.Close();

            return View();
        }
        public IActionResult Employee_mess(string mlunch, string mdinner, string tlunch, string tdinner, string wlunch, string wdinner, string thlunch, string thdinner, string flunch, string fdinner, string slunch, string sdinner, string sulunch, string sudinner)
        {
            SqlConnection con = new SqlConnection("data source=desktop-humtumm\\sadi976;initial catalog=hostel_management;integrated security=true");
            con.Open();

            SqlCommand cmd = new SqlCommand("select *from m_table", con);

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);

            DataSet ds = new DataSet();

            adapter.Fill(ds);



            mlunch = ds.Tables[0].Rows[0].ItemArray[1].ToString();
            mdinner = ds.Tables[0].Rows[0].ItemArray[2].ToString();


            tlunch = ds.Tables[0].Rows[1].ItemArray[1].ToString();
            tdinner = ds.Tables[0].Rows[1].ItemArray[2].ToString();


            wlunch = ds.Tables[0].Rows[2].ItemArray[1].ToString();
            wdinner = ds.Tables[0].Rows[2].ItemArray[2].ToString();


            thlunch = ds.Tables[0].Rows[3].ItemArray[1].ToString();
            thdinner = ds.Tables[0].Rows[3].ItemArray[2].ToString();


            flunch = ds.Tables[0].Rows[4].ItemArray[1].ToString();
            fdinner = ds.Tables[0].Rows[4].ItemArray[2].ToString();


            slunch = ds.Tables[0].Rows[5].ItemArray[1].ToString();
            sdinner = ds.Tables[0].Rows[5].ItemArray[2].ToString();


            sulunch = ds.Tables[0].Rows[6].ItemArray[1].ToString();
            sudinner = ds.Tables[0].Rows[6].ItemArray[2].ToString();


            ViewData["mlunch"] = mlunch;
            ViewData["mdinner"] = mdinner;



            ViewData["tlunch"] = tlunch;
            ViewData["tdinner"] = tdinner;

            ViewData["wlunch"] = wlunch;
            ViewData["wdinner"] = wdinner;


            ViewData["thlunch"] = thlunch;
            ViewData["thdinner"] = thdinner;

            ViewData["flunch"] = flunch;
            ViewData["fdinner"] = fdinner;


            ViewData["slunch"] = slunch;
            ViewData["sdinner"] = sdinner;


            ViewData["sulunch"] = sulunch;
            ViewData["sudinner"] = sudinner;


            con.Close();

            return View();
        }


    }
}
