namespace AtomicCms.Web.Controllers
{
    using System.ServiceModel.Syndication;
    using System.Web.Mvc;
    using Common.Abstract.DomainObjects;
    using Common.Abstract.Factories;
    using Common.Abstract.Models;
    using Core;
    using Core.ActionResults;
    using Core.Mvc;

    public class InfrastructureController : AtomicCmsController
    {
        internal readonly IServiceFactory serviceFactory;


        public InfrastructureController(IServiceFactory _modelFactory)
        {
            this.serviceFactory = _modelFactory;
        }

        [AcceptVerbs(HttpVerbs.Get)]
        [OutputCache(CacheProfile = "Syndication")]
        public ActionResult Sitemap()
        {
            ISitemap sitemap = this.serviceFactory.InfrastructureService
                .CreateSitemap(base.GenerateEntryUrl,
                               GetWebsiteUrl());

            return new SitemapResult(sitemap);
        }

        [OutputCache(CacheProfile = "Syndication")]
        public ActionResult Rss()
        {
            SyndicationFeed feed = this.GetSindicationFeed();
            return new SyndicationActionResult(
                new Rss20FeedFormatter(feed));
        }

        [OutputCache(CacheProfile = "Syndication")]
        public ActionResult Atom()
        {
            SyndicationFeed feed = this.GetSindicationFeed();
            return new SyndicationActionResult(
                new Atom10FeedFormatter(feed));
        }

        internal SyndicationFeed GetSindicationFeed()
        {
            string url = GetWebsiteUrl();
            return this.serviceFactory.InfrastructureService.BuildFeed(url,
                                                                       base.GenerateEntryUrl);
        }

        public ContentResult GenerateAlias(string source)
        {
            ISeoService service = this.serviceFactory.SeoService;
            return new ContentResult {Content = service.CreateAlias(source)};
        }
    }
}