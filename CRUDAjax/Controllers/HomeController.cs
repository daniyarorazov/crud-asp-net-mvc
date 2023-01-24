using CRUDAjax.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}