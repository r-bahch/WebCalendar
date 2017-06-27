using System.Data;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using WebCalendar.Data;

namespace WebCalendar.App.Controllers
{
    public class CategoriesController : BaseController
    {

        // GET: Categories
        public ActionResult Index()
        {
            return View(Context.Categories
                .Where(c => c.User.Username == HttpContext.User.Identity.Name || c.User == null)
                .ToList());
        }

        //// GET: Categories/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Category category = db.Categories.Find(id);
        //    if (category == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(category);
        //}

        //// GET: Categories/Create
        public ActionResult Create()
        {
            return View();
        }

        //// POST: Categories/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description")] Category category)
        {
            //TODO: Add id to User.Identity, easier
            category.User = Context.Users.Where(u => u.Username == User.Identity.Name).First();

            if (!ModelState.IsValid)
            {
                return View(category);
            }

            Context.Categories.Add(category);
            Context.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Categories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Category category = Context.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            if (category.User == null || category.User.Username != User.Identity.Name)
            {
                return new HttpUnauthorizedResult();
            }
            return View(category);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description")] Category category)
        {
            if (!ModelState.IsValid)
            {
                return View(category);
            }

            var existingCategory = Context.Categories.SingleOrDefault(c => c.Id == category.Id);
            if (existingCategory == null)
            {
                return HttpNotFound();
            }
            if (existingCategory.User == null || existingCategory.User.Username != User.Identity.Name)
            {
                return new HttpUnauthorizedResult();
            }
            existingCategory.Name = category.Name;
            existingCategory.Description = category.Description;
            Context.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Categories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = Context.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            if (category.User == null || category.User.Username != User.Identity.Name)
            {
                return new HttpUnauthorizedResult();
            }
            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            Category category = Context.Categories.Find(id);
            if (category.User == null || category.User.Username != User.Identity.Name)
            {
                return new HttpUnauthorizedResult();
            }

            Context.Categories.Remove(category);
            Context.SaveChanges();
            return RedirectToAction("Index");
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
