using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace Rehber.WebUI
{
    public partial class Default : System.Web.UI.Page
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
                    drpBirimler.DataSource = cmd.ExecuteReader();
                    drpBirimler.DataTextField = "BirimAdi";
                    drpBirimler.DataValueField = "BirimId";
                    drpBirimler.DataBind();
                    ListItem item = new ListItem("--- Tüm Birimler ---", "0");
                    drpBirimler.Items.Insert(0, item);
                }
            }
        }
    }
}