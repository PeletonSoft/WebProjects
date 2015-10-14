using System;

using System.Web;
using LocalService;
using System.Web.SessionState;
using Newtonsoft.Json.Linq;

namespace WebReceipt.Pages.Receipt
{
    /// <summary>
    /// Summary description for ContentSend
    /// </summary>
    public class Revise : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            if (context.Session["ShopId"] == null)
                throw new Exception("Ошибка обновления");

            int ShopId = Convert.ToInt32(context.Session["ShopId"]);
            int ArticleId = Convert.ToInt32(context.Request["ArticleId"]);
            int Price = Convert.ToInt32(context.Request["Price"]);

            var Error = LocalDBService.Revise(ShopId, ArticleId, Price);

            var JObject = new JObject(
                new JProperty("error", Error));

            context.Response.ContentType = "application/json";
            context.Response.Write(JObject.ToString());


        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}