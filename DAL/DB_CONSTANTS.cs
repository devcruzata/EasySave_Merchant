using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace DAL
{
    public class DB_CONSTANTS
    {
        private static string connString_Easy_Save = ConfigurationManager.AppSettings["connection_Easy_Save"].ToString();
        //private static string connString_master = ConfigurationManager.ConnectionStrings["connection_master"].ConnectionString;


        public static string ConnectionString_Easy_Save
        {
            get { return connString_Easy_Save; }
        }

        //public static string ConnectionString_master
        //{
        //    get { return connString_master; }
        //}
    }
}
