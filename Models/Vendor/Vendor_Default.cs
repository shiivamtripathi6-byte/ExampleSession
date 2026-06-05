using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ExampleSession.Models.Vendor;
using System.ComponentModel.DataAnnotations;
using DemoApp25.Models;

namespace ExampleSession.Models.Vendor
{
    public class Vendor_Default
    {
        [Required(ErrorMessage="Please Enter Name")]
        [Display(Name="Enter Vendor Name")]
        public string Vendor_Name { get; set; }

        [Required(ErrorMessage = "Please Enter Email")]
        [Display(Name = "Enter Vendor Email")]
        public string Vendor_Email { get; set; }
        [Required(ErrorMessage = "Please Enter location")]
        [Display(Name = "Enter Vendor location")]
        public string Vendor_Location { get; set; }

        [Required(ErrorMessage = "Please Enter mobile number")]
        [Display(Name = "Enter Vendor mobile number")]
        public string Vendor_mobile { get; set; }

        [Required(ErrorMessage = "Please Enter adhar number")]
        [Display(Name = "Enter Vendor adhar number")]
        public string Vendor_adhar { get; set; }

        [Required(ErrorMessage = "Please Enter password")]
        [Display(Name = "Enter Vendor password")]
        public string Vendor_password { get; set; }

        [Required(ErrorMessage = "Please Enter Confirm password")]
        [Display(Name = "Enter Vendor Confirm password")]
        public string Vendor_Confirm_password { get; set; }
        
        public string Add_Vendor_Details()
        {
            DatabaseManager db = new DatabaseManager();
            string query1 = "insert into Vendor(Vname,Vemail,Vmobile,Vlocation,Vadhar,Vdate,passwd,status) values('" + Vendor_Name + "','" + Vendor_Email + "','" + Vendor_mobile + "','" + Vendor_Location + "','" + Vendor_adhar + "','" + DateTime.Now.ToString() + "','" + Vendor_password + "','yes')";

            string query2 = "insert into login values('" + Vendor_Location + "','" + Vendor_password + "','Vendor','yes')";

            if (DatabaseManager.Insert_Update_Delete(query1) && DatabaseManager.Insert_Update_Delete(query2))
                return "Vendor Registration Done";
            else
                return "Server error";


            
        }

        public string Vendor_MyProfile_Update(string id)
        {
            string msg = "";
            string query = "update Vendor set vname='" + Vendor_Name + "',vemail='" + Vendor_Email + "',vmobile='" + Vendor_mobile + "' where vlocation='" + id + "'";
            if (DatabaseManager.Insert_Update_Delete(query))
                msg = "My Profile Updated";
            else
                msg = "server error";
            return msg;
        }
    }
}