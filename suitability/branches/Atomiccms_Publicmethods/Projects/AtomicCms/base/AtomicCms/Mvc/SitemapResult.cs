namespace AtomicCms.Web.Core.Mvc
{
    using System.Web.Mvc;
    using System.Xml.Serialization;
    using AtomicCms.Core.DomainObjectsImp;
    using Common.Abstract.DomainObjects;

    public class SitemapResult : ActionResult
    {
        public SitemapResult(ISitemap sitemap)
        {
            this.Sitemap = sitemap;
        }

        public ISitemap Sitemap { get; private set; }

        public override void ExecuteResult(ControllerContext context)
        {
            context.HttpContext.Response.ContentType = "text/xml";


            XmlSerializer serializer = new XmlSerializer(typeof (Sitemap));
            serializer.Serialize(context.HttpContext.Response.Output,
                                 this.Sitemap);
        }
    }
}