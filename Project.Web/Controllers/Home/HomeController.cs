using BAL.Merchant;
using Project.Entity;
using Project.Web.Common;
using Project.Web.Filters;
using Project.Web.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.Web.Controllers.Home
{
    public class HomeController : Controller
    {
        BAL.Dashboard.DashboardManager objDashboardManager = new BAL.Dashboard.DashboardManager();
        SessionHelper session;
        //
        // GET: /Home/

        [Authorize]
        [SessionTimeOut]
        public ActionResult MerchantDashboard()
        {
            objResponse Response = new objResponse();
            session = new SessionHelper();
            DashboardModel objDashboard = new DashboardModel();
            
            try
            {
                Response = objDashboardManager.GetDashboardData(session.UserSession.MerchantID);
                if (Response.ErrorCode == 0)
                {
                    if (Response.ResponseData.Tables[0].Rows.Count > 0)
                    {
                        objDashboard.TotalCustomer = Response.ResponseData.Tables[0].Rows[0][0].ToString();
                        objDashboard.TotalCardHoders = Response.ResponseData.Tables[0].Rows[0][0].ToString();
                    }
                    else
                    {
                        objDashboard.TotalCustomer = "0";
                        objDashboard.TotalCardHoders = "0";
                    }

                    if (Response.ResponseData.Tables[1].Rows.Count > 0)
                    {
                        objDashboard.TotalTransactions = Response.ResponseData.Tables[1].Rows[0][0].ToString();
                        objDashboard.TotalRevenue = Response.ResponseData.Tables[1].Rows[0][1].ToString();
                    }
                    else
                    {
                        objDashboard.TotalTransactions = "0";
                        objDashboard.TotalRevenue = "0";
                    }
                    
                    

                    if (Response.ResponseData.Tables[2].Rows.Count > 0)
                    {
                        foreach (DataRow dr in Response.ResponseData.Tables[2].Rows)
                        {
                            Project.Entity.Customer objCustomer = new Entity.Customer();
                            objCustomer.Customer_ID = Convert.ToInt64(dr["Customer_ID_Auto_PK"]);
                            objCustomer.FirstName = dr["FirstName"].ToString();
                            objCustomer.LastName = dr["LastName"].ToString();                            
                            objCustomer.Mobile = dr["Mobile"].ToString();                    
                            objCustomer.CardNo = dr["CardNo"].ToString();
                            objCustomer.MyPoints = dr["Tot_Points"].ToString();                                                  
                            objCustomer.TotalVisit = dr["TotalVisit"].ToString();
                            objCustomer.TotalSale = dr["TotalSale"].ToString();
                            objDashboard.customer.Add(objCustomer);
                        }
                    }

                    if (Response.ResponseData.Tables[3].Rows.Count > 0)
                    {
                        objDashboard.TotalEsAdminDue = Convert.ToDecimal(Response.ResponseData.Tables[3].Rows[0]["EasysaveAdminDue"]).ToString("0.##");
                        objDashboard.TotaEsCustDue = Convert.ToDecimal(Response.ResponseData.Tables[3].Rows[0]["EasysaveCustomerDue"]).ToString("0.##");
                        objDashboard.TotalDue = Convert.ToDecimal(Response.ResponseData.Tables[3].Rows[0]["EasysaveTotalDue"]).ToString("0.##");                       
                    }
                    else
                    {
                        objDashboard.TotalEsAdminDue = "0";
                        objDashboard.TotaEsCustDue = "0"; 
                        objDashboard.TotalDue = "0";                      
                    }
                }
            }
            catch (Exception ex)
            {
                BAL.Common.LogManager.LogError("MerchantDashboard", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
            }
            return View(objDashboard);
        }

        [Authorize]
        [SessionTimeOut]
        public ActionResult AddFliers()
        {
            session = new SessionHelper();
            BAL.User.UserManager objUserMnanager = new BAL.User.UserManager();
            FliersModel objModel = new FliersModel();
            objModel.fliers = objUserMnanager.GetFlierImage(Convert.ToInt64(session.UserSession.MerchantID));
            return View(objModel);
        }

        [Authorize]
        [HttpPost]
        [SessionTimeOut]
        public ActionResult AddFliersImage()
        {
            objResponse Response = new objResponse();
            session = new SessionHelper();
            BAL.User.UserManager objUserMnanager = new BAL.User.UserManager();
            try
            {
               // Int64 EstimateID = Convert.ToInt64(Request.Form["Estimate_ID"]);
              //  string comment = Request.Form["Comment"].ToString();
                string fname;

               
                if (Response.ErrorCode == 0)
                {
                    if (Request.Files.Count > 0)
                    {
                        HttpFileCollectionBase files = Request.Files;
                        for (int i = 0; i < files.Count; i++)
                        {
                            HttpPostedFileBase file = files[i];

                            // Checking for Internet Explorer  
                            if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                            {
                                string[] testfiles = file.FileName.Split(new char[] { '\\' });
                                fname = testfiles[testfiles.Length - 1];
                            }
                            else
                            {
                                fname = file.FileName;
                            }

                            string newFileName = "Flier_" + session.UserSession.MerchantID.ToString() + "_" + fname;
                            string newFilePath = Server.MapPath(ConfigurationManager.AppSettings["FlierImageDir"]) + newFileName;
                            file.SaveAs(newFilePath);

                            Response = objUserMnanager.AddFlierImage(Convert.ToInt64(session.UserSession.MerchantID), newFileName);
                            if (Response.ErrorCode == 0)
                            {
                                FliersModel objModel = new FliersModel();
                                objModel.fliers = objUserMnanager.GetFlierImage(Convert.ToInt64(session.UserSession.MerchantID));
                                return View("AjaxFlierHome",objModel);
                                //return Json("success", JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                if (System.IO.File.Exists(newFilePath))
                                {
                                    System.IO.File.Delete(newFilePath);
                                }
                                return Json("success", JsonRequestBehavior.AllowGet);
                            }
                        }
                        return Json("success", JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json("success", JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json("", JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                BAL.Common.LogManager.LogError("AddComment", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }

        [Authorize]
        [HttpPost]
        [SessionTimeOut]
        public ActionResult DeleteFlier(string Flier_ID_PK, string FlierImageName)
        {
           // string FlierImageName="";
            session = new SessionHelper();
            string response = "";
            BAL.User.UserManager objUserMnanager = new BAL.User.UserManager();
            try
            {
                response = objUserMnanager.DeleteFlier(Convert.ToInt64(Flier_ID_PK));               
                string newFilePath = Server.MapPath(ConfigurationManager.AppSettings["FlierImageDir"]) + FlierImageName;
                if (System.IO.File.Exists(newFilePath))
                {
                    System.IO.File.Delete(newFilePath);
                }
                FliersModel objModel = new FliersModel();
                objModel.fliers = objUserMnanager.GetFlierImage(Convert.ToInt64(session.UserSession.MerchantID));
                return View("AjaxFlierHome", objModel);
                //return Json(response, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                BAL.Common.LogManager.LogError("DeleteFlier Post Method", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }

        [Authorize]
        [HttpPost]
        [SessionTimeOut]
        public ActionResult ChangeFlierStatus(string Flier_ID_PK)
        {
            session = new SessionHelper();
            string response = "";
            BAL.User.UserManager objUserMnanager = new BAL.User.UserManager();
            try
            {
                response = objUserMnanager.ChangeFlierStatus(Convert.ToInt64(Flier_ID_PK), Convert.ToInt64(session.UserSession.MerchantID));
                FliersModel objModel = new FliersModel();
                objModel.fliers = objUserMnanager.GetFlierImage(Convert.ToInt64(session.UserSession.MerchantID));
                return View("AjaxFlierHome", objModel);
               // return Json(response, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                BAL.Common.LogManager.LogError("ChangeFlierStatus get Method", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }


        [Authorize]
        [SessionTimeOut]
        public ActionResult MyProfile()
        {
            objResponse Response = new objResponse();
            MerchantModel objMerchant = new MerchantModel();
            MerchantManager objMerchantManager = new MerchantManager();
            session = new SessionHelper();
            try
            {
                Response = objMerchantManager.GetMerchantsForEdit(Convert.ToInt64(session.UserSession.MerchantID));

                if (Response.ErrorCode == 0)
                {

                    objMerchant.Merchant_ID = Convert.ToInt64(Response.ResponseData.Tables[0].Rows[0]["Merchant_ID_Auto_PK"]);
                    objMerchant.OrganijationName = Response.ResponseData.Tables[0].Rows[0]["Organization_Name"].ToString();
                    objMerchant.ContactPerson = Response.ResponseData.Tables[0].Rows[0]["Contact_Person"].ToString();
                    objMerchant.Email = Response.ResponseData.Tables[0].Rows[0]["Contact_Email"].ToString();
                    objMerchant.Mobile = Response.ResponseData.Tables[0].Rows[0]["Contact_Mobile"].ToString();
                    objMerchant.AddressLine1 = Response.ResponseData.Tables[0].Rows[0]["Address_Line1"].ToString();
                    objMerchant.AddressLine2 = Response.ResponseData.Tables[0].Rows[0]["Address_Line2"].ToString();
                    objMerchant.City = Response.ResponseData.Tables[0].Rows[0]["City"].ToString();
                    objMerchant.State = Response.ResponseData.Tables[0].Rows[0]["State"].ToString();
                    objMerchant.Countary = Response.ResponseData.Tables[0].Rows[0]["Countary"].ToString();
                    objMerchant.Landline = Response.ResponseData.Tables[0].Rows[0]["Contact_Landline"].ToString();
                    objMerchant.OrganijationCode = Response.ResponseData.Tables[0].Rows[0]["Organization_Code"].ToString();
                    objMerchant.Website = Response.ResponseData.Tables[0].Rows[0]["Website_Url"].ToString();

                    if (Response.ResponseData.Tables[1].Rows.Count > 0)
                    {
                        objMerchant.Merchant_Logo = Response.ResponseData.Tables[1].Rows[0]["LogoImage"].ToString();
                    }

                    if (Response.ResponseData.Tables[2].Rows.Count > 0)
                    {
                        objMerchant.Merchant_Image = Response.ResponseData.Tables[2].Rows[0]["MerchantImage"].ToString();
                    }                    

                    return View(objMerchant);
                }
                else
                {
                    return View(objMerchant);
                }
            }
            catch (Exception ex)
            {
                BAL.Common.LogManager.LogError("MyProfile Get", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                return View(objMerchant);
            }
        }

        [Authorize]
        [HttpPost]
        [SessionTimeOut]
        public ActionResult MyProfile(MerchantModel objModel)
        {
            objResponse Response = new objResponse();
            session = new SessionHelper();
            Merchant objMerchant = new Merchant();
            MerchantManager objMerchantManager = new MerchantManager();
            try
            {
                objMerchant.OrganijationName = objModel.OrganijationName;
                objMerchant.OrganijationCode = objModel.OrganijationCode;
                objMerchant.AddressLine1 = objModel.AddressLine1;
                objMerchant.AddressLine2 = objModel.AddressLine2;
                objMerchant.City = objModel.City;
                objMerchant.State = objModel.State;
                objMerchant.Countary = objModel.Countary;
                objMerchant.ContactPerson = objModel.ContactPerson;
                objMerchant.Email = objModel.Email;
                objMerchant.Mobile = objModel.Mobile;
                objMerchant.Landline = objModel.Landline;
                objMerchant.Website = objModel.Website;
                objMerchant.Merchant_ID = objModel.Merchant_ID;
                Response = objMerchantManager.EditMerchant(objMerchant, Convert.ToInt64(session.UserSession.MerchantID));
                if (Response.ErrorCode == 0)
                {
                    // Uploading Customer Image and Saving Details to DB
                    try
                    {
                        foreach (HttpPostedFileBase file in objModel.Logo)
                        {
                            if (file != null)
                            {
                                
                                string filename = System.IO.Path.GetFileName(file.FileName);
                                Debug.WriteLine("file name is: " + filename);
                                string[] fileext = filename.Split('.');
                                int length = fileext.Length;
                                //string newFileName = "LEAD0"+Lead_ID+"_"+filename+"." + fileext[1];
                                string newFileName = fileext[0] + "_" + session.UserSession.MerchantID + "." + fileext[(fileext.Length - 1)];

                                string newFilePath = Server.MapPath(ConfigurationManager.AppSettings["MerlogoDir"]) + newFileName;

                                string oldFilePath = Server.MapPath(ConfigurationManager.AppSettings["MerlogoDir"]) + objModel.Merchant_Logo;
                                if (System.IO.File.Exists(oldFilePath))
                                {
                                    System.IO.File.Delete(oldFilePath);
                                }
                                file.SaveAs(newFilePath);

                                if (filename != "")
                                {
                                    Response = objMerchantManager.AddMerchantUpload(session.UserSession.MerchantID,newFileName,"LOGO",session.UserSession.Username);
                                    if (Response.ErrorCode != 0)
                                    {
                                        if (System.IO.File.Exists(newFilePath))
                                        {
                                            System.IO.File.Delete(newFilePath);
                                        }
                                    }
                                    else
                                    {
                                        Session["MerLogo"] = "~/Uploads/Logo/"+newFileName;                                        
                                    }
                                }
                            }
                        }

                        foreach (HttpPostedFileBase file in objModel.Image)
                        {
                            if (file != null)
                            {
                                string filename = System.IO.Path.GetFileName(file.FileName);
                                Debug.WriteLine("file name is: " + filename);
                                string[] fileext = filename.Split('.');

                                //string newFileName = "LEAD0"+Lead_ID+"_"+filename+"." + fileext[1];
                                string newFileName = fileext[0] + "_" + session.UserSession.MerchantID + "." + fileext[1];

                                string newFilePath = Server.MapPath(ConfigurationManager.AppSettings["MerImageDir"]) + newFileName;

                                string oldFilePath = Server.MapPath(ConfigurationManager.AppSettings["MerImageDir"]) + objModel.Merchant_Image;
                                if (System.IO.File.Exists(oldFilePath))
                                {
                                    System.IO.File.Delete(oldFilePath);
                                }
                                file.SaveAs(newFilePath);

                                if (filename != "")
                                {
                                    Response = objMerchantManager.AddMerchantUpload(session.UserSession.MerchantID, newFileName, "Image", session.UserSession.Username);
                                    if (Response.ErrorCode != 0)
                                    {
                                        if (System.IO.File.Exists(newFilePath))
                                        {
                                            System.IO.File.Delete(newFilePath);
                                        }
                                    }
                                    else
                                    {
                                        Session["MerImage"] = "~/Uploads/Merchant_Image/"+newFileName;
                                    }
                                }
                            }
                        }

                        // If we got this far , than there is something wrong. Redirect to LeadsHome Page
                        return RedirectToAction("MyProfile", "Home");
                    }
                    catch (Exception ex)
                    {
                        ViewBag.Error_Msg = ex.Message.ToString();
                        BAL.Common.LogManager.LogError("AddCustomer image Upload Post Method", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                        return View();
                    } 
                    return RedirectToAction("MyProfile","Home");
                }
                else
                {
                    ViewBag.ErrorMsg = Response.ErrorMessage;
                    return View();
                }

            }
            catch (Exception ex)
            {
                BAL.Common.LogManager.LogError("MyProfile Post", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                ViewBag.ErrorMsg = Response.ErrorMessage;
                return View();
            }
        }

        [Authorize]        
        [SessionTimeOut]
        public ActionResult PInfo()
        {
            return View();
        }
        
    }
}
