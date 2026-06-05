using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DemoApp25.Models;
using ExampleSession.Models.Home;
using ExampleSession.Models.Vendor;

using System.Data;
using System.Data.SqlClient;

namespace ExampleSession.Controllers
{
    public class AdminController : Controller
    {
        //
        // GET: /Admin/

        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult View_Contact()
        {
            string tbl = "";
            string query = "select * from contact1";
            DataTable dt = DatabaseManager.DispalyAllRecords(query);
            if (dt.Rows.Count > 0)
            {
                tbl += "<table class='table table-responsive'>";

                tbl += "<tr style='background:orange;color:white'><th>Id</th><th>Name</th><th>Email</th><th>Mobile</th><th>Message</th></tr>";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    tbl += "<tr><td>" + dt.Rows[i]["cid"] + "</td><td>" + dt.Rows[i]["cname"] + "</td><td>" + dt.Rows[i]["cemail"] + "</td><td>" + dt.Rows[i]["cmobile"] + "</td><td>" + dt.Rows[i]["msg"] + "</td></tr>";
                }
                tbl += "</table>";
                ViewBag.show = tbl;
            }
            return View();
        }
        [HttpPost]
        public ActionResult View_Contact(Contact cs)
        {
            if(ModelState.IsValid)
            {
                ViewBag.Clear();
            }
            return View();
        }
        [HttpGet]
        public ActionResult View_Registration()
        {
            string tbl = "";
            string query = "select uid, uname, uemail, umobile, uadhar, uaddress, cname as Country, sname as State,dname as District from Uregistration Join country  ON country = cid JOIN state ON state = sid JOIN district ON district = did ORDER BY uid";
            DataTable dt = DatabaseManager.DispalyAllRecords(query);
            if (dt.Rows.Count > 0)
            {
                tbl += "<table class='table table-responsive'>";

                tbl += "<tr style='background:orange;color:white'><th>Id</th><th>Name</th><th>Email</th><th>Mobile</th><th>Adhar</th><th>Address</th><th>Country</th><th>State</th><th>District</th></tr>";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    tbl += "<tr><td>" + dt.Rows[i]["uid"] + "</td><td>" + dt.Rows[i]["uname"] + "</td><td>" + dt.Rows[i]["uemail"] + "</td><td>" + dt.Rows[i]["umobile"] + "</td><td>" + dt.Rows[i]["uadhar"] + "</td><td>" + dt.Rows[i]["uaddress"] + "</td><td>" + dt.Rows[i]["country"] + "</td><td>" + dt.Rows[i]["state"] + "</td><td>" + dt.Rows[i]["district"] + "</td></tr>";
                }
                tbl += "</table>";
                ViewBag.show = tbl;
             }
            return View();
        }
        [HttpPost]
        public ActionResult View_Registration(UserRegistration ur)
        {
            if(ModelState.IsValid)
            {
                ViewBag.Clear();
            }
            return View();
        }
        public ActionResult View_Vendor_Details()
        {
            Vendor_Default ad;
            List<Vendor_Default> lst = new List<Vendor_Default>();
            string cmd = "select * from Vendor";
            DataTable dt = DatabaseManager.DispalyAllRecords(cmd);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ad = new Vendor_Default();
                    ad.Vendor_Name = Convert.ToString(dt.Rows[i]["vname"]);
                    ad.Vendor_Email = Convert.ToString(dt.Rows[i]["vemail"]);
                    ad.Vendor_mobile = Convert.ToString(dt.Rows[i]["vmobile"]);
                    ad.Vendor_Location = Convert.ToString(dt.Rows[i]["vlocation"]);
                    ad.Vendor_adhar = Convert.ToString(dt.Rows[i]["vadhar"]);
                    ViewBag.dt = Convert.ToString(dt.Rows[i]["vdate"]);
                    ViewBag.status = dt.Rows[i]["status"];
                    lst.Add(ad);
                }
            }
            return View(lst);
        }
        public ActionResult UpdateVendorDetails(string up)
        {
            Vendor_Default ad = null;
            string query = "select * from vendor where vlocation='" + up + "'";
            DataTable dt = DatabaseManager.DispalyAllRecords(query);
            if (dt.Rows.Count > 0)
            {
                ad = new Vendor_Default();
                ad.Vendor_Name = Convert.ToString(dt.Rows[0]["vname"]);
                ad.Vendor_Email = Convert.ToString(dt.Rows[0]["vemail"]);
                ad.Vendor_mobile = Convert.ToString(dt.Rows[0]["vmobile"]);
                ad.Vendor_Location = Convert.ToString(dt.Rows[0]["vlocation"]);
                ad.Vendor_adhar = Convert.ToString(dt.Rows[0]["vadhar"]);
            }
            return View(ad);
        }
        [HttpPost]
        public ActionResult UpdateVendorDetails(Vendor_Default vdd)
        {
            string query = "update vendor set vname='" + vdd.Vendor_Name + "',vmobile='" + vdd.Vendor_mobile + "',vadhar='" + vdd.Vendor_adhar + "' where vlocation='" + vdd.Vendor_Location + "'";
            if (DatabaseManager.Insert_Update_Delete(query))
                ViewBag.msg = "Update Details Successfully";
            else
                ViewBag.msg = "Unable to update";
            return View();
        }
       
    }
}
