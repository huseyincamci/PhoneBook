using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web;
using System.Web.Security;

namespace Rehber.WebUI
{
    public partial class GirisYap : System.Web.UI.Page
    {
        private readonly string _connString = ConfigurationManager.ConnectionStrings["PersonelDB"].ConnectionString;

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

            using (SqlConnection dbCon = new SqlConnection(_connString))
            {
                dbCon.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = dbCon;
                    command.CommandText = "SELECT YoneticiKullaniciAdi, YoneticiSifre FROM Yonetici WHERE YoneticiKullaniciAdi = @KullaniciAdi AND YoneticiSifre = @Sifre";
                    command.Parameters.AddWithValue("@KullaniciAdi", kullaniciAdi);
                    command.Parameters.AddWithValue("@Sifre", sifre);
                    var yonetici = command.ExecuteScalar();

                    if (yonetici != null)
                    {
                        FormsAuthentication.SetAuthCookie(kullaniciAdi, cbBeniHatirla.Checked);
                        Response.Redirect("Yonetim/Default.aspx");
                    }
                    else
                    {
                        Session.Add("GIRISBASARISIZ", "Kullanıcı Adı ya da Şife Geçersiz.");
                    }
                }
            }

        }
    }
}