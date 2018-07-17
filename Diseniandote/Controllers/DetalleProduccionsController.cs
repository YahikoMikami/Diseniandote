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
    public class DetalleProduccionsController : Controller
    {
        private diseniandoteEntities db = new diseniandoteEntities();

        // GET: DetalleProduccions
        public ActionResult Index()
        {
            var detalleProduccion = db.DetalleProduccion.Include(d => d.Material).Include(d => d.Produccion);
            return View(detalleProduccion.ToList());
        }

        // GET: DetalleProduccions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DetalleProduccion detalleProduccion = db.DetalleProduccion.Find(id);
            if (detalleProduccion == null)
            {
                return HttpNotFound();
            }
            return View(detalleProduccion);
        }

        // GET: DetalleProduccions/Create
        public ActionResult Create()
        {
            ViewBag.idMaterial = new SelectList(db.Material, "idMaterial", "nombre");
            ViewBag.idProduccion = new SelectList(db.Produccion, "idProduccion", "idProduccion");
            return View();
        }

        // POST: DetalleProduccions/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idDetalleProduccion,idMaterial,idProduccion,cantidad,subCosto")] DetalleProduccion detalleProduccion)
        {
            if (ModelState.IsValid)
            {
                db.DetalleProduccion.Add(detalleProduccion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idMaterial = new SelectList(db.Material, "idMaterial", "nombre", detalleProduccion.idMaterial);
            ViewBag.idProduccion = new SelectList(db.Produccion, "idProduccion", "idProduccion", detalleProduccion.idProduccion);
            return View(detalleProduccion);
        }

        // GET: DetalleProduccions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DetalleProduccion detalleProduccion = db.DetalleProduccion.Find(id);
            if (detalleProduccion == null)
            {
                return HttpNotFound();
            }
            ViewBag.idMaterial = new SelectList(db.Material, "idMaterial", "nombre", detalleProduccion.idMaterial);
            ViewBag.idProduccion = new SelectList(db.Produccion, "idProduccion", "idProduccion", detalleProduccion.idProduccion);
            return View(detalleProduccion);
        }

        // POST: DetalleProduccions/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idDetalleProduccion,idMaterial,idProduccion,cantidad,subCosto")] DetalleProduccion detalleProduccion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(detalleProduccion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idMaterial = new SelectList(db.Material, "idMaterial", "nombre", detalleProduccion.idMaterial);
            ViewBag.idProduccion = new SelectList(db.Produccion, "idProduccion", "idProduccion", detalleProduccion.idProduccion);
            return View(detalleProduccion);
        }

        // GET: DetalleProduccions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DetalleProduccion detalleProduccion = db.DetalleProduccion.Find(id);
            if (detalleProduccion == null)
            {
                return HttpNotFound();
            }
            return View(detalleProduccion);
        }

        // POST: DetalleProduccions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DetalleProduccion detalleProduccion = db.DetalleProduccion.Find(id);
            db.DetalleProduccion.Remove(detalleProduccion);
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
