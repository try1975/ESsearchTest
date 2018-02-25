using System.IO;
using System.Net;
using System.Web;
using Price.WebApi.Logic;

namespace Price.WebApi.GetFiles
{
    /// <summary>
    /// Summary description for GetFile
    /// </summary>
    public class GetFile : IHttpHandler
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public void ProcessRequest(HttpContext context)
        {
            //if (!context.User.Identity.IsAuthenticated)
            //{
            //    context.Response.StatusCode = (int) HttpStatusCode.Unauthorized;
            //    return;
            //}

            var id = context.Request.Params["id"];
            if (string.IsNullOrEmpty(id))
            {
                Logger.Log.Error($"{nameof(GetFile)}: param [id] not found");
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                context.Response.End();
            }
            var filename = $"{id}.png";
            var screenshot = Path.Combine(AppGlobal.ScreenshotPath, $"{filename}");
            if (!File.Exists(screenshot))
            {
                Logger.Log.Error($"{screenshot} not found");
                context.Response.StatusCode = (int) HttpStatusCode.NotFound;
                context.Response.End();
            }
            context.Response.ContentType = "image/png";
            context.Response.AddHeader("content-disposition", $"attachment; filename={filename}");
            context.Response.BinaryWrite(File.ReadAllBytes(screenshot));
        }

        public bool IsReusable { get; } = false;
    }
}