using Editor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Editor.Controllers
{

    public class UserController : Controller
    {
        
        public ActionResult Index()
        {
            if (Request.IsAuthenticated)
            {
                String[] roles = Roles.GetRolesForUser();
                if (roles.Contains("admin"))
                {
                    return RedirectToAction("Index", "Admin");
                }
                else if (roles.Contains("member"))
                {
                    return RedirectToAction("Index", "Member");

                }
            }
            return View();
        }

        [HttpGet]
        public ActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogIn(UserModel user)
        {
            if (ModelState.IsValid)
            {
                if (IsValid(user.Email, user.Password))
                {
                    FormsAuthentication.SetAuthCookie(user.Email, false);

                    return RedirectToAction("RedirectToDefault");
                }
                else
                {
                    ModelState.AddModelError("", "Login data is incorrect");
                }
            }

            return View(user);
        }

        public ActionResult RedirectToDefault()
        {

            String[] roles = Roles.GetRolesForUser();
            if (roles.Contains("admin"))
            {
                return RedirectToAction("Index", "Admin");
            }
            else if (roles.Contains("member"))
            {
                return RedirectToAction("Index", "Member");
                
            }
            else
            {
                return RedirectToAction("Index");
            }

        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "User");
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(UserModel user)
        {
            if (ModelState.IsValid)
            {
                using (var ctx = new StoresEntities())
                {
                    var crypto = new SimpleCrypto.PBKDF2();
                    var encrpPass = crypto.Compute(user.Password);

                    var sysUser = ctx.Users.Create();
                    sysUser.Email = user.Email;
                    sysUser.Password = encrpPass;
                    sysUser.PasswordSalt = crypto.Salt;
                    sysUser.UserId = Guid.NewGuid();

                    ctx.Users.Add(sysUser);
                    sysUser.Roles.Add(ctx.Roles.FirstOrDefault(r => r.Name.Equals("member")));
                    ctx.SaveChanges();

                    return RedirectToAction("Index", "User");
                }
            }

            ViewBag.Title = "Email is already in use!";
            return View(user);
        }

        [HttpGet]
        public ActionResult ChangePassword()
        {
            var loggedUser = User.Identity.Name;
            using (var ctx = new StoresEntities())
            {
                var user = ctx.Users.FirstOrDefault(u => u.Email.Equals(loggedUser));

                if(user == null)
                {
                    return HttpNotFound();
                }

                return View(new UserModel { Password = user.Password, Email = user.Email });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(string OldPassword, string NewPassword, string ConfirmNewPassword)
        {
            if (IsValid(User.Identity.Name.ToString(), OldPassword))
            {
                if (NewPassword.Equals(ConfirmNewPassword))
                {
                    using (var ctx = new StoresEntities())
                    {
                        var crypto = new SimpleCrypto.PBKDF2();
                        var encrpPass = crypto.Compute(NewPassword);

                        var sysUser = ctx.Users.FirstOrDefault(s => s.Email.Equals(User.Identity.Name.ToString()));
                        sysUser.PasswordSalt = crypto.Salt;
                        sysUser.Password = encrpPass;
                        ctx.SaveChanges();

                        return RedirectToAction("Index", "Member");
                    }
                }
                else
                {
                    ViewBag.Title = "New password does not match the confirmed one!";
                }
            }
            else
            {
                ViewBag.Title = "Original password does not match!";
            }

            using(var ctx = new StoresEntities())
            {
                var user = ctx.Users.FirstOrDefault(s => s.Email.Equals(User.Identity.Name.ToString()));
                return View(new UserModel { Password = user.Password, Email = user.Email });
            }
        }

        private bool IsValid(string email, string password)
        {
            var crypto = new SimpleCrypto.PBKDF2();
            bool isValid = false;

            using (var ctx = new StoresEntities())
            {
                var user = ctx.Users.FirstOrDefault(u => u.Email.Equals(email));

                if (user != null)
                {
                    if (user.Password.Equals(crypto.Compute(password, user.PasswordSalt)))
                    {
                        isValid = true;
                    }
                }
            }

            return isValid;
        }
    }
}