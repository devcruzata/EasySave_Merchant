using Project.Entity;
using Project.Web.Common;
using Project.Web.Filters;
using Project.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.Web.Controllers.Setings
{
    public class SetingsController : Controller
    {
        BAL.Seting.SetingManager objSetingManager = new BAL.Seting.SetingManager();
        SessionHelper session;
        //
        // GET: /Setings/

        [Authorize]
        [SessionTimeOut]
        public ActionResult ProgramSetings()
        {
            SetingModel objModel = new SetingModel();
            session = new SessionHelper();
            objResponse Response = new objResponse();
            try
            {
                Response = objSetingManager.GetProgramSeting(session.UserSession.MerchantID);
                if (Response.ErrorCode == 0)
                {
                   // objModel.AwardSeting = Response.ResponseData.Tables[0].Rows[0]["RewardRule"].ToString();
                    objModel.RedemSeting = (Convert.ToDouble(Response.ResponseData.Tables[0].Rows[0]["RedemptionRule"])*100).ToString();
                    
                }                
            }
            catch (Exception ex)
            {
                BAL.Common.LogManager.LogError("ProgramSetings Get Method", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
            }

            return View(objModel);
        }

        [Authorize]
        [HttpPost]
        [SessionTimeOut]
        public ActionResult ProgramSetings(SetingModel objSetingModel)
        {
            objResponse Response = new objResponse();
            session = new SessionHelper();
            try
            {
                string redemptionRule = (Convert.ToInt32(objSetingModel.RedemSeting) / 100).ToString();
                Response = objSetingManager.AddProgramSeting(session.UserSession.MerchantID, redemptionRule);

                if (Response.ErrorCode == 0)
                {
                    return RedirectToRoute("MerchantDashboard");
                }
                else
                {
                    ViewBag.Error_Msg = Response.ErrorMessage;
                    return View(objSetingModel);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error_Msg = ex.Message.ToString();
                BAL.Common.LogManager.LogError("ProgramSetings Post Method", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                return View(objSetingModel);  
            }
        }

    }
}
