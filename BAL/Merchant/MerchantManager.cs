using DAL;
using Project.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace BAL.Merchant
{
  public  class MerchantManager
    {
      public objResponse GetMerchantsForEdit(long Merchant_ID)
      {
          objResponse Response = new objResponse();
          try
          {
              SqlParameter[] sqlParameter = new SqlParameter[1];

              sqlParameter[0] = new SqlParameter("@Merchant_ID", SqlDbType.BigInt, 30);
              sqlParameter[0].Value = Merchant_ID;

              DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_getMerchantsForEdit", sqlParameter, DB_CONSTANTS.ConnectionString_Easy_Save);


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
              BAL.Common.LogManager.LogError("GetMerchantsForEdit", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
          }
          return Response;
      }

      public objResponse EditMerchant(Project.Entity.Merchant objMerchant, Int64 LogedUser)
      {
          objResponse Response = new objResponse();
          try
          {
              SqlParameter[] sqlParameter = new SqlParameter[17];

              sqlParameter[0] = new SqlParameter("@OrganijationName", SqlDbType.NVarChar, 150);
              sqlParameter[0].Value = objMerchant.OrganijationName;

              sqlParameter[1] = new SqlParameter("@OrganijationCode", SqlDbType.NVarChar, 20);
              sqlParameter[1].Value = objMerchant.OrganijationCode;

              sqlParameter[2] = new SqlParameter("@AddressLine1", SqlDbType.NVarChar, 80);
              sqlParameter[2].Value = objMerchant.AddressLine1;

              sqlParameter[3] = new SqlParameter("@AddressLine2", SqlDbType.NVarChar, 80);
              sqlParameter[3].Value = objMerchant.AddressLine2;

              sqlParameter[4] = new SqlParameter("@City", SqlDbType.NVarChar, 80);
              sqlParameter[4].Value = objMerchant.City;

              sqlParameter[5] = new SqlParameter("@State", SqlDbType.NVarChar, 80);
              sqlParameter[5].Value = objMerchant.State;

              sqlParameter[6] = new SqlParameter("@Countary", SqlDbType.NVarChar, 80);
              sqlParameter[6].Value = objMerchant.Countary;

              sqlParameter[7] = new SqlParameter("@ProgramType", SqlDbType.NVarChar, 30);
              sqlParameter[7].Value = objMerchant.ProgramType;

              sqlParameter[8] = new SqlParameter("@ContactPerson", SqlDbType.NVarChar, 80);
              sqlParameter[8].Value = objMerchant.ContactPerson;

              sqlParameter[9] = new SqlParameter("@Email", SqlDbType.NVarChar, 80);
              sqlParameter[9].Value = objMerchant.Email;

              sqlParameter[10] = new SqlParameter("@Mobile", SqlDbType.NVarChar, 15);
              sqlParameter[10].Value = objMerchant.Mobile;

              sqlParameter[11] = new SqlParameter("@Landline", SqlDbType.NVarChar, 15);
              sqlParameter[11].Value = objMerchant.Landline;

              sqlParameter[12] = new SqlParameter("@Website", SqlDbType.NVarChar, 100);
              sqlParameter[12].Value = objMerchant.Website;

              sqlParameter[13] = new SqlParameter("@CreatedBy", SqlDbType.BigInt, 60);
              sqlParameter[13].Value = LogedUser;

              sqlParameter[14] = new SqlParameter("@CreatedDate", SqlDbType.DateTime, 20);
              sqlParameter[14].Value = DateTime.Now;

              sqlParameter[15] = new SqlParameter("@Status", SqlDbType.NVarChar, 30);
              sqlParameter[15].Value = "Active";

              sqlParameter[16] = new SqlParameter("@Merchant_ID", SqlDbType.BigInt, 15);
              sqlParameter[16].Value = objMerchant.Merchant_ID;


              DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_EditMerchant", sqlParameter, DB_CONSTANTS.ConnectionString_Easy_Save);


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
              BAL.Common.LogManager.LogError("EditMerchant", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
          }

          return Response;
      }

      public objResponse AddMerchantUpload(string Merchant_ID, string FileName, string UploadType, string LogedUser)
      {
          objResponse Response = new objResponse();
          try
          {
              SqlParameter[] sqlParameter = new SqlParameter[6];

              sqlParameter[0] = new SqlParameter("@Merchant_ID", SqlDbType.BigInt, 10);
              sqlParameter[0].Value = Convert.ToInt64(Merchant_ID);

              sqlParameter[1] = new SqlParameter("@FileName", SqlDbType.NVarChar, 200);
              sqlParameter[1].Value = FileName;

              sqlParameter[2] = new SqlParameter("@UploadType", SqlDbType.NVarChar, 50);
              sqlParameter[2].Value = UploadType;

              sqlParameter[3] = new SqlParameter("@CreatedBy", SqlDbType.NVarChar, 60);
              sqlParameter[3].Value = LogedUser;

              sqlParameter[4] = new SqlParameter("@CreatedDate", SqlDbType.DateTime, 20);
              sqlParameter[4].Value = DateTime.Now;

              sqlParameter[5] = new SqlParameter("@Status", SqlDbType.NVarChar, 30);
              sqlParameter[5].Value = "Active";


              DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_AddMerchantUpload", sqlParameter, DB_CONSTANTS.ConnectionString_Easy_Save);


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
              BAL.Common.LogManager.LogError("AddMerchantUpload", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
          }

          return Response;
      }

      //public objResponse DeleteOldMerchantUpload(string Merchant_ID)
      //{
      //    objResponse Response = new objResponse();
      //    try
      //    {
      //        SqlParameter[] sqlParameter = new SqlParameter[6];

      //        sqlParameter[0] = new SqlParameter("@Merchant_ID", SqlDbType.BigInt, 10);
      //        sqlParameter[0].Value = Convert.ToInt64(Merchant_ID);

      //        DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_AddMerchantUpload", sqlParameter, DB_CONSTANTS.ConnectionString_Easy_Save);


      //        if (Response.ResponseData.Tables[0].Rows.Count > 0)
      //        {
      //            Response.ErrorCode = 0;
      //            Response.ErrorMessage = Response.ResponseData.Tables[0].Rows[0][0].ToString(); ;
      //        }
      //        else
      //        {
      //            Response.ErrorCode = 2001;
      //            Response.ErrorMessage = "There is an Error. Please Try After some time.";
      //        }
      //    }
      //    catch (Exception ex)
      //    {
      //        Response.ErrorMessage = ex.Message.ToString();
      //        BAL.Common.LogManager.LogError("DeleteOldMerchantUpload", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
      //    }

      //    return Response;
      //}

      
    }
}
