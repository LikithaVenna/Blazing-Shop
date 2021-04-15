using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Blazing_Shop.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Price is required.")]
        [RegularExpression(@"[0-9]*\.?[0-9]+", ErrorMessage = "{0} must be a Number.")]
        public double Price { get; set; }
        [Required(ErrorMessage = "It is required.")]
        public string ShadeColour { get; set; }
        public byte[] Image { get; set; }
        public int CID { get; set; }
        [ForeignKey("CID")]
        public Category Category { get; set; }

       
        
        

    }
}