using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace ShopCart.Models
{
    public class DbInitializer : DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            context.Notebooks.Add(new Notebook() { Name = "Asus ROG Strix G531GT-BQ027", Price = 26000, Producer = "Asus" });
            context.Notebooks.Add(new Notebook() { Name = "HP Pavilion Gaming 15-bc504ur", Price = 16000, Producer = "HP" });
            context.Notebooks.Add(new Notebook() { Name = "Acer Aspire 5 A515-54G", Price = 16000, Producer = "Acer" });
            context.Notebooks.Add(new Notebook() { Name = "Lenovo IdeaPad 330-15AST", Price = 6000, Producer = "Lenovo" });
            context.Notebooks.Add(new Notebook() { Name = "Lenovo IdeaPad 330-15ICH", Price = 17000, Producer = "Lenovo" });
            context.Notebooks.Add(new Notebook() { Name = "Asus ZenBook 14 UX433FN-A5409", Price = 25000, Producer = "Asus" });
            context.Notebooks.Add(new Notebook() { Name = "Dell Inspiron 3581 ", Price = 7900, Producer = "Dell" });
            context.Notebooks.Add(new Notebook() { Name = "MSI GF63 9RCX", Price = 21000, Producer = "MSI" });
            base.Seed(context);
        }
    }
}