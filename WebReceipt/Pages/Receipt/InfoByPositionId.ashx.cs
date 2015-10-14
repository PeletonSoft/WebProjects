using System;
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
    public class InfoByPositionId : IHttpHandler, IReadOnlySessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            if (context.Session["Authentication"] == null)
                throw new Exception("Ошибка обновления");

            int PositionId = Convert.ToInt32(context.Request["positionId"]);

            var dsReceipt = new dsReceipt();
            (new DataSets.dsReceiptTableAdapters.taInfo()).Fill
                (dsReceipt.tbInfo, PositionId);

            if (dsReceipt.tbInfo.Rows.Count <= 0)
                return;

            var InfoRow = (dsReceipt.tbInfoRow)dsReceipt.tbInfo.Rows[0];

            using (var dc = new ReceiptClassDataContext(LocalService.LocalDBService.GetConnectionString()))
            {
                var q = dc.Склад_характеристикиs
                    .Where(s => s.Код_артикула == InfoRow.Код_артикула)
                    .OrderBy(s => s.Характеристики.Название)
                    .Select(
                        s => new
                                 {
                                     Характеристика = s.Характеристики.Название,
                                     Значение = dc.Характеристики_получить_строку(s.Код_характеристики, s.Код_значения)
                                 })
                    .ToList()
                    .Select(
                        x => new JObject(
                                 new JProperty("Характеристика", x.Характеристика),
                                 new JProperty("Значение", x.Значение)
                                 ));

                var JObject = new JObject(
                    new JProperty("ID", PositionId),
                    new JProperty("Код_артикула", InfoRow.Код_артикула),
                    new JProperty("Артикул", InfoRow.Артикул),
                    new JProperty("Описание", InfoRow.Описание),
                    new JProperty("ПрИнфо", InfoRow.ПрИнфо),
                    new JProperty("Инфо", InfoRow.Инфо),
                    new JProperty("Вид_товара", InfoRow.Вид_товара),
                    new JProperty("Производитель", InfoRow.Производитель),
                    new JProperty("Поставщик", InfoRow.Поставщик),
                    new JProperty("Склад", InfoRow.Склад),
                    new JProperty("Статус", InfoRow.Статус),
                    new JProperty("Группа", InfoRow.IsГруппаNull() ? "" : InfoRow.Группа.ToString()),
                    new JProperty("Сектор", InfoRow.Сектор),
                    new JProperty("Цена", InfoRow.Цена),
                    new JProperty("Количество", InfoRow.Количество),
                    new JProperty("Характеристики", new JArray(q)));

                context.Response.ContentType = "application/json";
                context.Response.Write(JObject.ToString());
            }

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