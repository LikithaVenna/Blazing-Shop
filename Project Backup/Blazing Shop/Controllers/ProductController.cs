using Blazing_Shop.Models;
using Blazing_Shop.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blazing_Shop.Controllers
{
    public class ProductController : Controller
    {
        public ApplicationDbContext _context;
        public ProductController()
        {
            _context = new ApplicationDbContext();
        }

        // GET: Product
        public ActionResult Index()
        {
            var products = _context.Products.Include(c => c.Category).ToList();
            return View(products);
        }

        public ActionResult Details(int id)
        {
            var product = _context.Products.Include(c=>c.Category).SingleOrDefault(p => p.Id == id);
            return View(product);
        }

        public ActionResult Create()
        {
            var categories = _context.Categories.ToList();
            var vm = new NewProductsViewModel
            {
                Categories = categories
            };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Product product)
        {
            HttpPostedFileBase file = Request.Files["ImageData"];
            product.Image = ConvertToBytes(file);
            if (!ModelState.IsValid)
            {
                var vm = new NewProductsViewModel(product)
                {
                    Categories = _context.Categories.ToList()
                };
                return View("Create", vm);
            }

            if (product.Id == 0)
            {
                _context.Products.Add(product);
            }
            else
            {
                var prodInDb = _context.Products.Single(p => p.Id == product.Id);
                prodInDb.Name = product.Name;
                prodInDb.Price = product.Price;
                prodInDb.ShadeColour = product.ShadeColour;
                prodInDb.Image = product.Image;
                prodInDb.CID = product.CID;
            }
            _context.SaveChanges();
            return RedirectToAction("Index", "Product");

        }


        public ActionResult Edit(int id) {
            var product = _context.Products.SingleOrDefault(p => p.Id == id);
            if (product==null)
                return HttpNotFound();
            var vm = new NewProductsViewModel (product)
            {
                Categories=_context.Categories.ToList()
            };
            return View("Create", vm);
        }


        public ActionResult Delete(int id)
        {

            Product product = _context.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = _context.Products.Find(id);
            _context.Products.Remove(product);
            _context.SaveChanges();
            return RedirectToAction("Index", "Product");
        }



        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }



        public byte[] ConvertToBytes(HttpPostedFileBase image)
        {
            byte[] imageBytes;
            BinaryReader reader = new BinaryReader(image.InputStream);
            imageBytes = reader.ReadBytes((int)image.ContentLength);
            return imageBytes;
        }
        public ActionResult RetrieveImage(int id)
        {
            byte[] cover;
            var q = from p in _context.Products where p.Id == id select p.Image;
            cover = q.First();
            if (cover != null)
            {
                return File(cover, "image/jpg");
            }
            else
            {
                return null;
            }
        }
    }
}

