using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DemoApp25.Models;
using ExampleSession.Models.Home;
using System.Data;
using System.Data.SqlClient;

namespace ExampleSession.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AboutUs()
        {
            return View();
        }
        public ActionResult Gallery()
        {
            return View();
        }
        public ActionResult Story()
        {
            return View();
        }

        public ActionResult ContactUs()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ContactUs(Contact cs)
        {
            if(ModelState.IsValid)
            {
                ViewBag.msg = cs.Insert_Contact();
            }
               return View();
         }
        public ActionResult Login()
        {
            CaptchaManager cg = new CaptchaManager();
            ViewBag.cph = cg.CaptchaCode();
            return View();
        }
        [HttpPost]
        public ActionResult Login(Login lg, string txtcaptcha, string txtcaptchacode)
        {
            CaptchaManager cg = new CaptchaManager();
            ViewBag.cph = cg.CaptchaCode();
            string type = "";
            if (txtcaptcha == txtcaptchacode)
            {
                if (ModelState.IsValid)
                {
                    string cmd = "select * from login where userid='" + lg.UserId + "' and passwd='" + lg.Password + "' and status='yes'";
                    DataTable dt = DatabaseManager.DispalyAllRecords(cmd);
                    if (dt.Rows.Count > 0)
                    {
                        type = Convert.ToString(dt.Rows[0]["type"]).Trim().ToLower();
                        if (type == "vendor")
                        {
                            Session["vid"] = lg.UserId;
                            Response.Redirect("/Vendor/Dashboard");
                        }
                        else if (type == "user")
                        {
                            Session["uid"] = lg.UserId;
                            Response.Redirect("/User/Dashboard");
                        }
                        else if (type == "manager")
                        {
                            Session["mid"] = lg.UserId;
                            Response.Redirect("/Manager/Dashboard");
                        }
                        else if (type == "admin")
                        {
                            Session["aid"] = lg.UserId;
                            Response.Redirect("/Admin/Dashboard");
                        }
                        else
                        {
                            ViewBag.msg = "No Valid type here";
                        }

                    }
                    else
                    {
                        ViewBag.msg = "No record found here";
                    }

                }
            }
            else
            {
                ViewBag.msg = "Captcha code not match";
            }
            return View();
        }
        public JsonResult RefreshCaptcha()
        {
            CaptchaManager cg = new CaptchaManager();
            string msg = cg.CaptchaCode();
            return Json(msg, JsonRequestBehavior.AllowGet);
        }
        public ActionResult AddUser()
        {
            return View();
        }
        public JsonResult DDlState(string Country)
        {
            string ddl = "";
            string cmd = "select * from state where cid='" + Country + "'";
            DataTable dt = DatabaseManager.DispalyAllRecords(cmd);
            if(dt.Rows.Count>0)
            {
                for(int i=0;i<dt.Rows.Count;i++)
                {
                    ddl += "<option value='" + dt.Rows[i]["sid"] + "'>" + dt.Rows[i]["sname"] + "</option>";
                }
            }
            return Json(ddl, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DDlDistrict(string State)
        {
            string ddl = "";
            string cmd = "select * from district where sid='" + State + "'";
            DataTable dt = DatabaseManager.DispalyAllRecords(cmd);
            if(dt.Rows.Count>0)
            {
                for(int i=0;i<dt.Rows.Count;i++)
                {
                   ddl += "<option value='" + dt.Rows[i]["did"] + "'>" + dt.Rows[i]["dname"] + "</option>";
                }
            }
            return Json(ddl, JsonRequestBehavior.AllowGet);
        }
        public ActionResult UserRegistration()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult UserRegistration(UserRegistration ur)
        {
           if(ModelState.IsValid)
           {
               ViewBag.msg = ur.Insert_UserRegistration();
               
           }
            return View();
        }
        public ActionResult UserComplain()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UserComplain(UserComplain co)
        {
            if (ModelState.IsValid)
            {
                ViewBag.msg = co.Add_Complain();
            }
            return View();
        }
    }
}
