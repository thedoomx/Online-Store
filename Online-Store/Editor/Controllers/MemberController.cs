using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Editor.Models;

namespace Editor.Controllers
{
    [CustomAuthorize]
    public class MemberController : Controller
    {
        private StoreRepository repo = new StoreRepository();

        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult ShowAllStores()
        {
            List<StoreModel> list = new List<StoreModel>();

            foreach (var item in repo.GetAllStores())
            {
                list.Add(new StoreModel { Id = item.Id, Name = item.Name });
            }
            return PartialView("_showStores", list);

        }

        public ActionResult ShowStoreItems(int id)
        {
            Store store = repo.GetStoreById(id);
            var products = new ProductsModel();
            products.Apples = store.Apples.ToList();
            products.Bananas = store.Bananas.ToList();


            if (store == null)
            {
                return HttpNotFound();
            }

            return View(new StoreModel
            {
                Name = store.Name,
                Id = store.Id,
                Products = products
            });
        }

        [HttpGet]
        public ActionResult AddAppleToBasket(int id, int storeId)
        {
            var apple = repo.GetAppleById(id);
            ViewBag.storeId = storeId;

            return View(new AppleModel
            {
                Id = apple.Id,
                Price = apple.Price,
                Quantity = apple.Quantity,
                TypeOfApple = apple.TypeOfApple
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddAppleToBasket(AppleModel apple)
        {
            var appleInStorage = repo.GetAppleById(apple.Id);

            if (appleInStorage.Quantity <= 0 || apple.Quantity > appleInStorage.Quantity)
            {
                ViewBag.Title = "Please insert correct quantity for the product!";
                return View(appleInStorage);
            }
            else
            {
                repo.AppleDecreaseQuantity(apple.Quantity, appleInStorage.Id);
                repo.AddAppleToBasket(appleInStorage.Id, User.Identity.Name.ToString(), apple.Quantity);

                int storeId = Convert.ToInt32(TempData["storeId"]);
                return RedirectToAction("AppleToBasketConfirmed", new
                {
                    appleId = apple.Id,
                    orderedQuantity = apple.Quantity,
                    storeId = storeId
                });
            }
        }

        [HttpGet]
        public ActionResult AppleToBasketConfirmed(int appleId, int orderedQuantity, int storeId)
        {
            ViewBag.storeId = storeId;
            var apple = repo.GetAppleById(appleId);


            return View(new AppleModel
            {
                Id = appleId,
                Quantity = orderedQuantity,
                TypeOfApple = apple.TypeOfApple,
                Price = apple.Price
            });
        }

        [HttpGet]
        public ActionResult AddBananaToBasket(int id, int storeId)
        {
            var banana = repo.GetBananaById(id);
            ViewBag.storeId = storeId;

            return View(new BananaModel
            {
                Id = banana.Id,
                Price = banana.Price,
                Quantity = banana.Quantity,
                TypeOfBanana = banana.TypeOfBanana
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddBananaToBasket(BananaModel banana)
        {
            var bananaInStorage = repo.GetBananaById(banana.Id);

            if (bananaInStorage.Quantity <= 0 || banana.Quantity > bananaInStorage.Quantity)
            {
                ViewBag.Title = "Please insert correct quantity for the product!";
                return View(bananaInStorage);
            }
            else
            {
                repo.BananaDecreaseQuantity(banana.Quantity, bananaInStorage.Id);
                repo.AddBananaToBasket(bananaInStorage.Id, User.Identity.Name.ToString(), banana.Quantity);

                int storeId = Convert.ToInt32(TempData["storeId"]);
                return RedirectToAction("BananaToBasketConfirmed", new
                {
                    bananaId = banana.Id,
                    orderedQuantity = banana.Quantity,
                    storeId = storeId
                });
            }
        }

        [HttpGet]
        public ActionResult BananaToBasketConfirmed(int bananaId, int orderedQuantity, int storeId)
        {
            ViewBag.storeId = storeId;
            var banana = repo.GetBananaById(bananaId);


            return View(new BananaModel
            {
                Id = bananaId,
                Quantity = orderedQuantity,
                TypeOfBanana = banana.TypeOfBanana,
                Price = banana.Price
            });
        }

        public ActionResult InspectBasket()
        {
            var basket = repo.GetBasketById(User.Identity.Name);

            if (basket == null)
            {
                return HttpNotFound();
            }

            List<BasketItemModel> items = new List<BasketItemModel>();

            foreach (var item in repo.GetApplesInBasket(User.Identity.Name))
            {
                var apple = repo.GetAppleById(item.AppleId);
                items.Add(new BasketItemModel
                {
                    Id = apple.Id,
                    TypeOfItem = apple.TypeOfApple,
                    Price = apple.Price,
                    Quantity = item.Quantity,
                    BananaOrApple = "apple"
                });
            }

            foreach (var item in repo.GetBananasInBasket(User.Identity.Name))
            {
                var banana = repo.GetBananaById(item.BananaId);
                items.Add(new BasketItemModel
                {
                    Id = banana.Id,
                    TypeOfItem = banana.TypeOfBanana,
                    Price = banana.Price,
                    Quantity = item.Quantity,
                    BananaOrApple = "banana"
                });
            }

            ViewBag.items = items;

            return View(basket);
        }
    }
}