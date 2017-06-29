using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using WebCalendar.Data;

namespace WebCalendar.App.Controllers
{
    public class ContactsController : BaseController
    {
        // GET: Contacts
        public ActionResult Index()
        {
            return View(Context
                .Contacts
                .Where(c => c.User.Username == User.Identity.Name)
                .ToList());
        }

        // GET: Contacts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact contact = Context.Contacts.Find(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            if (contact.User.Username != User.Identity.Name)
            {
                return new HttpUnauthorizedResult();
            }
            return View(contact);
        }

        // GET: Contacts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Contacts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FirstName,LastName,Email,BirthDate,PhoneNumber,Address,AditionalInfo")] Contact contact)
        {
            if (string.IsNullOrWhiteSpace(contact.FirstName) && string.IsNullOrWhiteSpace(contact.LastName))
            {
                ModelState.AddModelError(string.Empty, "Provide either first name or last name for contact");
            }
            if (!ModelState.IsValid)
            {
                return View(contact);
            }

            var user = Context.Users.Where(u => u.Username == User.Identity.Name).First();
            contact.User = user;

            if (contact.BirthDate.HasValue)
            {
                DateTime meetingDate = new DateTime(contact.BirthDate.Value.Year, contact.BirthDate.Value.Month, contact.BirthDate.Value.Day, 14, 0, 0);
                meetingDate.AddDays(-1);
                contact.Meetings.Add(new Meeting()
                {
                    Category = Context.Categories.Find(2),
                    Time = meetingDate,
                    Comment = "Congratulate " + contact.FullName,
                    User = user
                });
            }

            Context.Contacts.Add(contact);
            Context.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Contacts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Contact contact = Context.Contacts.Find(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            if (contact.User.Username != User.Identity.Name)
            {
                return new HttpUnauthorizedResult();
            }
            return View(contact);
        }

        // POST: Contacts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,Email,BirthDate,PhoneNumber,Address,AditionalInfo")] Contact contact)
        {
            if (string.IsNullOrWhiteSpace(contact.FirstName) && string.IsNullOrWhiteSpace(contact.LastName))
            {
                ModelState.AddModelError(string.Empty, "Provide either first name or last name for contact");
            }

            if (!ModelState.IsValid)
            {
                return View(contact);
            }

            var existingContact = Context.Contacts.SingleOrDefault(c => c.Id == contact.Id);
            if (existingContact == null)
            {
                return HttpNotFound();
            }
            if (existingContact.User.Username != User.Identity.Name)
            {
                return new HttpUnauthorizedResult();
            }

            if (contact.BirthDate != null && (existingContact.BirthDate ?? new DateTime(1)) != contact.BirthDate)
            {
                DateTime meetingDate = new DateTime(DateTime.Now.Year, contact.BirthDate.Value.Month, contact.BirthDate.Value.Day, 14, 0, 0).AddDays(-1);
                var existingMeetings = existingContact.Meetings.Where(m => m.Category.Id == 2);
                Context.Meetings.RemoveRange(existingMeetings);
                existingContact.Meetings.Add(new Meeting()
                {
                    Category = Context.Categories.Find(2),
                    Time = meetingDate,
                    Comment = "Congratulate " + contact.FullName,
                    User = existingContact.User
                });

            }
            existingContact.FirstName = contact.FirstName;
            existingContact.LastName = contact.LastName;
            existingContact.Email = contact.Email;
            existingContact.BirthDate = contact.BirthDate;
            existingContact.PhoneNumber = contact.PhoneNumber;
            existingContact.Address = contact.Address;
            existingContact.AditionalInfo = contact.AditionalInfo;
            Context.SaveChanges();
            return RedirectToAction("Index");
        }

        //    // GET: Contacts/Delete/5
        //    public ActionResult Delete(int? id)
        //    {
        //        if (id == null)
        //        {
        //            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //        }
        //        Contact contact = db.Contacts.Find(id);
        //        if (contact == null)
        //        {
        //            return HttpNotFound();
        //        }
        //        return View(contact);
        //    }

        //    // POST: Contacts/Delete/5
        //    [HttpPost, ActionName("Delete")]
        //    [ValidateAntiForgeryToken]
        //    public ActionResult DeleteConfirmed(int id)
        //    {
        //        Contact contact = db.Contacts.Find(id);
        //        db.Contacts.Remove(contact);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    protected override void Dispose(bool disposing)
        //    {
        //        if (disposing)
        //        {
        //            db.Dispose();
        //        }
        //        base.Dispose(disposing);
        //    }
    }
}
