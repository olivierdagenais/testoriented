namespace AtomicCms.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Web.Mvc;
    using AtomicCms.Core.DomainObjectsImp;
    using Common.Abstract.DomainObjects;
    using Common.Abstract.Factories;
    using Common.Extensions;

    [Authorize]
    public class AdminMenuItemController : Controller
    {
        internal readonly IServiceFactory serviceFactory;

        public AdminMenuItemController(IServiceFactory serviceFactory)
        {
            this.serviceFactory = serviceFactory;
        }

        public ViewResult EditMenuItem(int? id)
        {
            List<SelectListItem> menuTypes = this.GetMenuTypes();

            if (null != id)
            {
                IMenuItem menuItem = this.serviceFactory.MenuService.LoadMenuItem(id.Value);
                if (menuItem.Entry == null)
                {
                    menuItem.Entry = Entry.NullObject;
                }

                return View(new MenuItemViewModel()
                                {
                                    MenuItem = menuItem,
                                    MenuTypes = menuTypes
                                });
            }

            // load empty screen
            return View(new MenuItemViewModel()
                            {
                                MenuItem = MenuItem.NullObject,
                                MenuTypes = menuTypes
                            });
        }


        [AcceptVerbs(HttpVerbs.Post)]
        public RedirectToRouteResult EditMenuItem(int? id, MenuItem menuItem, FormCollection form)
        {
            this.LoadEntryToCurrentMenuItem(form,
                                            menuItem);

            menuItem.NavigateUrl = menuItem.NavigateUrl.NullSafe();
            this.serviceFactory.MenuService.SaveMenuItem(menuItem);
            this.FormatResultMessage(menuItem,
                                id);

            return RedirectToAction("EditMenuItem",
                                    "AdminMenuItem",
                                    new {id = menuItem.Id});
        }

        internal void FormatResultMessage(IMenuItem menuItem, int? id)
        {
            if (null == id || 0 == id)
            {
                TempData["SaveResult"] = string.Format("Items was successfully created with Id = {0}",
                                                       menuItem.Id);
            }
            else
            {
                TempData["SaveResult"] = "Items was successfully updated";
            }
        }

        internal void LoadEntryToCurrentMenuItem(NameValueCollection form, IMenuItem menuItem)
        {
            string entryId = form["MenuItem.EntryId"];
            if (!string.IsNullOrEmpty(entryId))
            {
                int parsedEntryId = Int32.Parse(entryId);
                menuItem.Entry = this.serviceFactory.EntryService.Load(parsedEntryId);
            }
        }


        internal List<SelectListItem> GetMenuTypes()
        {
            ICollection<IMenu> menues = this.serviceFactory.MenuService.LoadMenues();
            List<SelectListItem> menuTypes = new List<SelectListItem>();
            foreach (IMenu menu in menues)
            {
                menuTypes.Add(new SelectListItem() {Text = menu.Type, Value = menu.Id.ToString()});
            }
            return menuTypes;
        }
    }
}