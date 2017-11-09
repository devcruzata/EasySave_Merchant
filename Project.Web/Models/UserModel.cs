using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.Web.Models
{
    public class UserModel
    {
        public long User_ID_Auto_Pk { get; set; }

        public long Relation_ID_Fk { get; set; }

        public string UserName { get; set; }

        public string FullName { get; set; }

        public string Password { get; set; }

        public string UserType { get; set; }

        public string UserProgramType { get; set; }

        

        
    }
}