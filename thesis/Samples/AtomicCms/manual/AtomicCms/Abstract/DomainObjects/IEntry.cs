namespace AtomicCms.Common.Abstract.DomainObjects
{
    using System;

    public interface IEntry : IDomainObject
    {
        string SeoTitle { get; set; }
        string EntryTitle { get; set; }
        string Alias { get; set; }
        string EntryBody { get; set; }
        IUser Author { get; set; }
        DateTime ModifiedAt { get; set; }
        string MetaDescription { get; set; }
        string MetaKeywords { get; set; }
    }
}