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
                    cmd.CommandText = "SELECT * FROM Personel ORDER BY PersonelId DESC";
                    gvPersoneller.DataSource = cmd.ExecuteReader();
                    gvPersoneller.DataBind();
                }
            }
        }
    }
}