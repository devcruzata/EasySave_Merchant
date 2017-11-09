using DAL;
using Project.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace BAL.Customers
{
   public class CustomerManager
    {
       public List<Customer> GetCustomers(long Merchant_ID)
       {
           objResponse Response = new objResponse();
           List<Customer> customer = new List<Customer>();

           TimeZoneInfo timeZoneInfo;
           timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Atlantic Standard Time");
           try
           {
               SqlParameter[] sqlParameter = new SqlParameter[1];

               sqlParameter[0] = new SqlParameter("@Merchant_ID", SqlDbType.BigInt, 10);
               sqlParameter[0].Value = Merchant_ID;

               DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_GetCustomer", sqlParameter, DB_CONSTANTS.ConnectionString_Easy_Save);


               if (Response.ResponseData.Tables[0].Rows.Count > 0)
               {
                   Response.ErrorCode = 0;
                   foreach (DataRow dr in Response.ResponseData.Tables[0].Rows)
                   {
                       Customer objCustomer = new Customer();
                       objCustomer.Customer_ID = Convert.ToInt64(dr["Customer_ID_Auto_PK"]);
                       objCustomer.FirstName = dr["FirstName"].ToString();
                       objCustomer.LastName = dr["LastName"].ToString();
                       objCustomer.Email = dr["Email"].ToString();
                       objCustomer.Mobile = dr["Mobile"].ToString();
                      // objCustomer.National_ID = dr["National_ID"].ToString();
                      // objCustomer.AddressLine1 = Convert.ToString(dr["AddressLine1"]);
                      // objCustomer.AddressLine2 = dr["AddressLine2"].ToString();
                     //  objCustomer.City = dr["City"].ToString();
                     //  objCustomer.State = dr["State"].ToString();
                    //   objCustomer.Countary = dr["Countary"].ToString();
                    //   objCustomer.Zipcode = dr["Zipcode"].ToString();
                    //   objCustomer.Gender = dr["Gender"].ToString();
                       objCustomer.CardNo = dr["CardNo"].ToString();
                       objCustomer.MyPoints = dr["Tot_Points"].ToString();
                       //objCustomer.DateGenreated = TimeZoneInfo.ConvertTime(Convert.ToDateTime(dr["GenreatedDate"]), timeZoneInfo).ToString("d MMM yyyy");
                       objCustomer.DateGenreated = Convert.ToDateTime(dr["GenreatedDate"]).ToString("d MMM yyyy");
                       //objCustomer.LastVisit = TimeZoneInfo.ConvertTime(Convert.ToDateTime(dr["LastVisit"]), timeZoneInfo).ToString("dd/MM/yyyy hh:mm tt");
                       objCustomer.LastVisit = Convert.ToDateTime(dr["LastVisit"]).ToString("dd/MM/yyyy hh:mm tt");
                       customer.Add(objCustomer);
                   }
               }
               else
               {
                   Response.ErrorCode = 2001;
                   Response.ErrorMessage = "There is an Error. Please Try After some time.";
               }
           }
           catch (Exception ex)
           {
               Response.ErrorMessage = ex.Message.ToString();
               BAL.Common.LogManager.LogError("GetCustomers", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
           }
           return customer;
       }
    }
}
