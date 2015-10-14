using System;
using System.Web;
using LocalService;
using System.Web.SessionState;
using Newtonsoft.Json.Linq;

namespace WebReceipt.Pages.Receipt
{
    /// <summary>
    /// Summary description for InfoByArticleId
    /// </summary>
    public class InfoByArticleId : IHttpHandler, IReadOnlySessionState
    {

        public void ProcessRequest(HttpContext context)
        {

            if (context.Session["Authentication"] == null)
                throw new Exception("Ошибка обновления");

            int ShopId = Convert.ToInt32(context.Session["ShopId"]);
            int ArticleId = Convert.ToInt32(context.Request["articleId"]);

            bool Error, Warning;
            string Message;
            int ErrorId, PosCount, StoreInfoId;

            
            LocalDBService.InfoByArticleId(
                ShopId, ArticleId, 
                out ErrorId, out Error, 
                out Warning, out Message,
                out PosCount, out StoreInfoId);

            var JObject = new JObject(
                new JProperty("articleId", ArticleId),
                new JProperty("errorId", ErrorId),
                new JProperty("error", Error),
                new JProperty("message", Message),
                new JProperty("warning", Warning),
                new JProperty("posCount", PosCount),
                new JProperty("storeInfoId", StoreInfoId));

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