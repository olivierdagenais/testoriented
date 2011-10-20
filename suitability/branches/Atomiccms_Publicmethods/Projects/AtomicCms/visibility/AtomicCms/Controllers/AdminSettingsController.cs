namespace AtomicCms.Web.Controllers
{
    using System.Web.Mvc;
    using AtomicCms.Core.ControllerHelpers;
    using AtomicCms.Core.DomainObjectsImp;
    using Common.Abstract.DomainObjects;
    using Common.Abstract.Factories;

    [Authorize]
    public class AdminSettingsController : Controller
    {
        internal readonly IServiceFactory serviceFactory;

        public AdminSettingsController(IServiceFactory serviceFactory)
        {
            this.serviceFactory = serviceFactory;
        }

        public ActionResult Index()
        {
            return View(this.serviceFactory.SiteService.LoadSiteAttributes().Attributes);
        }

        public ViewResult EditSetting(int? id)
        {
            if (null != id)
            {
                ISiteAttribute attr = this.serviceFactory.SiteService.LoadSiteAttribute(id.Value);
                return View(attr);
            }
            else
            {
                return View(SiteAttribute.NullObject);
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateInput(false)]
        public ActionResult EditSetting(SiteAttribute attr)
        {
            this.serviceFactory.SiteService.Update(attr);
            return RedirectToAction(ControllerNaming.Action<AdminSettingsController>(x => x.EditSetting(attr.Id)),
                                    ControllerNaming.Name<AdminSettingsController>(),
                                    new {id = attr.Id});
        }
    }
}