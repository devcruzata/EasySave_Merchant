
using DAL;
using Project.Entity;
using Project.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace BAL.Common
{
   public static class UtilityManager
    {


       public static List<TextValue> GetMerchantsForDropDown()
       {

           objResponse Response = new objResponse();
           List<TextValue> Merchants = new List<TextValue>();

           try
           {
               //SqlParameter[] sqlParameter = new SqlParameter[1];

               DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_GetMerchantForDropDown", DB_CONSTANTS.ConnectionString_Easy_Save);

               if (Response.ResponseData.Tables[0].Rows.Count > 0)
               {
                   Response.ErrorCode = 0;
                   foreach (DataRow dr in Response.ResponseData.Tables[0].Rows)
                   {
                       TextValue objText = new TextValue();
                       objText.Value = dr["Merchant_ID_Auto_PK"].ToString();
                       objText.Text = dr["Organization_Name"].ToString();
                       Merchants.Add(objText);
                   }
               }
           }
           catch (Exception ex)
           {
               Response.ErrorCode = 2001;
               BAL.Common.LogManager.LogError("GetMerchantsForDropDown", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
           }
           return Merchants;
       }

       public static objResponse ValidateCardPrefix(string CardPrefix)
       {
           objResponse Response = new objResponse();
          
           try
           {
               SqlParameter[] sqlParameter = new SqlParameter[1];

               sqlParameter[0] = new SqlParameter("@CardPrefix", SqlDbType.NVarChar, 20);
               sqlParameter[0].Value = CardPrefix;

               DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_ValidateCardPrefix", sqlParameter, DB_CONSTANTS.ConnectionString_Easy_Save);

               if (Response.ResponseData.Tables[0].Rows.Count > 0)
               {
                   Response.ErrorCode = 0;
                   Response.ErrorMessage = Response.ResponseData.Tables[0].Rows[0][0].ToString();
               }
               else
               {
                   Response.ErrorCode = 3001;
                   Response.ErrorMessage = "There is an error in processing your request";
               }
           }
           catch (Exception ex)
           {
               Response.ErrorCode = 2001;
               BAL.Common.LogManager.LogError("ValidateCardPrefix", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
           }
           return Response;
       }

       public static objResponse GetOrgCode()
       {
           objResponse Response = new objResponse();

           try
           {

               DATA_ACCESS_LAYER.Fill(Response.ResponseData, "getOrganizationCode", DB_CONSTANTS.ConnectionString_Easy_Save);

               if (Response.ResponseData.Tables[0].Rows.Count > 0)
               {
                   Response.ErrorCode = 0;
                   Response.ErrorMessage = Response.ResponseData.Tables[0].Rows[0][0].ToString();
               }
               else
               {
                   Response.ErrorCode = 3001;
                   Response.ErrorMessage = "There is an error in processing your request";
               }
           }
           catch (Exception ex)
           {
               Response.ErrorCode = 2001;
               BAL.Common.LogManager.LogError("GetOrgCode", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
           }
           return Response;
       } 

      
    }
}
