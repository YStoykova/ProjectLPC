using Lopoca.Core;
using Lopoca.Web.Models;
using Microsoft.AspNet.Identity;
using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LopocaFile = Lopoca.Data.Models.File;

namespace Lopoca.Web.Controllers
{
    [Authorize]
    public class FileController : Controller
    {
        private readonly IFileManager fileManager;

        public FileController(IFileManager fileManager)
        {
            this.fileManager = fileManager;
        }

        /// <summary>
        /// Index view
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View(new UploadViewModel());
        }

        /// <summary>
        /// List with files.
        /// </summary>
        /// <returns></returns>
        public ActionResult List()
        {
            var userId = System.Web.HttpContext.Current.User.Identity.GetUserId();
            var files = fileManager.FindBy(userId).ToList();

            var fileViewModel = files.Select(f => new FileViewModel
            {
                Id = f.Id,
                Name = f.Name,
                Path = f.Path,
                UserId = f.UserId,
            });

            return View(fileViewModel);
        }


        /// <summary>
        /// Upload the file
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Upload(UploadViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userId = System.Web.HttpContext.Current.User.Identity.GetUserId();

                if (model.File != null && model.File.ContentLength > 0)
                {
                    model.FileId = Guid.NewGuid();
                    model.UserId = userId;
                    model.FileName = Path.GetFileNameWithoutExtension(model.File.FileName);
                    model.FullPath = "~/Data/" + model.FileId + Path.GetExtension(model.File.FileName);

                    LopocaFile file = new LopocaFile
                    {
                        Id = model.FileId,
                        UserId = model.UserId,
                        Name = model.FileName,
                        Path = model.FullPath,
                    };
                    try
                    {
                        model.File.SaveAs(Server.MapPath(model.FullPath));
                        fileManager.Upload(file);
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("", ex.Message);
                        return View("Index", model);
                    }
                }
                return RedirectToAction("List");
            }
            else
            {
                return View("Index", model);
            }
        }

        /// <summary>
        /// Display a file history
        /// </summary>
        /// <param name="fileId"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult History(string fileId)
        {
            if (String.IsNullOrEmpty(fileId))
            {
                return PartialView("History");
            }
            Guid fileGuid = new Guid(fileId);
            var histories = fileManager.GetFileHistoryBy(fileGuid);

            var model = histories.Select(h => new FileHistoryViewModel
            {
                UserId = h.UserId,
                ActionTime = h.ActionTime,
                StatusTypeId = h.StatusTypeId
            });

            return PartialView("History", model);
        }


        /// <summary>
        /// Save file status. 
        /// </summary>
        /// <param name="fileId"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Export(string fileId)
        {
            LopocaFile file = fileManager.FindFileBy(fileId);

            var userId = System.Web.HttpContext.Current.User.Identity.GetUserId();   
            //set history record with status type "Open"
            fileManager.AddHistory(file.Id, userId);

            return Content(fileId);
        }

        /// <summary>
        /// Download a file with the original name
        /// </summary>
        /// <param name="fileId"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Download(string fileId)
        {
            if (String.IsNullOrEmpty(fileId))
            { 
                return Content("Empty file name."); 
            }
            LopocaFile file = fileManager.FindFileBy(fileId);
            if (file == null)
            {
                return Content("The file not exists."); 
            }

            string filepath = Server.MapPath(file.Path);
            string contentType = MimeMapping.GetMimeMapping(filepath);

            return File(filepath, contentType, file.Name);
        }

        /// <summary>
        /// Soft delete.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Delete(string fileId)
        {
            if (String.IsNullOrEmpty(fileId))
            {
                return Content("Empty file name.");
            }
            Guid fileGuid = new Guid(fileId);
            var userId = System.Web.HttpContext.Current.User.Identity.GetUserId();
            fileManager.Delete(fileGuid, userId);

            return JavaScript("location.reload(true)");
        }
    }
}