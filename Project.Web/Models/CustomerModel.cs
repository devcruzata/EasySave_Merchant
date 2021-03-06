﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.Web.Models
{
    public class CustomerModel
    {
        public long Customer_ID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string DOB { get; set; }

        public string DOA { get; set; }

        public string Email { get; set; }

        public string Mobile { get; set; }

        public string National_ID { get; set; }

        public string CardNo { get; set; }

        public string Status { get; set; }

        public string DateGenreated { get; set; }

        public string MyPoints { get; set; }

        public string AddressLine1 { get; set; }

        public string AddressLine2 { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Countary { get; set; }

        public string Zipcode { get; set; }

        public string Salutation { get; set; }

        public string Gender { get; set; }

        public List<Project.Entity.Customer> customer { get; set; }
    }
}