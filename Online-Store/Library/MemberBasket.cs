using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class MemberBasket
    {
        [Key, ForeignKey("User")]
        public string Email { get; set; }


        public virtual User User { get; set; }
        public virtual List<MemberBasketApple> Apples { get; set; }
        public virtual List<MemberBasketBanana> Bananas { get; set; }
    }
}
