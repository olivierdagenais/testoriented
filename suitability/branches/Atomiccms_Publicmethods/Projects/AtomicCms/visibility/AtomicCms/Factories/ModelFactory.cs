namespace AtomicCms.Core.Factories
{
    using System;
    using Common.Abstract.Factories;
    using Common.Abstract.Models;
    using Common.Abstract.Services;
    using Common.Utils;
    using DomainObjectsImp;
    using Models;

    public class ModelFactory : IServiceFactory
    {
        internal readonly IDaoFactory daoFactory;
        internal readonly IDomainObjectFactory domainObjectFactory;
        internal IEntryService _entryModel;
        internal IInfrastructureService _sitemapModel;
        internal ISiteService _siteModel;
        internal IUserService _userModel;
        internal IFileUploadService fileUploadService;

        internal IMenuService menuService;

        public ModelFactory(IDaoFactory daoFactory, IDomainObjectFactory domainObjectFactory)
        {
            this.daoFactory = daoFactory;
            this.domainObjectFactory = domainObjectFactory;
        }

        public IFileUploadService FileUploadService
        {
            get
            {
                return LazyLoader.Load(ref this.fileUploadService,
                                       typeof (FileUploadService));
            }
        }

        internal ISeoService seoService;
        public ISeoService SeoService
        {
            get {
                return LazyLoader.Load(ref this.seoService,
                                       typeof (SeoService)); }
        }

        #region IServiceFactory Members

        public IMenuService MenuService
        {
            get
            {
                return LazyLoader.Load(ref this.menuService,
                                       typeof (MenuService),
                                       this.daoFactory);
            }
        }

        public IEntryService EntryService
        {
            get
            {
                return LazyLoader.Load(ref this._entryModel,
                                       typeof (EntryService),
                                       this.daoFactory);
            }
        }

        public ISiteService SiteService
        {
            get
            {
                return LazyLoader.Load(ref this._siteModel,
                                       typeof (SiteService),
                                       this.daoFactory);
            }
        }

        public IInfrastructureService InfrastructureService
        {
            get
            {
                return LazyLoader.Load(ref this._sitemapModel,
                                       typeof (InfrastructureService),
                                       this.daoFactory,
                                       this.domainObjectFactory);
            }
        }

        public IUserService UserService
        {
            get
            {
                return LazyLoader.Load(ref this._userModel,
                                       typeof (UserService),
                                       this.daoFactory);
            }
        }

        #endregion
    }
}