namespace AtomicCms.Web.Core
{
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;
    using AtomicCms.Core.ControllerHelpers;
    using Common.Abstract.DomainObjects;

    public class AtomicCmsController : Controller
    {
        protected internal string GetWebsiteUrl()
        {
            HttpRequestBase request = HttpContext.Request;

            return Utils.GetBaseUrl(
                request.ApplicationPath,
                request.Url,
                true);
        }
        public static RouteValueDictionary GetRouteValues( IEntry entry)
        {
            if (null == entry)
            {
                return new RouteValueDictionary();
            }

            return new RouteValueDictionary(new { id = entry.Id, name = entry.Alias });
        }
        protected internal string GenerateEntryUrl(IEntry entry)
        {
            return VirtualPathUtility.RemoveTrailingSlash(GetWebsiteUrl()) + Url.Action("Content", "Home", GetRouteValues(entry));
        }
    }
}