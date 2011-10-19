namespace AtomicCms.Web.Controllers
{
    using System;
    using System.Security.Principal;
    using System.Threading;
    using System.Web.Mvc;
    using System.Web.Security;
    using AtomicCms.Core.ControllerHelpers;
    using Common.Abstract.DomainObjects;
    using Common.Abstract.Factories;

    [HandleError]
    public class AccountController : Controller
    {
        private readonly IServiceFactory _modelFactory;

        public AccountController(IServiceFactory _modelFactory)
        {
            this._modelFactory = _modelFactory;
        }


        public ActionResult LogOn()
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult LogOn(string userName, string password, bool rememberMe, string returnUrl)
        {
            if (!this.ValidateLogOn(userName, password))
            {
                return View();
            }

            FormsAuthentication.SetAuthCookie(userName, rememberMe);
            if (!String.IsNullOrEmpty(returnUrl))
            {
                return Redirect(returnUrl);
            }
            
            return RedirectToAction(ControllerNaming.Action<AdminController>(x=>x.Dashboard()), ControllerNaming.Name<AdminController>());
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction(ControllerNaming.Action<HomeController>(x=>x.Default()), ControllerNaming.Name<HomeController>());
        }


        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.User.Identity is WindowsIdentity)
            {
                throw new InvalidOperationException("Windows authentication is not supported.");
            }
        }

        #region Validation Methods

        private bool ValidateLogOn(string userName, string password)
        {
            if (String.IsNullOrEmpty(userName))
            {
                ModelState.AddModelError("username", "You must specify a username.");
            }
            if (String.IsNullOrEmpty(password))
            {
                ModelState.AddModelError("password", "You must specify a password.");
            }
            if (!this.IsValidateUserNameAndPassword(userName, password))
            {
                ModelState.AddModelError("password", "Password is not valid.");
                Thread.Sleep(5000);
            }

            return ModelState.IsValid;
        }

        private bool IsValidateUserNameAndPassword(string userName, string password)
        {
            IUser user = this._modelFactory.UserService.Login(userName, password);

            return user.IsValid;
        }

        #endregion
    }
}