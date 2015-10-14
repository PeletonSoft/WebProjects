using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebReceipt.DataSets;
using System.Web.SessionState;
using Newtonsoft.Json.Linq;

namespace WebReceipt.Pages.SearchArticle
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
            string Filter = context.Request["filter"].ToString();

            var dsStatistic = new dsStatistic();
            (new DataSets.dsStatisticTableAdapters.taSearchArticle()).Fill
                (dsStatistic.tbSearchArticle, ShopId, Filter);

            var JList = new JArray();
            foreach (dsStatistic.tbSearchArticleRow rw in dsStatistic.tbSearchArticle)
                JList.Add(new JObject(
                    new JProperty("Код_артикула", rw.Код_артикула),
                    new JProperty("Количество", rw.Количество),
                    new JProperty("Описание", rw.Описание),
                    new JProperty("Артикул", rw.Артикул),
                    new JProperty("Склад", rw.Склад)
                    ));

            var JObject = new JObject(
                new JProperty("Список", JList));

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