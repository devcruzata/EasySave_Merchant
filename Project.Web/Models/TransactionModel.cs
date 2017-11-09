using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.Web.Models
{
    public class TransactionModel
    {
        public string sDateString { get; set; }
        public string eDateString { get; set; }
        public DateTime? eDate { get; set; }
        public DateTime? sDate { get; set; }
        public bool hasReport { get; set; }
        public string errorMessage { get; set; }
        public List<TransactionReportItem> TransactionReport { get; set; }
        public List<RedemReportItem> RedemReport { get; set; }
        public List<RefundReportItem> RefundReport { get; set; }
    }

    public class TransactionReportItem
    {

        public string CardHolder { get; set; }

        public int SeriolNo { get; set; }

        public string CardNo { get; set; }

        public string DateOfTrans { get; set; }

        public string Store { get; set; }

        public long Merchant_ID { get; set; }

        public string Amount { get; set; }

        public string RewardPoint { get; set; }

        public string TotalPoints { get; set; }
    }

    public class RedemReportItem
    {

        public int SeriolNo { get; set; }

        public string CardHolder { get; set; }

        public string CardNo { get; set; }

        public string DateOfTrans { get; set; }

        public string Store { get; set; }

        public long Merchant_ID { get; set; }

        public string Amount { get; set; }

        public string RedemPoint { get; set; }

        public string TotalPoints { get; set; }
    }

    public class RefundReportItem
    {

        public string CardHolder { get; set; }

        public int SeriolNo { get; set; }

        public string CardNo { get; set; }

        public string DateOfTrans { get; set; }

        public string Store { get; set; }

        public long Merchant_ID { get; set; }

        public string Amount { get; set; }

        public string RefundPoint { get; set; }

        public string TotalPoints { get; set; }
    }
}