using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace Rehber.WebUI.Yonetim
{
    public partial class Gorev : System.Web.UI.Page
    {
        private readonly string _connString = ConfigurationManager.ConnectionStrings["PersonelDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GorevDrpDoldur();
            }
        }


        protected void GorevDrpDoldur()
        {
            using (SqlConnection dbConnection = new SqlConnection(_connString))
            {
                dbConnection.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = dbConnection;
                    cmd.CommandText = "SELECT * FROM Gorev" +
                                      " ORDER BY GorevAdi DESC";
                    grvGorevler.DataSource = cmd.ExecuteReader();
                    grvGorevler.DataBind();
                }
            }
        }

        protected void btnGorevEkle_Click(object sender, EventArgs e)
        {
            string gorev = txtGorev.Text.Trim();

            if (!string.IsNullOrEmpty(gorev))
            {
                using (SqlConnection dbConnection = new SqlConnection(_connString))
                {
                    dbConnection.Open();
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = dbConnection;
                        cmd.CommandText = "INSERT INTO Gorev(GorevAdi) VALUES(@GorevAdi)";
                        cmd.Parameters.AddWithValue("@GorevAdi", gorev);
                        cmd.ExecuteNonQuery();
                        GorevDrpDoldur();
                        txtGorev.Text = "";
                        Session.Add("GOREVEKLENDI", "Görev başarıyla eklendi.");
                        Response.Redirect("Gorev.aspx");
                    }
                }
            }
        }

        protected void grvGorevler_RowDeleting(object sender, System.Web.UI.WebControls.GridViewDeleteEventArgs e)
        {
            GridView gridView = sender as GridView;
            gridView.SelectedIndex = e.RowIndex;
            var gorevId = Convert.ToInt32(gridView.SelectedRow.Cells[1].Text);

            using (SqlConnection dbConnection = new SqlConnection(_connString))
            {
                dbConnection.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = dbConnection;
                    command.CommandText = "DELETE FROM Gorev WHERE GorevId = @GorevId";
                    command.Parameters.AddWithValue("@GorevId", gorevId);
                    command.ExecuteNonQuery();
                    Response.Redirect("Gorev.aspx");
                }
            }
        }

        protected void grvGorevler_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
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