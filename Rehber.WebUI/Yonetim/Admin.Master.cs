using System;
using System.Web;

namespace Rehber.WebUI.Yonetim
{
    public partial class Admin : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var isAuth = HttpContext.Current.User.Identity.IsAuthenticated;
            if (!isAuth)
            {
                Response.Redirect("~/GirisYap.aspx");
            }
        }
    }
}