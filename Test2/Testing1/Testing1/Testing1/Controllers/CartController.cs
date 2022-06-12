using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Testing1.Models;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Net.Mail;
using RazorEngine;

namespace Testing1.Controllers
{
    public class CartController : Controller
    {
        private ShopContext context;

        public CartController(ShopContext ctx)
        {
            context = ctx;
        }

        public CartController() { }


        public System.Web.Mvc.ActionResult Default()
        {
            return View();
        }

        public System.Web.Mvc.ActionResult MyCart()
        {
            /*return View();*/
            return View("~/Views/Cart/MyCart.cshtml");
        }


        public System.Web.Mvc.ActionResult AddItem(int id)
        {
            ShopContext context = new ShopContext();
            Product it = context.Products.FirstOrDefault(c => c.ProductID == id);
            return View(it);
        }

        [Microsoft.AspNetCore.Mvc.HttpPost]
 
        public async Task addDB(Product pr)
        {
            context.Add(pr);
            await context.SaveChangesAsync();
        }

        /*==================================Add To CART=========================================*/


        /*================================== Buy =========================================*/
        public System.Web.Mvc.ActionResult Buy(int id)
        {
            ShopContext context = new ShopContext();
            if (Session["cart"] == null)
            {
                List<CartItem> cart = new List<CartItem>();
                cart.Add(new CartItem { Product = context.Products.Find(id), Quantity = 1 });
                Session["cart"] = cart;
            }
            else
            {
                List<CartItem> cart = (List<CartItem>)Session["cart"];
                int index = isExist(id);
                if (index != -1)
                {
                    cart[index].Quantity++;
                }
                else
                {
                    cart.Add(new CartItem { Product = context.Products.Find(id), Quantity = 1 });
                }
                Session["cart"] = cart;
            }
            return RedirectToAction("MyCart");


        }
        /*================================== Remove =========================================*/
        public System.Web.Mvc.ActionResult Remove(int id)
        {
            List<CartItem> cart = (List<CartItem>)Session["cart"];
            int index = isExist(id);
            cart.RemoveAt(index);
            Session["cart"] = cart;
            return RedirectToAction("MyCart");
        }

        private int isExist(int id)
        {
            List<CartItem> cart = (List<CartItem>)Session["cart"];
            for (int i = 0; i < cart.Count; i++)
                if (cart[i].Product.ProductID.Equals(id))
                    return i;
            return -1;
        }

        /*================================== Mail To Customer =========================================*/
        [Microsoft.AspNetCore.Mvc.HttpPost]
        public System.Web.Mvc.ActionResult BillMail(Testing1.Models.Mail objMail)
        {
            if (ModelState.IsValid)
            {
                
                MailMessage mail = new MailMessage();
                mail.IsBodyHtml = true;
                string billTotal = (TempData["Data1"]).ToString();
                string CurrentUsers = (TempData["Data2"]).ToString();
                mail.To.Add(CurrentUsers);
                mail.From = new MailAddress("10119230@mu.edu.lb");
                mail.Subject = "Techno Store Bill";
                string Body = "Thank you for buying from our Store.Your bill total is :"
                    +billTotal+" $.The Delivery will arrive in approximately 5 days from the order date.";
                mail.Body = Body;

                /*--To send Gmail using System:Net:Mail:SmtpClient,Port 587 require SMTP Authentication--*/
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.UseDefaultCredentials = false;
                // Enter sender User name and password
                smtp.Credentials = new System.Net.NetworkCredential("10119230@mu.edu.lb", "Moha671998");  

                /*SSL:It encrypts data sent in internet commonly between server & client so that it remains private*/
                smtp.EnableSsl = true;
                smtp.Send(mail);
                Session.Clear();
                return RedirectToAction("MyCart");
            }
            else
            {
                return RedirectToAction("MyCart");
            }
        }

    }
}
