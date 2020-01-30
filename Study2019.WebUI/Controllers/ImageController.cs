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
        [HttpPost]
        public JsonResult UploadImage()
        {
            var imgIds = new List<int>();
            foreach (string fileName in Request.Files)
            {
                var file = Request.Files[fileName];

                var blobId = Guid.NewGuid();
                var ba = file.InputStream.ToByteArray();
                FileHelper.SaveFile(ba, blobId);
                var img = new Data.DTO.ImageDTO
                {
                    BlobId = blobId,
                    MimeType = file.ContentType,
                    UserName = User.Identity.Name
                };
                var id = Data.BLL.Db.CreateImage(img);
                imgIds.Add(id);                
            }

            return Json(imgIds);

        }
    }
}