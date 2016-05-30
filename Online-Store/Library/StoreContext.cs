using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class StoreContext : DbContext
    {
        public DbSet<Apple> Apples { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<Banana> Bananas { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<MemberBasket> MemberBaskets { get; set; }
        public DbSet<MemberBasketApple> MemberBasketApples { get; set; }
        public DbSet<MemberBasketBanana> MemberBasketBananas { get; set; }

    }
}
