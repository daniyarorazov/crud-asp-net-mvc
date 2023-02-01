using CRUDAjax.Models;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
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

        // This method returns the View
        public ActionResult Index()
        {
            return View();
        }

        // This method returns a JSON object with the result of the "ListAll" method of "prdDB"
        public JsonResult List()
        {
            return Json(prdDB.ListAll(), JsonRequestBehavior.AllowGet);
        }

        // This method returns a JSON object with the result of the "Add" method of "prdDB" with a parameter of "Product prd"
        public JsonResult Add(Product prd)
        {
            return Json(prdDB.Add(prd), JsonRequestBehavior.AllowGet);
        }

        // This method returns a JSON object of a Product that matches the specified ID from the result of the "ListAll" method of "prdDB"
        public JsonResult GetbyID(int ID)
        {
            var Product = prdDB.ListAll().Find(x => x.ProductId.Equals(ID));
            return Json(Product, JsonRequestBehavior.AllowGet);
        }

        // This method returns a JSON object with the result of the "Update" method of "prdDB" with a parameter of "Product prd"
        public JsonResult Update(Product prd)
        {
            return Json(prdDB.Update(prd), JsonRequestBehavior.AllowGet);
        }

        // This method returns a JSON object with the result of the "Delete" method of "prdDB" with a parameter of "ID"
        public JsonResult Delete(int ID)
        {
            return Json(prdDB.Delete(ID), JsonRequestBehavior.AllowGet);
        }

        
        // Method for upload file
        [HttpPost]
        public JsonResult Upload(HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
            {
                // Add all credentials for FTP server
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

                // Check that the upload was successful
                if (response.StatusCode == FtpStatusCode.ClosingData)
                {   
                    // Saving path
                    string filePath = Path.Combine(Server.MapPath("~/Data/images/"), file.FileName);
                    file.SaveAs(filePath);
                    return Json("Image uploaded successfully!");
                }
                else
                {
                    response.Close();
                    return Json("Upload failed: " + response.StatusDescription);
                }
            }
            else
            {
                return Json("No file uploaded.");
            }
        }

        // This method deletes an image file and returns a JSON object with a success message
        public JsonResult DeleteImage(HttpPostedFileBase file)
        {
            // Get the file path
            var filePath = Server.MapPath("~/Data/images" + file.FileName);
            // Delete the file
            System.IO.File.Delete(filePath);
            // Return success message
            return Json("Image uploaded successfully!");
        }
    }
}