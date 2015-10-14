using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebReceipt.DataSets;
using System.Data;
using System.Xml.Linq;
using System.Web.SessionState;
using Newtonsoft.Json.Linq;

namespace WebReceipt
{
    public class SelectShops : IHttpHandler,IReadOnlySessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            if (context.Session["Key"] == null)
                throw new Exception("Ошибка обновления");


            int? ShopId = null;
            try
            {
                ShopId = Convert.ToInt32(context.Session["ShopId"].ToString());
            }
            catch { }

            int? UserId = null;
            try
            {
                UserId = Convert.ToInt32(context.Session["UserId"].ToString());
            }
            catch { }


            var dsLogin = new DataSets.dsLogin();
            (new DataSets.dsLoginTableAdapters.taShops()).Fill(dsLogin.tbShops);
            (new DataSets.dsLoginTableAdapters.taUsers()).Fill(dsLogin.tbUsers);

            var JObject = new JObject();
            var JShops = new JArray();

            foreach (dsLogin.tbShopsRow rw in dsLogin.tbShops.Select("", "[Название] ASC"))
            {
                var rwc = dsLogin.tbUsers.Select(
                    String.Format("[Код магазина]={0:G}", rw.Код),
                    "[ФИО] ASC");

                if (rwc.Length <= 0)
                    continue;

                if (rw.Код == ShopId)
                    JObject.Add(new JProperty("Выбранный_магазин", ShopId));

                var JUsers = new JArray();

                foreach (dsLogin.tbUsersRow rwu in rwc)
                {
                    if (rw.Код == ShopId && rwu.Код == UserId)
                        JObject.Add(new JProperty("Выбранный_сотрудник", UserId));

                    JUsers.Add(new JObject(
                        new JProperty("Код", rwu.Код),
                        new JProperty("ФИО", rwu.ФИО)));
                }

                JShops.Add(new JObject(
                    new JProperty("Название", rw.Название),
                    new JProperty("Код", rw.Код),
                    new JProperty("Сотрудники",JUsers)));

            }

            JObject.Add(new JProperty("Магазины", JShops));

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