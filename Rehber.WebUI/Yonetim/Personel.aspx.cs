using System;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI.WebControls;

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
                    drpBirim.Items.Insert(0, new ListItem("--- Birim Seç ---", "0"));
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
                        if (fuFotograf.PostedFile.ContentLength > 2000000)
                        {
                            lblMaxBoyut.Text = "Dosya boyutu max 2MB olabilir.";
                            return;
                        }
                        fuFotograf.SaveAs(Server.MapPath($"/Images/{dosyaAdi}"));
                        cmd.Parameters.AddWithValue("@Resim", $"/Images/{dosyaAdi}");
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@Resim", "/Images/noPhoto.png");
                    }
                    cmd.ExecuteNonQuery();
                    PersonelGridDoldur();

                    txtAd.Text = "";
                    txtSoyad.Text = "";
                    txtEposta.Text = "";
                    txtTelefon.Text = "";
                    txtWeb.Text = "";
                    drpBirim.SelectedIndex = 0;
                }
            }
        }

        protected void gvPersoneller_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridView personelGridView = sender as GridView;
            int id = Convert.ToInt32(personelGridView?.SelectedRow.Cells[2].Text);

            using (SqlConnection dbConnection = new SqlConnection(_connString))
            {
                dbConnection.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = dbConnection;
                    cmd.CommandText = "SELECT * FROM Personel p " +
                                      "INNER JOIN Birim b " +
                                      "ON p.BirimId = b.BirimId" +
                                      $" WHERE p.PersonelId = {id}" +
                                      " ORDER BY PersonelId DESC";
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        txtAd.Text = reader["Ad"].ToString();
                        txtSoyad.Text = reader["Soyad"].ToString();
                        txtEposta.Text = reader["Eposta"].ToString();
                        txtTelefon.Text = reader["Telefon"].ToString();
                        txtWeb.Text = reader["Web"].ToString();
                        drpBirim.SelectedValue = reader["BirimId"].ToString();
                        hfFileName.Value = reader["Resim"].ToString();
                        hfPersonelId.Value = reader["PersonelId"].ToString();
                    }
                }
            }
            btnPersonelDuzenle.Enabled = true;
            btnPersonelEkle.Enabled = false;
        }

        protected void btnKisiAra_Click(object sender, EventArgs e)
        {
            string kisi = txtKisiAra.Text.Trim();
            using (SqlConnection dbConnection = new SqlConnection(_connString))
            {
                dbConnection.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = dbConnection;
                    command.CommandText = $"SELECT TOP 30 * FROM Personel p" +
                                          $" INNER JOIN Birim b ON" +
                                          $" p.BirimId = b.BirimId" +
                                          $" WHERE (p.Ad LIKE '%{kisi}%'" +
                                          $" OR p.Soyad LIKE '%{kisi}%'" +
                                          $" OR p.Ad + ' ' + p.Soyad LIKE '%{kisi}%'" +
                                          $" OR p.Telefon LIKE '%{kisi}%')";
                    gvPersoneller.DataSource = command.ExecuteReader();
                    gvPersoneller.DataBind();
                }
            }
        }

        protected void btnPersonelDuzenle_Click(object sender, EventArgs e)
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
                    cmd.CommandText = "UPDATE Personel " +
                                      "SET Ad = @Ad, Soyad = @Soyad, Telefon = @Telefon, Eposta = @Eposta, Web = @Web, Resim = @Resim, BirimId = @BirimId " +
                                      $"WHERE PersonelId = {hfPersonelId.Value}";
                    cmd.Parameters.AddWithValue("@Ad", ad);
                    cmd.Parameters.AddWithValue("@Soyad", soyad);
                    cmd.Parameters.AddWithValue("@Telefon", telefon);
                    cmd.Parameters.AddWithValue("@Eposta", eposta);
                    cmd.Parameters.AddWithValue("@Web", web);
                    cmd.Parameters.AddWithValue("@BirimId", birimId);
                    if (fuFotograf.HasFile)
                    {
                        if (fuFotograf.PostedFile.ContentLength > 2000000)
                        {
                            lblMaxBoyut.Text = "Dosya boyutu max 2MB olabilir.";
                            return;
                        }
                        string dosyaAdi = fuFotograf.FileName;
                        fuFotograf.SaveAs(Server.MapPath($"/Images/{dosyaAdi}"));
                        cmd.Parameters.AddWithValue("@Resim", $"/Images/{dosyaAdi}");
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@Resim", $"{hfFileName.Value}");
                    }
                    cmd.ExecuteNonQuery();
                    PersonelGridDoldur();
                }
            }
        }

        protected void gvPersoneller_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridView gridView = sender as GridView;
            gridView.SelectedIndex = e.RowIndex;
            var personelId = Convert.ToInt32(gridView.SelectedRow.Cells[2].Text);
            using (SqlConnection dbConnection = new SqlConnection(_connString))
            {
                dbConnection.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = dbConnection;
                    command.CommandText = "SELECT Resim FROM Personel WHERE PersonelId = @Id";
                    command.Parameters.AddWithValue("@Id", personelId);
                    SqlDataReader reader = command.ExecuteReader();
                    string resim = string.Empty;
                    while (reader.Read())
                    {
                        resim = reader["Resim"].ToString();
                    }

                    if (File.Exists(Server.MapPath(resim)))
                    {
                        File.Delete(Server.MapPath(resim));
                    }
                }
            }
            using (SqlConnection dbConnection = new SqlConnection(_connString))
            {
                dbConnection.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = dbConnection;
                    command.CommandText = "DELETE FROM Personel WHERE PersonelId = @PersonelId";
                    command.Parameters.AddWithValue("@PersonelId", personelId);
                    command.ExecuteNonQuery();
                    PersonelGridDoldur();
                }
            }
        }
    }
}