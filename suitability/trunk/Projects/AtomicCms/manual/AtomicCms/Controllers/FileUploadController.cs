namespace AtomicCms.Web.Controllers
{
    using System.Collections.Generic;
    using System.IO;
    using System.Web;
    using System.Web.Mvc;
    using Common.Abstract.Factories;
    using Common.Abstract.Models;
    using Core.Configuration;

    [Authorize]
    public class FileUploadController : Controller
    {
        private readonly IServiceFactory serviceFactory;

        public FileUploadController(IServiceFactory serviceFactory)
        {
            this.serviceFactory = serviceFactory;
        }

        //
        // GET: /FileUpload/

        public ActionResult Index()
        {
            IFileUploadService fileUploadService = this.serviceFactory.FileUploadService;
            string folder = string.Format("{0}{1}/",
                                          Server.MapPath("~/"),
                                          Configuration.UploadsFolder);
            ICollection<FileInfo> files = fileUploadService.GetFiles(folder);
            return View(files);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult UploadFile(FormCollection forms)
        {
            HttpPostedFileBase file = Request.Files["fileUpload"];

            if (null != file)
            {
                string fullPath = this.GetFullPath(file.FileName);
                if (!string.IsNullOrEmpty(fullPath))
                {
                    file.SaveAs(fullPath);
                }
            }

            return RedirectToAction("Index");
        }

        private string GetFullPath(string fileName)
        {
            return string.Format("{0}\\{1}", Server.MapPath("~/" + Configuration.UploadsFolder), fileName);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult DeleteFile(string fileName)
        {
            if (null != fileName)
            {
                string fullPath = this.GetFullPath(fileName);
                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }
            }
            return RedirectToAction("Index");
        }
    }
}