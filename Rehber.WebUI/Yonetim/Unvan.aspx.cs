using System;
using System.Configuration;
using System.Data.SqlClient;

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
                    cmd.CommandText = "SELECT * FROM Unvan";
                    gvUnvanlar.DataSource = cmd.ExecuteReader();
                    gvUnvanlar.DataBind();
                }
            }
        }
    }
}