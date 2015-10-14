using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using Newtonsoft.Json.Linq;
using System.Threading;
using System.Globalization;
using LocalService;

namespace WebReceipt.Pages.Receipt
{
    /// <summary>
    /// Summary description for ContentInsert
    /// </summary>
    public class ContentInsert : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            
            if (context.Session["Authentication"] == null)
                throw new Exception("Ошибка обновления");
             

            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            int StoreInfoId = Convert.ToInt32(context.Request["storeInfoId"]);

            int? ReceiptNumber = Convert.ToInt32(context.Session["ReceiptNumber"]);
            int ShopId = Convert.ToInt32(context.Session["ShopId"]);
            int UserId = Convert.ToInt32(context.Session["UserId"]);
            if (UserId == 0)
                throw new Exception("Ошибка обновления");
            int PositionId;
            int ArticleId;
            double Quant;
            double Discount;
            double Price;


            LocalDBService.ReceiptInsert(
                out PositionId, StoreInfoId, out ArticleId, out Quant,
                out Discount, out Price, ShopId, UserId, ref ReceiptNumber);

            context.Session.Add("ReceiptNumber", ReceiptNumber);
            var JObject = new JObject(
                new JProperty("positionId", PositionId),
                new JProperty("quant", Quant),
                new JProperty("discount", Discount),
                new JProperty("price", Price),
                new JProperty("articleId", ArticleId),
                new JProperty("storeInfoId", StoreInfoId),
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