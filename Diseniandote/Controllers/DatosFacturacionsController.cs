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
    public class DatosFacturacionsController : Controller
    {
        private diseniandoteEntities db = new diseniandoteEntities();

        // GET: DatosFacturacions
        public ActionResult Index()
        {
            var datosFacturacion = db.DatosFacturacion.Include(d => d.Cliente).Include(d => d.Domicilio);
            return View(datosFacturacion.ToList());
        }

        // GET: DatosFacturacions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DatosFacturacion datosFacturacion = db.DatosFacturacion.Find(id);
            if (datosFacturacion == null)
            {
                return HttpNotFound();
            }
            return View(datosFacturacion);
        }

        // GET: DatosFacturacions/Create
        public ActionResult Create()
        {
            ViewBag.idCliente = new SelectList(db.Cliente, "idCliente", "idCliente");
            ViewBag.idDatosFacturacion = new SelectList(db.Domicilio, "idDomicilio", "calle");
            return View();
        }

        // POST: DatosFacturacions/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idDatosFacturacion,rfc,razonSocial,idDomicilio,idCliente")] DatosFacturacion datosFacturacion)
        {
            if (ModelState.IsValid)
            {
                db.DatosFacturacion.Add(datosFacturacion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idCliente = new SelectList(db.Cliente, "idCliente", "idCliente", datosFacturacion.idCliente);
            ViewBag.idDatosFacturacion = new SelectList(db.Domicilio, "idDomicilio", "calle", datosFacturacion.idDatosFacturacion);
            return View(datosFacturacion);
        }

        // GET: DatosFacturacions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DatosFacturacion datosFacturacion = db.DatosFacturacion.Find(id);
            if (datosFacturacion == null)
            {
                return HttpNotFound();
            }
            ViewBag.idCliente = new SelectList(db.Cliente, "idCliente", "idCliente", datosFacturacion.idCliente);
            ViewBag.idDatosFacturacion = new SelectList(db.Domicilio, "idDomicilio", "calle", datosFacturacion.idDatosFacturacion);
            return View(datosFacturacion);
        }

        // POST: DatosFacturacions/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idDatosFacturacion,rfc,razonSocial,idDomicilio,idCliente")] DatosFacturacion datosFacturacion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(datosFacturacion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idCliente = new SelectList(db.Cliente, "idCliente", "idCliente", datosFacturacion.idCliente);
            ViewBag.idDatosFacturacion = new SelectList(db.Domicilio, "idDomicilio", "calle", datosFacturacion.idDatosFacturacion);
            return View(datosFacturacion);
        }

        // GET: DatosFacturacions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DatosFacturacion datosFacturacion = db.DatosFacturacion.Find(id);
            if (datosFacturacion == null)
            {
                return HttpNotFound();
            }
            return View(datosFacturacion);
        }

        // POST: DatosFacturacions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DatosFacturacion datosFacturacion = db.DatosFacturacion.Find(id);
            db.DatosFacturacion.Remove(datosFacturacion);
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
