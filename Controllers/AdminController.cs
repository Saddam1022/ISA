using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace Proj.Controllers
{
    public class AdminController : Controller
    {
        
        public IActionResult RegisterUnregister(string name,string cn,string email,string address,string cnic,string check,string searchbtn)
        {
            SqlConnection con = new SqlConnection("data source=desktop-humtumm\\sadi976;initial catalog=hostel_management;integrated security=true");
            con.Open();

            

            if(searchbtn=="tosearch")
            {
                SqlDataAdapter chk = new SqlDataAdapter("select s_name,email,p_address,cnic from s_table where cnic='" + cn + "'", con);

                DataTable st = new DataTable();
                chk.Fill(st);

                if (st.Rows.Count == 1)
                {
                    
                   SqlCommand cmd = new SqlCommand("select s_name,email,p_address,cnic from s_table where cnic=@a", con);

                   SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                   cmd.Parameters.AddWithValue("@a",cn);
                   DataSet ds = new DataSet();
                   adapter.Fill(ds);
                   
                    for(int i=0;i< ds.Tables[0].Rows.Count;i++)
                    {
                        name = ds.Tables[0].Rows[i].ItemArray[0].ToString();

                        email = ds.Tables[0].Rows[i].ItemArray[1].ToString();

                        address = ds.Tables[0].Rows[i].ItemArray[2].ToString();

                        cnic = ds.Tables[0].Rows[i].ItemArray[3].ToString();

                    }

                    ViewData["name"] = name;
                    ViewData["email"] = email;
                    ViewData["address"] = address;
                    ViewData["cnic"] = cnic;
                    ViewData["check"] = "Student";
                }

                else
                {

                    SqlCommand cmd = new SqlCommand("select name,email,p_address,cnic from e_table where cnic=@a", con);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    cmd.Parameters.AddWithValue("@a", cn);
                    DataSet ds = new DataSet();
                    adapter.Fill(ds);

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        name = ds.Tables[0].Rows[i].ItemArray[0].ToString();

                        email = ds.Tables[0].Rows[i].ItemArray[1].ToString();

                        address = ds.Tables[0].Rows[i].ItemArray[2].ToString();

                        cnic = ds.Tables[0].Rows[i].ItemArray[3].ToString();

                    }

                    ViewData["name"] = name;
                    ViewData["email"] = email;
                    ViewData["address"] = address;
                    ViewData["cnic"] = cnic;

                    ViewData["check"] = "Employee";

                }

            }
            else if(searchbtn=="tounregister")
            {
                if (check== "Employee")
                {
                    SqlCommand cmd = new SqlCommand("delete from e_table where cnic=@a", con);
                    cmd.Parameters.AddWithValue("@a", cnic);
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    SqlCommand cmd = new SqlCommand("delete from s_table where cnic=@a", con);
                    cmd.Parameters.AddWithValue("@a", cnic);
                    cmd.ExecuteNonQuery();
                }
                
            }
            con.Close();

            return View();
        }

        /// ///////////////////////

        public IActionResult AdmintoEmployee(string name, string fathername, string cnic, string email, string paddress, string praddress, string city, string province, string zip, string cn, string check,string salary,string emcategory, string searchbtn)
        {
            SqlConnection con = new SqlConnection("data source=desktop-humtumm\\sadi976;initial catalog=hostel_management;integrated security=true");
            con.Open();

            if (searchbtn == "searchforemployee")
            {

                SqlCommand cmd = new SqlCommand("select name,fname,cnic,email,p_address,pr_address,city,province,zip,salary,Employee_category from e_table where  cnic=@a", con);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                cmd.Parameters.AddWithValue("@a", cn);
                DataSet ds = new DataSet();
                adapter.Fill(ds);


                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    name = ds.Tables[0].Rows[i].ItemArray[0].ToString();

                    fathername = ds.Tables[0].Rows[i].ItemArray[1].ToString();

                    cnic = ds.Tables[0].Rows[i].ItemArray[2].ToString();

                    email = ds.Tables[0].Rows[i].ItemArray[3].ToString();

                    paddress = ds.Tables[0].Rows[i].ItemArray[4].ToString();

                    praddress = ds.Tables[0].Rows[i].ItemArray[5].ToString();

                    city = ds.Tables[0].Rows[i].ItemArray[6].ToString();

                    province = ds.Tables[0].Rows[i].ItemArray[7].ToString();

                    zip = ds.Tables[0].Rows[i].ItemArray[8].ToString();

                    salary = ds.Tables[0].Rows[i].ItemArray[9].ToString();

                    emcategory = ds.Tables[0].Rows[i].ItemArray[10].ToString();

                }

                ViewData["name"] = name;
                ViewData["fathername"] = fathername;
                ViewData["email"] = email;
                ViewData["cnic"] = cnic;
                ViewData["paddress"] = paddress;
                ViewData["praddress"] = praddress;
                ViewData["city"] = city;
                ViewData["province"] = province;
                ViewData["zip"] = zip;
                ViewData["salary"] = salary;
                ViewData["emcategory"] = emcategory;

                return View("adminemployee");
                
            }

            con.Close();

            return View();
        }

        public IActionResult updateEmployee(string name, string fathername, string cnic, string email, string paddress, string praddress, string city, string province,string salary,string emcategory, string zip, string employeebtn)
        {
            SqlConnection con = new SqlConnection("data source=desktop-humtumm\\sadi976;initial catalog=hostel_management;integrated security=true");
            con.Open();

            SqlCommand cmd = new SqlCommand("update e_table set name=@a,fname=@b,email=@c, p_address=@d,pr_address=@e,city=@f,province=@g,salary=@x,Employee_category=@y where cnic=@h", con);

            if (employeebtn == "employeevalue")
            {

                cmd.Parameters.AddWithValue("@a", name);
                cmd.Parameters.AddWithValue("@b", fathername);
                cmd.Parameters.AddWithValue("@c", email);
                cmd.Parameters.AddWithValue("@d", paddress);
                cmd.Parameters.AddWithValue("@e", praddress);
                cmd.Parameters.AddWithValue("@f", city);
                cmd.Parameters.AddWithValue("@g", province);
                cmd.Parameters.AddWithValue("@h", cnic);
                cmd.Parameters.AddWithValue("@x", salary);
                cmd.Parameters.AddWithValue("@y", emcategory);

                cmd.ExecuteNonQuery();
            }

            ViewData["name"] = name;
            ViewData["fathername"] = fathername;
            ViewData["email"] = email;
            ViewData["cnic"] = cnic;
            ViewData["paddress"] = paddress;
            ViewData["praddress"] = praddress;
            ViewData["city"] = city;
            ViewData["province"] = province;
            ViewData["salary"] = salary;
            ViewData["emcategory"] = emcategory;

            con.Close();

            return View("~/Views/Admin/adminemployee.cshtml");

        }

        //////////////////
        public IActionResult AdmintoStudent(string name,string fathername,string cnic,string email,string paddress,string praddress,string city,string province,string room,string roomno,string zip,string fee, string cn,string check, string searchbtn)
        {
            SqlConnection con = new SqlConnection("data source=desktop-humtumm\\sadi976;initial catalog=hostel_management;integrated security=true");
            con.Open();


                
            if (searchbtn == "searchforstudent")
            {

                  SqlCommand cmd = new SqlCommand("select s_name,f_name,cnic,email,p_address,pr_adrress,city,province,zip,room_type,fee,room_no from s_table where  cnic=@a", con);

                   SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                   cmd.Parameters.AddWithValue("@a", cn);
                   DataSet ds = new DataSet();
                   adapter.Fill(ds);


                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        name = ds.Tables[0].Rows[i].ItemArray[0].ToString();

                        fathername = ds.Tables[0].Rows[i].ItemArray[1].ToString();

                        cnic = ds.Tables[0].Rows[i].ItemArray[2].ToString();

                        email = ds.Tables[0].Rows[i].ItemArray[3].ToString();

                        paddress = ds.Tables[0].Rows[i].ItemArray[4].ToString();

                        praddress = ds.Tables[0].Rows[i].ItemArray[5].ToString();

                        city = ds.Tables[0].Rows[i].ItemArray[6].ToString();

                        province = ds.Tables[0].Rows[i].ItemArray[7].ToString();

                        zip = ds.Tables[0].Rows[i].ItemArray[8].ToString();
                       
                        room = ds.Tables[0].Rows[i].ItemArray[9].ToString();
                        
                       fee = ds.Tables[0].Rows[i].ItemArray[10].ToString();
                      
                       roomno = ds.Tables[0].Rows[i].ItemArray[11].ToString();

                }

                    ViewData["name"] = name;
                    ViewData["fathername"] = fathername;
                    ViewData["email"] = email;
                    ViewData["cnic"] = cnic;
                    ViewData["paddress"] = paddress;
                    ViewData["praddress"] = praddress;
                    ViewData["city"] = city;
                    ViewData["province"] = province;
                    ViewData["zip"] = zip;
                    ViewData["room"] = room;
                    ViewData["roomno"] = roomno;
                    ViewData["fee"] = fee;


                return View("adminstudent");
            }
            
                con.Close();

                return View();
        }

        public IActionResult updateStudent(string name, string fathername, string cnic, string email, string paddress, string praddress, string city, string province,string room,string fee,string roomno, string zip, string cn, string update)
        {
            SqlConnection con = new SqlConnection("data source=desktop-humtumm\\sadi976;initial catalog=hostel_management;integrated security=true");
            con.Open();

            SqlCommand cmd = new SqlCommand("update s_table set s_name=@a,f_name=@b,email=@c, p_address=@d,pr_adrress=@e,city=@f,province=@g,room_type=@x,fee=@y,room_no=@z where cnic=@h", con);

            if (update== "updates")
            {

                cmd.Parameters.AddWithValue("@a", name);
                cmd.Parameters.AddWithValue("@b", fathername);
                cmd.Parameters.AddWithValue("@c", email);
                cmd.Parameters.AddWithValue("@d", paddress);
                cmd.Parameters.AddWithValue("@e", praddress);
                cmd.Parameters.AddWithValue("@f", city);
                cmd.Parameters.AddWithValue("@g", province);
                cmd.Parameters.AddWithValue("@h", cnic);
                cmd.Parameters.AddWithValue("@x", room);
                cmd.Parameters.AddWithValue("@y", fee);
                cmd.Parameters.AddWithValue("@z", roomno);


                cmd.ExecuteNonQuery();
            }

            ViewData["name"] = name;
            ViewData["fathername"] = fathername; 
            ViewData["email"] = email;
            ViewData["cnic"] = cnic;
            ViewData["paddress"] = paddress;
            ViewData["praddress"] = praddress;
            ViewData["city"] = city;
            ViewData["province"] = province;
            ViewData["room"] = room;
            ViewData["roomno"] = roomno;
            ViewData["province"] = fee;

            con.Close();

            return View("~/Views/Admin/adminstudent.cshtml");

        }


            public IActionResult AdmintoMess(string mlunch,string mdinner, string tlunch, string tdinner, string wlunch, string wdinner, string thlunch, string thdinner, string flunch, string fdinner, string slunch, string sdinner, string sulunch, string sudinner, string id, string id2, string id3, string id4, string id5, string id6, string id7)
        {
            SqlConnection con = new SqlConnection("data source=desktop-humtumm\\sadi976;initial catalog=hostel_management;integrated security=true");
            con.Open();

            SqlCommand cmd = new SqlCommand("select *from m_table ", con);
            
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
           
            DataSet ds = new DataSet();
            
            adapter.Fill(ds);

            
            id= ds.Tables[0].Rows[0].ItemArray[0].ToString();
            mlunch = ds.Tables[0].Rows[0].ItemArray[1].ToString();
            mdinner = ds.Tables[0].Rows[0].ItemArray[2].ToString();

            id2 = ds.Tables[0].Rows[1].ItemArray[0].ToString();
            tlunch = ds.Tables[0].Rows[1].ItemArray[1].ToString();
            tdinner = ds.Tables[0].Rows[1].ItemArray[2].ToString();

            id3 = ds.Tables[0].Rows[2].ItemArray[0].ToString();
            wlunch = ds.Tables[0].Rows[2].ItemArray[1].ToString();
            wdinner = ds.Tables[0].Rows[2].ItemArray[2].ToString();

            id4 = ds.Tables[0].Rows[3].ItemArray[0].ToString();
            thlunch = ds.Tables[0].Rows[3].ItemArray[1].ToString();
            thdinner = ds.Tables[0].Rows[3].ItemArray[2].ToString();

            id5 = ds.Tables[0].Rows[4].ItemArray[0].ToString();
            flunch = ds.Tables[0].Rows[4].ItemArray[1].ToString();
            fdinner = ds.Tables[0].Rows[4].ItemArray[2].ToString();

            id6 = ds.Tables[0].Rows[5].ItemArray[0].ToString();
            slunch = ds.Tables[0].Rows[5].ItemArray[1].ToString();
            sdinner = ds.Tables[0].Rows[5].ItemArray[2].ToString();

            id7= ds.Tables[0].Rows[6].ItemArray[0].ToString();
            sulunch = ds.Tables[0].Rows[6].ItemArray[1].ToString();
            sudinner = ds.Tables[0].Rows[6].ItemArray[2].ToString();


            ViewData["mlunch"] = mlunch;
            ViewData["mdinner"] = mdinner;
            ViewData["id"] = id;

            ViewData["id2"] = id2;
            ViewData["tlunch"] = tlunch;
            ViewData["tdinner"] = tdinner;

            ViewData["id3"] = id3;
            ViewData["wlunch"] = wlunch;
            ViewData["wdinner"] = wdinner;

            ViewData["id4"] = id4;
            ViewData["thlunch"] = thlunch;
            ViewData["thdinner"] = thdinner;

            ViewData["id5"] = id5;
            ViewData["flunch"] = flunch;
            ViewData["fdinner"] = fdinner;

            ViewData["id6"] = id6;
            ViewData["slunch"] = slunch;
            ViewData["sdinner"] = sdinner;

            ViewData["id7"] = id7;
            ViewData["sulunch"] = sulunch;
            ViewData["sudinner"] = sudinner;
        

            con.Close();



            return View("~/Views/Admin/AdmintoMess.cshtml");
       
        }

        public IActionResult updateMess(string mlunch, string mdinner, string tlunch, string tdinner, string wlunch, string wdinner, string thlunch, string thdinner, string flunch, string fdinner, string slunch, string sdinner, string sulunch, string sudinner, string ID,string button, string ID2,string ID3,string ID4,string ID5,string ID6, string ID7)
        {
            SqlConnection con = new SqlConnection("data source=desktop-humtumm\\sadi976;initial catalog=hostel_management;integrated security=true");
            con.Open();

            SqlCommand cmd = new SqlCommand("update m_table set Lunch=@a,Dinner=@b where id=@c", con);
            
            if (button == "MUpdate")
            {

                cmd.Parameters.AddWithValue("@a", mlunch);
                cmd.Parameters.AddWithValue("@b", mdinner);
                cmd.Parameters.AddWithValue("@c", ID);

                cmd.ExecuteNonQuery();
            }

            else if (button== "TUpdate")
            {
                cmd.Parameters.AddWithValue("@a", tlunch);
                cmd.Parameters.AddWithValue("@b", tdinner);
                cmd.Parameters.AddWithValue("@c", ID2);

                cmd.ExecuteNonQuery();
            }
            else if (button == "WUpdate")
            {
                cmd.Parameters.AddWithValue("@a", wlunch);
                cmd.Parameters.AddWithValue("@b", wdinner);
                cmd.Parameters.AddWithValue("@c", ID3);

                cmd.ExecuteNonQuery();
            }
            else if (button == "THUpdate")
            {
                cmd.Parameters.AddWithValue("@a", thlunch);
                cmd.Parameters.AddWithValue("@b", thdinner);
                cmd.Parameters.AddWithValue("@c", ID4);

                cmd.ExecuteNonQuery();
            }
            else if (button == "FUpdate")
            {
                cmd.Parameters.AddWithValue("@a", flunch);
                cmd.Parameters.AddWithValue("@b", fdinner);
                cmd.Parameters.AddWithValue("@c", ID5);

                cmd.ExecuteNonQuery();
            }
            else if (button == "SUpdate")
            {
                cmd.Parameters.AddWithValue("@a", slunch);
                cmd.Parameters.AddWithValue("@b", sdinner);
                cmd.Parameters.AddWithValue("@c", ID6);

                cmd.ExecuteNonQuery();
            }

            else if (button == "SUUpdate")
            {
                cmd.Parameters.AddWithValue("@a", sulunch);
                cmd.Parameters.AddWithValue("@b", sudinner);
                cmd.Parameters.AddWithValue("@c", ID7);

                cmd.ExecuteNonQuery();
            }

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


            return View("~/Views/Admin/AdmintoMess.cshtml");
        }

        public IActionResult adminemployee(string name, string fathername, string cnic, string email, string paddress, string praddress, string city, string province, string zip)
        {
            SqlConnection con = new SqlConnection("data source=desktop-humtumm\\sadi976;initial catalog=hostel_management;integrated security=true");
            con.Open();

            ViewData["name"] = name;
            ViewData["fathername"] = fathername;
            ViewData["email"] = email;
            ViewData["cnic"] = cnic;
            ViewData["paddress"] = paddress;
            ViewData["praddress"] = praddress;
            ViewData["city"] = city;
            ViewData["province"] = province;
            ViewData["zip"] = zip;

            con.Close();


            return View();
        }

        public IActionResult adminstudent(string name, string fathername, string cnic, string email, string paddress, string praddress, string city, string province, string zip)
        {

            SqlConnection con = new SqlConnection("data source=desktop-humtumm\\sadi976;initial catalog=hostel_management;integrated security=true");
            con.Open();

            ViewData["name"] = name;
            ViewData["fathername"] = fathername;
            ViewData["email"] = email;
            ViewData["cnic"] = cnic;
            ViewData["paddress"] = paddress;
            ViewData["praddress"] = praddress;
            ViewData["city"] = city;
            ViewData["province"] = province;
            ViewData["zip"] = zip;

            con.Close();

            return View();
        }
       public IActionResult Adminportal()
        {
           
            return View();
        }

    }
}
