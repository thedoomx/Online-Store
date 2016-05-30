using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Editor.Models
{
    public class BasketItemModel
    {
        public int Id { get; set; }
        public string TypeOfItem { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string BananaOrApple { get; set; }
    }
}