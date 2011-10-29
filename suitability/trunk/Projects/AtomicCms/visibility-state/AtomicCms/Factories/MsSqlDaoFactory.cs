namespace AtomicCms.Data.NHibernate.Factories
{
    using System;
    using Common.Abstract.Dao;
    using Common.Abstract.Factories;
    using Common.Utils;
    using DaoObjects;
    using global::NHibernate;
    using global::NHibernate.Cfg;

    public class MsSqlDaoFactory : IDaoFactory
    {
        internal readonly ISessionFactory sessionFactory;
        internal IEntryDao entryDao;
        internal ISiteDao siteDao;
        internal IMenuDao menuDao;
        internal IUserDao userDao;

        public MsSqlDaoFactory()
        {
            Configuration config = new Configuration();
            config.Configure();
            this.sessionFactory = config.BuildSessionFactory();
        }

        #region IDaoFactory Members

        public IEntryDao EntryDao
        {
            get { return LazyLoader.Load(ref this.entryDao, typeof (EntryDao), this.sessionFactory); }
        }

        public ISiteDao SiteDao
        {
            get { return LazyLoader.Load(ref this.siteDao, typeof (SiteDao), this.sessionFactory); }
        }

        public IMenuDao MenuDao
        {
            get { return LazyLoader.Load(ref this.menuDao, typeof (MenuDao), this.sessionFactory); }
        }

        public IUserDao UserDao
        {
            get { return LazyLoader.Load(ref this.userDao, typeof (UserDao), this.sessionFactory); }
        }

        #endregion
    }
}