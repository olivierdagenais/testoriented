namespace AtomicCms.Core.ViewModels
{
    using Common.Abstract.DomainObjects;

    public class ViewModelHome
    {
        public IEntry Entry { get; set; }
        public ISiteAttributes Attributes { get; set; }
    }
}