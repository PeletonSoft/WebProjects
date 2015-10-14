using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Notify
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["Key"] != "12456")
            {
                Response.Redirect("../Error/404.html");
                return;
            }

            Session.Add("Key", true);

            try
            {
                if (Convert.ToInt32(Request["ShopId"]) > 0)
                    Session.Add("ShopId", Convert.ToInt32(Request["ShopId"]));
            }
            catch
            {
                Response.Redirect("../Error/405.html");
                return;
            }

            try
            {
                if (Convert.ToInt32(Request["UserId"]) > 0)
                    Session.Add("UserId", Convert.ToInt32(Request["UserId"]));
            }
            catch 
            {
                Response.Redirect("../Error/404.html");
                return;
            }

            /*
            var userId = Convert.ToInt32(Request["UserId"]);
            var shopId = Convert.ToInt32(Request["ShopId"]);
            var dc = new ShopDataClassesDataContext();
            int? port = null;
            dc.Оповещение_аренда(userId, shopId, ref port, 1);
            if(port != null && port > 0)
            {
                Session.Add("Port", port);                
            }
            else
            {
                Response.Redirect("../Error/40.html");
                return;
            }
            */
        }
    }
}
