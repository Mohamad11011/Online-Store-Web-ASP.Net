using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Testing1.Areas.Admin.Controllers;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Testing1.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json.Linq;
using System.Web.Mvc;
using System.Configuration;
using System.Web.Helpers;

/// <Main>
/// ///////-------Web-Asp-.netframework of template MVC-------////////////////
/// </Main>
namespace Testing1.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {

        public readonly IHostingEnvironment _enviroment;
        public ProductController(IHostingEnvironment enviroment)
        { _enviroment = enviroment; }

        public List<Product> products { get; set; }

        public ProductController()
        { products = new List<Product>(); }


        [Microsoft.AspNetCore.Mvc.Route("[area]/[controller]s/{id?}")]
        public System.Web.Mvc.ActionResult List()
        {
            ShopContext context = new ShopContext();
            products = context.Products.ToList();  
            return View(products);

        }

        //Add Item**********************//

        [Microsoft.AspNetCore.Mvc.HttpGet]
        public System.Web.Mvc.ActionResult AddItem()
        {
            return View();
        }

        [Microsoft.AspNetCore.Mvc.HttpPost]
        public async Task<System.Web.Mvc.ActionResult> AddItem1(VProduct acc)
        {
            if (!checkName(acc.Name))
            {
                ShopContext context = new ShopContext();
                Product pro = registerDB(acc);

                var fileName = Guid.NewGuid().ToString().Replace("-", "") + "_" + Path.GetFileName(acc.Image.FileName);
                var path = Path.Combine(Server.MapPath("~/UploadedPhotos"), fileName);
                acc.Image.SaveAs(path);
                pro.imageURL = fileName;
                context.Products.Update(pro);
                await context.SaveChangesAsync();
                return RedirectToAction("List", "Product", new { area = "Admin" });
            }

            return RedirectToAction("AddItem", "Product", new { area = "Admin" });
        }

        public bool checkName(string un)
        {
            ShopContext context = new ShopContext();
            var checker = context.Products.Where(x => x.Name.ToLower() == un.ToLower()).FirstOrDefault();

            if (checker != null)
            {
                return true;
            }
            return false;
        }

        public Product registerDB(VProduct firstTime)
        {
            ShopContext context = new ShopContext();
            Product ac = new Product();

            ac.Name = firstTime.Name;
            ac.Category = firstTime.Category;
            ac.Price = firstTime.Price;
            context.Add(ac);
            context.SaveChanges();
            return ac;
        }

        //Add Item************************


        //Remove Item**********************//

        public System.Web.Mvc.ActionResult Delete(int id)
        {
            ShopContext context = new ShopContext();
            var product = context.Products.Find(id);
            context.Remove(product);
            context.SaveChanges();

            return RedirectToAction("List", "Product", new { area = "Admin" });
        }

        //Remove Item************************


        //Update Item**********************//
        public System.Web.Mvc.ActionResult Update(int id)
        {
            ShopContext context = new ShopContext();
            Product it = new Product();
            it = context.Products.FirstOrDefault(c => c.ProductID == id);
            return View(it);
        }

        [System.Web.Mvc.HttpPost]
        public async Task<System.Web.Mvc.ActionResult> Update(Product item)
        {
            ShopContext context = new ShopContext();
            await updateDB(item);
            return RedirectToAction("List", "Product", new { area = "Admin" });
        }

        public async Task updateDB(Product pr)
        {
            ShopContext context = new ShopContext();
            context.Update(pr);
            await context.SaveChangesAsync();
        }

        //Update Item************************

        //Search Item************************

        public System.Web.Mvc.ActionResult SearchItem()
        {
            return View();
        }

        [System.Web.Mvc.HttpGet]
        public System.Web.Mvc.ActionResult List(string search)
        {
            ShopContext context = new ShopContext();
            if (search != null) 
            { 
                return View(products = context.Products.Where(x =>  x.Name == search).ToList());
            }
            else
            {
               return View (products = context.Products.OrderBy(p => p.ProductID).ToList());
            }

        }
    }

}