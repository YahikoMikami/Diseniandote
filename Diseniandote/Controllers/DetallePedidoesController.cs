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
    public class DetallePedidoesController : Controller
    {
        private diseniandoteEntities db = new diseniandoteEntities();

        // GET: DetallePedidoes
        public ActionResult Index()
        {
            var detallePedido = db.DetallePedido.Include(d => d.Pedido).Include(d => d.Producto);
            return View(detallePedido.ToList());
        }

        // GET: DetallePedidoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DetallePedido detallePedido = db.DetallePedido.Find(id);
            if (detallePedido == null)
            {
                return HttpNotFound();
            }
            return View(detallePedido);
        }

        // GET: DetallePedidoes/Create
        public ActionResult Create()
        {
            ViewBag.idPedido = new SelectList(db.Pedido, "idPedido", "numTarjeta");
            ViewBag.idProducto = new SelectList(db.Producto, "idProducto", "nombre");
            return View();
        }

        // POST: DetallePedidoes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idDetallePedido,cantidad,descuento,precioU,precioDesc,idProducto,idPedido")] DetallePedido detallePedido)
        {
            if (ModelState.IsValid)
            {
                db.DetallePedido.Add(detallePedido);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idPedido = new SelectList(db.Pedido, "idPedido", "numTarjeta", detallePedido.idPedido);
            ViewBag.idProducto = new SelectList(db.Producto, "idProducto", "nombre", detallePedido.idProducto);
            return View(detallePedido);
        }

        // GET: DetallePedidoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DetallePedido detallePedido = db.DetallePedido.Find(id);
            if (detallePedido == null)
            {
                return HttpNotFound();
            }
            ViewBag.idPedido = new SelectList(db.Pedido, "idPedido", "numTarjeta", detallePedido.idPedido);
            ViewBag.idProducto = new SelectList(db.Producto, "idProducto", "nombre", detallePedido.idProducto);
            return View(detallePedido);
        }

        // POST: DetallePedidoes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idDetallePedido,cantidad,descuento,precioU,precioDesc,idProducto,idPedido")] DetallePedido detallePedido)
        {
            if (ModelState.IsValid)
            {
                db.Entry(detallePedido).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idPedido = new SelectList(db.Pedido, "idPedido", "numTarjeta", detallePedido.idPedido);
            ViewBag.idProducto = new SelectList(db.Producto, "idProducto", "nombre", detallePedido.idProducto);
            return View(detallePedido);
        }

        // GET: DetallePedidoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DetallePedido detallePedido = db.DetallePedido.Find(id);
            if (detallePedido == null)
            {
                return HttpNotFound();
            }
            return View(detallePedido);
        }

        // POST: DetallePedidoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DetallePedido detallePedido = db.DetallePedido.Find(id);
            db.DetallePedido.Remove(detallePedido);
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
