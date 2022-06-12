using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using System.Web;

namespace Testing1.Models
{
    public class VProduct
    {
        public VProduct() { }
        public int ProductID { get; set; }

        
        [Required(ErrorMessage = "Please enter a product name.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter a product price.")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }


        [Required(ErrorMessage = "Please select a category.")]
        public string Category { get; set; }
        public HttpPostedFileBase Image {get;set;}
        
    }
}