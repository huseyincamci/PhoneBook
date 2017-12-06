using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;

namespace Rehber.WebUI.Yonetim
{
    public partial class Personel : System.Web.UI.Page
    {
        private readonly string _connString = ConfigurationManager.ConnectionStrings["PersonelDB"].ConnectionString;
        private readonly string _uploads = ConfigurationManager.AppSettings["UPLOADS"];

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PersonelGridDoldur();
                BirimDrpDoldur();
                UnvanDrpDoldur();
            }

            if (Request.QueryString["kisi"] != null)
            {
                AramaYap(Request.QueryString["kisi"]);
            }
        }

        protected void PersonelGridDoldur()
        {
            using (SqlConnection dbConnection = new SqlConnection(_connString))
            {
                var commandText = "SELECT TOP 70 * FROM Personel p " +
                                      "INNER JOIN Birim b " +
                                      "ON p.BirimId = b.BirimId " +
                                      "LEFT JOIN Unvan u " +
                                      "ON p.UnvanId = u.UnvanId" +
                                      " ORDER BY PersonelId DESC";
                using (SqlDataAdapter da = new SqlDataAdapter(commandText, dbConnection))
                {
                    DataTable personeller = new DataTable();
                    da.Fill(personeller);
                    gvPersoneller.DataSource = personeller;
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


        protected void UnvanDrpDoldur()
        {
            using (SqlConnection dbConnection = new SqlConnection(_connString))
            {
                dbConnection.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = dbConnection;
                    cmd.CommandText = "SELECT * FROM Unvan ORDER BY UnvanAdi ASC";
                    drpUnvan.DataSource = cmd.ExecuteReader();
                    drpUnvan.DataTextField = "UnvanAdi";
                    drpUnvan.DataValueField = "UnvanId";
                    drpUnvan.DataBind();
                    drpUnvan.Items.Insert(0, new ListItem("--- Unvan Seç ---", "0"));
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
            int unvanId = Convert.ToInt32(drpUnvan.SelectedValue);
            string sicilNo = drpSicilEk.SelectedValue + '-' + txtSicil.Text.Trim();

            using (SqlConnection dbCon = new SqlConnection(_connString))
            {
                dbCon.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = dbCon;
                    command.CommandText = "SELECT * FROM Personel WHERE SicilNo = @SicilNo AND UnvanId = @UnvanId AND BirimId = @BirimId";
                    command.Parameters.AddWithValue("@SicilNo", sicilNo);
                    command.Parameters.AddWithValue("@UnvanId", unvanId);
                    command.Parameters.AddWithValue("@BirimId", birimId);
                    var personel = command.ExecuteScalar();
                    if (personel != null)
                    {
                        Session.Add("HATA", "Sicil No, Unvan ve Birim bilgileri ile daha önce kayıt yapılmıştır.");
                        Response.Redirect("Personel.aspx");
                        return;
                    }
                }
            }

            using (SqlConnection con = new SqlConnection(_connString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "INSERT INTO Personel" +
                                      "(SicilNo, Ad, Soyad, Telefon, Eposta, Web, Resim, BirimId, UnvanId) " +
                                      "VALUES(@SicilNo, @Ad, @Soyad, @Telefon, @Eposta, @Web, @Resim, @BirimId, @UnvanId)";
                    cmd.Parameters.AddWithValue("@SicilNo", sicilNo);
                    cmd.Parameters.AddWithValue("@Ad", ad);
                    cmd.Parameters.AddWithValue("@Soyad", soyad);
                    cmd.Parameters.AddWithValue("@Telefon", telefon);
                    cmd.Parameters.AddWithValue("@Eposta", eposta);
                    cmd.Parameters.AddWithValue("@Web", web);
                    cmd.Parameters.AddWithValue("@BirimId", birimId);
                    cmd.Parameters.AddWithValue("@UnvanId", unvanId);
                    if (fuFotograf.HasFile)
                    {
                        string dosyaAdi = fuFotograf.FileName;
                        if (fuFotograf.PostedFile.ContentLength > 4000000)
                        {
                            lblMaxBoyut.Text = "Dosya boyutu max 4MB olabilir.";
                            return;
                        }
                        string directory = $"{_uploads}/{sicilNo}";
                        Directory.CreateDirectory(Server.MapPath(directory));
                        fuFotograf.SaveAs(Server.MapPath($"{_uploads}/{sicilNo}/{dosyaAdi}"));
                        cmd.Parameters.AddWithValue("@Resim", $"{_uploads}/{sicilNo}/{dosyaAdi}");
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@Resim", "/Images/profile.gif");
                    }
                    cmd.ExecuteNonQuery();
                    PersonelGridDoldur();

                    txtAd.Text = "";
                    txtSoyad.Text = "";
                    txtEposta.Text = "";
                    txtTelefon.Text = "";
                    txtWeb.Text = "";
                    drpBirim.SelectedIndex = 0;

                    Session.Add("BASARILI", "Personel başarıyla eklendi.");
                    Response.Redirect("Personel.aspx");
                }
            }
        }

        protected void gvPersoneller_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridView personelGridView = sender as GridView;
            int id = Convert.ToInt32(personelGridView.SelectedDataKey.Values["PersonelId"]);

            using (SqlConnection dbConnection = new SqlConnection(_connString))
            {
                dbConnection.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = dbConnection;
                    cmd.CommandText = "SELECT TOP 1 * FROM Personel p " +
                                      "INNER JOIN Birim b " +
                                      "ON p.BirimId = b.BirimId " +
                                      "LEFT JOIN Unvan u ON " +
                                      "p.UnvanId = u.UnvanId " +
                                      "WHERE p.PersonelId = @Id " +
                                      "ORDER BY PersonelId DESC";
                    cmd.Parameters.AddWithValue("@Id", id);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        drpSicilEk.SelectedValue = reader["SicilNo"].ToString().Split('-')[0];
                        txtSicil.Text = reader["SicilNo"].ToString().Split('-')[1];
                        txtAd.Text = reader["Ad"].ToString();
                        txtSoyad.Text = reader["Soyad"].ToString();
                        txtEposta.Text = reader["Eposta"].ToString();
                        txtTelefon.Text = reader["Telefon"].ToString();
                        txtWeb.Text = reader["Web"].ToString();
                        drpBirim.SelectedValue = reader["BirimId"].ToString();
                        drpUnvan.SelectedValue = reader["UnvanId"]?.ToString();
                        hfFileName.Value = reader["Resim"].ToString();
                        hfPersonelId.Value = reader["PersonelId"].ToString();
                    }
                    drpSicilEk.Enabled = false;
                    txtSicil.Enabled = false;
                }
            }
            btnPersonelDuzenle.Enabled = true;
            btnPersonelIptal.Enabled = true;
            btnPersonelEkle.Enabled = false;
        }

        protected void btnKisiAra_Click(object sender, EventArgs e)
        {
            string kisi = txtKisiAra.Text.Trim();
            Response.Redirect($"Personel.aspx?kisi={kisi}");
        }

        private void AramaYap(string kisi)
        {
            using (SqlConnection dbConnection = new SqlConnection(_connString))
            {
                var commandText = "SELECT TOP 70 * FROM Personel p" +
                                  " INNER JOIN Birim b ON" +
                                  " p.BirimId = b.BirimId" +
                                  " LEFT JOIN Unvan u" +
                                  " ON p.UnvanId = u.UnvanId" +
                                  " WHERE (p.Ad LIKE '%'+@Kisi+'%'" +
                                  " OR p.Soyad LIKE '%'+@Kisi+'%'" +
                                  " OR p.Ad + ' ' + p.Soyad LIKE '%'+@Kisi+'%'" +
                                  " OR p.Telefon LIKE '%'+@Kisi+'%')";
                using (SqlDataAdapter da = new SqlDataAdapter(commandText, dbConnection))
                {
                    da.SelectCommand.Parameters.AddWithValue("@Kisi", kisi);
                    DataTable personeller = new DataTable();
                    da.Fill(personeller);
                    gvPersoneller.DataSource = personeller;
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
            int unvanId = Convert.ToInt32(drpUnvan.SelectedValue);
            string sicilNo = drpSicilEk.SelectedValue + '-' + txtSicil.Text.Trim();

            using (SqlConnection con = new SqlConnection(_connString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "UPDATE Personel " +
                                      "SET SicilNo = @SicilNo, Ad = @Ad, Soyad = @Soyad, Telefon = @Telefon, Eposta = @Eposta, Web = @Web, Resim = @Resim, BirimId = @BirimId, UnvanId = @UnvanId " +
                                      "WHERE PersonelId = @PersonelId";
                    cmd.Parameters.AddWithValue("@SicilNo", sicilNo);
                    cmd.Parameters.AddWithValue("@Ad", ad);
                    cmd.Parameters.AddWithValue("@Soyad", soyad);
                    cmd.Parameters.AddWithValue("@Telefon", telefon);
                    cmd.Parameters.AddWithValue("@Eposta", eposta);
                    cmd.Parameters.AddWithValue("@Web", web);
                    cmd.Parameters.AddWithValue("@BirimId", birimId);
                    cmd.Parameters.AddWithValue("@UnvanId", unvanId);
                    cmd.Parameters.AddWithValue("@PersonelId", hfPersonelId.Value);
                    if (fuFotograf.HasFile)
                    {
                        if (fuFotograf.PostedFile.ContentLength > 2000000)
                        {
                            lblMaxBoyut.Text = "Dosya boyutu max 2MB olabilir.";
                            return;
                        }
                        string dosyaAdi = fuFotograf.FileName;
                        string directory = $"{_uploads}/{sicilNo}";
                        Directory.CreateDirectory(Server.MapPath(directory));
                        fuFotograf.SaveAs(Server.MapPath($"{_uploads}/{sicilNo}/{dosyaAdi}"));
                        cmd.Parameters.AddWithValue("@Resim", $"{_uploads}/{sicilNo}/{dosyaAdi}");
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@Resim", $"{hfFileName.Value}");
                    }
                    cmd.ExecuteNonQuery();
                    PersonelGridDoldur();

                    txtAd.Text = "";
                    txtSoyad.Text = "";
                    txtEposta.Text = "";
                    txtTelefon.Text = "";
                    txtWeb.Text = "";
                    drpBirim.SelectedIndex = 0;

                    Session.Add("GUNCEL", "Personel başarıyla güncenllendi.");
                    Response.Redirect("Personel.aspx");
                }
            }
        }

        protected void gvPersoneller_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridView gridView = sender as GridView;
            var personelId = Convert.ToInt32(gridView.DataKeys[e.RowIndex].Values["PersonelId"]);
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

                    Regex reg = new Regex("/Images/profile.gif");
                    if (!reg.IsMatch(resim))
                    {
                        if (File.Exists(Server.MapPath(resim)))
                        {
                            File.Delete(Server.MapPath(resim));
                        }
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
                    Response.Redirect("Personel.aspx");
                }
            }
        }

        protected void gvPersoneller_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowState != DataControlRowState.Edit)
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    string adSoyad = e.Row.Cells[3].Text + " " + e.Row.Cells[4].Text;
                    LinkButton lb = (LinkButton)e.Row.Cells[0].Controls[0];
                    LinkButton lbSelect = (LinkButton)e.Row.Cells[0].Controls[2];
                    lb.Attributes.Add("onclick", "return ConfirmOnDelete('" + adSoyad + "');");
                    lb.Attributes.Add("class", "btn btn-danger btn-sm");
                    lbSelect.Attributes.Add("class", "btn btn-primary btn-sm");
                }
            }
        }

        protected void btnPersonelIptal_Click(object sender, EventArgs e)
        {
            Response.Redirect("Personel.aspx");
        }

        protected void gvPersoneller_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvPersoneller.PageIndex = e.NewPageIndex;
            if (Request.QueryString["kisi"] != null)
            {
                AramaYap(Request.QueryString["kisi"]);
            }
            else
            {
                PersonelGridDoldur();
            }
        }
    }
}