using ShopCart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopCart.Controllers
{
    public class ShopController : Controller
    {
        // GET: Shop
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                List<Notebook> notebooks = new List<Notebook>();
                using (ApplicationDbContext context = new ApplicationDbContext())
                {
                    notebooks = context.Notebooks.ToList();
                }
                return View(notebooks);
            }
            else
            {
                return RedirectToAction("../Account/Login");
            }
        }

       
    }
}