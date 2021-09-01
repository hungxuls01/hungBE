using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Service.Controllers
{
    public class UploadController : Controller
    {
        // GET: Upload
        [HttpPost]
        public ActionResult UploadImage(HttpPostedFileBase file, string CKEditorFuncNum, string CKEditor, string langCode)
        {
            if (file.ContentLength <= 0)
                return null;

            // here logic to upload image
            // and get file path of the image

            const string uploadFolder = "Assets/img/";

            var fileName = Path.GetFileName(file.FileName);
            var path = Path.Combine(Server.MapPath(string.Format("~/{0}", uploadFolder)), fileName);
            file.SaveAs(path);

            var url = string.Format("{0}{1}/{2}/{3}", Request.Url.GetLeftPart(UriPartial.Authority),
                Request.ApplicationPath == "/" ? string.Empty : Request.ApplicationPath,
                uploadFolder, fileName);

            // passing message success/failure
            const string message = "Image was saved correctly";

            // since it is an ajax request it requires this string
            var output = string.Format(
                "<html><body><script>window.parent.CKEDITOR.tools.callFunction({0}, \"{1}\", \"{2}\");</script></body></html>",
                CKEditorFuncNum, url, message);

            return Content(output);
        }
    }
}