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
                    cmd.CommandText = "SELECT * FROM Unvan ORDER BY UnvanId DESC";
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
                    command.ExecuteNonQuery();
                    Response.Redirect("Unvan.aspx");
                }
            }
        }

        protected void gvUnvanlar_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
        {
            if (e.Row.RowState != DataControlRowState.Edit)
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    LinkButton lb = (LinkButton)e.Row.Cells[1].Controls[0];
                    lb.Attributes.Add("onclick", "return ConfirmOnDelete();");
                    lb.Attributes.Add("class", "btn btn-danger btn-sm");
                }
            }
        }
    }
}