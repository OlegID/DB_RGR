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
    public class CompositionOfRequestsController : Controller
    {
        private DB_RGREntities db = new DB_RGREntities();

        // GET: CompositionOfRequests
        public ActionResult Index()
        {
            var compositionOfRequest = db.CompositionOfRequest.Include(c => c.Request).Include(c => c.Work);
            return View(compositionOfRequest.ToList());
        }

        // GET: CompositionOfRequests/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompositionOfRequest compositionOfRequest = db.CompositionOfRequest.Find(id);
            if (compositionOfRequest == null)
            {
                return HttpNotFound();
            }
            return View(compositionOfRequest);
        }

        // GET: CompositionOfRequests/Create
        public ActionResult Create()
        {
            ViewBag.request_id = new SelectList(db.Request, "id", "comment");
            ViewBag.work_id = new SelectList(db.Work, "id", "name");
            return View();
        }

        // POST: CompositionOfRequests/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,request_id,work_id,date")] CompositionOfRequest compositionOfRequest)
        {
            if (ModelState.IsValid)
            {
                db.CompositionOfRequest.Add(compositionOfRequest);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.request_id = new SelectList(db.Request, "id", "comment", compositionOfRequest.request_id);
            ViewBag.work_id = new SelectList(db.Work, "id", "name", compositionOfRequest.work_id);
            return View(compositionOfRequest);
        }

        // GET: CompositionOfRequests/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompositionOfRequest compositionOfRequest = db.CompositionOfRequest.Find(id);
            if (compositionOfRequest == null)
            {
                return HttpNotFound();
            }
            ViewBag.request_id = new SelectList(db.Request, "id", "comment", compositionOfRequest.request_id);
            ViewBag.work_id = new SelectList(db.Work, "id", "name", compositionOfRequest.work_id);
            return View(compositionOfRequest);
        }

        // POST: CompositionOfRequests/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,request_id,work_id,date")] CompositionOfRequest compositionOfRequest)
        {
            if (ModelState.IsValid)
            {
                db.Entry(compositionOfRequest).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.request_id = new SelectList(db.Request, "id", "comment", compositionOfRequest.request_id);
            ViewBag.work_id = new SelectList(db.Work, "id", "name", compositionOfRequest.work_id);
            return View(compositionOfRequest);
        }

        // GET: CompositionOfRequests/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompositionOfRequest compositionOfRequest = db.CompositionOfRequest.Find(id);
            if (compositionOfRequest == null)
            {
                return HttpNotFound();
            }
            return View(compositionOfRequest);
        }

        // POST: CompositionOfRequests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CompositionOfRequest compositionOfRequest = db.CompositionOfRequest.Find(id);
            db.CompositionOfRequest.Remove(compositionOfRequest);
            db.SaveChanges();
            return RedirectToAction("Index");
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
