using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json.Linq;
using System.Web.SessionState;
using LocalService;
using System.Globalization;
using System.Threading;

namespace WebReceipt.Pages.Receipt
{
    /// <summary>
    /// Summary description for ContentUpdate
    /// </summary>
    public class ContentUpdate : IHttpHandler, IReadOnlySessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            if (context.Session["Authentication"] == null)
                throw new Exception("Ошибка обновления");

            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

            int PositionId = Convert.ToInt32(context.Request["positionId"]);
            double Quant = Convert.ToDouble(context.Request["quant"]);
            double Discount = Convert.ToDouble(context.Request["discount"]);
            double Price;
            
            LocalDBService.ReceiptUpdate(
                PositionId, ref Quant, ref Discount, out Price);
            
            var JObject = new JObject(
                new JProperty("positionId", PositionId),
                new JProperty("quant",Quant),
                new JProperty("discount", Discount),
                new JProperty("price", Price));

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