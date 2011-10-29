namespace AtomicCms.Common.Dto
{
    using System;

    public class CmsArticle
    {
        public int Id { get; set; }

        public string SeoPageTitle { get; set; }

        public string ArticleTitle { get; set; }

        public string Alias { get; set; }

        public bool IsDefault { get; set; }

        public string ArticleBody { get; set; }

        public CmsUser Author { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime ModifiedDate { get; set; }

        public string MetaDescription { get; set; }

        public string MetaKeywords { get; set; }
    }
}