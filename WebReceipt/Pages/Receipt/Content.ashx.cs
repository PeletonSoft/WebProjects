using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json.Linq;
using WebReceipt.DataSets;
using System.Web.SessionState;

namespace WebReceipt.Pages.Receipt
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
            int UserId = Convert.ToInt32(context.Session["UserId"]);
            int ReceiptNumber = Convert.ToInt32(context.Session["ReceiptNumber"]);

            var JObject = new JObject();

            var dsReceipt = new dsReceipt();
            (new DataSets.dsReceiptTableAdapters.taReceipt()).Fill
                (dsReceipt.tbReceipt, ShopId, ReceiptNumber);

            var JReceipt = new JArray();
            foreach (dsReceipt.tbReceiptRow rw in dsReceipt.tbReceipt)
                JReceipt.Add(new JObject(
                    new JProperty("ID", rw.ID),
                    new JProperty("Код_размещения", rw.Код_размещения),
                    new JProperty("Код_артикула", rw.Код_артикула),
                    new JProperty("Количество", rw.Количество),
                    new JProperty("Скидка", rw.Скидка),
                    new JProperty("Цена_со_скидкой", rw.Цена_со_скидкой)));

            JObject.Add(new JProperty("Чек",JReceipt));
            JObject.Add(new JProperty("Номер_чека", ReceiptNumber));

            JObject.Add(new JProperty("Код_магазина", ShopId));
            JObject.Add(new JProperty("Код_сотрудника", UserId));

            JObject.Add(new JProperty("Магазин", context.Session["ShopName"].ToString()));
            JObject.Add(new JProperty("Сотрудник", context.Session["UserName"].ToString()));

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