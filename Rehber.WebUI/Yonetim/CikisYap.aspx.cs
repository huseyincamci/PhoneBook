using System;
using System.Web;
using System.Web.Security;

namespace Rehber.WebUI.Yonetim
{
    public partial class CikisYap : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (HttpContext.Current.User.Identity.IsAuthenticated)
                FormsAuthentication.SignOut();
            Response.Redirect("/GirisYap.aspx");
        }
    }
}