using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace Service.Controllers
{
    public class fileuploadController : ApiController
    {
        private static Logger _logger;
        public fileuploadController()
        {
            _logger = LogManager.GetCurrentClassLogger();
        }

        [HttpPost]
        public Task<HttpResponseMessage> PostNotUser(string ckEditorFuncNum)
        {
            var savedFilePath = "";
            // Check if the request contains multipart/form-data
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }
            var url = "/Public/Images/" + DateTime.Now.Day + "_" + DateTime.Now.Month + "_" + DateTime.Now.Year;
            string rootPath = HttpContext.Current.Server.MapPath("~" + url);
            if (!Directory.Exists(rootPath))
                Directory.CreateDirectory(rootPath);
            //Get the path of folder where we want to upload all files.
            var provider = new MultipartFileStreamProvider(rootPath);
            // Read the form data.
            //If any error(Cancelled or any fault) occurred during file read , return internal server error
            var output = "";
            var task = Request.Content.ReadAsMultipartAsync(provider).
                ContinueWith<HttpResponseMessage>(t =>
                {
                    if (t.IsCanceled || t.IsFaulted)
                    {
                        Request.CreateErrorResponse(HttpStatusCode.InternalServerError, t.Exception);
                    }
                    try
                    {
                        foreach (MultipartFileData dataitem in provider.FileData)
                        {
                            if (dataitem.Headers.ContentType != null)
                            {
                                //Replace / from file name
                                string name = dataitem.Headers.ContentDisposition.FileName.Replace("\"", "").ToLower();
                                //Create New file name using GUID to prevent duplicate file name
                                string time = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeMilliseconds().ToString();
                                string newFileName = Path.GetFileNameWithoutExtension(name) + time + Path.GetExtension(name);
                                //Move file from current location to target folder.
                                if (File.Exists(Path.Combine(rootPath, newFileName)))
                                {
                                    File.Delete(Path.Combine(rootPath, newFileName));
                                }
                                File.Move(dataitem.LocalFileName, Path.Combine(rootPath, newFileName));
                                if (savedFilePath == "")
                                {
                                    savedFilePath = "https://localhost:44395" + url + "/" + newFileName;
                                }
                                else
                                {
                                    savedFilePath = savedFilePath + ";" + url + "/" + newFileName;
                                }
                            }

                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.Error(ex.Message);
                    }
                    var message = "Saved!";
                    output = savedFilePath;
                    output = string.Format("<html><body><script>window.parent.CKEDITOR.tools.callFunction({0}, '{1}', '{2}');</script></body></html>",
                    ckEditorFuncNum,
                    savedFilePath,
                    message);
                    return Request.CreateResponse(HttpStatusCode.Created, output);
                });
            return task;
        }


        [HttpPost]
        public Task<HttpResponseMessage> UploadMutiFile()
        {
            var savedFilePath = "";
            // Check if the request contains multipart/form-data
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }
            var url = "/Public/Images/" + DateTime.Now.Day + "_" + DateTime.Now.Month + "_" + DateTime.Now.Year;
            string rootPath = HttpContext.Current.Server.MapPath("~" + url);
            if (!Directory.Exists(rootPath))
                Directory.CreateDirectory(rootPath);
            //Get the path of folder where we want to upload all files.
            var provider = new MultipartFileStreamProvider(rootPath);
            // Read the form data.
            //If any error(Cancelled or any fault) occurred during file read , return internal server error
            var task = Request.Content.ReadAsMultipartAsync(provider).
                ContinueWith<HttpResponseMessage>(t =>
                {
                    if (t.IsCanceled || t.IsFaulted)
                    {
                        Request.CreateErrorResponse(HttpStatusCode.InternalServerError, t.Exception);
                    }
                    try
                    {
                        foreach (MultipartFileData dataitem in provider.FileData)
                        {
                            if (dataitem.Headers.ContentType != null)
                            {
                                //Replace / from file name
                                string name = dataitem.Headers.ContentDisposition.FileName.Replace("\"", "").ToLower();
                                //Create New file name using GUID to prevent duplicate file name
                                string time = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeMilliseconds().ToString();
                                string newFileName = Path.GetFileNameWithoutExtension(name) + time + Path.GetExtension(name);
                                //Move file from current location to target folder.
                                if (File.Exists(Path.Combine(rootPath, newFileName)))
                                {
                                    File.Delete(Path.Combine(rootPath, newFileName));
                                }
                                File.Move(dataitem.LocalFileName, Path.Combine(rootPath, newFileName));
                                if (savedFilePath == "")
                                {
                                    savedFilePath = url + "/" + newFileName;
                                }
                                else
                                {
                                    savedFilePath = savedFilePath + ";" + url + "/" + newFileName;
                                }
                            }

                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.Error(ex.Message);
                    }
                    return Request.CreateResponse(HttpStatusCode.Created, savedFilePath);
                });
            return task;
        }
    }
}
