namespace AtomicCms.Common.Abstract.Dao
{
    using System.Collections.Generic;
    using DomainObjects;

    public interface IEntryDao 
    {
        IEntry Load(int id);
        IEntry LoadDefault();
        void Save(IEntry entry);
        void Delete(int id);
        ICollection<IEntry> LoadAll();
        ICollection<IEntry> LoadLast(int numberOfLastPages);
    }
}