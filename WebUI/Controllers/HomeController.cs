﻿using System.Threading.Tasks;
using System.Web.Mvc;
using BLL.IService;

namespace WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDropboxService _dropboxService;

        public HomeController(IDropboxService dropboxService)
        {
            _dropboxService = dropboxService;
        }

        public async Task<ActionResult> Index()
        {
            if (Session["dropbox"] != null)
              return RedirectToAction("DropBox");

            var uri = await _dropboxService.AuthorizeAsync();
            Response.Redirect(uri.ToString());

            return View();
        }

        public async Task<ActionResult> SetCode(string code, string error = "")
        {
            await _dropboxService.SetCode(code, error);
            Session["dropbox"] = code;
            return RedirectToAction("DropBox");
        }

        public ActionResult DropBox()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> DropBox(string path)
        {
            var files = await _dropboxService.FindFilesByPath(path);
            return PartialView("_FilesPartial", files);
        }
    }
}