using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebRGRApplication;

namespace WebRGRApplication.Controllers
{
    public class RequestsController : Controller
    {
        private DB_RGREntities db = new DB_RGREntities();

        // GET: Requests
        public ActionResult Index()
        {
            var request = db.Request.Include(r => r.Client).Include(r => r.Employee);
            return View(request.ToList());
        }

        // GET: Requests/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Request request = db.Request.Find(id);
            if (request == null)
            {
                return HttpNotFound();
            }
            return View(request);
        }

        // GET: Requests/Create
        public ActionResult Create()
        {
            ViewBag.client_id = new SelectList(db.Client, "id", "name");
            ViewBag.employee_id = new SelectList(db.Employee, "id", "name");
            return View();
        }

        // POST: Requests/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,client_id,employee_id,date,comment")] Request request)
        {
            if (ModelState.IsValid)
            {
                db.Request.Add(request);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.client_id = new SelectList(db.Client, "id", "name", request.client_id);
            ViewBag.employee_id = new SelectList(db.Employee, "id", "name", request.employee_id);
            return View(request);
        }

        // GET: Requests/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Request request = db.Request.Find(id);
            if (request == null)
            {
                return HttpNotFound();
            }
            ViewBag.client_id = new SelectList(db.Client, "id", "name", request.client_id);
            ViewBag.employee_id = new SelectList(db.Employee, "id", "name", request.employee_id);
            return View(request);
        }

        // POST: Requests/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,client_id,employee_id,date,comment")] Request request)
        {
            if (ModelState.IsValid)
            {
                db.Entry(request).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.client_id = new SelectList(db.Client, "id", "name", request.client_id);
            ViewBag.employee_id = new SelectList(db.Employee, "id", "name", request.employee_id);
            return View(request);
        }

        // GET: Requests/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Request request = db.Request.Find(id);
            if (request == null)
            {
                return HttpNotFound();
            }
            return View(request);
        }

        // POST: Requests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Request request = db.Request.Find(id);
            db.Request.Remove(request);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Requests/Composition/5
        public ActionResult Composition(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var composition = db.CompositionOfRequest.Where(r => r.request_id == id).Include(c => c.Request).Include(c => c.Work);
            if (composition == null)
            {
                return HttpNotFound();
            }
            return View(composition.ToList());
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
