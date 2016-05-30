using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Editor.Models;
using System.Data.Entity;

namespace Editor.Controllers
{
    public class StoreRepository
    {
        private StoresEntities context = new StoresEntities();

        public List<Store> GetAllStores()
        {
            return context.Stores.ToList();
        }

        public Store GetStoreById(int id)
        {
            return context.Stores.Find(id);
        }

        public void DeleteStore(int id)
        {
            Store store = context.Stores.FirstOrDefault(s => s.Id == id);

            if (store != null)
            {
                context.Stores.Remove(store);
                context.SaveChanges();
            }
        }

        public void AddStore(StoreModel store)
        {
            Store newStore = new Store { Name = store.Name };

            context.Stores.Add(newStore);
            context.SaveChanges();
        }

        public List<User> GetAllUsers()
        {
            return context.Users.ToList();
        }

        public void UpdateEditedStore(Store store)
        {
            context.Entry(store).State = EntityState.Modified;
            context.SaveChanges();
        }

        public User GetUserById(string userId)
        {
            return context.Users.FirstOrDefault(u => u.UserId.ToString().Equals(userId));
        }

        public void DeleteUser(string userId)
        {
            User user = context.Users.FirstOrDefault(u => u.UserId.ToString().Equals(userId));


            if (user != null)
            {
                context.Users.Remove(user);
                context.SaveChanges();
            }
        }

        public void UpdateEditedUser(User user)
        {
            context.Entry(user).State = EntityState.Modified;
            context.SaveChanges();
        }

        public List<Apple> GetAllApples()
        {
            return context.Apples.ToList();
        }

        public List<Banana> GetAllBananas()
        {
            return context.Bananas.ToList();
        }

        public void AddApple(AppleModel apple)
        {
            Apple newApple = new Apple { TypeOfApple = apple.TypeOfApple, Price = apple.Price, Quantity = apple.Quantity };
            context.Apples.Add(newApple);
            context.SaveChanges();
        }

        public void AddBanana(BananaModel banana)
        {
            Banana newBanana = new Banana { TypeOfBanana = banana.TypeOfBanana, Price = banana.Price, Quantity = banana.Quantity };
            context.Bananas.Add(newBanana);
            context.SaveChanges();
        }

        public Apple GetAppleById(int id)
        {
            return context.Apples.FirstOrDefault(a => a.Id == id);
        }

        public Banana GetBananaById(int id)
        {
            return context.Bananas.FirstOrDefault(b => b.Id == id);
        }

        public void DeleteApple(int id)
        {
            Apple apple = context.Apples.FirstOrDefault(a => a.Id == id);

            if (apple != null)
            {
                context.Apples.Remove(apple);
                context.SaveChanges();
            }
        }

        public void DeleteBanana(int id)
        {
            Banana banana = context.Bananas.FirstOrDefault(b => b.Id == id);

            if (banana != null)
            {
                context.Bananas.Remove(banana);
                context.SaveChanges();
            }
        }

        public void UpdateEditedApple(Apple apple)
        {
            context.Entry(apple).State = EntityState.Modified;
            context.SaveChanges();
        }

        public void UpdateEditedBanana(Banana banana)
        {
            context.Entry(banana).State = EntityState.Modified;
            context.SaveChanges();
        }

        public List<Apple> GetAvailableApples()
        {
            return context.Apples.Where(a => a.Stores.FirstOrDefault() == null).ToList();
        }

        public List<Banana> GetAvailableBananas()
        {
            return context.Bananas.Where(b => b.Stores.FirstOrDefault() == null).ToList();
        }

        public void AddAppleToStore(Apple apple, Store store)
        {
            store.Apples.Add(apple);
            context.SaveChanges();
        }

        public void AddBananaToStore(Banana banana, Store store)
        {
            store.Bananas.Add(banana);
            context.SaveChanges();
        }

        public void AppleDecreaseQuantity(int quantity, int id)
        {
            var apple = GetAppleById(id);
            apple.Quantity -= quantity;

            context.SaveChanges();
        }

        public void BananaDecreaseQuantity(int quantity, int id)
        {
            var banana = GetBananaById(id);
            banana.Quantity -= quantity;

            context.SaveChanges();
        }

