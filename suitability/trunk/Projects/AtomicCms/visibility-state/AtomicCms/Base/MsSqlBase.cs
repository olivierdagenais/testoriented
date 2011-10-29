namespace AtomicCms.Data.NHibernate.Base
{
    using System.Diagnostics;
    using global::NHibernate;

    public class MsSqlBase
    {
        internal readonly ISessionFactory _sessionFactory;

        protected MsSqlBase(ISessionFactory factory)
        {
            this._sessionFactory = factory;
        }

        [DebuggerStepThrough]
        protected ISession OpenSession()
        {
            return this._sessionFactory.OpenSession();
        }
    }
}