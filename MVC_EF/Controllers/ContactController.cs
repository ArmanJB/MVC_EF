using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVC_EF.DAL;
using MVC_EF.Models;
using MVC_EF.ViewModels;

namespace MVC_EF.Controllers
{
    public class ContactController : Controller
    {
        private BusinessContext db = new BusinessContext();

        // GET: Contact
        public async Task<ActionResult> Index()
        {
            var contacts = db.Contacts.Include(c => c.Area);
            return View(await contacts.ToListAsync());
        }

        // GET: Contact/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact contact = await db.Contacts.FindAsync(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }

        // GET: Contact/Create
        public ActionResult Create()
        {
            ViewBag.AreaID = new SelectList(db.Areas, "AreaID", "Name");
            return View();
        }

        // POST: Contact/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ContactID,AreaID,FirstName,LastName,BirthDate,Addresses")] Contact contact)
        {
            if (ModelState.IsValid)
            {
                db.Contacts.Add(contact);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.AreaID = new SelectList(db.Areas, "AreaID", "Name", contact.AreaID);
            return View(contact);
        }

        // GET: Contact/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact contact = await db.Contacts.FindAsync(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            ViewBag.AreaID = new SelectList(db.Areas, "AreaID", "Name", contact.AreaID);
            return View(contact);
        }

        // POST: Contact/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ContactID,AreaID,FirstName,LastName,BirthDate")] Contact contact)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contact).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.AreaID = new SelectList(db.Areas, "AreaID", "Name", contact.AreaID);
            return View(contact);
        }

        // GET: Contact/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact contact = await db.Contacts.FindAsync(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }

        // POST: Contact/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Contact contact = await db.Contacts.FindAsync(id);
            db.Contacts.Remove(contact);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public ActionResult CreateWithAddress()
        {
            ViewBag.AreaID = new SelectList(db.Areas, "AreaID", "Name");
            return View(new ContactAddressViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateWithAddress(ContactAddressViewModel model, string action)
        {
            if (action.Equals("save"))
            {
                if (model.IsValidContact())
                {
                    db.Contacts.Add(model.ToModel());
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("Contact", "Invalid Contact");
                }
            }
            else if (action.Equals("add_address"))
            {
                if (model.ValidAddressAdded())
                {
                    model.AddItemAddress();
                }
                else
                {
                    ModelState.AddModelError("Add_Address", "Invalid Address");
                }

            }else if (action.Equals("remove_address"))
            {
                model.RemoveItemAddress();
            }

            ViewBag.AreaID = new SelectList(db.Areas, "AreaID", "Name", model.AreaID);
            return View(model);

            if (ModelState.IsValid)
            {
                //db.Contacts.Add(model);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.AreaID = new SelectList(db.Areas, "AreaID", "Name", model.AreaID);
            return View(model);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
