namespace AtomicCms.Web.Controllers
{
    using System.Collections.Generic;
    using System.Web.Mvc;
    using AtomicCms.Core.DomainObjectsImp;
    using Common.Abstract.DomainObjects;
    using Common.Abstract.Factories;

    [Authorize]
    public class AdminMenuController : Controller
    {
        public ActionResult MenuList()
        {
            ICollection<IMenu> menues = this.serviceFactory.MenuService.LoadMenues();
            return View(menues);
        }

        private readonly IServiceFactory serviceFactory;

        public AdminMenuController(IServiceFactory _modelFactory)
        {
            this.serviceFactory = _modelFactory;
        }

        private ActionResult UpdateMenu(int id, IMenu menu)
        {
            IMenu oldMenu = this.serviceFactory.MenuService.LoadMenu(id);
            menu.MenuItems = oldMenu.MenuItems;
            this.serviceFactory.MenuService.SaveMenu(menu);
            TempData["SaveResult"] = "Items was successfully saved";
            return View(menu);
        }

        private ActionResult CreateNewMenu(IMenu menu)
        {
            this.serviceFactory.MenuService.SaveMenu(menu);
            TempData["SaveResult"] = string.Format("Items was successfully created with Id = {0}",
                                                   menu.Id);
            return RedirectToAction("EditMenu",
                                    "Menu",
                                    new {id = menu.Id});
        }

        public ViewResult EditMenu(int? id)
        {
            if (null != id)
            {
                IMenu menu = this.serviceFactory.MenuService.LoadMenu(id.Value);
                return View(menu);
            }

            return View(Menu.NullObject);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditMenu(int id, Menu menu)
        {
            if (id == 0)
            {
                return this.CreateNewMenu(menu);
            }

            return this.UpdateMenu(id,
                                   menu);
        }
    }
}