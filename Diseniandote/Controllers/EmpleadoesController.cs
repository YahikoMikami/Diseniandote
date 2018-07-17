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
    public class EmpleadoesController : Controller
    {
        private diseniandoteEntities db = new diseniandoteEntities();

        // GET: Empleadoes
        public ActionResult Index()
        {
            var empleado = db.Empleado.Include(e => e.Persona).Include(e => e.TipoEmpleado).Include(e => e.Usuario);
            return View(empleado.ToList());
        }

        // GET: Empleadoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empleado empleado = db.Empleado.Find(id);
            if (empleado == null)
            {
                return HttpNotFound();
            }
            return View(empleado);
        }

        // GET: Empleadoes/Create
        public ActionResult Create()
        {
            ViewBag.idPersona = new SelectList(db.Persona, "idpersona", "nombre");
            ViewBag.idTipoEmpleado = new SelectList(db.TipoEmpleado, "idTipoEmpleado", "descripcion");
            ViewBag.idUsuario = new SelectList(db.Usuario, "idUsuario", "correoElectronico");
            return View();
        }

        // POST: Empleadoes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idEmpleado,fechaIngreso,fechaSalida,salario,nss,estatus,idPersona,idTipoEmpleado,idUsuario")] Empleado empleado)
        {
            if (ModelState.IsValid)
            {
                db.Empleado.Add(empleado);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idPersona = new SelectList(db.Persona, "idpersona", "nombre", empleado.idPersona);
            ViewBag.idTipoEmpleado = new SelectList(db.TipoEmpleado, "idTipoEmpleado", "descripcion", empleado.idTipoEmpleado);
            ViewBag.idUsuario = new SelectList(db.Usuario, "idUsuario", "correoElectronico", empleado.idUsuario);
            return View(empleado);
        }

        // GET: Empleadoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empleado empleado = db.Empleado.Find(id);
            if (empleado == null)
            {
                return HttpNotFound();
            }
            ViewBag.idPersona = new SelectList(db.Persona, "idpersona", "nombre", empleado.idPersona);
            ViewBag.idTipoEmpleado = new SelectList(db.TipoEmpleado, "idTipoEmpleado", "descripcion", empleado.idTipoEmpleado);
            ViewBag.idUsuario = new SelectList(db.Usuario, "idUsuario", "correoElectronico", empleado.idUsuario);
            return View(empleado);
        }

        // POST: Empleadoes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idEmpleado,fechaIngreso,fechaSalida,salario,nss,estatus,idPersona,idTipoEmpleado,idUsuario")] Empleado empleado)
        {
            if (ModelState.IsValid)
            {
                db.Entry(empleado).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idPersona = new SelectList(db.Persona, "idpersona", "nombre", empleado.idPersona);
            ViewBag.idTipoEmpleado = new SelectList(db.TipoEmpleado, "idTipoEmpleado", "descripcion", empleado.idTipoEmpleado);
            ViewBag.idUsuario = new SelectList(db.Usuario, "idUsuario", "correoElectronico", empleado.idUsuario);
            return View(empleado);
        }

        // GET: Empleadoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empleado empleado = db.Empleado.Find(id);
            if (empleado == null)
            {
                return HttpNotFound();
            }
            return View(empleado);
        }

        // POST: Empleadoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Empleado empleado = db.Empleado.Find(id);
            db.Empleado.Remove(empleado);
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
