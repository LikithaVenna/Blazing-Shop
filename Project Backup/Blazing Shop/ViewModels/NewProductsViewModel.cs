using Blazing_Shop.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Blazing_Shop.ViewModels
{
    public class NewProductsViewModel
    {

        public IEnumerable<Category> Categories { get; set; }

        public int ?Id { get; set; }
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Price is required.")]
        [RegularExpression(@"[0-9]*\.?[0-9]+", ErrorMessage = "{0} must be a Number.")]
        public double ?Price { get; set; }
        [Required(ErrorMessage = "It is required.")]
        public string ShadeColour { get; set; }
        public byte[] Image { get; set; }
        public int ?CID { get; set; }

        public NewProductsViewModel()
        {
            Id = 0;
        }

        public NewProductsViewModel(Product product)
        {
            Id = product.Id;
            Name = product.Name;
            Price = product.Price;
            ShadeColour = product.ShadeColour;
            Image = product.Image;
            CID = product.CID;
        }



    }
}