namespace AtomicCms.Common.Abstract.DomainObjects
{
    using ShapovalovCms.Common.Enumeration;

    public interface ISitemapUrl
    {
        string Location { get; set; }
        string LastModified { get; set; }
        ChangeFrequency ChangeFrequency { get; set; }
        double Priority { get; set; }
    }
}