using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class Banana
    {
        public int Id { get; set; }
        public string TypeOfBanana { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public virtual List<Store> Stores { get; set; }
        public virtual List<MemberBasketBanana> MemberBaskets { get; set; }
    }
}
