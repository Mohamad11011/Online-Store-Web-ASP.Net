using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using Testing1.Models;



namespace Testing1.Controllers
{

    public class ProductController : Controller
    {
        public readonly ShopContext context;
        public List<Product> products { get; set; }
        public Product product1 { get; set; }
        public int id { get; set; }
        
        public ProductController(ShopContext ctx) { context = ctx;  }
       
        public ProductController()
        {
            products = new List<Product>();
            product1 = new Product();
        }

        public System.Web.Mvc.ActionResult Index()
        {
            return View();
        }

        [Microsoft.AspNetCore.Mvc.Route("[controller]s")]

        public System.Web.Mvc.ActionResult List()
        {
            ShopContext context = new ShopContext();
            products = context.Products.OrderBy(p => p.ProductID).ToList();
                return View(products);
                    
            // products to view
        }

        public System.Web.Mvc.ActionResult Laptop(string type = "Laptop")
        {

            ShopContext context = new ShopContext();
            products = context.Products
                    .Where(p => p.Category == type)
                    .OrderBy(p => p.ProductID).ToList();

            // products to view
            return View(products);
        }

        public System.Web.Mvc.ActionResult PC(string type = "PC")
        {
            ShopContext context = new ShopContext();
            products = context.Products
                    .Where(p => p.Category == type)
                    .OrderBy(p => p.ProductID).ToList();

            // products to view
            return View(products);
        }

        public System.Web.Mvc.ActionResult Accessory(string type = "Accessory")
        {

            ShopContext context = new ShopContext();
            products = context.Products
                    .Where(p => p.Category == type)
                    .OrderBy(p => p.ProductID).ToList();

            // products to view
            return View(products);
        }

        public System.Web.Mvc.ActionResult Details(int id)
        {
            
            ShopContext context = new ShopContext();
             product1 = context.Products.Find(id);
            

            ViewBag.imageURL = product1.imageURL;
            

            return View(product1);
        }

    }

   
}