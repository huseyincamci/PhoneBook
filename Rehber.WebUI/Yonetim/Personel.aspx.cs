using System;
using System.Configuration;
using System.Data.SqlClient;

namespace Rehber.WebUI.Yonetim
{
    public partial class Personel : System.Web.UI.Page
    {
        private readonly string _connString = ConfigurationManager.ConnectionStrings["PersonelDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PersonelGridDoldur();
                BirimDrpDoldur();
            }
        }

        protected void PersonelGridDoldur()
        {
            using (SqlConnection dbConnection = new SqlConnection(_connString))
            {
                dbConnection.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = dbConnection;
                    cmd.CommandText = "SELECT * FROM Personel p " +
                                      "INNER JOIN Birim b " +
                                      "ON p.BirimId = b.BirimId" +
                                      " ORDER BY PersonelId DESC";
                    gvPersoneller.DataSource = cmd.ExecuteReader();
                    gvPersoneller.DataBind();
                }
            }
        }

        protected void BirimDrpDoldur()
        {
            using (SqlConnection dbConnection = new SqlConnection(_connString))
            {
                dbConnection.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = dbConnection;
                    cmd.CommandText = "SELECT * FROM Birim ORDER BY BirimAdi ASC";
                    drpBirim.DataSource = cmd.ExecuteReader();
                    drpBirim.DataTextField = "BirimAdi";
                    drpBirim.DataValueField = "BirimId";
                    drpBirim.DataBind();
                    drpBirim.Items.Insert(0, "--- Birim Seç ---");
                }
            }
        }

        protected void btnPersonelEkle_Click(object sender, EventArgs e)
        {
            string ad = txtAd.Text.Trim();
            string soyad = txtSoyad.Text.Trim();
            string telefon = txtTelefon.Text.Trim();
            string eposta = txtEposta.Text.Trim();
            string web = txtWeb.Text.Trim();
            int birimId = Convert.ToInt32(drpBirim.SelectedValue);

            using (SqlConnection con = new SqlConnection(_connString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "INSERT INTO Personel" +
                                      "(Ad, Soyad, Telefon, Eposta, Web, Resim, BirimId) " +
                                      "VALUES(@Ad, @Soyad, @Telefon, @Eposta, @Web, @Resim, @BirimId)";
                    cmd.Parameters.AddWithValue("@Ad", ad);
                    cmd.Parameters.AddWithValue("@Soyad", soyad);
                    cmd.Parameters.AddWithValue("@Telefon", telefon);
                    cmd.Parameters.AddWithValue("@Eposta", eposta);
                    cmd.Parameters.AddWithValue("@Web", web);
                    cmd.Parameters.AddWithValue("@BirimId", birimId);
                    if (fuFotograf.HasFile)
                    {
                        string dosyaAdi = fuFotograf.FileName;
                        fuFotograf.SaveAs(Server.MapPath($"/Images/{dosyaAdi}"));
                        cmd.Parameters.AddWithValue("@Resim", $"/Images/{dosyaAdi}");
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@Resim", "/Images/noPhoto.png");
                    }
                    cmd.ExecuteNonQuery();
                    PersonelGridDoldur();
                }
            }
        }
    }
}