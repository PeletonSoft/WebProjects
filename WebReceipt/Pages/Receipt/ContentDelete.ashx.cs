using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading;
using System.Globalization;
using LocalService;
using Newtonsoft.Json.Linq;
using System.Web.SessionState;

namespace WebReceipt.Pages.Receipt
{
    /// <summary>
    /// Summary description for ContentDelete
    /// </summary>
    public class ContentDelete : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            if (context.Session["Authentication"] == null)
                throw new Exception("Ошибка обновления");

            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

            int PositionId = Convert.ToInt32(context.Request["positionId"]);

            int ReceiptNumber = LocalDBService.ReceiptDelete(PositionId);

            context.Session.Add("ReceiptNumber", ReceiptNumber);

            var JObject = new JObject(
                new JProperty("positionId", PositionId),
                new JProperty("receiptNumber", ReceiptNumber));

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