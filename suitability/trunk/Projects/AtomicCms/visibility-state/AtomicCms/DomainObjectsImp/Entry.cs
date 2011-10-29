namespace AtomicCms.Core.DomainObjectsImp
{
    using System;
    using Common.Abstract.DomainObjects;

    public class Entry : IEntry
    {
        internal static readonly IEntry nullObject = new Entry
                                                        {
                                                            Alias = string.Empty,
                                                            SeoTitle = string.Empty,
                                                            EntryBody = string.Empty,
                                                            EntryTitle = string.Empty,
                                                            CreatedAt = DateTime.Now,
                                                            Id = 0,
                                                            MetaDescription = string.Empty,
                                                            MetaKeywords = string.Empty,
                                                            ModifiedAt = DateTime.Now,
                                                            Author = null
                                                        };

        public static IEntry NullObject
        {
            get { return nullObject; }
        }

        #region IEntry Members

        public int Id { get; set; }

        public DateTime CreatedAt { get; set; }

        public string SeoTitle { get; set; }
        public string EntryTitle { get; set; }
        public string Alias { get; set; }
        public string EntryBody { get; set; }
        public IUser Author { get; set; }
        public DateTime ModifiedAt { get; set; }
        public string MetaDescription { get; set; }
        public string MetaKeywords { get; set; }

        #endregion
    }
}