using BAL.Customers;
using Project.Web.Common;
using Project.Web.Filters;
using Project.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.Web.Controllers.Customer
{
    public class CustomerController : Controller
    {
        CustomerManager objCustomerManager = new CustomerManager();
        SessionHelper session;
        //
        // GET: /Customer/

        [Authorize]
        [SessionTimeOut]
        public ActionResult CustomerHome()
        {
            session = new SessionHelper();
            CustomerModel objCustomer = new CustomerModel();
            objCustomer.customer = objCustomerManager.GetCustomers(Convert.ToInt64(session.UserSession.MerchantID));
            return View(objCustomer);
        }

    }
}
