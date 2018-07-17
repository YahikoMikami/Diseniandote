using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Diseniandote.Models;

namespace Diseniandote.Controllers
{
    public class AlmacensController : Controller
    {
        private diseniandoteEntities db = new diseniandoteEntities();

        // GET: Almacens
        public ActionResult Index()
        {
            var almacen = db.Almacen.Include(a => a.Material);
            return View(almacen.ToList());
        }

        // GET: Almacens/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Almacen almacen = db.Almacen.Find(id);
            if (almacen == null)
            {
                return HttpNotFound();
            }
            return View(almacen);
        }

        // GET: Almacens/Create
        public ActionResult Create()
        {
            ViewBag.idMaterial = new SelectList(db.Material, "idMaterial", "nombre");
            return View();
        }

        // POST: Almacens/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idAlmacen,cantidad,idMaterial")] Almacen almacen)
        {
            if (ModelState.IsValid)
            {
                db.Almacen.Add(almacen);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idMaterial = new SelectList(db.Material, "idMaterial", "nombre", almacen.idMaterial);
            return View(almacen);
        }

        // GET: Almacens/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Almacen almacen = db.Almacen.Find(id);
            if (almacen == null)
            {
                return HttpNotFound();
            }
            ViewBag.idMaterial = new SelectList(db.Material, "idMaterial", "nombre", almacen.idMaterial);
            return View(almacen);
        }

        // POST: Almacens/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idAlmacen,cantidad,idMaterial")] Almacen almacen)
        {
            if (ModelState.IsValid)
            {
                db.Entry(almacen).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idMaterial = new SelectList(db.Material, "idMaterial", "nombre", almacen.idMaterial);
            return View(almacen);
        }

        // GET: Almacens/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Almacen almacen = db.Almacen.Find(id);
            if (almacen == null)
            {
                return HttpNotFound();
            }
            return View(almacen);
        }

        // POST: Almacens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Almacen almacen = db.Almacen.Find(id);
            db.Almacen.Remove(almacen);
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
