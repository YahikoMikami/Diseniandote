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
    public class DatosEnviosController : Controller
    {
        private diseniandoteEntities db = new diseniandoteEntities();

        // GET: DatosEnvios
        public ActionResult Index()
        {
            var datosEnvio = db.DatosEnvio.Include(d => d.Cliente).Include(d => d.Domicilio);
            return View(datosEnvio.ToList());
        }

        // GET: DatosEnvios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DatosEnvio datosEnvio = db.DatosEnvio.Find(id);
            if (datosEnvio == null)
            {
                return HttpNotFound();
            }
            return View(datosEnvio);
        }

        // GET: DatosEnvios/Create
        public ActionResult Create()
        {
            ViewBag.idCliente = new SelectList(db.Cliente, "idCliente", "idCliente");
            ViewBag.idDomicilio = new SelectList(db.Domicilio, "idDomicilio", "calle");
            return View();
        }

        // POST: DatosEnvios/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idDatosEnvio,idCliente,idDomicilio")] DatosEnvio datosEnvio)
        {
            if (ModelState.IsValid)
            {
                db.DatosEnvio.Add(datosEnvio);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idCliente = new SelectList(db.Cliente, "idCliente", "idCliente", datosEnvio.idCliente);
            ViewBag.idDomicilio = new SelectList(db.Domicilio, "idDomicilio", "calle", datosEnvio.idDomicilio);
            return View(datosEnvio);
        }

        // GET: DatosEnvios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DatosEnvio datosEnvio = db.DatosEnvio.Find(id);
            if (datosEnvio == null)
            {
                return HttpNotFound();
            }
            ViewBag.idCliente = new SelectList(db.Cliente, "idCliente", "idCliente", datosEnvio.idCliente);
            ViewBag.idDomicilio = new SelectList(db.Domicilio, "idDomicilio", "calle", datosEnvio.idDomicilio);
            return View(datosEnvio);
        }

        // POST: DatosEnvios/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idDatosEnvio,idCliente,idDomicilio")] DatosEnvio datosEnvio)
        {
            if (ModelState.IsValid)
            {
                db.Entry(datosEnvio).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idCliente = new SelectList(db.Cliente, "idCliente", "idCliente", datosEnvio.idCliente);
            ViewBag.idDomicilio = new SelectList(db.Domicilio, "idDomicilio", "calle", datosEnvio.idDomicilio);
            return View(datosEnvio);
        }

        // GET: DatosEnvios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DatosEnvio datosEnvio = db.DatosEnvio.Find(id);
            if (datosEnvio == null)
            {
                return HttpNotFound();
            }
            return View(datosEnvio);
        }

        // POST: DatosEnvios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DatosEnvio datosEnvio = db.DatosEnvio.Find(id);
            db.DatosEnvio.Remove(datosEnvio);
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
