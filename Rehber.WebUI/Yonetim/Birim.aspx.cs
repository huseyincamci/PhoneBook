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

        protected void gvBirimler_RowDeleting(object sender, System.Web.UI.WebControls.GridViewDeleteEventArgs e)
        {
            GridView gridView = sender as GridView;
            gridView.SelectedIndex = e.RowIndex;
            var birimId = Convert.ToInt32(gridView.SelectedRow.Cells[1].Text);

            using (SqlConnection dbConnection = new SqlConnection(_connString))
            {
                dbConnection.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = dbConnection;
                    command.CommandText = "DELETE FROM Birim WHERE BirimId = @BirimId";
                    command.Parameters.AddWithValue("@BirimId", birimId);
                    command.ExecuteNonQuery();
                    Response.Redirect("Birim.aspx");
                }
            }
        }

        protected void gvBirimler_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
        {
            if (e.Row.RowState != DataControlRowState.Edit)
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    LinkButton lb = (LinkButton)e.Row.Cells[0].Controls[0];
                    lb.Attributes.Add("onclick", "return ConfirmOnDelete();");
                }
            }
        }
    }
}