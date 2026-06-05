using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using ExampleSession.Models.Home;
using DemoApp25.Models;

namespace ExampleSession.Models.Home
{
    public class Contact
    {
        [Display(Name = "Enter Name")]
        [Required(ErrorMessage = "Please Enter Name")]
        public string Name { get; set; }

        [Display(Name = "Enter Email")]
        [Required(ErrorMessage = "Please Enter Email")]
        public string Email { get; set; }

        [Display(Name = "Enter Mobile")]
        [Required(ErrorMessage = "Please Enter Mobile")]
        public string Mobile { get; set; }

        [Display(Name = "Enter Message")]
        [Required(ErrorMessage = "Please Enter Message")]
        public string Message { get; set; }
        public string Insert_Contact()
        {
            string msg = "";
            string query = "insert into contact1(cname,cemail,cmobile,msg) values('" + Name + "','" + Email + "','"+Mobile+"','"+Message+"')";
            if (DatabaseManager.Insert_Update_Delete(query))
                msg = "Contact Added";
            else
                msg = "Server Error";
            return msg;
        }
    }
}
 
