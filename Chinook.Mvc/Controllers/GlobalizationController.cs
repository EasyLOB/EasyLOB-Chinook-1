using System;
using System.Web;
using System.Web.Mvc;

namespace EasyLOB.Library.Mvc
{
    public class GlobalizationController : Controller
    {
        #region Methods

        public void Culture(string language, string locale)
        {
            // en-US
            // pt-BR
            string culture = String.Format("{0}-{1}", language, locale);

            HttpCookie cookie = Request.Cookies["ZCulture"];
            if (cookie != null)
            {
                cookie.Value = culture;
            }
            else
            {
                cookie = new HttpCookie("ZCulture");
                cookie.Value = culture;
                cookie.Expires = DateTime.Now.AddYears(1);
            }
            Response.Cookies.Add(cookie);

            Response.Redirect(Url.Content("~/Home"));
        }

        #endregion Methods
    }
}