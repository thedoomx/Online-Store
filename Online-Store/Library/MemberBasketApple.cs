using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class MemberBasketApple
    {
        [Key, Column(Order = 0)]
        public string Email { get; set; }
        [Key, Column(Order = 1)]
        public int AppleId { get; set; }

        public virtual MemberBasket MemberBasket { get; set; }
        public virtual Apple Apple { get; set; }

        public int Quantity { get; set; }
    }
}
