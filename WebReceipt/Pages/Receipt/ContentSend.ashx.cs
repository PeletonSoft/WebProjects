using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LocalService;
using System.Web.SessionState;
using Newtonsoft.Json.Linq;

namespace WebReceipt.Pages.Receipt
{
    /// <summary>
    /// Summary description for ContentSend
    /// </summary>
    public class ContentSend : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            if (context.Session["Authentication"] == null)
                throw new Exception("Ошибка обновления");

            int ShopId = Convert.ToInt32(context.Session["ShopId"]);
            int ReceiptNumber = Convert.ToInt32(context.Session["ReceiptNumber"]);
            int UserId = Convert.ToInt32(context.Request["UserId"]);
            string UserName = context.Session["UserName"].ToString();
            string Message;

            var Error = LocalDBService.ReceiptSend(ShopId, ReceiptNumber, UserName, out Message);
            if (!Error)
                context.Session.Add("ReceiptNumber",0);

            var JObject = new JObject(
                new JProperty("error", Error),
                new JProperty("message", Message));

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