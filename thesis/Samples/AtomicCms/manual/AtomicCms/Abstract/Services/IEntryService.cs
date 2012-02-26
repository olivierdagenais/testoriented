namespace AtomicCms.Common.Abstract.Models
{
    using System.Collections.Generic;
    using DomainObjects;

    public interface IEntryService
    {
        IEntry Load(int id);
        IEntry LoadDefault();
        void Save(IEntry entry);
        void CreateEntry(IEntry entry);
        void Delete(int id);
        ICollection<IEntry> LoadAll();
        ICollection<IEntry> LoadLast(int numberOfLastPages);
    }
}