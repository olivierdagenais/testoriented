namespace AtomicCms.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Text.RegularExpressions;
    using System.Web.Mvc;
    using AtomicCms.Core.ViewModels;
    using Common.Abstract.DomainObjects;
    using Common.Abstract.Factories;
    using Common.Attributes;
    using Common.CodeContract;
    using Common.Exceptions;
    using Core.Mvc;

    [HandleError]
    [SupportSkin]
    public class HomeController : Controller
    {
        internal readonly IServiceFactory serviceFactory;

        public HomeController(IServiceFactory serviceFactory)
        {
            Check.Argument.IsNotNull(serviceFactory,
                                     "serviceFactory");
            this.serviceFactory = serviceFactory;
        }

        public ActionResult Default()
        {
            IEntry defaultEntry = this.serviceFactory.EntryService.LoadDefault();
            return this.ShowPage(defaultEntry);
        }

        [OutputCache(CacheProfile = "Page")]
        public ActionResult Content(int id, string name)
        {
            try
            {
                IEntry entry = this.serviceFactory.EntryService.Load(id);
                return this.ShowPage(entry);
            }
            catch (EntryNotFoundException)
            {
                return PageNotFound();
            }
        }

        private NotFound404Result PageNotFound()
        {
            return new NotFound404Result();
        }

        private ActionResult ShowPage(IEntry entry)
        {
            ISiteAttributes attributes = this.serviceFactory.SiteService.LoadSiteAttributes();
            return View("Index",
                        new ViewModelHome
                            {
                                Entry = entry,
                                Attributes = attributes
                            });
        }

        [OutputCache(CacheProfile = "Page")]
        public PartialViewResult LastPages()
        {
            ICollection<IEntry> lastEntries = serviceFactory.EntryService.LoadLast(10);
            return PartialView("LastPages",lastEntries);
        }
    }
}