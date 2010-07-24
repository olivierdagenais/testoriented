namespace AtomicCms.Common.Abstract.Services
{
    using System;
    using System.ServiceModel.Syndication;
    using AtomicCms.Common.Abstract.DomainObjects;

    public interface IInfrastructureService
    {
        ISitemap CreateSitemap(Func<IEntry, string> generateEntryUrl, string homePage);
        SyndicationFeed BuildFeed(string websiteUrl, Func<IEntry, string> generateEntryUrl);
    }
}