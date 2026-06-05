using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
namespace DemoApp25.Models
{
    public class DatabaseManager
    {
        static SqlCommand cmd = null; static SqlDataAdapter sa = null;
        static DataTable dt = null;

        static SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["mycon"].ConnectionString);

       
        //code for insert update delete command

        public static bool Insert_Update_Delete(string command)
        {
           
            if (con.State == ConnectionState.Closed)
                con.Open();
            cmd = new SqlCommand(command, con);
            int n = cmd.ExecuteNonQuery();
            if (n > 0)
                return true;
            else
                return false;
        }
        public static DataTable DispalyAllRecords(string command)
        {
            dt = new DataTable();
            sa = new SqlDataAdapter(command, con);
            sa.Fill(dt);
            return dt;
        }

        
    }
}