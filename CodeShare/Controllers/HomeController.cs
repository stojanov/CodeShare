using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CodeShare.Models;

namespace CodeShare.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _dbCtx = new ApplicationDbContext();

        [AllowAnonymous]
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated) return RedirectToAction("Main");

            return View();
        }

        [Authorize]
        public ActionResult Main() => View();

        [AllowAnonymous]
        public ActionResult ViewPublicFiles() => View(_dbCtx.PublicUploads);

        [AllowAnonymous]
        public ActionResult ViewFile(int id)
        {
            var fileData = _dbCtx.PublicUploads.Single(file => file.Id == id);

            var filePath = Server.MapPath(fileData.Location);

            if (!System.IO.File.Exists(filePath)) return View("NotFound");

            var fileContent = System.IO.File.ReadAllText(filePath);

            return View(new ViewFileModel{ Data = fileContent, Details = fileData});
        }

        [Authorize]
        public ActionResult ViewMyFile(int id)
        {
            
            PrivateFileModel fileData;

            try
            {
                fileData = _dbCtx.PrivateUploads.Single(upload => upload.Id == id);
            }
            catch (InvalidOperationException e)
            {
                return View("NotFound");
            }

            var filePath = Server.MapPath(fileData.Location);

            if (!System.IO.File.Exists(filePath)) return View("NotFound");

            var fileContent = System.IO.File.ReadAllText(filePath);

            return View(new ViewFileModel { Data = fileContent, Details = new FileModel
            {
                Location = fileData.Location, 
                CreatedAt = fileData.CreatedAt,
                Name = fileData.Name
            } });
        }

        [Authorize]
        public ActionResult ViewMyFiles() => View(_dbCtx.PrivateUploads.Where(upload => upload.Owner == User.Identity.Name));
    }


}