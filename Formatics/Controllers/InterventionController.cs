using Formatics.Models;
using Microsoft.AspNet.Identity;
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

        public ActionResult Index()//Treatment Plan Homepage
        {
            DateTime currentDate = DateTime.Today;
            string userId = User.Identity.GetUserId();
            Patient patient = db.patients.Where(e => e.ApplicationId == userId).SingleOrDefault();
            PatientDiagnosis patientDiagnosis = db.patientDiagnoses.Where(e => e.PatientNumber == patient.PatientNumber).SingleOrDefault(); //only one
            Diagnosis diagnosis = db.diagnoses.Where(e => e.DiagnosisId == patientDiagnosis.DiagnosisId).SingleOrDefault();//only one
            Intervention intervention = db.interventions.Where(e => e.InterventionId == diagnosis.InterventionId).SingleOrDefault();
            Steps step = db.steps.Where(e => e.Date.Day == currentDate.Day && e.InterventionId == intervention.InterventionId).SingleOrDefault();

            Feedback mood = new Feedback();
            mood.type = "Mood";
            mood.PatientNumber = patient.PatientNumber;
            mood.StepId = step.StepId;
            db.SaveChanges();
            Feedback condition = new Feedback();
            condition.type = "Condition";
            condition.PatientNumber = patient.PatientNumber;
            condition.StepId = step.StepId;
            db.SaveChanges();
            // ViewData["Steps"] =
            // ViewData["StepsMedicine"] =
            // ViewData["StepsProcedure"] =
            // ViewData["Procedure"] =
            // ViewData["Medicine"] =

            ViewData["Diagnosis"] = diagnosis;
            ViewData["Submit Mood"] = mood;
            ViewData["Submit Condition"] = condition;
            ViewData["Daily Planner"] = step;
            return View();
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
