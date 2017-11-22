﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Text;
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
        public void GetPersonels(string ad, string birim)
        {
            //Thread.Sleep(1000);
            var personeller = new List<Personel>();

            StringBuilder command = new StringBuilder();
            if (!string.IsNullOrEmpty(ad) && !string.IsNullOrEmpty(birim))
            {
                if (!string.IsNullOrEmpty(ad))
                {
                    command.Append($"SELECT TOP 30 * FROM Personel p" +
                                   $" INNER JOIN Birim b ON" +
                                   $" p.BirimId = b.BirimId" +
                                   $" WHERE (p.Ad LIKE '%{ad}%'" +
                                   $" OR p.Soyad LIKE '%{ad}%'" +
                                   $" OR p.Ad + ' ' + p.Soyad LIKE '%{ad}%'" +
                                   $" OR p.Telefon LIKE '%{ad}%')");
                }

                if (!string.IsNullOrEmpty(birim) && Convert.ToInt32(birim) != 0)
                {
                    command.Append($" AND p.BirimId = {birim}");
                }
            }
            else
            {
                command.Append("SELECT TOP 30 * FROM Personel p" +
                                   " INNER JOIN Birim b ON" +
                                   " p.BirimId = b.BirimId");

                if (!string.IsNullOrEmpty(birim))
                {
                    if (Convert.ToInt32(birim) != 0)
                    {
                        command.Append($" WHERE p.BirimId = {birim}");
                    }
                }
            }
            command.Append(" ORDER BY p.Ad ASC");

            using (SqlConnection con = new SqlConnection(_connString))
            {
                using (SqlCommand sqlCommand = new SqlCommand(command.ToString(), con))
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
                            personel.BirimAdi = reader["BirimAdi"].ToString();
                            personel.Web = reader["Web"].ToString();
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
