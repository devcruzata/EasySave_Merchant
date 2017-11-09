using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.Web.Models
{
    public class DashboardModel
    {
        public string TotalCustomer { get; set; }

        public string TotalCardHoders { get; set; }

        public string TotalTransactions { get; set; }

        public string TotalRevenue { get; set; }

        public string TotalEsAdminDue { get; set; }

        public string TotaEsCustDue { get; set; }

        public string TotalDue { get; set; }

        public List<Project.Entity.Customer> customer { get; set; }

        public DashboardModel()
        {
            customer = new List<Entity.Customer>();
        }
    }
}