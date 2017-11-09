using DAL;
using Project.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace BAL.Seting
{
   public class SetingManager
    {
       public objResponse AddProgramSeting(string MerchantID, string RedemSeting)
       {
           objResponse Response = new objResponse();
           try
           {
               SqlParameter[] sqlParameter = new SqlParameter[2];

               sqlParameter[0] = new SqlParameter("@MerchantID", SqlDbType.BigInt, 10);
               sqlParameter[0].Value = Convert.ToInt64(MerchantID);

               //sqlParameter[1] = new SqlParameter("@AwarSeting", SqlDbType.NVarChar, 10);
               //sqlParameter[1].Value = AwarSeting;

               sqlParameter[1] = new SqlParameter("@RedemSeting", SqlDbType.NVarChar, 10);
               sqlParameter[1].Value = RedemSeting;

               DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_AddSeting", sqlParameter, DB_CONSTANTS.ConnectionString_Easy_Save);


               if (Response.ResponseData.Tables[0].Rows.Count > 0)
               {
                   Response.ErrorCode = 0;
                   Response.ErrorMessage = Response.ResponseData.Tables[0].Rows[0][0].ToString(); ;
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
               BAL.Common.LogManager.LogError("AddProgramSeting", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
           }

           return Response;
       }

       public objResponse GetProgramSeting(string MerchantID)
       {
           objResponse Response = new objResponse();
           try
           {
               SqlParameter[] sqlParameter = new SqlParameter[1];

               sqlParameter[0] = new SqlParameter("@MerchantID", SqlDbType.BigInt, 10);
               sqlParameter[0].Value = Convert.ToInt64(MerchantID);


               DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_GetProgramSeting", sqlParameter, DB_CONSTANTS.ConnectionString_Easy_Save);


               if (Response.ResponseData.Tables[0].Rows.Count > 0)
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
               BAL.Common.LogManager.LogError("GetProgramSeting", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
           }

           return Response;
       } 
    }
}
