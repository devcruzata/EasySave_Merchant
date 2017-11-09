using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.Web.Models
{
    public class MerchantModel
    {
        public long Merchant_ID { get; set; }

        public string OrganijationName { get; set; }

        public string OrganijationCode { get; set; }

        public string AddressLine1 { get; set; }

        public string AddressLine2 { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Countary { get; set; }

        public string Zipcode { get; set; }

        public string Merchant_Logo { get; set; }

        public string Merchant_Image { get; set; }

        public HttpPostedFileBase[] Logo { get; set; }

        public HttpPostedFileBase[] Image { get; set; }

        public string ProgramType { get; set; }

        public string ContactPerson { get; set; }

        public string Email { get; set; }

        public string Mobile { get; set; }

        public string Landline { get; set; }

        public string Website { get; set; }

        public string Status { get; set; }

        public string CardPrefix { get; set; }
    }
}