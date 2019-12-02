using Formatics.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Formatics.Controllers
{
    public class ProcedureController : Controller
    {
        // GET: Procedure
        ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {

            return View();
        }
        
        // GET: Procedure/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Procedure/Create
        public ActionResult Create()
        {
          

            return View();
        }

        // POST: Procedure/Create
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

        // GET: Procedure/Edit/5
        public ActionResult Edit(int id)
        {
           
            return View();
        }

        // POST: Procedure/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Diagnosis diagnosis)
        {
            try
            {
              
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Procedure/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Procedure/Delete/5
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
