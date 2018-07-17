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
    public class ProduccionsController : Controller
    {
        private diseniandoteEntities db = new diseniandoteEntities();

        // GET: Produccions
        public ActionResult Index()
        {
            var produccion = db.Produccion.Include(p => p.Producto).Include(p => p.TipoProduccion);
            return View(produccion.ToList());
        }

        // GET: Produccions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Produccion produccion = db.Produccion.Find(id);
            if (produccion == null)
            {
                return HttpNotFound();
            }
            return View(produccion);
        }

        // GET: Produccions/Create
        public ActionResult Create()
        {
            ViewBag.idProducto = new SelectList(db.Producto, "idProducto", "nombre");
            ViewBag.idTipoProduccion = new SelectList(db.TipoProduccion, "idTipoProduccion", "descripcion");
            return View();
        }

        // POST: Produccions/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idProduccion,idTipoProduccion,costoDisenio,costoManoxTiempo,porcentajeGanancia,subtotalMateriales,idProducto,cantidadProducto")] Produccion produccion)
        {
            if (ModelState.IsValid)
            {
                db.Produccion.Add(produccion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idProducto = new SelectList(db.Producto, "idProducto", "nombre", produccion.idProducto);
            ViewBag.idTipoProduccion = new SelectList(db.TipoProduccion, "idTipoProduccion", "descripcion", produccion.idTipoProduccion);
            return View(produccion);
        }

        // GET: Produccions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Produccion produccion = db.Produccion.Find(id);
            if (produccion == null)
            {
                return HttpNotFound();
            }
            ViewBag.idProducto = new SelectList(db.Producto, "idProducto", "nombre", produccion.idProducto);
            ViewBag.idTipoProduccion = new SelectList(db.TipoProduccion, "idTipoProduccion", "descripcion", produccion.idTipoProduccion);
            return View(produccion);
        }

        // POST: Produccions/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idProduccion,idTipoProduccion,costoDisenio,costoManoxTiempo,porcentajeGanancia,subtotalMateriales,idProducto,cantidadProducto")] Produccion produccion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(produccion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idProducto = new SelectList(db.Producto, "idProducto", "nombre", produccion.idProducto);
            ViewBag.idTipoProduccion = new SelectList(db.TipoProduccion, "idTipoProduccion", "descripcion", produccion.idTipoProduccion);
            return View(produccion);
        }

        // GET: Produccions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Produccion produccion = db.Produccion.Find(id);
            if (produccion == null)
            {
                return HttpNotFound();
            }
            return View(produccion);
        }

        // POST: Produccions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Produccion produccion = db.Produccion.Find(id);
            db.Produccion.Remove(produccion);
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
