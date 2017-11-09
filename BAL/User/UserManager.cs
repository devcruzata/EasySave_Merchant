using DAL;
using Project.Entity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace BAL.User
{
   public class UserManager
    {
       public objResponse validateUser(string UserName , string Password)
       {
           objResponse Response = new objResponse();
           try
           {
               SqlParameter[] sqlParameter = new SqlParameter[2];

               sqlParameter[0] = new SqlParameter("@UserName", SqlDbType.NVarChar, 60);
               sqlParameter[0].Value = UserName;

               sqlParameter[1] = new SqlParameter("@Password", SqlDbType.NVarChar, 20);
               sqlParameter[1].Value = Password;

               DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_ValidateUser", sqlParameter, DB_CONSTANTS.ConnectionString_Easy_Save);


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
               BAL.Common.LogManager.LogError("validate User", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));              
           }

           return Response;
       }

       //public objResponse AddUser(string FirstName, string LastName, string Email, string Type, string LogedUser, string PIN)
       //{
       //    objResponse Response = new objResponse();
       //    try
       //    {
       //        SqlParameter[] sqlParameter = new SqlParameter[8];

       //        sqlParameter[0] = new SqlParameter("@FirstName", SqlDbType.NVarChar, 60);
       //        sqlParameter[0].Value = FirstName;

       //        sqlParameter[1] = new SqlParameter("@LastName", SqlDbType.NVarChar, 60);
       //        sqlParameter[1].Value = LastName;

       //        sqlParameter[2] = new SqlParameter("@Email", SqlDbType.NVarChar, 80);
       //        sqlParameter[2].Value = Email;

       //        sqlParameter[3] = new SqlParameter("@CreatedBy", SqlDbType.NVarChar, 60);
       //        sqlParameter[3].Value = LogedUser;

       //        sqlParameter[4] = new SqlParameter("@CreatedDate", SqlDbType.DateTime, 20);
       //        sqlParameter[4].Value = DateTime.Now;

       //        sqlParameter[5] = new SqlParameter("@Status", SqlDbType.NVarChar, 30);
       //        sqlParameter[5].Value = "Invitation Pending";

       //        sqlParameter[6] = new SqlParameter("@User_Type", SqlDbType.NVarChar, 10);
       //        sqlParameter[6].Value = Type;

       //        sqlParameter[7] = new SqlParameter("@PIN", SqlDbType.BigInt, 10);
       //        sqlParameter[7].Value = Convert.ToInt64(PIN);

       //        DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_AddUser", sqlParameter, DB_CONSTANTS.ConnectionString_ERP_CRUZATA);


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
       //        BAL.Common.LogManager.LogError("AddUser", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
       //    }

       //    return Response;
       //}

       //public List<Users> GetUsers(string PIN)
       //{
       //    objResponse Response = new objResponse();
       //    List<Users> user = new List<Users>();
       //    try
       //    {
       //        SqlParameter[] sqlParameter = new SqlParameter[1];

       //        sqlParameter[0] = new SqlParameter("@PIN", SqlDbType.BigInt, 10);
       //        sqlParameter[0].Value = Convert.ToInt64(PIN);

       //        DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_GetUsers", sqlParameter, DB_CONSTANTS.ConnectionString_ERP_CRUZATA);


       //        if (Response.ResponseData.Tables[0].Rows.Count > 0)
       //        {
       //            Response.ErrorCode = 0;
       //            foreach (DataRow dr in Response.ResponseData.Tables[0].Rows)
       //            {
       //                Users objUser = new Users();
       //                objUser.User_ID_PK = Convert.ToInt64(dr["User_ID_Auto_PK"]);
       //                objUser.FullName = dr["User_FirstName"].ToString() + " " + dr["User_LastName"].ToString();
       //                objUser.Email = dr["User_Email"].ToString();
       //                objUser.Status = dr["Status"].ToString();
       //                objUser.Date = Convert.ToDateTime(dr["CreatedDate"]);
       //                if (dr["User_Type"].ToString() == "ADM")
       //                {
       //                    objUser.UserType = "Admin";
       //                }
       //                else
       //                {
       //                    objUser.UserType = "User";
       //                }
       //                user.Add(objUser);
       //            }
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
       //        BAL.Common.LogManager.LogError("GetUsers", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
       //    }
       //    return user;
       //}

       public string[] AuthenticateUserForActivation(string MerchantID, string Activation_ID)
       {
           objResponse Response = new objResponse();
           string[] ActivationResults = new string[3];
           try
           {
               SqlParameter[] sqlParameter = new SqlParameter[2];

               sqlParameter[0] = new SqlParameter("@MerchantID", SqlDbType.BigInt, 10);
               sqlParameter[0].Value = Convert.ToInt64(MerchantID);

               sqlParameter[1] = new SqlParameter("@Activation_ID", SqlDbType.NVarChar, 50);
               sqlParameter[1].Value = Activation_ID;

               DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_AuthenticateUserForActivation", sqlParameter, DB_CONSTANTS.ConnectionString_Easy_Save);


               if (Response.ResponseData.Tables[0].Rows.Count > 0)
               {
                   Response.ErrorCode = 0;
                   ActivationResults[0] = Response.ResponseData.Tables[0].Rows[0][0].ToString();
                   if (ActivationResults[0] == "User Pending For Activation")
                   {
                       ActivationResults[1]  = Response.ResponseData.Tables[1].Rows[0][0].ToString();
                       ActivationResults[2]  =  Response.ResponseData.Tables[1].Rows[0][1].ToString();
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
               BAL.Common.LogManager.LogError("AuthenticateUserForActivation", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
           }

           return ActivationResults;
       }

       public objResponse ActivateMerchantAccount(string MerchantID, string UserName , string Password)
       {
           objResponse Response = new objResponse();
           try
           {
               SqlParameter[] sqlParameter = new SqlParameter[4];

               sqlParameter[0] = new SqlParameter("@MerchantID", SqlDbType.BigInt, 10);
               sqlParameter[0].Value = Convert.ToInt64(MerchantID);

               sqlParameter[1] = new SqlParameter("@UserName", SqlDbType.NVarChar, 100);
               sqlParameter[1].Value = UserName;

               sqlParameter[2] = new SqlParameter("@Password", SqlDbType.NVarChar, 10);
               sqlParameter[2].Value = Password;

               sqlParameter[3] = new SqlParameter("@CreatedDate", SqlDbType.DateTime, 50);
               sqlParameter[3].Value = DateTime.Now;

               DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_ActivateMerchantAccount", sqlParameter, DB_CONSTANTS.ConnectionString_Easy_Save);


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
               BAL.Common.LogManager.LogError("ActivateMerchantAccount", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
           }

           return Response;
       }

       public objResponse AddFlierImage(long Merchant_ID , string FlierImage)
       {
           objResponse Response = new objResponse();
           try
           {
               SqlParameter[] sqlParameter = new SqlParameter[2];

               sqlParameter[0] = new SqlParameter("@MerchantID", SqlDbType.BigInt, 10);
               sqlParameter[0].Value = Merchant_ID;

               sqlParameter[1] = new SqlParameter("@FlierImage", SqlDbType.NVarChar, 100);
               sqlParameter[1].Value = FlierImage;

               DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_AddFlierImage", sqlParameter, DB_CONSTANTS.ConnectionString_Easy_Save);


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
               BAL.Common.LogManager.LogError("AddFlierImage", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
           }
           return Response;
       }

       public List<Fliers> GetFlierImage(long Merchant_ID)
       {
           objResponse Response = new objResponse();
           List<Fliers> fliers = new List<Fliers>();
           try
           {
               SqlParameter[] sqlParameter = new SqlParameter[1];

               sqlParameter[0] = new SqlParameter("@MerchantID", SqlDbType.BigInt, 10);
               sqlParameter[0].Value = Merchant_ID;

               DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_GetFlierImage", sqlParameter, DB_CONSTANTS.ConnectionString_Easy_Save);


               if (Response.ResponseData.Tables[0].Rows.Count > 0)
               {
                   Response.ErrorCode = 0;
                   foreach (DataRow dr in Response.ResponseData.Tables[0].Rows)
                   {
                       Fliers objFliers = new Fliers();
                       objFliers.Flier_ID = Convert.ToInt64(dr["Flier_ID_Auto_PK"]);
                       objFliers.FlierImage = Convert.ToString(dr["Flier_Image"]);
                       objFliers.FlierImagePath = ConfigurationManager.AppSettings["FlierImageDir"] + Convert.ToString(dr["Flier_Image"]);
                       objFliers.Status = Convert.ToString(dr["Status"]);
                       fliers.Add(objFliers);
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
               BAL.Common.LogManager.LogError("GetFlierImage", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
           }
           return fliers;
       }

       public string DeleteFlier(Int64 Flier_id_pk)
       {
           objResponse response = new objResponse();
           DataTable dt = new DataTable();
           string result = "";
           try
           {
               SqlParameter[] sqlParameter = new SqlParameter[1];

               sqlParameter[0] = new SqlParameter("@Flier_id_pk", SqlDbType.BigInt, 0);
               sqlParameter[0].Value = Flier_id_pk;

               DATA_ACCESS_LAYER.Fill(response.ResponseData, "usp_DeleteFlier", sqlParameter, DB_CONSTANTS.ConnectionString_Easy_Save);

               dt = response.ResponseData.Tables[0];
               if (dt.Rows.Count > 0)
               {
                   response.ErrorCode = 0;
                   response.ErrorMessage = "Success";
                   result = Convert.ToString(dt.Rows[0][0]);
               }

           }
           catch (Exception ex)
           {
               response.ErrorCode = 2001;
               response.ErrorMessage = "Error while processing: " + ex.Message;
               BAL.Common.LogManager.LogError("DeleteFlier", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
           }
           return result;
       }

       public string ChangeFlierStatus(Int64 Flier_id_pk, long Merchant_ID)
       {
           objResponse response = new objResponse();
           DataTable dt = new DataTable();
           string result = "";
           try
           {
               SqlParameter[] sqlParameter = new SqlParameter[2];

               sqlParameter[0] = new SqlParameter("@Flier_id_pk", SqlDbType.BigInt, 0);
               sqlParameter[0].Value = Flier_id_pk;

               sqlParameter[1] = new SqlParameter("@MerchantID", SqlDbType.BigInt, 10);
               sqlParameter[1].Value = Merchant_ID;

               DATA_ACCESS_LAYER.Fill(response.ResponseData, "usp_ChangeFlierStatus", sqlParameter, DB_CONSTANTS.ConnectionString_Easy_Save);

               dt = response.ResponseData.Tables[0];
               if (dt.Rows.Count > 0)
               {
                   response.ErrorCode = 0;
                   response.ErrorMessage = "Success";
                   result = Convert.ToString(dt.Rows[0][0]);
               }

           }
           catch (Exception ex)
           {
               response.ErrorCode = 2001;
               response.ErrorMessage = "Error while processing: " + ex.Message;
               BAL.Common.LogManager.LogError("ChangeFlierStatus", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
           }
           return result;
       }

       public objResponse forgotPassword(string UserName)
       {
           objResponse Response = new objResponse();
           try
           {
               SqlParameter[] sqlParameter = new SqlParameter[1];

               sqlParameter[0] = new SqlParameter("@UserName", SqlDbType.NVarChar, 60);
               sqlParameter[0].Value = UserName;

               DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_ForgotPassword", sqlParameter, DB_CONSTANTS.ConnectionString_Easy_Save);


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
               BAL.Common.LogManager.LogError("forgotPassword", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
           }

           return Response;
       }

    }
}
