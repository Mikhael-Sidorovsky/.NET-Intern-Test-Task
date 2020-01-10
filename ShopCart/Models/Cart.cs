using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopCart.Models
{
    public class Cart
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public int NotebookId { get; set; }

        public string NotebookName { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public decimal Total
        {
            get => Price* Quantity;      
        }

        public decimal AmountToPay { get; set; }

        public string DeliveryAddress { get; set; }

    }
}