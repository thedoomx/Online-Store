using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class Apple
    {
        public int Id { get; set; }
        public string TypeOfApple { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public virtual List<Store> Stores { get; set; }
        public virtual List<MemberBasketApple> MemberBaskets { get; set; }
    }
}
