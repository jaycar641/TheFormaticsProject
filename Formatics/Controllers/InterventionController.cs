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
            Diagnosis diagnosis1 = db.diagnoses.Where(e => e.isCurrent == true).SingleOrDefault();

            PatientDiagnosis patientDiagnosis = db.patientDiagnoses.Where(e => e.PatientNumber == patient.PatientNumber && e.DiagnosisId == diagnosis1.DiagnosisId).SingleOrDefault(); //only one
            Diagnosis diagnosis = db.diagnoses.Where(e => e.DiagnosisId == patientDiagnosis.DiagnosisId).SingleOrDefault();//only one
            Intervention intervention = db.interventions.Where(e => e.InterventionId == diagnosis.InterventionId).SingleOrDefault();
            Steps step = db.steps.Where(e => e.Date.Day == currentDate.Day && e.InterventionId == intervention.InterventionId).SingleOrDefault();
            List<Steps> steps = db.steps.Where(e => e.InterventionId == intervention.InterventionId && e.Date >= currentDate).ToList(); //all your steps
            List<StepMedicine> stepMedicines = new List<StepMedicine>();
            List<StepProcedure> stepProcedures = new List<StepProcedure>();
            List<Procedure> procedures = new List<Procedure>();
            List<Medicine> medicines = new List<Medicine>();
            List<int> ratings = new List<int>();
            for(int i = 1; i <=10; i++)
            {
                ratings.Add(i);
            }

            Alert appointmentAlert = new Alert();
            appointmentAlert.type = "Appointment";
            appointmentAlert.frequency = 1;
            appointmentAlert.time = DateTime.Today.Date;
            db.alerts.Add(appointmentAlert);
            db.SaveChanges();
            Feedback mood = new Feedback();
            Feedback condition = new Feedback();

            Feedback test = db.feedbacks.Where(e => e.comments == null && e.rating == 0).SingleOrDefault();
            if (test != null  ) // when you first load the page there will be no empty feed backs
            {
                mood.type = "Mood";
                mood.PatientNumber = patient.PatientNumber;
                mood.StepId = step.StepId;
                mood.date = DateTime.Now;
                db.feedbacks.Add(mood);
                db.SaveChanges();

                condition.type = "Condition";
                condition.PatientNumber = patient.PatientNumber;
                condition.StepId = step.StepId;
                condition.date = DateTime.Now;
                db.feedbacks.Add(condition);
                db.SaveChanges();
            }
          
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
            ViewData["Mood"] = mood;
            ViewData["Condition"] = condition;
            ViewData["Glimpse"] = step.day;
            ViewData["Day"] = step.description;
            ViewData["Patient"] = patient;
            ViewData["Appointment"] = appointmentAlert;
            ViewData["AppointmentTime"] = appointmentAlert.time;
            ViewData["AppointmentType"] = appointmentAlert.type; 

            return View();
        }

        // GET: Intervention/Details/5
        public ActionResult Details()
        {

            string userId = User.Identity.GetUserId();
            Patient patient = db.patients.Where(e => e.ApplicationId == userId).SingleOrDefault();
            ViewData["Patient"] = patient; //temporary
            return RedirectToAction("Details", "Patient", new { PatientNumber = patient.PatientNumber });
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

        public ActionResult Medication(int patientNumber)
        {
            string userId = User.Identity.GetUserId();
            Patient patient = db.patients.Where(e => e.ApplicationId == userId).SingleOrDefault();
            ViewData["Patient"] = patient; //temporary
            return RedirectToAction("Index", "Medicine");

        }

        public ActionResult SetAppointment(int AlertId)
        {
            Alert alert = db.alerts.Where(e => e.AlertId == AlertId).SingleOrDefault();
            //twillio
           return PartialView("_Results", alert);
        }

        
    }
}