        public void AddAppleToBasket(int appleId, string userEmail, int appleQuantity)
        {
            if (context.MemberBaskets.FirstOrDefault(m => m.Email.Equals(userEmail)) == null)
            {
                context.MemberBaskets.Add(new MemberBasket { Email = userEmail });
                context.SaveChanges();

                var member = context.MemberBaskets.FirstOrDefault(m => m.Email.Equals(userEmail));

                member.MemberBasketApples.Add(new MemberBasketApple
                {
                    AppleId = appleId,
                    Email = userEmail,
                    Quantity = appleQuantity
                });

                context.SaveChanges();

            }
            else
            {
                var member = context.MemberBaskets.FirstOrDefault(m => m.Email.Equals(userEmail));

                if (context.MemberBasketApples.FirstOrDefault(
                    mba => mba.Email.Equals(userEmail) && mba.AppleId.Equals(appleId)) == null)
                {
                    member.MemberBasketApples.Add(new MemberBasketApple
                    {
                        AppleId = appleId,
                        Email = userEmail,
                        Quantity = appleQuantity
                    });

                    context.SaveChanges();
                }
                else
                {
                    var increaseAmountOfApples = context.MemberBasketApples.FirstOrDefault(
                    mba => mba.Email.Equals(userEmail) && mba.AppleId.Equals(appleId));

                    increaseAmountOfApples.Quantity += appleQuantity;

                    context.SaveChanges();
                }
            }

        }

        public void AddBananaToBasket(int bananaId, string userEmail, int bananaQuantity)
        {
            if (context.MemberBaskets.FirstOrDefault(m => m.Email.Equals(userEmail)) == null)
            {
                context.MemberBaskets.Add(new MemberBasket { Email = userEmail });
                context.SaveChanges();

                var member = context.MemberBaskets.FirstOrDefault(m => m.Email.Equals(userEmail));

                member.MemberBasketBananas.Add(new MemberBasketBanana
                {
                    BananaId = bananaId,
                    Email = userEmail,
                    Quantity = bananaQuantity
                });

                context.SaveChanges();

            }
            else
            {
                var member = context.MemberBaskets.FirstOrDefault(m => m.Email.Equals(userEmail));

                if (context.MemberBasketBananas.FirstOrDefault(
                    mba => mba.Email.Equals(userEmail) && mba.BananaId.Equals(bananaId)) == null)
                {
                    member.MemberBasketBananas.Add(new MemberBasketBanana
                    {
                        BananaId = bananaId,
                        Email = userEmail,
                        Quantity = bananaQuantity
                    });

                    context.SaveChanges();
                }
                else
                {
                    var increaseAmountOfBananas = context.MemberBasketBananas.FirstOrDefault(
                    mba => mba.Email.Equals(userEmail) && mba.BananaId.Equals(bananaId));

                    increaseAmountOfBananas.Quantity += bananaQuantity;

                    context.SaveChanges();
                }
            }

        }

        public MemberBasket GetBasketById(string email)
        {
            return context.MemberBaskets.Find(email);
        }

        public List<MemberBasketApple> GetApplesInBasket(string email)
        {

            return context.MemberBasketApples.Where(mba => mba.Email.Equals(email)).ToList();
        }

        public List<MemberBasketBanana> GetBananasInBasket(string email)
        {

            return context.MemberBasketBananas.Where(mbb => mbb.Email.Equals(email)).ToList();
        }

        public void RemoveAppleFromBasketAndRestoreToStore(int appleId, string userEmail)
        {
            MemberBasketApple mba = context.MemberBasketApples.FirstOrDefault
                                    (m => m.AppleId.Equals(appleId) && m.Email.Equals(userEmail));

            var apple = context.Apples.FirstOrDefault(a => a.Id.Equals(appleId));

            apple.Quantity += mba.Quantity;

            context.MemberBasketApples.Remove(mba);
            context.SaveChanges();
        }

        public void RemoveBananaFromBasketAndRestoreToStore(int bananaId, string userEmail)
        {
            MemberBasketBanana mbb = context.MemberBasketBananas.FirstOrDefault
                                    (m => m.BananaId.Equals(bananaId) && m.Email.Equals(userEmail));

            var banana = context.Bananas.FirstOrDefault(b => b.Id.Equals(bananaId));

            banana.Quantity += mbb.Quantity;

            context.MemberBasketBananas.Remove(mbb);
            context.SaveChanges();
        }
    }
}