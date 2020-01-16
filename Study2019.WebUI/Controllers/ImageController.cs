using Study2019.WebUI.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Study2019.WebUI.Controllers
{
    public class ImageController : Controller
    {
        // GET: Image
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Get(int? id)
        {
            if (id.HasValue)
            {
                var image = Data.BLL.Db.GetImage(id.Value);
                var ba = FileHelper.ReadFile(image.BlobId);
                return File(ba, image.MimeType);
            }
            else
                return HttpNotFound("File not found");
        }
    }
}