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
    public class TipoProduccionsController : Controller
    {
        private diseniandoteEntities db = new diseniandoteEntities();

        // GET: TipoProduccions
        public ActionResult Index()
        {
            return View(db.TipoProduccion.ToList());
        }

        // GET: TipoProduccions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoProduccion tipoProduccion = db.TipoProduccion.Find(id);
            if (tipoProduccion == null)
            {
                return HttpNotFound();
            }
            return View(tipoProduccion);
        }

        // GET: TipoProduccions/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TipoProduccions/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idTipoProduccion,descripcion,estatus")] TipoProduccion tipoProduccion)
        {
            if (ModelState.IsValid)
            {
                db.TipoProduccion.Add(tipoProduccion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tipoProduccion);
        }

        // GET: TipoProduccions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoProduccion tipoProduccion = db.TipoProduccion.Find(id);
            if (tipoProduccion == null)
            {
                return HttpNotFound();
            }
            return View(tipoProduccion);
        }

        // POST: TipoProduccions/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idTipoProduccion,descripcion,estatus")] TipoProduccion tipoProduccion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tipoProduccion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tipoProduccion);
        }

        // GET: TipoProduccions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoProduccion tipoProduccion = db.TipoProduccion.Find(id);
            if (tipoProduccion == null)
            {
                return HttpNotFound();
            }
            return View(tipoProduccion);
        }

        // POST: TipoProduccions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TipoProduccion tipoProduccion = db.TipoProduccion.Find(id);
            db.TipoProduccion.Remove(tipoProduccion);
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
