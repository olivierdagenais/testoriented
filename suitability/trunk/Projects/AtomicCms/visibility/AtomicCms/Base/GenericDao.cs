namespace AtomicCms.Data.NHibernate.Base
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using global::NHibernate;
    using global::NHibernate.Criterion;

    public class GenericDao<T> : MsSqlBase where T : class
    {
        public GenericDao(ISessionFactory factory) : base(factory)
        {
        }

        [DebuggerStepThrough]
        public virtual void Delete(T o)
        {
            using (ISession session = base.OpenSession())
            using (ITransaction tx = session.BeginTransaction())
            {
                session.Delete(o);
                tx.Commit();
            }
        }

        [DebuggerStepThrough]
        public virtual ICollection<T> LoadAll()
        {
            using (ISession session = base.OpenSession())
            {
                return session.CreateCriteria<T>().List<T>();
            }
        }

        [DebuggerStepThrough]
        public virtual void Save(T o)
        {
            using (ISession session = base.OpenSession())
            {
                using (ITransaction tx = session.BeginTransaction())
                {
                    session.SaveOrUpdate(o);
                    tx.Commit();
                }
            }
        }

        [DebuggerStepThrough]
        public virtual T Get(int id)
        {
            using (ISession session = base.OpenSession())
            {
                return session.Get<T>(id);
            }
        }

        [DebuggerStepThrough]
        public virtual T Load(int id)
        {
            using (ISession session = base.OpenSession())
            {
                return session.Load<T>(id);
            }
        }

        [DebuggerStepThrough]
        protected internal T Get(string propertyName, object propertyValue)
        {
            using (ISession session = base.OpenSession())
            {
                return session.CreateCriteria(typeof (T))
                    .Add(Restrictions.Eq(propertyName,
                                         propertyValue)).UniqueResult<T>();
            }
        }

        [DebuggerStepThrough]
        public T Get(string alias)
        {
            return this.Get("Alias", alias);
        }
    }
}