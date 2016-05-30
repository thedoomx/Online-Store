using Editor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace Editor.Controllers
{
    [CustomAuthorize(Roles = "admin")]
    public class AdminController : Controller
    {
        private StoreRepository repo = new StoreRepository();

        public ActionResult Index()
        {
            return View();
        }

        //Users section
        //Users section
        //Users section
        public ActionResult ManageUsers()
        {
            List<AdminUserModel> users = new List<AdminUserModel>();

            foreach (var item in repo.GetAllUsers())
            {
                users.Add(new AdminUserModel
                {
                    Email = item.Email,
                    UserId = item.UserId.ToString(),
                    Role = item.Roles.FirstOrDefault().Name,
                    Password = item.Password
                });
            }

            return View(users);
        }

        [HttpGet]
        public ActionResult UsersEdit(string id)
        {
            var user = repo.GetUserById(id);

            if (user == null)
            {
                return HttpNotFound();
            }

            return View(new AdminUserModel
            {
                Email = user.Email,
                Password = user.Password,
                Role = user.Roles.FirstOrDefault().Name,
                UserId = user.UserId.ToString()
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UsersEdit(AdminUserModel user)
        {
            if (ModelState.IsValid)
            {
                User toBeUpdated = repo.GetUserById(user.UserId.ToString());
                toBeUpdated.Roles.FirstOrDefault().Name = user.Role;

                repo.UpdateEditedUser(toBeUpdated);
                return RedirectToAction("ManageUsers");
            }

            return View(user);
        }

        [HttpGet]
        public ActionResult UsersDelete(string id)
        {
            var user = repo.GetUserById(id);

            if (user == null)
            {
                return HttpNotFound();
            }

            return View(new AdminUserModel
            {
                Email = user.Email,
                Password = user.Password,
                Role = user.Roles.FirstOrDefault().Name,
                UserId = user.UserId.ToString()
            });
        }

        [HttpPost, ActionName("UsersDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult UsersDeleteConfirmed(string id = null)
        {
            repo.DeleteUser(id);

            return RedirectToAction("ManageUsers");
        }

        public ActionResult UsersDetails(string id = null)
        {
            var user = repo.GetUserById(id);

            if (user == null)
            {
                return HttpNotFound();
            }

            return View(new AdminUserModel
            {
                Email = user.Email,
                Password = user.Password,
                Role = user.Roles.FirstOrDefault().Name,
                UserId = user.UserId.ToString()
            });
        }

        //Stores section
        //Stores section
        //Stores section
        public ActionResult ManageStores()
        {
            List<StoreModel> stores = new List<StoreModel>();

            foreach (var item in repo.GetAllStores())
            {
                var products = new ProductsModel
                {
                    Apples = item.Apples.ToList(),
                    Bananas = item.Bananas.ToList()
                };

                stores.Add(new StoreModel
                {
                    Name = item.Name,
                    Id = item.Id,
                    Products = products
                });
            }

            return View(stores);
        }

        public ActionResult StoresDetails(int id = 0)
        {
            var store = repo.GetStoreById(id);
            var products = new ProductsModel
            {
                Apples = store.Apples.ToList(),
                Bananas = store.Bananas.ToList()
            };

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
        public ActionResult StoresDelete(int id)
        {
            var store = repo.GetStoreById(id);
            var products = new ProductsModel
            {
                Apples = store.Apples.ToList(),
                Bananas = store.Bananas.ToList()
            };

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

        [HttpPost, ActionName("StoresDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult StoresDeleteConfirmed(int id)
        {
            repo.DeleteStore(id);

            return RedirectToAction("ManageStores");
        }

        [HttpGet]
        public ActionResult AdminCreateStore()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AdminCreateStore(StoreModel store)
        {
            if (ModelState.IsValid)
            {
                repo.AddStore(store);

                return RedirectToAction("ManageStores");
            }

            return View(store);
        }

        [HttpGet]
        public ActionResult StoresEdit(int id = 0)
        {
            var store = repo.GetStoreById(id);
            var products = new ProductsModel
            {
                Apples = store.Apples.ToList(),
                Bananas = store.Bananas.ToList()
            };

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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult StoresEdit(StoreModel store)
        {
            if (ModelState.IsValid)
            {
                Store toBeUpdated = repo.GetStoreById(store.Id);
                if (store.Products != null)
                {
                    if (store.Products.Apples != null) toBeUpdated.Apples = store.Products.Apples;
                    if (store.Products.Bananas != null) toBeUpdated.Bananas = store.Products.Bananas;
                };

                toBeUpdated.Name = store.Name;

                repo.UpdateEditedStore(toBeUpdated);
                return RedirectToAction("ManageStores");
            }

            return View(store);
        }

        public PartialViewResult AvailableApples(StoreModel store)
        {
            List<AppleModel> list = new List<AppleModel>();

            foreach (var item in repo.GetAvailableApples())
            {
                list.Add(new AppleModel { Id = item.Id, Price = item.Price, Quantity = item.Quantity, TypeOfApple = item.TypeOfApple });
            }

            ViewBag.list = list;
            return PartialView("_availableApples", store);
        }

        public PartialViewResult AvailableBananas(StoreModel store)
        {
            List<BananaModel> list = new List<BananaModel>();

            foreach (var item in repo.GetAvailableBananas())
            {
                list.Add(new BananaModel { Id = item.Id, Price = item.Price, Quantity = item.Quantity, TypeOfBanana = item.TypeOfBanana });
            }

            ViewBag.list = list;
            return PartialView("_availableBananas", store);
        }

        [HttpGet]
        public ActionResult AddAppleToStore(int id, int storeId)
        {
            var store = repo.GetStoreById(storeId);

            var products = new ProductsModel
            {
                Apples = store.Apples.ToList(),
                Bananas = store.Bananas.ToList()
            };
            
            var apple = repo.GetAppleById(id);

            if (apple == null || store == null)
            {
                return HttpNotFound();
            }

            repo.AddAppleToStore(apple, store);

            return RedirectToAction("StoresEdit", new StoreModel
            {
                Name = store.Name,
                Id = store.Id,
                Products = products
            });
        }

        [HttpGet]
        public ActionResult AddBananaToStore(int id, int storeId)
        {
            var store = repo.GetStoreById(storeId);
            var products = new ProductsModel
            {
                Apples = store.Apples.ToList(),
                Bananas = store.Bananas.ToList()
            };

            var banana = repo.GetBananaById(id);

            if (banana == null || store == null)
            {
                return HttpNotFound();
            }

            repo.AddBananaToStore(banana, store);

            return RedirectToAction("StoresEdit", new StoreModel
            {
                Name = store.Name,
                Id = store.Id,
                Products = products
            });
        }

        //Products section
        //Products section
        //Products section
        public ActionResult ManageProducts()
        {
            ProductsModel products = new ProductsModel
            {
                Apples = repo.GetAllApples(),
                Bananas = repo.GetAllBananas()
            };

            return View(products);
        }

        public ActionResult AdminAddProduct()
        {
            return View();
        }

        [HttpGet]
        public PartialViewResult AdminAddApple()
        {
            return PartialView("_addApple");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AdminAddApple(AppleModel apple)
        {
            if (ModelState.IsValid)
            {
                repo.AddApple(apple);

                return RedirectToAction("AddAppleConfirmed", apple);
            }

            return View(apple);
        }

        public ActionResult AddAppleConfirmed(AppleModel apple)
        {
            return View(apple);
        }

        [HttpGet]
        public PartialViewResult AdminAddBanana()
        {
            return PartialView("_addBanana");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AdminAddBanana(BananaModel banana)
        {
            if (ModelState.IsValid)
            {
                repo.AddBanana(banana);

                return RedirectToAction("AddBananaConfirmed", banana);
            }

            return View(banana);
        }

        public ActionResult AddBananaConfirmed(BananaModel banana)
        {
            return View(banana);
        }

        [HttpGet]
        public ActionResult DeleteApple(int id = 0)
        {
            Apple apple = repo.GetAppleById(id);

            if (apple == null)
            {
                return HttpNotFound();
            }

            return View(new AppleModel
            {
                TypeOfApple = apple.TypeOfApple,
                Id = apple.Id,
                Price = apple.Price,
                Quantity = apple.Quantity
            });
        }

        [HttpPost, ActionName("DeleteApple")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteAppleConfirmed(int id)
        {
            repo.DeleteApple(id);

            return RedirectToAction("ManageProducts");
        }

        [HttpGet]
        public ActionResult DeleteBanana(int id = 0)
        {
            Banana banana = repo.GetBananaById(id);

            if (banana == null)
            {
                return HttpNotFound();
            }

            return View(new BananaModel
            {
                TypeOfBanana = banana.TypeOfBanana,
                Id = banana.Id,
                Price = banana.Price,
                Quantity = banana.Quantity
            });
        }

        [HttpPost, ActionName("DeleteBanana")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteBananaConfirmed(int id)
        {
            repo.DeleteBanana(id);

            return RedirectToAction("ManageProducts");
        }

        [HttpGet]
        public ActionResult AppleEdit(int id)
        {
            var apple = repo.GetAppleById(id);

            if (apple == null)
            {
                return HttpNotFound();
            }

            return View(new AppleModel
            {
                TypeOfApple = apple.TypeOfApple,
                Id = apple.Id,
                Price = apple.Price,
                Quantity = apple.Quantity
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AppleEdit(AppleModel apple)
        {
            if (ModelState.IsValid)
            {
                Apple toBeUpdated = repo.GetAppleById(apple.Id);

                toBeUpdated.Price = apple.Price;
                toBeUpdated.Quantity = apple.Quantity;

                repo.UpdateEditedApple(toBeUpdated);

                return RedirectToAction("ManageProducts");
            }

            return View(apple);
        }

        [HttpGet]
        public ActionResult BananaEdit(int id)
        {
            var banana = repo.GetBananaById(id);

            if (banana == null)
            {
                return HttpNotFound();
            }

            return View(new BananaModel
            {
                TypeOfBanana = banana.TypeOfBanana,
                Id = banana.Id,
                Price = banana.Price,
                Quantity = banana.Quantity
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BananaEdit(BananaModel banana)
        {
            if (ModelState.IsValid)
            {
                Banana toBeUpdated = repo.GetBananaById(banana.Id);

                toBeUpdated.Price = banana.Price;
                toBeUpdated.Quantity = banana.Quantity;

                repo.UpdateEditedBanana(toBeUpdated);

                return RedirectToAction("ManageProducts");
            }

            return View(banana);
        }

        public ActionResult AppleDetails(int id = 0)
        {
            Apple apple = repo.GetAppleById(id);

            return View(new AppleModel
            {
                TypeOfApple = apple.TypeOfApple,
                Id = apple.Id,
                Price = apple.Price,
                Quantity = apple.Quantity
            });
        }

        public ActionResult BananaDetails(int id = 0)
        {
            Banana banana = repo.GetBananaById(id);

            return View(new BananaModel
            {
                TypeOfBanana = banana.TypeOfBanana,
                Id = banana.Id,
                Price = banana.Price,
                Quantity = banana.Quantity
            });
        }
    }
}