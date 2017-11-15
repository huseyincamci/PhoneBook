using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Rehber.WebUI
{
    public partial class KayitListele : System.Web.UI.UserControl
    {
        private readonly string _connString = ConfigurationManager.ConnectionStrings["PersonelDB"].ConnectionString;

        public object VeriKaynagi
        {
            set
            {

                Bulunan_Kayitlar.DataSource = value;
                Bulunan_Kayitlar.DataBind();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void Bulunan_Kayitlar_PageIndexChanging(object sender, System.Web.UI.WebControls.GridViewPageEventArgs e)
        {
            Bulunan_Kayitlar.PageIndex = e.NewPageIndex;
            if (Request.QueryString["aranacak_kelime"] != null)
            {
                var kelime = Request.QueryString["aranacak_kelime"];
                FillGrid($"SELECT * FROM Personel WHERE Ad LIKE '%{kelime}%'");
            }
            else
            {
                FillGrid($"SELECT * FROM Personel");
            }
        }

        protected void FillGrid(string sqlCommand)
        {
            using (SqlDataAdapter da = new SqlDataAdapter(sqlCommand, _connString))
            {
                DataTable personeller = new DataTable();
                da.Fill(personeller);

                VeriKaynagi = personeller;
            }
        }
    }
}