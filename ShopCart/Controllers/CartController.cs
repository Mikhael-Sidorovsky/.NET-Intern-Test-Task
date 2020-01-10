using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using ShopCart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopCart.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext()
                    .GetUserManager<ApplicationUserManager>()
                    .FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
                List<Cart> listCarts = Session["cart"] as List<Cart> ?? new List<Cart>();
                if (listCarts.Count == 0 || Session["cart"] == null)
                {
                    ViewBag.Message = "Your cart is empty!";
                    return View();
                }
                decimal toPay = 0;
                decimal total = 0;
                foreach (var el in listCarts)
                {
                    el.UserId = user.Id;
                    total += el.Total;
                    el.AmountToPay = el.Total * (100 - user.Discount) / 100;
                    toPay += el.AmountToPay;
                }
                ViewBag.ToPay = toPay;
                ViewBag.GrandTotal = total;
                return View(listCarts);
            }
            else
            {
                return RedirectToAction("../Account/Login");
            }
        }

        public ActionResult CartPartial()
        {
            Cart model = new Cart();
            int quantity = 0;
            decimal price = 0m;

            if (Session["cart"] != null)
            {
                List<Cart> listCarts = Session["cart"] as List<Cart>;
                foreach (var el in listCarts)
                {
                    quantity += el.Quantity;
                    price += el.Quantity * el.Price;
                }

                model.Quantity = quantity;
                model.Price = price;
            }
            else
            {
                model.Quantity = 0;
                model.Price = 0m;
            }
            return PartialView("_CartPartial", model);
        }

        public ActionResult AddToCartPartial(int id)
        {
            List<Cart> carts = Session["cart"] as List<Cart> ?? new List<Cart>();
            Cart model = new Cart();
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                Notebook notebook = context.Notebooks.Find(id);
                var notebookInCart = carts.FirstOrDefault(x => x.NotebookId == id);
                if (notebookInCart == null)
                {
                    carts.Add(new Cart()
                    {
                        NotebookId = notebook.Id,
                        NotebookName = notebook.Name,
                        Price = notebook.Price,
                        Quantity = 1
                    });
                }
                else
                {
                    notebookInCart.Quantity++;
                }
            }

            int quantity = 0;
            decimal price = 0m;

            foreach (var el in carts)
            {
                quantity += el.Quantity;
                price += el.Quantity * el.Price;
            }
            model.Quantity = quantity;
            model.Price = price;
            Session["cart"] = carts;
            return PartialView("_AddToCartPartial", model);
        }

        public void RemoveProduct(int productId)
        {
            List<Cart> carts = Session["cart"] as List<Cart>;
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                Cart model = carts.FirstOrDefault(x => x.NotebookId == productId);
                carts.Remove(model);
            }
        }

        public ActionResult ConfirmOrder(List<Cart> carts)
        {

            return View();
        }

        public ActionResult SendOrder(string DeliveryAddress)
        {
            List<Cart> carts = Session["cart"] as List<Cart>;
            decimal toPay = 0;
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                foreach (var el in carts)
                {
                    el.DeliveryAddress = DeliveryAddress;
                    Order order = new Order();
                    order.NotebookId = el.NotebookId;
                    order.NotebookName = el.NotebookName;
                    order.Price = el.Price;
                    order.Quantity = el.Quantity;
                    order.Total = el.Total;
                    order.AmountToPay = el.AmountToPay;
                    order.UserId = el.UserId;
                    context.Orders.Add(order);
                    context.SaveChanges();
                    context.Users.FirstOrDefault(x => x.Id == el.UserId).TotalPoints += el.AmountToPay;
                    context.SaveChanges();
                }
            }            
            carts.Clear();
            Session["cart"] = carts;
            return View();
        }
    }
}