namespace AtomicCms.Web.Core.Mvc
{
    using System.Net;
    using System.Web.Mvc;

    public class NotFound404Result : ActionResult
    {
        public override void ExecuteResult(ControllerContext context)
        {
            context.HttpContext.Response.Status = "404 Not Found";
            context.HttpContext.Response.StatusCode = (int) HttpStatusCode.NotFound;
            context.HttpContext.Response.Flush();
            context.HttpContext.Response.End();
            //this.result.ExecuteResult(context);
        }
    }
}