using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//using BoutiqueDataConnection.DataComponents;
using BoutiqueDataConnection.DataLayer;
using BoutiqueDataConnection.Entities;

namespace Boutique.Controllers
{
    public class BoutiqueController : Controller
    {
        // GET: Boutique
        public ActionResult GetAllProducts()
        {
            //Get the Model data
            var connection= ProductFactory.CreateComponent(); 
            var model = connection.GetAllProducts();
            //Send it to the view
            return View(model);
        }
       
        public ActionResult UpdateProduct(string id)
        {
            int productId = Convert.ToInt32(id);
            var connection = ProductFactory.CreateComponent();
            try
            {
                return View();
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpPost]
        public ActionResult UpdateProduct(ProductClass product)
        {
            var component = ProductFactory.CreateComponent();
            try
            {
                component.UpdateProduct(product);
                return RedirectToAction("GetAllProducts");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ActionResult AddNewProduct()
        {
            return View(new ProductClass());
        }
        [HttpPost]
        public ActionResult AddNewProduct(ProductClass product)
        {
            var connection = ProductFactory.CreateComponent();
            try
            {
                connection.AddNewProduct(product);
                //throw new Exception("Testing Error!!!");
                return RedirectToAction("GetAllProducts");
                //return View(new ProductClass());
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                ViewBag.ErrorMessage = message;
                return View(new ProductClass());
            }
        }
    }
}