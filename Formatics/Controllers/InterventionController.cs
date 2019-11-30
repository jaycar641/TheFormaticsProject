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
            List<Steps> steps = db.steps.Where(e => e.InterventionId == intervention.InterventionId && e.Date >= currentDate).ToList(); //all your steps
            List<StepMedicine> stepMedicines = new List<StepMedicine>();
            List<StepProcedure> stepProcedures = new List<StepProcedure>();
            List<Procedure> procedures = new List<Procedure>();
            List<Medicine> medicines = new List<Medicine>();
            
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
            List<Medicine> medlist = db.medicine.ToList();
            List<Procedure> prolist = db.procedures.ToList();
            foreach(Steps steps1 in steps)
            {
                if (medlist.Count != 0)
                {
                    StepMedicine stepMedicine = db.stepMedicines.Where(e => e.StepId == steps1.StepId).SingleOrDefault();
                    stepMedicines.Add(stepMedicine);
                }
            }
            foreach (Steps steps1 in steps)
            {
                if (prolist.Count != 0)
                {
                    try
                    {
                        StepProcedure stepProcedure = db.stepProcedures.Where(e => e.StepId == steps1.StepId).SingleOrDefault();
                        stepProcedures.Add(stepProcedure);
                    }
                    catch
                    {
                        stepProcedures = stepProcedures;
                    }
                }
            }

            foreach (StepProcedure stepProcedure1 in stepProcedures)
            {
                if (prolist.Count != 0)
                {
                    try
                    {
                        Procedure procedure = db.procedures.Where(e => e.ProcedureId == stepProcedure1.ProcedureId).SingleOrDefault();
                        procedures.Add(procedure);
                    }
                    catch
                    {
                        procedures = procedures;
                    }
                }
            }

            foreach (StepMedicine stepMedicine1 in stepMedicines)
            {
                if (medlist.Count != 0)
                {
                    Medicine medicine = db.medicine.Where(e => e.MedicineId == stepMedicine1.MedicineId).SingleOrDefault();
                    medicines.Add(medicine);
                }
            }

            ViewData["Steps"] = steps;
            ViewData["StepsMedicine"] = stepMedicines;
            ViewData["StepsProcedure"] = stepProcedures;
            ViewData["Procedure"] = procedures;
            ViewData["Medicine"] = medicines;
            ViewData["Diagnosis"] = diagnosis.category;
            ViewData["MoodRating"] = mood.rating;
            ViewData["MoodComment"] = mood.comments;
            ViewData["ConditionRating"] = condition.rating;
            ViewData["ConditionComment"] = condition.comments;
            ViewData["Glimpse"] = step.day;
            ViewData["Day"] = step.description;
            ViewData["Patient"] = patient; 

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
