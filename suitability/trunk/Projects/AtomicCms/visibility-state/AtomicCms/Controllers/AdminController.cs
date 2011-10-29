namespace AtomicCms.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;
    using AtomicCms.Core.DomainObjectsImp;
    using AtomicCms.Core.ViewModels;
    using Common.Abstract.DomainObjects;
    using Common.Abstract.Factories;
    using Common.Exceptions;
    using Common.Extensions;

    public class MenuItemViewModel
    {
        public IMenuItem MenuItem { get; set; }
        public IList<SelectListItem> MenuTypes { get; set; }

    }

    [Authorize]
    public class AdminController : Controller
    {
        internal readonly IServiceFactory modelFactory;
        

        public AdminController(IServiceFactory _modelFactory)
        {
            this.modelFactory = _modelFactory;
        }

        public ActionResult Dashboard()
        {
            return View();
        }
    }
}