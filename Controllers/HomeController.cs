using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Proj.Models;


namespace Proj.Controllers
{
    public class HomeController : Controller
    {
      
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ContactUs()
        {
            return View();
        }
        public IActionResult Register(string name,string fathername,string cnic,string email,string password,string confirmpassword,string presentaddress,string permanentaddress,string city,string province,string room,string zip,string option)
        {
            if (name != null && fathername != null && cnic != null && email != null && password != null && confirmpassword != null && presentaddress != null && permanentaddress != null && city != null &&province!=null &&room!=null && zip!=null)
            {
                if (option == "Student")
                {
                    SqlConnection con = new SqlConnection("data source=desktop-humtumm\\sadi976;initial catalog=hostel_management;integrated security=true");
                    con.Open();
                    SqlCommand cmd = new SqlCommand("insert into s_table (s_name,f_name,cnic,email,password,confirmpassword,p_address,pr_adrress,city,province,zip,room_type) values(@a,@b,@c,@d,@e,@f,@g,@h,@i,@j,@k,@l)", con);
                    cmd.Parameters.AddWithValue("@a", name);
                    cmd.Parameters.AddWithValue("@b", fathername);
                    cmd.Parameters.AddWithValue("@c", cnic);
                    cmd.Parameters.AddWithValue("@d", email);
                    cmd.Parameters.AddWithValue("@e", password);
                    cmd.Parameters.AddWithValue("@f", confirmpassword);
                    cmd.Parameters.AddWithValue("@g", presentaddress);
                    cmd.Parameters.AddWithValue("@h", permanentaddress);
                    cmd.Parameters.AddWithValue("@i", city);
                    cmd.Parameters.AddWithValue("@j", province);
                    cmd.Parameters.AddWithValue("@k", zip);
                    cmd.Parameters.AddWithValue("@l", room);
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                else
                {
                    SqlConnection con = new SqlConnection("data source=desktop-humtumm\\sadi976;initial catalog=hostel_management;integrated security=true");
                    con.Open();
                    SqlCommand cmd = new SqlCommand("insert into e_table (name,fname,cnic,email,password,confirmpassword,p_address,pr_address,city,province,zip)  values(@a,@b,@c,@d,@e,@f,@g,@h,@i,@j,@k)", con);
                    cmd.Parameters.AddWithValue("@a", name);
                    cmd.Parameters.AddWithValue("@b", fathername);
                    cmd.Parameters.AddWithValue("@c", cnic);
                    cmd.Parameters.AddWithValue("@d", email);
                    cmd.Parameters.AddWithValue("@e", password);
                    cmd.Parameters.AddWithValue("@f", confirmpassword);
                    cmd.Parameters.AddWithValue("@g", presentaddress);
                    cmd.Parameters.AddWithValue("@h", permanentaddress);
                    cmd.Parameters.AddWithValue("@i", city);
                    cmd.Parameters.AddWithValue("@j", province);
                    cmd.Parameters.AddWithValue("@k", zip);
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            return View();
        }
        public IActionResult Login(string Email,string Password)
        {
            SqlConnection con = new SqlConnection("data source=desktop-humtumm\\sadi976;initial catalog=hostel_management;integrated security=true");
            con.Open();
            //SqlCommand cmd = new SqlCommand("insert into e_table  values(@a,@b,@c,@d,@e,@f,@g,@h,@i,@j,@k)", con);
            SqlCommand s = new SqlCommand("select* from s_table where email=@a and password=@b", con);
            SqlCommand e = new SqlCommand("select* from e_table where email=@c and password=@d", con);
            SqlCommand a = new SqlCommand("select* from a_table where email=@e  and password=@f", con);

            s.Parameters.AddWithValue("@a", Email ?? (object)DBNull.Value);
            s.Parameters.AddWithValue("@b", Password ?? (object)DBNull.Value);

            e.Parameters.AddWithValue("@c", Email ?? (object)DBNull.Value);
            e.Parameters.AddWithValue("@d", Password ?? (object)DBNull.Value);
            
            a.Parameters.AddWithValue("@e", Email ?? (object)DBNull.Value);
            a.Parameters.AddWithValue("@f", Password ?? (object)DBNull.Value);

            SqlDataAdapter St = new SqlDataAdapter(s);

            SqlDataAdapter Et = new SqlDataAdapter(e);
            
            SqlDataAdapter At = new SqlDataAdapter(a);
            con.Close();
            DataSet Stt = new DataSet();
            DataSet Ett = new DataSet();
            DataSet Att = new DataSet();

            St.Fill(Stt);

            Et.Fill(Ett);
            
            At.Fill(Att);

            if (Stt.Tables[0].Rows.Count > 0) {
                ViewData["Message"]= Email;
                return View("~/Views/Student/Studentportal.cshtml");
            }
            else if(Ett.Tables[0].Rows.Count > 0)
            {
                ViewData["Message"] = Email;
                return View("~/Views/Employee/Employeeportal.cshtml");
            }
            else if(Att.Tables[0].Rows.Count > 0)
            {
                ViewBag.User = Email;
                return View("~/Views/Admin/Adminportal.cshtml");
            }
            else
            {
                ViewBag.Message="Email or Password is invalid";
                
                return View("~/Views/Home/Login.cshtml"); 
            }

        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
