namespace AtomicCms.Core.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.ServiceModel.Syndication;
    using Common.Abstract.DomainObjects;
    using Common.Abstract.Factories;
    using Common.Abstract.Services;
    using Common.Constants;
    using ShapovalovCms.Common.Enumeration;

    public class InfrastructureService : IInfrastructureService
    {
        private readonly IDaoFactory daoFactory;
        private readonly IDomainObjectFactory domainObjectFactory;

        public InfrastructureService(IDaoFactory daoFactory, IDomainObjectFactory domainObjectFactory)

        {
            this.domainObjectFactory = domainObjectFactory;
            this.daoFactory = daoFactory;
        }

        #region IInfrastructureService Members

        public ISitemap CreateSitemap(Func<IEntry, string> generateEntryUrl, string homePage)
        {
            ICollection<IEntry> entries = this.daoFactory.EntryDao.LoadAll();
            int homePageId =
                Convert.ToInt32(this.daoFactory.SiteDao.LoadAttributes().GetValue(Constant.Settings.DefaultPageId));

            ISitemap sitemap = this.domainObjectFactory.CreateSitemap();
            foreach (IEntry page in entries)
            {
                if (this.IsDefaultPage(page,
                                       homePageId))
                {
                    this.AddHomePage(sitemap,
                                     page,
                                     homePage);
                    continue;
                }

                ISitemapUrl url = this.domainObjectFactory.CreateSitemapUrl();

                url.Location = generateEntryUrl(page);

                url.ChangeFrequency = CalculateFrequency(page.ModifiedAt);
                url.Priority = 0.7;
                url.LastModified = page.ModifiedAt.ToString();
                sitemap.Add(url);
            }

            return sitemap;
        }

        public SyndicationFeed BuildFeed(string websiteUrl, Func<IEntry, string> generateEntryUrl)
        {
            ISiteAttributes siteAttributes = this.daoFactory.SiteDao.LoadAttributes();
            string rssFeedTitle = siteAttributes.GetValue(Constant.Settings.SiteLogo);
            string rssFeedDescr = siteAttributes.GetValue(Constant.Settings.SiteSubLogo);

            ICollection<IEntry> entries = this.daoFactory.EntryDao.LoadLast(10);
            List<SyndicationItem> items =
                entries.Select(entry => CreateSyndicationItem(entry, generateEntryUrl)).ToList();

            return new SyndicationFeed(rssFeedTitle,
                                       rssFeedDescr,
                                       new Uri(websiteUrl),
                                       "syndicationID",
                                       DateTime.Now,
                                       items);
        }

        internal static SyndicationItem CreateSyndicationItem(IEntry entry, Func<IEntry, string> generateEntryUrl)
        {
            var uriString = generateEntryUrl(entry);
            return CreateSyndicationItem(entry, uriString);
        }

        internal static SyndicationItem CreateSyndicationItem(IEntry entry, string uriString)
        {
            return new SyndicationItem(entry.SeoTitle,
                                       SyndicationContent.
                                           CreateHtmlContent(entry.EntryBody),
                                       new Uri(uriString),
                                       string.Format("item-id-{0}", entry.Id),
                                       entry.CreatedAt);
        }

        #endregion

        private void AddHomePage(ISitemap sitemap, IEntry page, string homePage)
        {
            ISitemapUrl url = this.domainObjectFactory.CreateSitemapUrl();
            url.Location = homePage;
            url.ChangeFrequency = CalculateFrequency(page.ModifiedAt);
            url.Priority = 0.9;
            url.LastModified = page.ModifiedAt.ToString();
            sitemap.Insert(0,
                           url);
        }

        private bool IsDefaultPage(IEntry page, int homePageId)
        {
            return page.Id == homePageId;
        }

        private static ChangeFrequency CalculateFrequency(DateTime modifiedAt)
        {
            return CalculateFrequency(DateTime.Now, modifiedAt);
        }

        internal static ChangeFrequency CalculateFrequency(DateTime referenceTime, DateTime modifiedAt)
        {
            ChangeFrequency frequency = ChangeFrequency.Hourly;
            if (modifiedAt < referenceTime.AddMonths(-12))
            {
                frequency = ChangeFrequency.Yearly;
            }
            else if (modifiedAt < referenceTime.AddDays(-60))
            {
                frequency = ChangeFrequency.Monthly;
            }
            else if (modifiedAt < referenceTime.AddDays(-14))
            {
                frequency = ChangeFrequency.Weekly;
            }
            else if (modifiedAt < referenceTime.AddDays(-2))
            {
                frequency = ChangeFrequency.Daily;
            }
            return frequency;
        }
    }
}