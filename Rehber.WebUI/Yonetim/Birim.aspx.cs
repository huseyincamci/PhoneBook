using System;
using System.Configuration;
using System.Data.SqlClient;

namespace Rehber.WebUI.Yonetim
{
    public partial class Birim : System.Web.UI.Page
    {
        private readonly string _connString = ConfigurationManager.ConnectionStrings["PersonelDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BirimGridDoldur();
            }
        }

        protected void BirimGridDoldur()
        {
            using (SqlConnection dbConnection = new SqlConnection(_connString))
            {
                dbConnection.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = dbConnection;
                    cmd.CommandText = "SELECT * FROM Birim ORDER BY BirimAdi ASC";
                    gvBirimler.DataSource = cmd.ExecuteReader();
                    gvBirimler.DataBind();
                }
            }
        }

        protected void btnBirimEkle_Click(object sender, EventArgs e)
        {
            string birim = txtBirim.Text.Trim();

            if (!string.IsNullOrEmpty(birim))
            {
                using (SqlConnection dbConnection = new SqlConnection(_connString))
                {
                    dbConnection.Open();
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = dbConnection;
                        cmd.CommandText = "INSERT INTO Birim(BirimAdi) VALUES(@BirimAdi)";
                        cmd.Parameters.AddWithValue("@BirimAdi", birim);
                        cmd.ExecuteNonQuery();
                        BirimGridDoldur();
                        txtBirim.Text = "";
                        Session.Add("BIRIMEKLENDI", "Birim başarıyla eklendi.");
                        Response.Redirect("Birim.aspx");
                    }
                }
            }
        }
    }
}