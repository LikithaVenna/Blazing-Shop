using Blazing_Shop.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Blazing_Shop.Controllers.api
{
    public class ProductController : ApiController
    {

        private ApplicationDbContext db;
        public ProductController()
        {
            db = new ApplicationDbContext();
        }

        //Get /api/appointmentapi
        public IEnumerable<Product> GetProducts()
        {
            return db.Products.Include(c => c.Category).ToList();


        }

        public Product GetProduct(int id)
        {
            var product = db.Products.Include(c => c.Category).SingleOrDefault(c => c.Id == id);
            if (product == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return product;
        }

        //Post /api/appointmentapi
        [HttpPost]
        public Product CreateProduct(Product product)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            db.Products.Add(product);
            db.SaveChanges();
            return product;
        }

        //Put /api/customerapi/1
        [HttpPut]
        public void UpdateProduct(int id, Product product)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            var prodInDb = db.Products.Include(c => c.Category).SingleOrDefault(c => c.Id == id);
            if (prodInDb == null)
            {

                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            prodInDb.Name = product.Name;
            prodInDb.Price = product.Price;
            prodInDb.ShadeColour = product.ShadeColour;
            prodInDb.Image = product.Image;
            prodInDb.CID = product.CID;
            

            db.SaveChanges();

        }

        //Delete /api/appointmentapi/1

        public void DeleteProduct(int id)
        {

            var prodInDb = db.Products.SingleOrDefault(c => c.Id == id);
            if (prodInDb == null)
            {

                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            db.Products.Remove(prodInDb);
            db.SaveChanges();

        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
        }

    }
}
