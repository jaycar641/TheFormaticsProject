using Formatics.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Formatics.Controllers
{
    public class InterventionController : Controller
    {
        // GET: Intervention
        ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index(int patientNumber)//Treatment Plan Homepage
        {
            PatientDiagnosis patientDiagnosis = db.patientDiagnoses.Where(e => e.PatientNumber == patientNumber).SingleOrDefault(); //only one
            Diagnosis diagnosis = db.diagnoses.Where(e => e.DiagnosisId == patientDiagnosis.DiagnosisId).SingleOrDefault();//only one
            Intervention intervention = db.interventions.Where(e => e.InterventionId == diagnosis.InterventionId).SingleOrDefault();

            return View(intervention);
        }

        // GET: Intervention/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }


        // GET: Intervention/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Intervention/Create
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

        // GET: Intervention/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Intervention/Edit/5
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

        // GET: Intervention/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Intervention/Delete/5
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
