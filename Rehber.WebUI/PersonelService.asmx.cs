using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Script.Serialization;
using System.Web.Services;

namespace Rehber.WebUI
{
    /// <summary>
    /// Summary description for PersonelService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class PersonelService : System.Web.Services.WebService
    {
        private readonly string _connString = ConfigurationManager.ConnectionStrings["PersonelDB"].ConnectionString;

        [WebMethod]
        public void GetPersonels(string ad)
        {
            //Thread.Sleep(1000);
            var personeller = new List<Personel>();
            var command = ad != null
                ? $"SELECT * FROM Personel WHERE Ad LIKE '%{ad}%'" +
                  $"OR Soyad LIKE '%{ad}%'" +
                  $"OR Ad + ' ' + Soyad LIKE '%{ad}%'" +
                  $"OR Telefon LIKE '%{ad}%'"
                : "SELECT * FROM Personel";

            using (SqlConnection con = new SqlConnection(_connString))
            {
                using (SqlCommand sqlCommand = new SqlCommand(command, con))
                {
                    con.Open();
                    using (SqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var personel = new Personel();
                            personel.Ad = reader["Ad"].ToString();
                            personel.Soyad = reader["Soyad"].ToString();
                            personel.Telefon = reader["Telefon"].ToString();
                            personel.Eposta = reader["Eposta"].ToString();
                            personel.Fotograf = reader["Resim"].ToString().Remove(0, 1);
                            personeller.Add(personel);
                        }
                    }
                }
            }
            JavaScriptSerializer js = new JavaScriptSerializer();
            Context.Response.Write(js.Serialize(personeller));
        }
    }
}
