using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using ExampleSession.Models.Home;
using DemoApp25.Models;

namespace ExampleSession.Models.Home
{
    public class UserRegistration
    {
        [Display(Name = "Enter Name")]
        [Required(ErrorMessage = "Please Enter Name")]
        public string Name { get; set; }

        [Display(Name = "Enter Email")]
        [Required(ErrorMessage = "Please Enter Email")]
        public string Email { get; set; }

        [Display(Name = "Enter Mobile Number")]
        [Required(ErrorMessage = "Please Enter your Mobile Number")]
        public string Mobile { get; set; }

        [Display(Name = "Enter Adhar Number")]
        [Required(ErrorMessage = "Please Enter your Adhar Number")]
        public string Adhar { get; set; }

        [Display(Name = "Enter Address Number")]
        [Required(ErrorMessage = "Please Enter your Address Number")]
        public string Address { get; set; }

        [Display(Name = "Enter Country")]
        [Required(ErrorMessage = "Please Enter your Country Name")]
        public string Country { get; set; }

        [Display(Name = "Enter State")]
        [Required(ErrorMessage = "Please Enter your State Name")]
        public string State { get; set; }

        [Display(Name = "Enter District")]
        [Required(ErrorMessage = "Please Enter your District Name")]
        public string District { get; set; }

        public string Insert_UserRegistration()
        {
            string msg = "";
            string query = "insert into Uregistration(uname,uemail,umobile,uadhar,uaddress,country,state,district) values('"+Name+"','"+Email+"','"+Mobile+"','"+Adhar+"','"+Address+"','"+Country+"','"+State+"','"+District+"')";
            if(DatabaseManager.Insert_Update_Delete(query))
                msg = "User Registration Successfully";
            else
                msg = "Server Error";
            return msg;
        }
    }
}