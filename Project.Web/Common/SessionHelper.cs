using System.Web;

namespace Project.Web.Common
{
    public class SessionHelper
    {
        public string CurrentCulture
        {
            get
            {
                if (HttpContext.Current.Session["CurrentCulture"] == null)
                {
                    HttpContext.Current.Session["CurrentCulture"] = "en-US";
                }
                return (string)HttpContext.Current.Session["CurrentCulture"];
            }
            set
            {
                HttpContext.Current.Session["CurrentCulture"] = value;
            }
        }

        public UserSession UserSession
        {
            get
            {
                if (HttpContext.Current.Session["UserSession"] == null)
                {
                    return null;
                }
                return (UserSession)HttpContext.Current.Session["UserSession"];
            }
            set
            {
                HttpContext.Current.Session["UserSession"] = value;
            }
        }

        
    }
}