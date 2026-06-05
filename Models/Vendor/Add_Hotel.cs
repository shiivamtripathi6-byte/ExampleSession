using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ExampleSession.Models.Vendor;
using System.ComponentModel.DataAnnotations;
using DemoApp25.Models;

namespace ExampleSession.Models.Vendor
{
    public class Add_Hotel
    {
        [Required(ErrorMessage = "Please Enter Hotel Name")]
        [Display(Name = "Enter Hotel Name")]
        public string Hotel_Name { get; set; }

        [Required(ErrorMessage = "Please Enter Hotel price")]
        [Display(Name = "Enter price")]
        public string Hotel_Price { get; set; }
        public string Hotel_Location { get; set; }
        public string Hotel_Address { get; set; }

        public string Add_Hotel_Details()
        {
            string msg = "";
            string cmd = "insert into hotel(hname,hprice,hlocation,haddress,hdate,status) values('" + Hotel_Name + "','" + Hotel_Price + "','" + Hotel_Location + "','" + Hotel_Address + "','" + DateTime.Now.ToString() + "','1')";
            if (DatabaseManager.Insert_Update_Delete(cmd))
                msg = "Hotel Add Successfully";
            else
                msg = "unable to add";
            return msg;
        }
    }
}