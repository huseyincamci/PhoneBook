using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

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
                        cmd.Parameters.AddWithValue("@BirimAdi", birim.ToUpper());
                        cmd.ExecuteNonQuery();
                        BirimGridDoldur();
                        txtBirim.Text = "";
                        Session.Add("BIRIMEKLENDI", "Birim başarıyla eklendi.");
                        Response.Redirect("Birim.aspx");
                    }
                }
            }
        }

        protected void gvBirimler_RowDeleting(object sender, System.Web.UI.WebControls.GridViewDeleteEventArgs e)
        {
            GridView gridView = sender as GridView;
            var birimId = Convert.ToInt32(gridView.DataKeys[e.RowIndex].Values["BirimId"]);

            using (SqlConnection dbConnection = new SqlConnection(_connString))
            {
                dbConnection.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = dbConnection;
                    command.CommandText = "DELETE FROM Birim WHERE BirimId = @BirimId";
                    command.Parameters.AddWithValue("@BirimId", birimId);
                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        if (ex.Message.Contains("FK_"))
                        {
                            string error = "alert('Birim Personel tablosu tarafından kullanıldığı için silinemez.');";
                            if (!Page.ClientScript.IsStartupScriptRegistered("ErrorBirim"))
                            {
                                ClientScript.RegisterStartupScript(GetType(), "ErrorBirim", error, true);
                                return;
                            }
                        }
                    }
                    Response.Redirect("Birim.aspx");
                }
            }
        }

        protected void gvBirimler_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvBirimler.EditIndex = e.NewEditIndex;
            BirimGridDoldur();;
        }

        protected void gvBirimler_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int birimId = (int) gvBirimler.DataKeys[e.RowIndex].Value;
            TextBox txtBirim = (TextBox) gvBirimler.Rows[e.RowIndex].FindControl("txtBirim");

            using (SqlConnection dbCon = new SqlConnection(_connString))
            {
                dbCon.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = dbCon;
                    command.CommandText = "UPDATE Birim SET BirimAdi = @BirimAdi WHERE BirimId = @BirimId";
                    command.Parameters.AddWithValue("@BirimAdi", txtBirim.Text.Trim().ToUpper());
                    command.Parameters.AddWithValue("@BirimId", birimId);
                    command.ExecuteNonQuery();
                    gvBirimler.EditIndex = -1;
                    BirimGridDoldur();
                }
            }
        }

        protected void gvBirimler_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvBirimler.EditIndex = -1;
            BirimGridDoldur();
        }
    }
}