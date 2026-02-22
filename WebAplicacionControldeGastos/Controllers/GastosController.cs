using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebAplicacionControldeGastos.Models; // Conexión a tus modelos

namespace WebAplicacionControldegastos.Controllers
{
    public class GastosController : Controller
    {
        // Tu conexión a la base de datos
        private gastosmodel db = new gastosmodel();

        // ==========================================
        // 1. INDEX: Muestra la tabla de gastos
        // ==========================================
        public ActionResult Index()
        {
            // Usamos .Include() para traer también el nombre de la categoría, no solo su número de ID
            var gastos = db.Gastos.Include(g => g.Categorias).ToList();
            return View(gastos);
        }

        // ==========================================
        // 2. DETAILS: Muestra un solo gasto
        // ==========================================
        public ActionResult Details(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Gastos gasto = db.Gastos.Find(id);
            if (gasto == null) return HttpNotFound();

            return View(gasto);
        }

        // ==========================================
        // 3. CREATE (GET): Muestra el formulario vacío
        // ==========================================
        public ActionResult Create()
        {
            // Magia: Preparamos la lista desplegable de categorías para enviarla a la vista
            ViewBag.CategoriaId = new SelectList(db.Categorias, "Id", "Nombre");
            return View();
        }

        // ==========================================
        // 4. CREATE (POST): Guarda el gasto en SQL
        // ==========================================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Concepto,Monto,FechaRegistro,CategoriaId")] Gastos gasto)
        {
            if (ModelState.IsValid)
            {
                db.Gastos.Add(gasto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            // Si hay error, volvemos a cargar la lista desplegable antes de regresar a la pantalla
            ViewBag.CategoriaId = new SelectList(db.Categorias, "Id", "Nombre", gasto.CategoriaId);
            return View(gasto);
        }

        // ==========================================
        // 5. EDIT (GET): Muestra el formulario lleno
        // ==========================================
        public ActionResult Edit(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Gastos gasto = db.Gastos.Find(id);
            if (gasto == null) return HttpNotFound();

            // Cargamos la lista desplegable y dejamos seleccionada la que el gasto ya tenía
            ViewBag.CategoriaId = new SelectList(db.Categorias, "Id", "Nombre", gasto.CategoriaId);
            return View(gasto);
        }

        // ==========================================
        // 6. EDIT (POST): Actualiza en SQL
        // ==========================================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Concepto,Monto,FechaRegistro,CategoriaId")] Gastos gasto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gasto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoriaId = new SelectList(db.Categorias, "Id", "Nombre", gasto.CategoriaId);
            return View(gasto);
        }

        // ==========================================
        // 7. DELETE (GET): Pantalla de confirmación
        // ==========================================
        public ActionResult Delete(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Gastos gasto = db.Gastos.Find(id);
            if (gasto == null) return HttpNotFound();

            return View(gasto);
        }

        // ==========================================
        // 8. DELETE (POST): Borrado definitivo
        // ==========================================
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Gastos gasto = db.Gastos.Find(id);
            db.Gastos.Remove(gasto);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) db.Dispose();
            base.Dispose(disposing);
        }
    }
}