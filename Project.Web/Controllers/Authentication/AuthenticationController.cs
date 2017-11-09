
using BAL.User;
using Project.Entity;
using Project.Web.Common;
using Project.Web.Filters;
using Project.Web.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Project.Web.Controllers.Authentication
{
    public class AuthenticationController : Controller
    {
        UserManager objUserManager = new UserManager();
        //
        // GET: /Authentication/

        public ActionResult MerchantLogin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult MerchantLogin(LoginModel objModel)
        {
            objResponse Response = new objResponse();
            try
            {
                Response = objUserManager.validateUser(objModel.Username, objModel.Password);

                if (Response.ErrorCode == 0)
                {
                    if (Response.ErrorMessage != "Incorrect UserName" && Response.ErrorMessage != "Incorrect Password" && Response.ErrorMessage != "Inactive account, Please contact to Admin to activate your account.")
                    {
                        FormsAuthentication.SetAuthCookie(objModel.Username, false);
                        Session["User"] = Response.ResponseData.Tables[0].Rows[0]["Full_Name"].ToString();
                        Session["User_Type"] = Response.ResponseData.Tables[0].Rows[0]["User_Type"].ToString();
                        Session["UserName"] = Response.ResponseData.Tables[0].Rows[0]["UserName"].ToString();
                        Session["UserID"] = Response.ResponseData.Tables[0].Rows[0]["User_ID_Auto_PK"].ToString();
                        Session["SetingFlag"] = Response.ResponseData.Tables[0].Rows[0]["Seting_Flag"].ToString();
                        if (Response.ResponseData.Tables[0].Rows.Count > 0)
                        {
                            Session["MerLogo"] = "~/Uploads/Logo/" + Response.ResponseData.Tables[0].Rows[0]["LogoImage"].ToString();
                        }
                        else
                        {
                            Session["MerLogo"] = "~/Uploads/Logo/";
                        }

                        if (Response.ResponseData.Tables[0].Rows.Count > 0)
                        {
                            Session["MerImage"] = "~/Uploads/Merchant_Image/" + Response.ResponseData.Tables[0].Rows[0]["MerchantImage"].ToString();
                        }
                        else
                        {
                            Session["MerImage"] = "~/Uploads/Merchant_Image/";
                        }
                        
                        SessionHelper session = new SessionHelper();
                        session.UserSession = new UserSession()
                        {
                            UserId = Convert.ToInt64(Response.ResponseData.Tables[0].Rows[0]["User_ID_Auto_PK"]),
                            Username = Response.ResponseData.Tables[0].Rows[0]["UserName"].ToString(),
                            FullName = Response.ResponseData.Tables[0].Rows[0]["Full_Name"].ToString(),
                            MerchantID = Response.ResponseData.Tables[0].Rows[0]["Relation_ID_Fk"].ToString()
                           
                        };
                        if (Response.ResponseData.Tables[0].Rows[0]["User_Type"].ToString() == "MER")
                        {
                            return RedirectToRoute("MerchantDashboard");
                        }
                        else
                        {
                            ViewBag.Error_Msg = "Incorrect Username or Password.";
                            return View();         
                        }
                    }
                    else
                    {
                         ViewBag.Error_Msg = Response.ErrorMessage;                         
                         return View();                       
                    }
                }
                else
                {
                    ViewBag.Error_Msg = Response.ErrorMessage;                    
                    return View();                  
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error_Msg = ex.Message.ToString();               
                BAL.Common.LogManager.LogError("MerchantLogin Post Method", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                return View();               
            }
        }

        [Authorize]
        [SessionTimeOut]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            return RedirectToAction("MerchantLogin", "Authentication");
            //return RedirectToAction("/Authentication/AdminLogin");
        }

        
        public ActionResult ActivationPage(string Merchant_ID_Pk, string Activation_ID)
        {
            objResponse Response = new objResponse();
            UserModel objUserModel = new UserModel();
            string[] Results = new string[3];
            try
            {
                Results = objUserManager.AuthenticateUserForActivation(Merchant_ID_Pk , Activation_ID);

                if (Response.ErrorCode == 0)
                {
                    if (Results[0] != "User Already Activated")
                    {
                        objUserModel.UserName = Results[2];
                        ViewBag.OrgName = Results[1];
                        ViewBag.MerchantID = Merchant_ID_Pk;
                        return View(objUserModel);
                    }
                    else
                    {
                        return RedirectToAction("MerchantLogin", "Authentication");
                    }
                }
                else
                {
                    return View("Error");
                }
            }
            catch (Exception ex)
            {                
                BAL.Common.LogManager.LogError("ActivationPage get Method", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                return View("Error"); 
            }
        }

        
        [HttpPost]
        
        public ActionResult ActivationPage(UserModel objModel)
        {
            objResponse Response = new objResponse();
            try
            {
                Response = objUserManager.ActivateMerchantAccount(objModel.Relation_ID_Fk.ToString(), objModel.UserName, objModel.Password);
                if (Response.ErrorCode == 0)
                {
                    FormsAuthentication.SetAuthCookie(objModel.UserName, false);
                    Session["User"] = Response.ResponseData.Tables[0].Rows[0]["Full_Name"].ToString();
                    Session["User_Type"] = Response.ResponseData.Tables[0].Rows[0]["User_Type"].ToString();
                    Session["UserName"] = Response.ResponseData.Tables[0].Rows[0]["UserName"].ToString();
                    Session["UserID"] = Response.ResponseData.Tables[0].Rows[0]["User_ID_Auto_PK"].ToString();
                    Session["MerLogo"] = "~/Uploads/Logo/" + Response.ResponseData.Tables[0].Rows[0]["LogoImage"].ToString();
                    Session["MerImage"] = "~/Uploads/Merchant_Image/" + Response.ResponseData.Tables[0].Rows[0]["MerchantImage"].ToString();
                    SessionHelper session = new SessionHelper();
                    session.UserSession = new UserSession()
                    {
                        UserId = Convert.ToInt64(Response.ResponseData.Tables[0].Rows[0]["User_ID_Auto_PK"]),
                        Username = Response.ResponseData.Tables[0].Rows[0]["UserName"].ToString(),
                        FullName = Response.ResponseData.Tables[0].Rows[0]["Full_Name"].ToString(),
                        MerchantID = Response.ResponseData.Tables[0].Rows[0]["Relation_ID_Fk"].ToString()

                    };
                    return RedirectToRoute("MerchantDashboard");
                }
                else
                {
                    ViewBag.Error_Msg = Response.ErrorMessage;
                    return View(objModel);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error_Msg = ex.Message.ToString();
                BAL.Common.LogManager.LogError("ActivationPage post Method", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                return View(objModel);
            }
        }

      
        public ActionResult ESProgramLogin(string sessionid, string UserName, string Password)
        {
            UserModel objUserModel = new UserModel();
            objUserModel.UserName = UserName;
            objUserModel.Password = Password;
            return View(objUserModel);
        }

              
        public ActionResult ForgotPassword(string email)
        {
            objResponse Response = new objResponse();
            try
            {
                Response = objUserManager.forgotPassword(email);
                if (Response.ErrorCode == 0)
                {
                    if (Response.ErrorMessage != "The Email you provided is not associate to any EasySave account" || Response.ErrorMessage != "Inactive account, Please contact to Admin to activate your account.")
                    {
                        string Body = EasySave_API.Structure.MailHelper.MailBodyBuilder.PopulateBody(email, Response.ResponseData.Tables[0].Rows[0][0].ToString(), "~/MailHelper/ForgotPassword.html");

                        BAL.Helper.Helper.SendEmail(email, "EasySave Forgotten Password", Body);

                        return Json("A Email is sent to your registered Email, Please Check", JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(Response.ErrorMessage, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json("An Error Occured in reseting your password.", JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                BAL.Common.LogManager.LogError("MerchantLogin Post Method", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }
    }
}
