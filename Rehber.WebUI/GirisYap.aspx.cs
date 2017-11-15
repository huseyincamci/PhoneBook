using System;
using System.Web;
using System.Web.Security;

namespace Rehber.WebUI
{
    public partial class GirisYap : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var isAuth = HttpContext.Current.User.Identity.IsAuthenticated;
            if (isAuth)
                Response.Redirect("/Yonetim/Default.aspx");
        }

        protected void btnGirisYap_Click(object sender, EventArgs e)
        {
            string kullaniciAdi = txtKullaniciAdi.Text.Trim();
            string sifre = txtSifre.Text.Trim();

            if (string.IsNullOrEmpty(kullaniciAdi) && string.IsNullOrEmpty(sifre))
                return;

            FormsAuthentication.SetAuthCookie(kullaniciAdi, cbBeniHatirla.Checked);
            Response.Redirect("Yonetim/Default.aspx");
        }
    }
}