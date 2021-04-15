using Blazing_Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blazing_Shop.Controllers
{
    public class CategoryController : Controller
    {
        public ApplicationDbContext _context;
        public CategoryController()
        {
            _context = new ApplicationDbContext();
        }
        // GET: Category
        public ActionResult Index()
        {
            var category = _context.Categories.ToList();
            return View(category);
        }

        public ActionResult Details(int id)
        {
            Category category = _context.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        public ActionResult New()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Category category)
        {
            if (category.Id == 0)
            {
                _context.Categories.Add(category);
            }
            else
            {
                var custInDB = _context.Categories.Single(c => c.Id == category.Id);
                custInDB.CName = category.CName;
            }
            _context.SaveChanges();
            return RedirectToAction("Index", "Category");
        }

        public ActionResult Edit(int id)
        {

            var updateCust = _context.Categories.SingleOrDefault(c => c.Id == id);
            return View("New", updateCust);

        }

        public ActionResult Popup()
        {

            ViewBag.Message = " ";

            return View();
        }

        public ActionResult Delete(int id)
        {

            Category category = _context.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Category category = _context.Categories.Find(id);
            _context.Categories.Remove(category);
            _context.SaveChanges();
            return RedirectToAction("Index", "Category");
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

    }
}