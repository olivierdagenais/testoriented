namespace AtomicCms.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Web.Mvc;
    using AtomicCms.Core.DomainObjectsImp;
    using Common.Abstract.DomainObjects;
    using Common.Abstract.Factories;
    using Common.Exceptions;

    [Authorize]
    public class AdminEntryController : Controller
    {
        internal readonly IServiceFactory modelFactory;

        public AdminEntryController(IServiceFactory modelFactory)
        {
            this.modelFactory = modelFactory;
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateInput(false)]
        public ActionResult EditEntry(int? id, Entry entry)
        {
            if (null != id)
            {
                return this.UpdateEntry(entry);
            }

            return this.CreateNewEntry(entry);
        }

        private ActionResult CreateNewEntry(IEntry entry)
        {
            this.modelFactory.EntryService.CreateEntry(entry);
            TempData["SaveResult"] = "Items was successfully saved with Id = " + entry.Id;

            return RedirectToAction("EditEntry",
                                    "AdminEntry",
                                    new {id = entry.Id});
        }

        private ViewResult UpdateEntry(IEntry entry)
        {
            this.modelFactory.EntryService.Save(entry);
            TempData["SaveResult"] = "Items was successfully saved";
            return View(entry);
        }

        public ViewResult EditEntry(int? id)
        {
            if (null != id)
            {
                return this.LoadEntry(id.Value);
            }

            return View(Entry.NullObject);
        }

        private ViewResult LoadEntry(int id)
        {
            IEntry entry = this.modelFactory.EntryService.Load(id);
            return View(entry);
        }

        public ViewResult EntryList()
        {
            ICollection<IEntry> allPages = this.modelFactory.EntryService.LoadAll();
            return View(allPages);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Delete(int id)
        {
            modelFactory.EntryService.Delete(id);
            return RedirectToAction("EntryList", "AdminEntry");
        }
    }
}