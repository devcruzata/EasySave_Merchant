using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;


namespace EasySave_API.Structure.MailHelper
{
    public class MailBodyBuilder
    {
        public static string PopulateBody(string email,string password ,string templatePath)
        {
            string body = string.Empty;
            using (StreamReader reader = new StreamReader(HostingEnvironment.MapPath(templatePath)))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{Email}", email);
            body = body.Replace("{password}", password);           
            return body;
        }
    }
}