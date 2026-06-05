using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ExampleSession.Models.Vendor;
using System.ComponentModel.DataAnnotations;
using DemoApp25.Models;
namespace ExampleSession.Models.Vendor
{
    public class Add_Destination
    {
        [Required(ErrorMessage = "Please Enter destination Name")]
        [Display(Name = "Enter Vendor Name")]
        public string Destination_Name { get; set; }

        [Required(ErrorMessage = "Please choose your image")]
        [Display(Name = "Please choose your image")]
        public HttpPostedFileBase image { get; set; }

        [Required(ErrorMessage = "Please Enter your price")]
        [Display(Name = "Enter your price")]
        public int price { get; set; }
        public string about { get; set; }
        public string duration { get; set; }

        public string Insert_Destination()
        {
            string msg = "";
            string query = "insert into destination(dname,dpic,dprice,dduration,ddate,about) values('" + Destination_Name + "','" + image.FileName + "','" + price + "','" + duration + "','" + DateTime.Now.ToString() + "','" + about + "')";
            if (DatabaseManager.Insert_Update_Delete(query))
                msg = "Destination Added";
            else
                msg = "No destination added";
            return msg;
        }
    }
}