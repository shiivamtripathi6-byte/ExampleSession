using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DemoApp25.Models;


namespace ExampleSession.Models.Vendor
{
    public class VendorComplain
    {
        public string Name { get; set; }
        public string Message { get; set; }
        public string status { get; set; }
        public string cdate { get; set; }
        public string Add_Complain()
        {
            DatabaseManager db = new DatabaseManager();
            string query = "insert into Complain(name,msg,status,cdate) values('" + Name + "','" + Message + "','user','" + DateTime.Now.ToString() + "')";
            if (DatabaseManager.Insert_Update_Delete(query))
                return "Your Complain Submitted";
            else
                return "Server Error";
        }
    }
}