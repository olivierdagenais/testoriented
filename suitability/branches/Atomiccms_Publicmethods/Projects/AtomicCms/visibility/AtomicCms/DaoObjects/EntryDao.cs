namespace AtomicCms.Data.NHibernate.DaoObjects
{
    using System;
    using System.Collections.Generic;
    using Base;
    using Common.Abstract.Dao;
    using Common.Abstract.DomainObjects;
    using Common.Exceptions;
    using Common.Utils;
    using Core.DomainObjectsImp;
    using global::NHibernate;
    using global::NHibernate.Criterion;

    public class EntryDao : GenericDao<Entry>, IEntryDao
    {
        public EntryDao(ISessionFactory sessionFactory) : base(sessionFactory)
        {
        }

        #region IEntryDao Members

        public new IEntry Load(int id)
        {
            try
            {
                return base.Load(id);
            }
            catch (ObjectNotFoundException)
            {
                throw new EntryNotFoundException(string.Format("The requested entry {0} was not found.",
                                                               id));
            }
        }

        public IEntry LoadDefault()
        {
            throw new NotImplementedException();
        }

        public void Save(IEntry entry)
        {
            using (ISession session = OpenSession())
            {
                using (ITransaction tx = session.BeginTransaction())
                {
                    session.SaveOrUpdate(entry);
                    tx.Commit();
                }
            }
        }

        public void Delete(int id)
        {
            base.Delete(base.Load(id));
        }

        public new ICollection<IEntry> LoadAll()
        {
            return base.LoadAll().Convert<IEntry, Entry>();
        }

        public ICollection<IEntry> LoadLast(int numberOfLastPages)
        {
            using (ISession s = OpenSession())
            {
                return
                    s.CreateCriteria(typeof (IEntry)).AddOrder(
                        Order.Desc(Strong<IEntry>.Name(x => x.CreatedAt))).SetMaxResults(numberOfLastPages).List<IEntry>
                        ();
            }
        }

        #endregion
    }
}