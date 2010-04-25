namespace AtomicCms.Common.Abstract.Dao
{
    using DomainObjects;

    public interface ISiteDao
    {
        ISiteAttributes LoadAttributes();
        ISiteAttribute LoadAttribute(int id);
        void Update(ISiteAttribute attribute);
    }
}