using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using Newtonsoft.Json.Linq;
using WebReceipt.DataSets;

namespace WebReceipt.Pages.Receipt
{
    public class DescriptionByArticleId : IHttpHandler, IReadOnlySessionState
    {

        #region IHttpHandler Members

        public bool IsReusable
        {
            // Return false in case your Managed Handler cannot be reused for another request.
            // Usually this would be false in case you have some state information preserved per request.
            get { return true; }
        }

        public void ProcessRequest(HttpContext context)
        {
            if (context.Session["Authentication"] == null)
                throw new Exception("Ошибка обновления");

            var shopId = Convert.ToInt32(context.Session["ShopId"]);
            var articleId = Convert.ToInt32(context.Request["articleId"]);

            using (var dc = new ReceiptClassDataContext(LocalService.LocalDBService.GetConnectionString()))
            {
                var info = dc.Склад_подробноs
                    .Single(x => x.Код == articleId);

                var q = dc.Склад_характеристикиs
                    .Where(s => s.Код_артикула == articleId)
                    .OrderBy(s => s.Характеристики.Название)
                    .Select(
                        s => new
                        {
                            Характеристика = s.Характеристики.Название,
                            Значение = dc.Характеристики_получить_строку(s.Код_характеристики, s.Код_значения)
                        })
                    .ToList()
                    .Select(x => new JProperty(x.Характеристика, x.Значение));

                var list =
                    new[]
                        {
                            new JProperty("Артикул", info.Артикул),
                            new JProperty("Описание", info.Описание),
                            new JProperty("Вид товара", info.Вид_товара),
                            new JProperty("Производитель", info.Производитель)
                            //new JProperty("Поставщик", info.Поставщик)
                        };

                var jObject = new JObject(list.Union(q));
                context.Response.ContentType = "application/json";
                context.Response.Write(jObject.ToString());
            }
        }

        #endregion
    }

}