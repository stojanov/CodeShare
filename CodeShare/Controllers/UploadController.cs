using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using CodeShare.Models;

namespace CodeShare.Controllers
{
    public class UploadController : Controller
    {
        private readonly ApplicationDbContext _dbCtx = new ApplicationDbContext();

        [HttpGet]
        [AllowAnonymous]
        public ActionResult PublicFile()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult PublicFile(HttpPostedFileBase file)
        {
            try
            {
                if (file.ContentLength > 0)
                {
                    var dir = Server.MapPath(Constants.PublicDir);

                    string FileName = Path.GetFileName(file.FileName);
                    string path = Path.Combine(dir, FileName);

                    _dbCtx.PublicUploads.Add(new FileModel
                    {
                        CreatedAt = DateTime.Now, 
                        Name = FileName, 
                        Location = Path.Combine(Constants.PublicDir, FileName)
                    });

                    _dbCtx.SaveChanges();

                    file.SaveAs(path);
                }

                ViewBag.Message = "File Uploaded";

                return View();
            }
            catch
            {
                ViewBag.Message = "File upload failed!";

                return View();
            }
        }

        [HttpGet]
        [Authorize]
        public ActionResult File()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public ActionResult File(HttpPostedFileBase file)
        {
            try
            {
                if (file.ContentLength > 0)
                {
                    var rawdir = Constants.GetDir(User.Identity.Name);

                    var dir = Server.MapPath(rawdir);

                    if (!Directory.Exists(dir))
                    {
                        Directory.CreateDirectory(dir);
                    }

                    string FileName = Path.GetFileName(file.FileName);
                    string path = Path.Combine(dir, FileName);
                    file.SaveAs(path);

                    _dbCtx.PrivateUploads.Add(new PrivateFileModel()
                    {
                        CreatedAt = DateTime.Now,
                        Name = FileName,
                        Location = Path.Combine(rawdir, FileName),
                        Owner = User.Identity.Name
                    });

                    _dbCtx.SaveChanges();

                }

                ViewBag.Message = "File Uploaded";

                return View();
            }
            catch
            {
                ViewBag.Message = "File upload failed!";

                return View();
            }
        }
    }
}