using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebAplicacionControldeGastos.Models;

namespace WebAplicacionControldegastos.Controllers
{
    public class CategoriasController : Controller
    {
        // Conexión principal a la base de datos
        private gastosmodel db = new gastosmodel();

        // ==========================================
        // 1. INDEX: Muestra la tabla principal
        // ==========================================
        public ActionResult Index()
        {
            var listaCategorias = db.Categorias.ToList();
            return View(listaCategorias);
        }

        // ==========================================
        // 2. DETAILS: Muestra la información de una sola categoría
        // ==========================================
        public ActionResult Details(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Categorias categoria = db.Categorias.Find(id);
            if (categoria == null) return HttpNotFound();

            return View(categoria);
        }

        // ==========================================
        // 3. CREATE (GET): Muestra el formulario en blanco
        // ==========================================
        public ActionResult Create()
        {
            return View();
        }

        // ==========================================
        // 4. CREATE (POST): Guarda la nueva categoría en la BD
        // ==========================================
        [HttpPost]
        [ValidateAntiForgeryToken] // Seguridad contra hackeos
        public ActionResult Create([Bind(Include = "Id,Nombre")] Categorias categoria)
        {
            if (ModelState.IsValid)
            {
                db.Categorias.Add(categoria);
                db.SaveChanges(); // Guarda en SQL
                return RedirectToAction("Index"); // Regresa a la tabla
            }
            return View(categoria);
        }

        // ==========================================
        // 5. EDIT (GET): Muestra el formulario con los datos llenos
        // ==========================================
        public ActionResult Edit(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Categorias categoria = db.Categorias.Find(id);
            if (categoria == null) return HttpNotFound();

            return View(categoria);
        }

        // ==========================================
        // 6. EDIT (POST): Actualiza los datos modificados en la BD
        // ==========================================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nombre")] Categorias categoria)
        {
            if (ModelState.IsValid)
            {
                db.Entry(categoria).State = EntityState.Modified;
                db.SaveChanges(); // Actualiza en SQL
                return RedirectToAction("Index");
            }
            return View(categoria);
        }

        // ==========================================
        // 7. DELETE (GET): Pantalla de confirmación para borrar
        // ==========================================
        public ActionResult Delete(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Categorias categoria = db.Categorias.Find(id);
            if (categoria == null) return HttpNotFound();

            return View(categoria);
        }

        // ==========================================
        // 8. DELETE (POST): Ejecuta el borrado final en la BD
        // ==========================================
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Categorias categoria = db.Categorias.Find(id);
            db.Categorias.Remove(categoria);
            db.SaveChanges(); // Elimina de SQL
            return RedirectToAction("Index");
        }

        // ==========================================
        // MÉTODO DE LIMPIEZA: Cierra la conexión a la BD
        // ==========================================
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