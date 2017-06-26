using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
using WebCalendar.Data;

namespace MaznaMutska.App.Controllers
{
    [Authorize] // All controllers that inherit from this one will be available only for registered users
    public abstract class BaseController : Controller
    {
        private WebCalendarDb context;

        public BaseController()
        {
            context = new WebCalendarDb();
        }

        public BaseController(WebCalendarDb context)
        {
            this.context = context;
        }


        protected WebCalendarDb Context
        {
            get
            {
                return context;
            }
        }

        // Authentication logic, runs for any action, marked with Authorize attribute
        protected override void OnAuthentication(AuthenticationContext filterContext)
        {
            if (this.HttpContext.Request.Cookies != null)
            {
                var tokenCookie = this.HttpContext.Request.Cookies.Get("userToken");
                if (tokenCookie != null && !string.IsNullOrEmpty(tokenCookie.Value))
                {
                    var user = Context.Users.FirstOrDefault(u => u.Token == tokenCookie.Value);
                    if (user != null)
                    {
                        this.HttpContext.User = new GenericPrincipal(new GenericIdentity(user.Username), new string[] { } );
                        this.HttpContext.Session["userToken"] = tokenCookie.Value;
                    }
                    else Response.SetCookie(new HttpCookie("userToken", null)); // if user token is not in the database, delete userToken in cookie
                }
            }

            base.OnAuthentication(filterContext);
        }

    }
}