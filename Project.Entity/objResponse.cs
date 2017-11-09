using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Project.Entity
{
    public class objResponse
    {
        private int errorcode;
        private string errormsg;
        private DataSet responsedata;
        private List<string> resposedata;

        public objResponse()
        {
            this.ResponseData = new DataSet();

        }

        public int ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public List<string> ResponseMessage { get; set; }
        public DataSet ResponseData { get; set; }
    }
}
