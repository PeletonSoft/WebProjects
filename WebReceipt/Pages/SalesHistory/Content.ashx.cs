using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using Newtonsoft.Json.Linq;
using WebReceipt.DataSets;

namespace WebReceipt.Pages.SalesHistory
{
    /// <summary>
    /// Summary description for Content
    /// </summary>
    public class Content : IHttpHandler, IReadOnlySessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            if (context.Session["Authentication"] == null)
                throw new Exception("Ошибка обновления");

            int ShopId = Convert.ToInt32(context.Session["ShopId"]);
            int ArticleId = Convert.ToInt32(context.Request["ArticleId"]);

            var dsStatistic = new dsStatistic();
            (new DataSets.dsStatisticTableAdapters.taSalesHistory()).Fill
                (dsStatistic.tbSalesHistory, ShopId, ArticleId);

            var JHistory = new JArray();
            foreach (dsStatistic.tbSalesHistoryRow rw in dsStatistic.tbSalesHistory)
                JHistory.Add(new JObject(
                    new JProperty("Дата_продажи",rw.Дата_продажи),
                    new JProperty("Номер_чека", rw.Номер_чека),
                    new JProperty("Количество", rw.Количество),
                    new JProperty("ПрИнфо", rw.ПрИнфо),
                    new JProperty("Инфо", rw.Инфо)
                    ));

            var JObject = new JObject(
                new JProperty("Код_артикула", ArticleId),
                new JProperty("История", JHistory));

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