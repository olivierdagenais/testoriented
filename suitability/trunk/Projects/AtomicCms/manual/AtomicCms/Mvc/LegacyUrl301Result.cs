namespace AtomicCms.Web.Core.Mvc
{
    using System.Web;
    using System.Web.Mvc;
    using Common.CodeContract;
    using Extensions;

    public class LegacyUrl301Result : ActionResult
    {
        public LegacyUrl301Result(string redirectTo)
        {
            Check.Argument.IsNotNull(redirectTo,
                                     "redirectTo");
            this.NewUrl = redirectTo;
        }

        public string NewUrl { get; private set; }

        public override void ExecuteResult(ControllerContext context)
        {
            HttpResponseBase response = context.RequestContext.HttpContext.Response;
            response.PermanentRedirect(this.NewUrl);
        }
    }
}