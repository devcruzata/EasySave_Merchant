using DAL;
using Project.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace BAL.Dashboard
{
   public class DashboardManager
    {
       public objResponse GetDashboardData(string MerchantID)
       {
           objResponse Response = new objResponse();
           try
           {
               SqlParameter[] sqlParameter = new SqlParameter[1];

               sqlParameter[0] = new SqlParameter("@MerchantID", SqlDbType.BigInt, 10);
               sqlParameter[0].Value = Convert.ToInt64(MerchantID);


               DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_GetDashboardData", sqlParameter, DB_CONSTANTS.ConnectionString_Easy_Save);


               if (Response.ResponseData.Tables[0].Rows.Count > 0 || Response.ResponseData.Tables[1].Rows.Count > 0 || Response.ResponseData.Tables[2].Rows.Count > 0)
               {
                   Response.ErrorCode = 0;
                   Response.ErrorMessage = "Success";
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
               BAL.Common.LogManager.LogError("GetDashboardData", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
           }

           return Response;
       } 
    }
}
