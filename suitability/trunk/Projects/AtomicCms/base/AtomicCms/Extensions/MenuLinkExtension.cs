namespace AtomicCms.Web.Core.Extensions
{
    using System.Web.Mvc;
    using AtomicCms.Core.DomainObjectsImp;
    using Common.Abstract.DomainObjects;

    public static class MenuLinkExtension
    {
        public static string BuildMenuLink(this UrlHelper urlHelper, IMenuItem item)
        {
            if (item.IsExternalUrl)
            {
                return item.NavigateUrl;
            }

            return urlHelper.Action("Content",
                                    "Home",
                                    new {id = item.Entry.Id, name = item.Entry.Alias});
        }
    }
}