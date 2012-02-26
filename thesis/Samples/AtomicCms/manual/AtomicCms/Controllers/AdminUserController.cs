namespace AtomicCms.Web.Controllers
{
    using System.Web.Mvc;
    using AtomicCms.Core.ControllerHelpers;
    using AtomicCms.Core.DomainObjectsImp;
    using Common.Abstract.DomainObjects;
    using Common.Abstract.Factories;

    [Authorize]
    public class AdminUserController : Controller
    {
        private readonly IServiceFactory serviceFactory;

        public AdminUserController(IServiceFactory serviceFactory)
        {
            this.serviceFactory = serviceFactory;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ViewResult ListAll()
        {
            return View(this.serviceFactory.UserService.LoadAll());
        }

        public ViewResult EditUser(int id)
        {
            IUser user = this.serviceFactory.UserService.Load(id);
            return View(user);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditUser(User user, FormCollection forms)
        {
            user.Password = forms.Get("Password");

            this.serviceFactory.UserService.Update(user);
            return RedirectToAction(ControllerNaming.Action<AdminUserController>(x => x.EditUser(user.Id)),
                                    ControllerNaming.Name<AdminUserController>(),
                                    new {id = user.Id});
        }
    }
}