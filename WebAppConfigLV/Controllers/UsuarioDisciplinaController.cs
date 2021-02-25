using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebAppConfigLV.Controllers
{
    public class UsuarioDisciplinaController : Controller
    {
        // GET: UsuarioDisciplina
        public ActionResult Index()
        {
            return View();
        }

        // GET: UsuarioDisciplina/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UsuarioDisciplina/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UsuarioDisciplina/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: UsuarioDisciplina/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UsuarioDisciplina/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: UsuarioDisciplina/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UsuarioDisciplina/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
