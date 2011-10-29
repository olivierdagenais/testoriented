namespace AtomicCms.Common.Abstract.Factories
{
    using Models;
    using Services;

    public interface IServiceFactory
    {
        IMenuService MenuService { get; }
        IEntryService EntryService { get; }
        ISiteService SiteService { get; }
        IInfrastructureService InfrastructureService { get; }
        IUserService UserService { get; }
        IFileUploadService FileUploadService { get; }
        ISeoService SeoService { get; }
    }
}