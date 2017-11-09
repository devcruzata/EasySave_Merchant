using BAL.Transaction;
using Project.Entity;
using Project.Web.Common;
using Project.Web.Filters;
using Project.Web.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.Web.Controllers.Transactions
{
    public class TransactionsController : Controller
    {
        TransactionManager objTransactionManager = new TransactionManager();
        SessionHelper session;

        //
        // GET: /Transactions/

        [Authorize]
        [SessionTimeOut]
        public ActionResult TransactionHome()
        {
            TransactionModel objTransaction = new TransactionModel();
            return View(objTransaction);
        }

        [Authorize]
        [HttpPost]
        [SessionTimeOut]
        public ActionResult TransactionHome(TransactionModel model)
        {
            objResponse response;
            model.hasReport = false;
            session = new SessionHelper();

            TimeZoneInfo timeZoneInfo;
            timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Atlantic Standard Time");
            try
            {

                //model.eDate = TimeZoneInfo.ConvertTime(BAL.Helper.Helper.ConvertToDateNullable(model.eDateString, "dd/MM/yyyy"), timeZoneInfo);
                model.eDate = BAL.Helper.Helper.ConvertToDateNullable(model.eDateString, "dd/MM/yyyy");

               // model.sDate = TimeZoneInfo.ConvertTime(BAL.Helper.Helper.ConvertToDateNullable(model.sDateString, "dd/MM/yyyy"), timeZoneInfo);
                model.sDate = BAL.Helper.Helper.ConvertToDateNullable(model.sDateString, "dd/MM/yyyy");


                response = objTransactionManager.MyTransactionReport(session.UserSession.MerchantID, model.sDate, model.eDate);
                if (response != null && response.ErrorCode == 0 && response.ResponseData != null && response.ResponseData.Tables.Count > 0 && response.ResponseData.Tables[0].Rows.Count > 0)
                {
                    model.TransactionReport = new List<TransactionReportItem>();
                    int Counter = 0;
                    foreach (DataRow item in response.ResponseData.Tables[0].Rows)
                    {
                        model.TransactionReport.Add(new TransactionReportItem()
                        {
                            SeriolNo = ++Counter,
                            CardNo = item[5].ToString(),
                            CardHolder = item[6].ToString(),
                            //DateOfTrans = TimeZoneInfo.ConvertTime(Convert.ToDateTime(item[0]), timeZoneInfo).ToString("dd/MM/yyyy hh:mm tt"),
                            DateOfTrans = Convert.ToDateTime(item[0]).ToString("dd/MM/yyyy hh:mm tt"),
                            Store = item[4].ToString(),
                            Amount = item[1].ToString(),
                            RewardPoint = item[2].ToString(),
                            TotalPoints = item[3].ToString()
                        });
                    }


                    model.hasReport = true;
                    model.errorMessage = string.Empty;
                }
                else
                {
                    model.hasReport = false;
                    model.errorMessage = "Request report did not found.";
                }

            }
            catch (Exception ex)
            {
                BAL.Common.LogManager.LogError("TransactionHome post Method", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
            }
            return View(model);
        }

        [Authorize]
        [SessionTimeOut]
        public ActionResult RedemTransHome()
        {
            TransactionModel objTransaction = new TransactionModel();
            return View(objTransaction);
        }

        [Authorize]
        [HttpPost]
        [SessionTimeOut]
        public ActionResult RedemTransHome(TransactionModel model)
        {
            objResponse response;
            model.hasReport = false;
            session = new SessionHelper();

            TimeZoneInfo timeZoneInfo;
            timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Atlantic Standard Time");
            try
            {

                //model.eDate = TimeZoneInfo.ConvertTime(BAL.Helper.Helper.ConvertToDateNullable(model.eDateString, "dd/MM/yyyy"), timeZoneInfo);
                 model.eDate = BAL.Helper.Helper.ConvertToDateNullable(model.eDateString, "dd/MM/yyyy");

                //model.sDate = TimeZoneInfo.ConvertTime(BAL.Helper.Helper.ConvertToDateNullable(model.sDateString, "dd/MM/yyyy"), timeZoneInfo);
                  model.sDate = BAL.Helper.Helper.ConvertToDateNullable(model.sDateString, "dd/MM/yyyy");


                response = objTransactionManager.MyRedemTransactionReport(session.UserSession.MerchantID, model.sDate, model.eDate);
                if (response != null && response.ErrorCode == 0 && response.ResponseData != null && response.ResponseData.Tables.Count > 0 && response.ResponseData.Tables[0].Rows.Count > 0)
                {
                    model.RedemReport = new List<RedemReportItem>();
                    int Counter = 0;
                    foreach (DataRow item in response.ResponseData.Tables[0].Rows)
                    {
                        model.RedemReport.Add(new RedemReportItem()
                        {
                            SeriolNo = ++Counter,
                            CardNo = item[5].ToString(),
                            CardHolder = item[6].ToString(),
                            //DateOfTrans = TimeZoneInfo.ConvertTime(Convert.ToDateTime(item[0]), timeZoneInfo).ToString("dd/MM/yyyy hh:mm tt"),
                            DateOfTrans = Convert.ToDateTime(item[0]).ToString("dd/MM/yyyy hh:mm tt"),
                            Store = item[4].ToString(),
                            Amount = item[1].ToString(),
                            RedemPoint = item[2].ToString(),
                            TotalPoints = item[3].ToString()
                        });
                    }


                    model.hasReport = true;
                    model.errorMessage = string.Empty;
                }
                else
                {
                    model.hasReport = false;
                    model.errorMessage = "Request report did not found.";
                }

            }
            catch (Exception ex)
            {
                BAL.Common.LogManager.LogError("TransactionHome post Method", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
            }
            return View(model);
        }

        [Authorize]
        [SessionTimeOut]
        public ActionResult RefundTransHome()
        {
            TransactionModel objTransaction = new TransactionModel();
            return View(objTransaction);
        }

        [Authorize]
        [HttpPost]
        [SessionTimeOut]
        public ActionResult RefundTransHome(TransactionModel model)
        {
            objResponse response;
            model.hasReport = false;
            session = new SessionHelper();
            try
            {

                model.eDate = BAL.Helper.Helper.ConvertToDateNullable(model.eDateString, "dd/MM/yyyy");

                model.sDate = BAL.Helper.Helper.ConvertToDateNullable(model.sDateString, "dd/MM/yyyy");


                response = objTransactionManager.MyRefundTransactionReport(session.UserSession.MerchantID, model.sDate, model.eDate);
                if (response != null && response.ErrorCode == 0 && response.ResponseData != null && response.ResponseData.Tables.Count > 0 && response.ResponseData.Tables[0].Rows.Count > 0)
                {
                    model.RefundReport = new List<RefundReportItem>();
                    int Counter = 0;
                    foreach (DataRow item in response.ResponseData.Tables[0].Rows)
                    {
                        model.RefundReport.Add(new RefundReportItem()
                        {
                            SeriolNo = ++Counter,
                            CardNo = item[5].ToString(),
                            CardHolder = item[6].ToString(),
                            DateOfTrans = item[0].ToString(),
                            Store = item[4].ToString(),
                            Amount = item[1].ToString(),
                            RefundPoint = item[2].ToString(),
                            TotalPoints = item[3].ToString()
                        });
                    }


                    model.hasReport = true;
                    model.errorMessage = string.Empty;
                }
                else
                {
                    model.hasReport = false;
                    model.errorMessage = "Request report did not found.";
                }

            }
            catch (Exception ex)
            {
                BAL.Common.LogManager.LogError("RefundTransHome post Method", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
            }
            return View(model);
        }
    }
}
