using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace Rehber.WebUI.Yonetim
{
    public partial class Unvanlar : System.Web.UI.Page
    {
        private readonly string _connString = ConfigurationManager.ConnectionStrings["PersonelDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                UnvanGridDoldur();
            }
        }

        protected void btnUnvanEkle_Click(object sender, EventArgs e)
        {
            string unvan = txtUnvan.Text.Trim();
            txtUnvan.Text = "";
            txtUnvan.Focus();

            if (!string.IsNullOrEmpty(unvan))
            {
                using (SqlConnection dbConnection = new SqlConnection(_connString))
                {
                    dbConnection.Open();
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO Unvan VALUES(@Unvan)", dbConnection))
                    {
                        cmd.Parameters.Add(new SqlParameter() {ParameterName = "@Unvan", Value = unvan});
                        cmd.ExecuteNonQuery();
                        UnvanGridDoldur();
                        Session.Add("UNVANEKLENDI", "Unvan başarıyla eklendi.");
                        Response.Redirect("Unvan.aspx");
                    }
                }
            }
        }

        protected void UnvanGridDoldur()
        {
            using (SqlConnection dbConnection = new SqlConnection(_connString))
            {
                dbConnection.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = dbConnection;
                    cmd.CommandText = "SELECT * FROM Unvan ORDER BY UnvanAdi ASC";
                    gvUnvanlar.DataSource = cmd.ExecuteReader();
                    gvUnvanlar.DataBind();
                }
            }
        }

        protected void gvUnvanlar_RowDeleting(object sender, System.Web.UI.WebControls.GridViewDeleteEventArgs e)
        {
            GridView gridView = sender as GridView;
            var unvanId = Convert.ToInt32(gridView.DataKeys[e.RowIndex].Values["UnvanId"]);

            using (SqlConnection dbConnection = new SqlConnection(_connString))
            {
                dbConnection.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = dbConnection;
                    command.CommandText = "DELETE FROM Unvan WHERE UnvanId = @UnvanId";
                    command.Parameters.AddWithValue("@UnvanId", unvanId);
                    try
                    {
                        command.ExecuteNonQuery();
                        Session.Add("UNVANSILINDI", "Unvan silindi.");
                    }
                    catch (SqlException ex)
                    {
                        if (ex.Message.Contains("FK_"))
                        {
                            string error = "alert('Unvan Personel tablosu tarafından kullanıldığı için silinemez.');";
                            if (!Page.ClientScript.IsStartupScriptRegistered("ErrorUnvan"))
                            {
                                ClientScript.RegisterStartupScript(GetType(), "ErrorUnvan", error, true);
                                return;
                            }
                        }
                    }
                    Response.Redirect("Unvan.aspx");
                }
            }
        }

        protected void gvUnvanlar_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvUnvanlar.EditIndex = e.NewEditIndex;
            UnvanGridDoldur();
        }

        protected void gvUnvanlar_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvUnvanlar.EditIndex = -1;
            UnvanGridDoldur();
        }

        protected void gvUnvanlar_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int unvanId = (int) gvUnvanlar.DataKeys[e.RowIndex].Value;
            TextBox txtUnvan = (TextBox) gvUnvanlar.Rows[e.RowIndex].FindControl("txtUnvan");

            using (SqlConnection dbCon = new SqlConnection(_connString))
            {
                dbCon.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = dbCon;
                    command.CommandText = "UPDATE Unvan SET UnvanAdi = @UnvanAdi WHERE UnvanId = @UnvanId";
                    command.Parameters.AddWithValue("@UnvanAdi", txtUnvan.Text.Trim());
                    command.Parameters.AddWithValue("@UnvanId", unvanId);
                    command.ExecuteNonQuery();
                    gvUnvanlar.EditIndex = -1;
                    UnvanGridDoldur();
                }
            }
        }
    }
}