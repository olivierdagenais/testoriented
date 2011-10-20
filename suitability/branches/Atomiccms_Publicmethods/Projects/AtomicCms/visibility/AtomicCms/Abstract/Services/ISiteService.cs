namespace AtomicCms.Common.Abstract.Models
{
    using DomainObjects;

    public interface ISiteService
    {
        ISiteAttributes LoadSiteAttributes();
        ISiteAttribute LoadSiteAttribute(int id);
        void Update(ISiteAttribute attribute);
    }
}