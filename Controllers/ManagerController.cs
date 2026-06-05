using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ExampleSession.Models.Vendor;
using System.Data;
using System.Data.SqlClient;
using DemoApp25.Models;


namespace ExampleSession.Controllers
{
    public class ManagerController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult View_Vendor()
        {
            string tbl = "";
            string query = "select * from Vendor";
            DataTable dt = DatabaseManager.DispalyAllRecords(query);
            if (dt.Rows.Count > 0)
            {
                tbl += "<table class='table table-responsive'>";

                tbl += "<tr style='background:orange;color:white'><th>Id</th><th>Name</th><th>Email</th><th>Mobile</th><th>Location</th><th>Adhar</th><th>Date</th><th>Password</th><th>Status</th></tr>";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    tbl += "<tr><td>" + dt.Rows[i]["vid"] + "</td><td>" + dt.Rows[i]["Vname"] + "</td><td>" + dt.Rows[i]["Vemail"] + "</td><td>" + dt.Rows[i]["Vmobile"] +
                        "</td><td>" + dt.Rows[i]["Vlocation"] + "</td><td>" + dt.Rows[i]["Vadhar"] + "</td><td>" + dt.Rows[i]["Vdate"] + "</td><td>" + dt.Rows[i]["passwd"] + "</td><td>" + dt.Rows[i]["status"] + "</td><td></tr>";
                }
                tbl += "</table>";
                ViewBag.show = tbl;
            }
            return View();
        }
        [HttpPost]
        public ActionResult View_Vendor(Vendor_Default vendor)
        {
            if (ModelState.IsValid)
            {
                ViewBag.Clear();
            }
            return View();
        }
        public ActionResult ViewDestination()
        {
            ViewDestination ad;
            List<ViewDestination> lst = new List<ViewDestination>();
            string cmd = "select * from Destination";
            DataTable dt = DatabaseManager.DispalyAllRecords(cmd);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ad = new ViewDestination();
                    ad.Destination_Name = Convert.ToString(dt.Rows[i]["dname"]);
                    ad.about = Convert.ToString(dt.Rows[i]["about"]);
                    ad.price = Convert.ToString(dt.Rows[i]["dprice"]);
                    ad.duration = Convert.ToString(dt.Rows[i]["dduration"]);
                    ad.image = Convert.ToString(dt.Rows[i]["dpic"]);
                    ViewBag.dt = Convert.ToString(dt.Rows[i]["ddate"]);
                    lst.Add(ad);
                }
            }
            return View(lst);
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
        public ActionResult Add_Vendor()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add_Vendor(Vendor_Default vendor)
        {
            if (ModelState.IsValid)
            {
                ViewBag.msg = vendor.Add_Vendor_Details();
            }
            return View();
        }
    }
}
    
