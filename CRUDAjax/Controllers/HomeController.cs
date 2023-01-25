using CRUDAjax.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace CRUDAjax.Controllers
{
    public class HomeController : Controller
    {
        ProductDB prdDB = new ProductDB();
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult List()
        {
            return Json(prdDB.ListAll(), JsonRequestBehavior.AllowGet);
        }
        public JsonResult Add(Product prd)
        {
            return Json(prdDB.Add(prd), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetbyID(int ID)
        {
            var Product = prdDB.ListAll().Find(x => x.ProductId.Equals(ID));
            return Json(Product, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Update(Product prd)
        {
            return Json(prdDB.Update(prd), JsonRequestBehavior.AllowGet);
        }
        public JsonResult Delete(int ID)
        {
            return Json(prdDB.Delete(ID), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Upload(HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
            {
                var fileName = Path.GetFileName(file.FileName);
                var ftpServer = "ftp://127.0.0.1/images/";
                var ftpUsername = "example";
                var ftpPassword = "123123";

                // Create a new FTP request
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ftpServer + fileName);
                request.Method = WebRequestMethods.Ftp.UploadFile;
                request.Credentials = new NetworkCredential(ftpUsername, ftpPassword);

                // Read the image data into a byte array
                byte[] fileData = new byte[file.InputStream.Length];
                file.InputStream.Read(fileData, 0, fileData.Length);

                // Write the image data to the request stream
                Stream requestStream = request.GetRequestStream();
                requestStream.Write(fileData, 0, fileData.Length);
                requestStream.Close();

                // Get the response from the FTP server
                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                return Json(response);

                // Check that the upload was successful
                /*if (response.StatusCode == FtpStatusCode.ClosingData)
                {
                    // Save the image name in the database
                    var image = new ImageModel
                    {
                        Name = fileName
                    };
                    _db.Images.Add(image);
                    _db.SaveChanges();
                    response.Close();
                    return Json("Image uploaded successfully!");
                }
                else
                {
                    response.Close();
                    return Json("Upload failed: " + response.StatusDescription);
                }*/
            }
            else
            {
                return Json("No file uploaded.");
            }
            return Json("Maybe");
        }
    }
}