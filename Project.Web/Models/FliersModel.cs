using Project.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.Web.Models
{
    public class FliersModel
    {
        public long Flier_ID { get; set; }

        public string FlierImage { get; set; }

        public string Status { get; set; }

        public List<Fliers> fliers { get; set; }
    }
}