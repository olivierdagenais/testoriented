namespace AtomicCms.Data.NHibernate.DaoObjects
{
    using System.Linq;
    using System.Collections.Generic;
    using Base;
    using Common.Abstract.Dao;
    using Common.Abstract.DomainObjects;
    using Common.Utils;
    using Core.DomainObjectsImp;
    using global::NHibernate;
    using global::NHibernate.Criterion;

    public class MenuDao : GenericDao<Menu>, IMenuDao
    {
        public MenuDao(ISessionFactory factory) : base(factory)
        {
        }

        #region IMenuDao Members

        public IMenu LoadMenu(string menuName)
        {
            using (ISession session = base.OpenSession())
            {
                return session.CreateCriteria(typeof (Menu))
                    .Add(Restrictions.Eq("Type",
                                         menuName)).List<IMenu>().FirstOrDefault();
            }
        }

        public new ICollection<IMenu> LoadAll()
        {
            return base.LoadAll().Convert<IMenu, Menu>();
        }

        public new IMenu Load(int id)
        {
            return base.Load(id);
        }

        public IMenuItem LoadMenuItem(int id)
        {
            using (ISession s = base.OpenSession())
            {
                return s.Load<MenuItem>(id);
            }
        }

        public void Save(IMenu menu)
        {
            using (ISession s = base.OpenSession())
            {
                using (ITransaction t = s.BeginTransaction())
                {
                    s.SaveOrUpdate(menu);
                    t.Commit();
                }
            }
        }

        public void SaveMenuItem(IMenuItem item)
        {
            using (ISession s = base.OpenSession())
            {
                using (ITransaction t = s.BeginTransaction())
                {
                    s.SaveOrUpdate(item);
                    t.Commit();
                }
            }

        }

        #endregion
    }
}