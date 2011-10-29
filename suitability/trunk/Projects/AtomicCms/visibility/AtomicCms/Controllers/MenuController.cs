namespace AtomicCms.Web.Controllers
{
    using System.Web.Mvc;
    using Common.Abstract.DomainObjects;
    using Common.Abstract.Factories;

    public class MenuController : Controller
    {
        internal readonly IServiceFactory serviceFactory;

        public MenuController(IServiceFactory serviceFactory)
        {
            this.serviceFactory = serviceFactory;
        }

        [OutputCache(CacheProfile = "Menu")]
        public ActionResult Show(string menuName)
        {
            IMenu menu = this.serviceFactory.MenuService.LoadMenu(menuName);
            return PartialView(
                menuName,
                menu);
        }
    }
}