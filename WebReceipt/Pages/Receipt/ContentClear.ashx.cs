using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using Newtonsoft.Json.Linq;
using LocalService;

namespace WebReceipt.Pages.Receipt
{
    /// <summary>
    /// Summary description for ContentClear
    /// </summary>
    public class ContentClear : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            if (context.Session["Authentication"] == null)
                throw new Exception("Ошибка обновления");

            int ShopId = Convert.ToInt32(context.Session["ShopId"]);
            int ReceiptNumber = Convert.ToInt32(context.Session["ReceiptNumber"]);
            int UserId = Convert.ToInt32(context.Request["UserId"]);


            LocalDBService.ReceiptClear(ShopId, ReceiptNumber);

            context.Session.Add("ReceiptNumber",0);

            var JObject = new JObject(
                new JProperty("error", false));

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