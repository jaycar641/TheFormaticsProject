using Formatics.Models;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace Formatics.Controllers
{
    public class InterventionController : Controller
    {
        // GET: Intervention
        ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()//Treatment Plan Homepage
        {
            ////////////////////////////////////////////LOAD CURRENT INFO
            DateTime currentDate = DateTime.Today;
            string userId = User.Identity.GetUserId();
            Patient patient = db.patients.Where(e => e.ApplicationId == userId).SingleOrDefault();
            Diagnosis diagnosis1 = db.diagnoses.Where(e => e.isCurrent == true).SingleOrDefault();

            PatientDiagnosis patientDiagnosis = db.patientDiagnoses.Where(e => e.PatientNumber == patient.PatientNumber && e.DiagnosisId == diagnosis1.DiagnosisId).SingleOrDefault(); //only one
            Diagnosis diagnosis = db.diagnoses.Where(e => e.DiagnosisId == patientDiagnosis.DiagnosisId).SingleOrDefault();//only one
            Intervention intervention = db.interventions.Where(e => e.InterventionId == diagnosis.InterventionId).SingleOrDefault();
            Steps step = db.steps.Where(e => e.Date.Day == currentDate.Day && e.Date.Month == currentDate.Month && e.Date.Year == currentDate.Year && e.InterventionId == intervention.InterventionId).SingleOrDefault();
            List<Steps> steps = db.steps.Where(e => e.InterventionId == intervention.InterventionId && e.Date >= currentDate).ToList(); //all your steps
            List<StepMedicine> stepMedicines = new List<StepMedicine>();
            List<StepProcedure> stepProcedures = new List<StepProcedure>();
            List<Procedure> procedures = new List<Procedure>();
            Medicine med = db.medicine.Where(e => e.isCurrent).SingleOrDefault();
            List<Medicine> medicines = new List<Medicine>();
            List<int> ratings = new List<int>();
            for(int i = 1; i <=10; i++)
            {
                ratings.Add(i);
            }

            //Alert alert = db.alerts.Where(e=> e.type == "Appointment" && e.description == null && e.)
            ////////////////////////////////////////////////LOAD ALERT
                Alert appointmentAlert = new Alert();
                appointmentAlert.frequency = 1;
                appointmentAlert.time = DateTime.Today;
                ViewData["Appointment"] = appointmentAlert;
       
            ///////////////////////////////////////FEEDBACK FUNCTION
            Feedback feedback1 = db.feedbacks.Where(e => e.date.Day == currentDate.Day && e.date.Month == currentDate.Month && e.date.Year == currentDate.Year && e.type == "Mood").SingleOrDefault();
            Feedback feedback2 = db.feedbacks.Where(e => e.date.Day == currentDate.Day && e.date.Month == currentDate.Month && e.date.Year == currentDate.Year && e.type == "Condition").SingleOrDefault();
            
            if (feedback1 == null && feedback2 != null)
            {
                Feedback condition = db.feedbacks.Where(e => e.date.Day == currentDate.Day && e.date.Month == currentDate.Month && e.date.Year == currentDate.Year && e.type == "Condition").SingleOrDefault();

                Feedback mood = new Feedback();
                mood.type = "Mood";
                mood.PatientNumber = patient.PatientNumber;
                mood.StepId = step.StepId;
                mood.date = DateTime.Today;
                ViewData["Mood"] = mood;
                ViewData["Condition"] = condition;
                ViewBag.Name = "display: none";


            }
            else if (feedback1 != null && feedback2 == null)
            {
                Feedback mood1 = db.feedbacks.Where(e => e.date.Day == currentDate.Day && e.date.Month == currentDate.Month && e.date.Year == currentDate.Year && e.type == "Mood").SingleOrDefault();

                Feedback condition1 = new Feedback();

                condition1.type = "Condition";
                condition1.PatientNumber = patient.PatientNumber;
                condition1.StepId = step.StepId;
                condition1.date = DateTime.Today;
                ViewData["Condition"] = condition1;
                ViewData["Mood"] = mood1;
                ViewBag.PartialStyle = "display: none";

            }
            else if (feedback1 == null && feedback2 == null)
            {
                Feedback condition2 = new Feedback();
                Feedback mood2 = new Feedback();

                mood2.type = "Mood";
                mood2.PatientNumber = patient.PatientNumber;
                mood2.StepId = step.StepId;
                mood2.date = DateTime.Today;
           
                condition2.type = "Condition";
                condition2.PatientNumber = patient.PatientNumber;
                condition2.StepId = step.StepId;
                condition2.date = DateTime.Today;
                ViewData["Condition"] = condition2;
                ViewData["Mood"] = mood2;


            }

            else
            {

                Feedback mood4 = db.feedbacks.Where(e => e.date.Day == currentDate.Day && e.date.Month == currentDate.Month && e.date.Year == currentDate.Year && e.type == "Mood").SingleOrDefault();
                Feedback condition4 = db.feedbacks.Where(e => e.date.Day == currentDate.Day && e.date.Month == currentDate.Month && e.date.Year == currentDate.Year && e.type == "Condition").SingleOrDefault();
                ViewData["Mood"] = mood4;
                ViewData["Condition"] = condition4;
                ViewBag.PartialStyle = "display: none";
                ViewBag.Name = "display: none";

            }


            List<Medicine> medlist = db.medicine.ToList();
            List<Procedure> prolist = db.procedures.ToList();

            ///////////////////////////////////////LOAD JUNCTIONS
            foreach(Steps steps1 in steps)
            {
                if (medlist.Count != 0)
                {
                    try
                    {
                        StepMedicine stepMedicine = db.stepMedicines.Where(e => e.StepId == steps1.StepId).SingleOrDefault();
                        stepMedicines.Add(stepMedicine);
                    }
                    catch
                    {
                        continue;
                    }
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
                        continue;
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
                        continue;
                    }
                }
            }

            foreach (StepMedicine stepMedicine1 in stepMedicines)
            {
                if (medlist.Count != 0)
                {
                    try
                    {
                        Medicine medicine = db.medicine.Where(e => e.MedicineId == stepMedicine1.MedicineId).SingleOrDefault();
                        medicines.Add(medicine);
                    }
                    catch
                    {
                        continue;
                    }
                }
            }


            ViewData["Steps"] = steps;
            ViewData["StepsMedicine"] = stepMedicines;
            ViewData["StepsProcedure"] = stepProcedures;
            ViewData["Procedure"] = procedures;
            ViewData["Medicine"] = medicines;
            ViewData["Diagnosis"] = diagnosis.category;
            ViewData["Glimpse"] = step.day;
            ViewData["Day"] = step.description;
            ViewData["Patient"] = patient;
            ViewData["CurrentMed"] = med.name;



            /////////////////////////////////DATAPOINTS FUNCTION
            List<DataPoint> dataPoints1 = new List<DataPoint>();
            List<DataPoint> dataPoints2 = new List<DataPoint>();
            for (int i = 0; i < intervention.duration; i ++)
            {
                DateTime testfeed = diagnosis.dateDiagnosed.AddDays(i);
                Feedback feedback = db.feedbacks.Where(e => e.date.Day == testfeed.Day  && e.date.Month == testfeed.Month && e.date.Year == testfeed.Year && e.type == "Mood").SingleOrDefault();
                try
                {
                    dataPoints1.Add(new DataPoint(testfeed.ToShortDateString(), feedback.rating));
                }
                catch
                {
                    dataPoints1.Add(new DataPoint(testfeed.ToShortDateString(), 0));

                }


            }



            for (int i = 0; i < intervention.duration; i++)
            {
                DateTime testfeed1 = diagnosis.dateDiagnosed.AddDays(i);
                Feedback feedback = db.feedbacks.Where(e => e.date.Day == testfeed1.Day && e.date.Month == testfeed1.Month && e.date.Year == testfeed1.Year && e.type == "Condition").SingleOrDefault();
                try
                {
                    dataPoints2.Add(new DataPoint(testfeed1.ToShortDateString(), feedback.rating));
                }
                catch
                {
                    dataPoints2.Add(new DataPoint(testfeed1.ToShortDateString(), 0));

                }
            }


            ViewBag.DataPoints1 = JsonConvert.SerializeObject(dataPoints1); //mood
            ViewBag.DataPoints2 = JsonConvert.SerializeObject(dataPoints2); //condition

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

        public ActionResult DrawChart()
        {
            Feedback feedback = new Feedback();
            return View(feedback);
        }
        
        public ActionResult test ()
        {
          
            return View();
        }
    }
}
