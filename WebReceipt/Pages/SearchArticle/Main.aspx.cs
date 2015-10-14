using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebReceipt.Pages.SearchArticle
{
    public partial class SearchArticle : System.Web.UI.Page
    {
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (Request["Theme"] != null)
                Session.Add("Theme", Request["Theme"]);

            if (Session["Theme"] != null)
                try
                {
                    Theme = Session["Theme"].ToString();
                }
                catch { };
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Authentication"] == null)
            {
                Response.Redirect("../Error/404.html");
                return;
            }
        }
    }
}