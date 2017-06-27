using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebCalendar.App.Controllers
{
    [AllowAnonymous]
    public class ValidationsController : BaseController
    {
        public JsonResult IsUsernameAvailble(string username)
        {
            if (Context.Users.Where(u => u.Username == username).Count() > 0)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public JsonResult IsEmailAvailable(string email)
        {
            if (Context.Users.Where(u => u.Email == email).Count() > 0)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}