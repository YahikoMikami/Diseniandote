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
    public class ClientesController : Controller
    {
        private diseniandoteEntities db = new diseniandoteEntities();

        // GET: Clientes
        public ActionResult Index()
        {
            var cliente = db.Cliente.Include(c => c.Persona).Include(c => c.Tarjeta).Include(c => c.Usuario);
            return View(cliente.ToList());
        }

        // GET: Clientes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = db.Cliente.Find(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        // GET: Clientes/Create
        public ActionResult Create()
        {
            ViewBag.idPersona = new SelectList(db.Persona, "idpersona", "nombre");
            ViewBag.idTarjeta = new SelectList(db.Tarjeta, "idTarjeta", "nombre");
            ViewBag.idUsuario = new SelectList(db.Usuario, "idUsuario", "correoElectronico");
            return View();
        }

        // POST: Clientes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idCliente,idTarjeta,idPersona,idUsuario,estatus")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                db.Cliente.Add(cliente);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idPersona = new SelectList(db.Persona, "idpersona", "nombre", cliente.idPersona);
            ViewBag.idTarjeta = new SelectList(db.Tarjeta, "idTarjeta", "nombre", cliente.idTarjeta);
            ViewBag.idUsuario = new SelectList(db.Usuario, "idUsuario", "correoElectronico", cliente.idUsuario);
            return View(cliente);
        }

        // GET: Clientes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = db.Cliente.Find(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            ViewBag.idPersona = new SelectList(db.Persona, "idpersona", "nombre", cliente.idPersona);
            ViewBag.idTarjeta = new SelectList(db.Tarjeta, "idTarjeta", "nombre", cliente.idTarjeta);
            ViewBag.idUsuario = new SelectList(db.Usuario, "idUsuario", "correoElectronico", cliente.idUsuario);
            return View(cliente);
        }

        // POST: Clientes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idCliente,idTarjeta,idPersona,idUsuario,estatus")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cliente).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idPersona = new SelectList(db.Persona, "idpersona", "nombre", cliente.idPersona);
            ViewBag.idTarjeta = new SelectList(db.Tarjeta, "idTarjeta", "nombre", cliente.idTarjeta);
            ViewBag.idUsuario = new SelectList(db.Usuario, "idUsuario", "correoElectronico", cliente.idUsuario);
            return View(cliente);
        }

        // GET: Clientes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = db.Cliente.Find(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cliente cliente = db.Cliente.Find(id);
            db.Cliente.Remove(cliente);
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
