using System;
using System.Configuration;
using System.Data.SqlClient;

namespace Rehber.WebUI.Yonetim
{
    public partial class Default : System.Web.UI.Page
    {
        private readonly string _connString = ConfigurationManager.ConnectionStrings["PersonelDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                using (SqlConnection dbConnection = new SqlConnection(_connString))
                {
                    dbConnection.Open();
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = dbConnection;
                        command.CommandText = "SELECT" +
                                              "(SELECT COUNT(*) FROM Personel) AS PersonelSayisi," +
                                              "(SELECT COUNT(*) FROM Birim) AS BirimSayisi," +
                                              "(SELECT COUNT(*) FROM Unvan) AS UnvanSayisi";
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            lblPersonel.Text = reader["PersonelSayisi"].ToString();
                            lblBirim.Text = reader["BirimSayisi"].ToString();
                            lblUnvan.Text = reader["UnvanSayisi"].ToString();
                        }
                    }
                }
            }
        }
    }
}