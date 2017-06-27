using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebCalendar.App.Models.ViewModels;
using WebCalendar.App.Utilities;
using WebCalendar.Data;

namespace WebCalendar.App.Controllers
{
    public class UsersController : BaseController
    {
        // Add New User
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View(new RegisterViewModel());
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]  //for security, in the form we have Html.AntiforgeryToken()
        public ActionResult Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = new User
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Username = model.Username,
                PasswordHash = Encryption.GetHashSha256(model.Password),
                Email = model.Email,
            };

            Context.Users.Add(user);
            Context.SaveChanges();
            return RedirectToAction("Login", "Users");
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login()
        {
            if (this.User.Identity.IsAuthenticated)
            {
                //TODO: Change redirect
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var passwordHash = Encryption.GetHashSha256(model.Password);
            var user = Context.Users.FirstOrDefault(u => u.Username == model.Username);
            if (user == null || user.PasswordHash != passwordHash)
            {
                ModelState.AddModelError(string.Empty, "Invalid username or password");
                return View(model);
            }

            if (string.IsNullOrEmpty(user.Token))
            {
                user.Token = Guid.NewGuid().ToString();
            }

            Context.SaveChanges();
            Response.AppendCookie(new HttpCookie("userToken", user.Token));
            Response.Cookies["userToken"].Expires = DateTime.Now.AddDays(10);
            return this.RedirectToAction("Index", "Home");
        }

        //Post because of browser pre-fetching pages
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
            var token = (string)this.Session["userToken"];
            var user = this.Context.Users.FirstOrDefault(u => u.Token == token);
            if (user == null)
            {
                return this.RedirectToAction("Login");
            }

            user.Token = null;
            this.Context.SaveChanges();
            this.Response.SetCookie(new HttpCookie("userToken", user.Token));
            return this.RedirectToAction(actionName: "Login", controllerName: "Users");
        }

    }
}