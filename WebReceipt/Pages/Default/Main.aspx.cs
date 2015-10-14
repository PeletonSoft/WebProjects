using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebReceipt.DataSets;
using System.Data;
using System.Data.SqlClient;
using LocalService;
using System.Configuration;

namespace WebReceipt
{
    public partial class Default : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["Key"] !=
                ConfigurationManager.AppSettings["SecurityKey"])
            {
                Response.Redirect("../Error/404.html");
                return;
            }

            Session.Add("Key", true);

            if (Request["Theme"] != null)
                Session.Add("Theme", Request["Theme"]);

            try
            {
                if (Convert.ToInt32(Request["ShopId"]) > 0)
                    Session.Add("ShopId", Convert.ToInt32(Request["ShopId"]));
            }
            catch { }

            try
            {
                if (Convert.ToInt32(Request["UserId"]) > 0)
                    Session.Add("UserId", Convert.ToInt32(Request["UserId"]));
            }
            catch { }

        }
    }
}