using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using Newtonsoft.Json.Linq;
using LocalService;
using WebReceipt.DataSets;

namespace WebReceipt.Pages.Login
{
    public class Authentication : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            if (context.Session["Key"] == null)
                throw new Exception("Ошибка обновления");

            int? UserId = null;
            try
            {
                UserId = Convert.ToInt32(context.Request["UserId"].ToString());
            }
            catch { }

            int? ShopId = null;
            try
            {
                ShopId = Convert.ToInt32(context.Request["ShopId"].ToString());
            }
            catch { }

            if (UserId == null || ShopId == null)
                return;


            string Password = "";
            try
            {
                Password = context.Request["Password"].ToString();
            }
            catch { }

            int? CurrentUserId = null;
            try
            {
                CurrentUserId = Convert.ToInt32(context.Session["UserId"].ToString());
            }
            catch { }

            var JObject = new JObject();

            if (UserId < 0 || UserId > 0 && UserId == CurrentUserId ||
                LocalDBService.CheckPassword((int)UserId, Password))
            {

                context.Session.Add("UserId", (int)UserId);
                context.Session.Add("ShopId", (int)ShopId);
                context.Session.Add("Authentication", true);

                var dsLogin = new DataSets.dsLogin();
                (new DataSets.dsLoginTableAdapters.taShops()).Fill(dsLogin.tbShops);
                (new DataSets.dsLoginTableAdapters.taUsers()).Fill(dsLogin.tbUsers);

                if (UserId > 0)
                {
                    var rwUser = (dsLogin.tbUsersRow)
                                 (dsLogin.tbUsers.Select(String.Format("[Код]={0}", UserId))[0]);
                    context.Session.Add("UserName", rwUser.ФИО);
                }
                else
                {
                    context.Session.Add("UserName", "Покупатель #" + (-(int)UserId).ToString());
                }
                var rwShop = (dsLogin.tbShopsRow)
                    (dsLogin.tbShops.Select(String.Format("[Код]={0}", ShopId))[0]);
                context.Session.Add("ShopName", rwShop.Название);

                context.Session.Add("ReceiptNumber",
                    LocalDBService.FirstReceiptNumber((int)UserId, (int)ShopId));

                JObject.Add(new JProperty("hasSuccess", true));
                JObject.Add(new JProperty("message", "Аутентификация прошла успешно"));
            }
            else
            {
                JObject.Add(new JProperty("hasSuccess", false));
                JObject.Add(new JProperty("message", "Вы ввели неправильный пароль"));
            }

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