using System.Threading.Tasks;
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

            //Getting the OAuth request Url
            var uri = await _dropboxService.AuthorizeAsync();
            Response.Redirect(uri.ToString());

            //
            return View();
        }

        public async Task<ActionResult> SetCode(string code, string error = "")
        {
            //In dropbox account we set Redirect URI for this Action
            // this method exchange the Authorization Code with Access/Refresh tokens
            await _dropboxService.SetCode(code, error);

            //save in session access code so wen we try to redirect to root of Site http://localhost:19760 it wouldn't throw error that we already have acc. token
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
            //Return files and folders by path
            var files = await _dropboxService.FindFilesByPath(path);
            return PartialView("_FilesPartial", files);
        }
    }
}