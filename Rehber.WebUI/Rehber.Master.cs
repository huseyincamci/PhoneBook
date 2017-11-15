using System;
using System.Configuration;

namespace Rehber.WebUI
{
    public partial class Rehber : System.Web.UI.MasterPage
    {
        private readonly string _connString = ConfigurationManager.ConnectionStrings["PersonelDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}