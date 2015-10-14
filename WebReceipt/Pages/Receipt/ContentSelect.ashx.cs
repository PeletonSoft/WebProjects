using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using Newtonsoft.Json.Linq;
using WebReceipt.DataSets;

namespace WebReceipt.Pages.Select
{
    /// <summary>
    /// Summary description for Content
    /// </summary>
    public class ContentSelect : IHttpHandler, IReadOnlySessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            if (context.Session["Authentication"] == null)
                throw new Exception("Ошибка обновления");

            int ArticleId = Convert.ToInt32(context.Request["articleId"]);
            int ShopId = Convert.ToInt32(context.Session["ShopId"]);

            var dsReceipt = new dsReceipt();
            (new DataSets.dsReceiptTableAdapters.taSelect()).Fill
                (dsReceipt.tbSelect, ShopId, ArticleId);

            var JContent = new JArray();
            foreach (dsReceipt.tbSelectRow rw in dsReceipt.tbSelect)
                JContent.Add(new JObject(
                    new JProperty("Код_размещения", rw.Код_размещения),
                    new JProperty("ПрИнфо", rw.ПрИнфо),
                    new JProperty("Инфо", rw.Инфо),
                    new JProperty("Аббревиатура", rw.Аббревиатура),
                    new JProperty("Количество", rw.Количество)));

            var JObject = new JObject(
                new JProperty("Код_артикула", ArticleId),
                new JProperty("Позиции", JContent));

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