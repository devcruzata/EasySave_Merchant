using DAL;
using Project.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace BAL.Transaction
{
    public class TransactionManager
    {
        public objResponse MyTransactionReport(string MerchantID, DateTime? start, DateTime? end)
        {
            objResponse Response = new objResponse();
            try
            {
                SqlParameter[] sqlParameter = new SqlParameter[5];

                sqlParameter[0] = new SqlParameter("@MerchantID", SqlDbType.BigInt, 17);
                sqlParameter[0].Value = Convert.ToInt64(MerchantID);

                sqlParameter[1] = new SqlParameter("@StartDate", SqlDbType.Date);
                sqlParameter[1].Value = start;

                sqlParameter[2] = new SqlParameter("@EndDate", SqlDbType.Date);
                sqlParameter[2].Value = end;

                sqlParameter[3] = new SqlParameter("@errorCode", SqlDbType.Int);
                sqlParameter[3].Direction = ParameterDirection.Output;

                sqlParameter[4] = new SqlParameter("@errorMessage", SqlDbType.NVarChar, 200);
                sqlParameter[4].Direction = ParameterDirection.Output;


                DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_GenerateTransactionReportForMerchant", sqlParameter, DB_CONSTANTS.ConnectionString_Easy_Save);

                Response.ErrorCode = Helper.Helper.ConvertToInt(sqlParameter[3].Value.ToString());
                Response.ErrorMessage = sqlParameter[4].Value.ToString();
            }
            catch (Exception ex)
            {
                Response.ErrorCode = 2001;
                Response.ErrorMessage = "Error while processing: " + ex.Message;
                BAL.Common.LogManager.LogError("MyTransactionReport", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
            }
            return Response;
        }

        public objResponse MyRedemTransactionReport(string MerchantID, DateTime? start, DateTime? end)
        {
            objResponse Response = new objResponse();
            try
            {
                SqlParameter[] sqlParameter = new SqlParameter[5];

                sqlParameter[0] = new SqlParameter("@MerchantID", SqlDbType.BigInt, 17);
                sqlParameter[0].Value = Convert.ToInt64(MerchantID);

                sqlParameter[1] = new SqlParameter("@StartDate", SqlDbType.Date);
                sqlParameter[1].Value = start;

                sqlParameter[2] = new SqlParameter("@EndDate", SqlDbType.Date);
                sqlParameter[2].Value = end;

                sqlParameter[3] = new SqlParameter("@errorCode", SqlDbType.Int);
                sqlParameter[3].Direction = ParameterDirection.Output;

                sqlParameter[4] = new SqlParameter("@errorMessage", SqlDbType.NVarChar, 200);
                sqlParameter[4].Direction = ParameterDirection.Output;


                DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_GenerateRedemTransactionReportForMerchant", sqlParameter, DB_CONSTANTS.ConnectionString_Easy_Save);

                Response.ErrorCode = Helper.Helper.ConvertToInt(sqlParameter[3].Value.ToString());
                Response.ErrorMessage = sqlParameter[4].Value.ToString();
            }
            catch (Exception ex)
            {
                Response.ErrorCode = 2001;
                Response.ErrorMessage = "Error while processing: " + ex.Message;
                BAL.Common.LogManager.LogError("MyRedemTransactionReport", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
            }
            return Response;
        }

        public objResponse MyRefundTransactionReport(string MerchantID, DateTime? start, DateTime? end)
        {
            objResponse Response = new objResponse();
            try
            {
                SqlParameter[] sqlParameter = new SqlParameter[5];

                sqlParameter[0] = new SqlParameter("@MerchantID", SqlDbType.BigInt, 17);
                sqlParameter[0].Value = Convert.ToInt64(MerchantID);

                sqlParameter[1] = new SqlParameter("@StartDate", SqlDbType.Date);
                sqlParameter[1].Value = start;

                sqlParameter[2] = new SqlParameter("@EndDate", SqlDbType.Date);
                sqlParameter[2].Value = end;

                sqlParameter[3] = new SqlParameter("@errorCode", SqlDbType.Int);
                sqlParameter[3].Direction = ParameterDirection.Output;

                sqlParameter[4] = new SqlParameter("@errorMessage", SqlDbType.NVarChar, 200);
                sqlParameter[4].Direction = ParameterDirection.Output;


                DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_GenerateREFUNDTransactionReportForMerchant", sqlParameter, DB_CONSTANTS.ConnectionString_Easy_Save);

                Response.ErrorCode = Helper.Helper.ConvertToInt(sqlParameter[3].Value.ToString());
                Response.ErrorMessage = sqlParameter[4].Value.ToString();
            }
            catch (Exception ex)
            {
                Response.ErrorCode = 2001;
                Response.ErrorMessage = "Error while processing: " + ex.Message;
                BAL.Common.LogManager.LogError("MyRedemTransactionReport", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
            }
            return Response;
        }
    }
}
