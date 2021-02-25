using LVModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppConfigLV.Models;

namespace WebAppConfigLV.Controllers
{
    public class UsuariosController : Controller
    {
        // GET: Usuarios
        public ActionResult Index()
        {
            List<UsuarioViewModel> lista = new List<UsuarioViewModel>();
            var listaUsuarios = new ListaUsuarios().Lista;
            foreach (var usuario in listaUsuarios)
            {
                lista.Add(new UsuarioViewModel(usuario));
            }


            var listaOrdenada = lista.OrderBy(x => x.GuidUsuario);

            return View(listaOrdenada);
        }

        public ActionResult EditaDisciplinasUser(string guid)
        {

            UserDisciplinaViewModel userDisciplinaViewModel = new UserDisciplinaViewModel(guid);

            List<CheckDisciplina> lista = userDisciplinaViewModel.ListaChecks;
           
            return View(lista);
        }


        public ActionResult IncluiExcluiDisciplinaUsuario(string guidUsuario, int idDisciplina)
        {

            UserDisciplinaViewModel userDisciplinaViewModel = new UserDisciplinaViewModel(guidUsuario);

            userDisciplinaViewModel.Altera(idDisciplina);

            return RedirectToAction("EditaDisciplinasUser", "Usuarios", new { guid = guidUsuario });
        }


        // GET: Usuarios/Details/5
        public ActionResult Details(string guid)
        {
            List<UsuarioViewModel> lista = new List<UsuarioViewModel>();
            var listaUsuarios = new ListaUsuarios().Lista;
            foreach (var user in listaUsuarios)
            {
                lista.Add(new UsuarioViewModel(user));
            }
            var usuario = lista.Find(x => x.GuidUsuario == guid);
            return View(usuario);
        }

        // GET: Usuarios/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Usuarios/Create
        [HttpPost]
        public ActionResult Create(UsuarioViewModel usuarioViewModel)
        {
            if (ModelState.IsValid)
            {
                usuarioViewModel.PersisteNovo();
                return RedirectToAction("Index");
            }

            return View();
        }

        public ActionResult ValidaSiglaDisciplina(string siglaDisciplina)
        {
            var existe = new ListaDisciplinas().ExisteDestaSigla(siglaDisciplina);

            return Json(existe, JsonRequestBehavior.AllowGet);
        }

        // GET: Usuarios/Edit/5
        public ActionResult Edit(string guid)
        {
            UsuarioViewModel usuarioViewModel = new UsuarioViewModel(guid);
            return View(usuarioViewModel);
        }

        // POST: Usuarios/Edit/5
        [HttpPost]
        public ActionResult Edit(UsuarioViewModel usuarioViewModel)
        {
            if (ModelState.IsValid)
            {
                usuarioViewModel.Update();
                return RedirectToAction("Index");
            }

            return View(usuarioViewModel);
        }

        // GET: Usuarios/Delete/5
        public ActionResult Delete(string guid)
        {
            return View();
        }

        // POST: Usuarios/Delete/5
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
