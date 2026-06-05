using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ExampleSession.Models.Vendor;
using System.IO;
using DemoApp25.Models;
using System.Data.SqlClient;
using System.Data;

namespace ExampleSession.Controllers
{
    public class VendorController : Controller
    {
        public ActionResult Dashboard()
        {
            ViewBag.show = Convert.ToString(Session["vid"]);
            return View();
        }
       
        public ActionResult Add_places()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add_Places(Add_Destination ad)
        {
            if(ModelState.IsValid)
            {
                using (var ms = new MemoryStream())
                {
                    var targetImagePath = Server.MapPath("/Content/Upload/" + (ad.image.FileName));
                    byte[] image = new byte[ad.image.ContentLength];
                    ad.image.InputStream.Read(image, 0, image.Length);
                    File_Upload_Compress.Compressimage(targetImagePath, "", image);
                    ViewBag.msg = ad.Insert_Destination();
                }
            }
           
            return View();
        }
        public ActionResult Change_Password()
        {
            return View();
        }
        [HttpPost]

        public ActionResult Change_Password(string txtconfirmpass,string txtnewpass,string txtoldpass)
        {
            string vid = Convert.ToString(Session["vid"]);
            if(txtconfirmpass==txtnewpass)
            {
                string cmd = "update login set passwd='" + txtnewpass + "' where userid='" + vid + "' and passwd='" + txtoldpass + "'";
                if (DatabaseManager.Insert_Update_Delete(cmd))
                    ViewBag.msg = "Password change successfully";
                else
                    ViewBag.msg = "unable to update";
            }
            else
            {
                ViewBag.msg = "password and confirm password not match";
            }
            return View();
        }
        public ActionResult Vendor_Profile()
        {
            var id = Session["vid"];
            Vendor_Default vd = null;
            string cmd = "select * from Vendor where vlocation='" + id + "'";
            DataTable dt = DatabaseManager.DispalyAllRecords(cmd);
            if(dt.Rows.Count>0)
            {
                vd = new Vendor_Default();
                vd.Vendor_Name = dt.Rows[0]["Vname"].ToString();
                vd.Vendor_Email = dt.Rows[0]["Vemail"].ToString();
                vd.Vendor_mobile = dt.Rows[0]["Vmobile"].ToString();
                vd.Vendor_adhar = dt.Rows[0]["Vadhar"].ToString();
                vd.Vendor_password = dt.Rows[0]["passwd"].ToString();
                vd.Vendor_Location = dt.Rows[0]["Vlocation"].ToString();
                ViewBag.vid = dt.Rows[0]["vid"].ToString();
                ViewBag.status = dt.Rows[0]["status"].ToString();
                ViewBag.Vdate = dt.Rows[0]["Vdate"].ToString();
                ViewBag.Vlocation = dt.Rows[0]["vlocation"].ToString();
                
            }
            return View();
        }
        [HttpPost]
        public ActionResult Vendor_Profile(Vendor_Default v)
        {
           var id = Session["vid"] + "";
            ViewBag.show = v.Vendor_MyProfile_Update(id);
            return View();
        }
        public ActionResult Add_Hotel()
        {
            Add_Hotel ad = new Add_Hotel();
            var vid = Session["vid"];
            if(vid != null && vid != "")
            {
                ad = new Add_Hotel();
                ad.Hotel_Location = Convert.ToString(Session["vid"]);
            }
            else
            {
                Response.Redirect("/Home/Login");
            }
            return View(ad);
        }
        [HttpPost]
        public ActionResult Add_Hotel(Add_Hotel aa)
        {
            if(ModelState.IsValid)
            {
                Session["Hotel"] = aa.Hotel_Name;
                ViewBag.msg = aa.Add_Hotel_Details();
                ModelState.Clear();
                aa.Hotel_Location = Convert.ToString(Session["vid"]);
            }
            return View();
        }
        [HttpGet]
        public ActionResult Add_Hotel_Picture()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add_Hotel_Picture(HttpPostedFileBase pic,string ddlname)
        {
            var targetImagepath = Server.MapPath("/Content/HotelPic/" + (pic.FileName));
            byte[] image = new byte[pic.ContentLength];
            pic.InputStream.Read(image, 0, image.Length);
            File_Upload_Compress.Compressimage(targetImagepath, "", image);
            string cmd = "insert into Hotel_pic(picture,hid) values('" + pic.FileName+ "','" + ddlname + "')";
            if(DatabaseManager.Insert_Update_Delete(cmd))
                ViewBag.msg = "Hotel Picture Uploaded";
            else
                ViewBag.msg = "Unable to add";
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
        public ActionResult Vendor_LogOut()
        {
            var id = Session["vid"];
            if(id!=null && id!="")
            {
                Session.Clear();
                Session.RemoveAll();
                Response.Redirect("/Home/Login");
            }
            else
            {
                Response.Redirect("/Home/Login");
            }
            return View();
        }
        public ActionResult Complain()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Complain(VendorComplain co)
        {
            if(ModelState.IsValid)
            {
                ViewBag.msg = co.Add_Complain();
            }
            return View();
        }
        public ActionResult View_Complain()
        {
            VendorComplain co;
            List<VendorComplain> lst = new List<VendorComplain>();
            string cmd = "select * from Complain";
            DataTable dt = DatabaseManager.DispalyAllRecords(cmd);
            if(dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    co = new VendorComplain();
                    co.Name = Convert.ToString(dt.Rows[i]["name"]);
                    co.Message = Convert.ToString(dt.Rows[i]["msg"]);
                    co.status = Convert.ToString(dt.Rows[i]["status"]);
                    co.cdate = Convert.ToString(dt.Rows[i]["cdate"]);
                    lst.Add(co);
                }
            }
            return View(lst);
        }
    }
}
