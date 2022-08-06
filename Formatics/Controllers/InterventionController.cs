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
        // </summary>
      

          /// <summary>
        /// //////////3 LoadAllSteps////////////////////////////
        /// </summary>
        public void LoadAllSteps(int duration, int interventionId, int patientNumber)
        {
            Intervention intervention = db.interventions.Where(e => e.InterventionId == interventionId).SingleOrDefault();
            Patient patient = db.patients.Where(e => e.PatientNumber == patientNumber).SingleOrDefault();
            
            for (int i = 0; i <= duration - 1; i++)
            {
                Steps steps = new Steps();

                PatientStep patientStep = new PatientStep();
                patientStep.PatientNumber = patient.PatientNumber;
                patientStep.StepId = steps.StepId;
                patientStep.Date = intervention.startDate.AddDays(i);

                steps.InterventionId = interventionId;
                steps.day = i + 1;
                steps.description = null;

                steps.Date = intervention.startDate.AddDays(i);
                db.steps.Add(steps);
                db.patientSteps.Add(patientStep);
                db.SaveChanges();

            }

        }
         public Intervention interventionLoad(string diagnosis, Patient patient)
        {
            Intervention intervention = new Intervention();
            Patient patient1 = db.patients.Where(e => e.PatientNumber == patient.PatientNumber).SingleOrDefault();

            switch (diagnosis) //Use database lingo 
            {
                case "Acute Pain":
                    intervention.category = "Acute Pain Control";
                    intervention.startDate = DateTime.Now;
                    intervention.duration = 180;
                    intervention.endDate = patient1.enrollDate.AddDays(intervention.duration);
                    intervention.expectedOutcome = "Improve";
                    break;
                case "Respiration Alteration":
                    intervention.category = "Breathing Excercises";
                    intervention.startDate = DateTime.Now;
                    intervention.duration = 90;
                    intervention.endDate = patient1.enrollDate.AddDays(intervention.duration);
                    intervention.expectedOutcome = "Improve";
                    break;
                case "Sleep Pattern Disturbance":
                    intervention.startDate = DateTime.Now;
                    intervention.category = "Sleep Patten Control";
                    intervention.duration = 21;
                    intervention.endDate = patient1.enrollDate.AddDays(intervention.duration);
                    intervention.expectedOutcome = "Improve";
                    break;


                default:
                    intervention.category = "Nausea Care";
                    intervention.startDate = DateTime.Now;
                    intervention.duration = 30;
                    intervention.endDate = patient1.enrollDate.AddDays(intervention.duration);
                    intervention.expectedOutcome = "Improve";
                    break;

            }
            return intervention;

        }

        public List<DataPoint> loadDataPointCondition(Intervention intervention, Diagnosis diagnosis)
        {
            DateTime currentDate = DateTime.Today;
            Feedback feedback2 = db.feedbacks.Where(e => e.date.Day == currentDate.Day && e.date.Month == currentDate.Month && e.date.Year == currentDate.Year && e.type == "Condition").SingleOrDefault();

            List<DataPoint> dataPoints1 = new List<DataPoint>();

                    for (int i = 0; i < intervention.duration; i++)
                    {
                        DateTime testfeed1 = diagnosis.dateDiagnosed.AddDays(i);
                        Feedback feedback = db.feedbacks.Where(e => e.date.Day == testfeed1.Day && e.date.Month == testfeed1.Month && e.date.Year == testfeed1.Year && e.type == "Condition").SingleOrDefault();
                        try
                        {
                            dataPoints1.Add(new DataPoint(testfeed1.ToShortDateString(), feedback.rating));
                        }
                        catch
                        {
                            dataPoints1.Add(new DataPoint(testfeed1.ToShortDateString(), 0));

                        }
                    }

            
            return dataPoints1;

        }

        public List<DataPoint> loadDataPointMood(Intervention intervention, Diagnosis diagnosis)
        {
            DateTime currentDate = DateTime.Today;
            Feedback feedback1 = db.feedbacks.Where(e => e.date.Day == currentDate.Day && e.date.Month == currentDate.Month && e.date.Year == currentDate.Year && e.type == "Mood").SingleOrDefault();
            List<DataPoint> dataPoints1 = new List<DataPoint>();

            
                    for (int i = 0; i < intervention.duration; i++)
                    {
                        DateTime testfeed = diagnosis.dateDiagnosed.AddDays(i);
                        Feedback feedback = db.feedbacks.Where(e => e.date.Day == testfeed.Day && e.date.Month == testfeed.Month && e.date.Year == testfeed.Year && e.type == "Mood").SingleOrDefault();
                        try
                        {
                            dataPoints1.Add(new DataPoint(testfeed.ToShortDateString(), feedback.rating));
                        }
                        catch
                        {
                            dataPoints1.Add(new DataPoint(testfeed.ToShortDateString(), 0));

                        }


                    }

            return dataPoints1;

        }
        
        public Feedback LoadCondition(Steps step, Patient patient)
        {
            DateTime currentDate = DateTime.Today;
            Feedback feedback1 = db.feedbacks.Where(e => e.date.Day == currentDate.Day && e.date.Month == currentDate.Month && e.date.Year == currentDate.Year && e.type == "Mood").SingleOrDefault();
            Feedback feedback2 = db.feedbacks.Where(e => e.date.Day == currentDate.Day && e.date.Month == currentDate.Month && e.date.Year == currentDate.Year && e.type == "Condition").SingleOrDefault();


            if (feedback1 == null && feedback2 != null)
            {
                Feedback condition = db.feedbacks.Where(e => e.date.Day == currentDate.Day && e.date.Month == currentDate.Month && e.date.Year == currentDate.Year && e.type == "Condition").SingleOrDefault();
                return condition;
                //ViewBag.Name = "display: none";


            }
            else if (feedback1 != null && feedback2 == null)
            {

                Feedback condition1 = new Feedback();

                condition1.type = "Condition";
                condition1.PatientNumber = patient.PatientNumber;
                condition1.StepId = step.StepId;
                condition1.date = DateTime.Today;
                return condition1;
               // ViewBag.PartialStyle = "display: none";

            }
            else if (feedback1 == null && feedback2 == null)
            {
                Feedback condition2 = new Feedback();
                condition2.type = "Condition";
                condition2.PatientNumber = patient.PatientNumber;
                condition2.StepId = step.StepId;
                condition2.date = DateTime.Today;
                return condition2;


            }

            else
            {

                Feedback condition4 = db.feedbacks.Where(e => e.date.Day == currentDate.Day && e.date.Month == currentDate.Month && e.date.Year == currentDate.Year && e.type == "Condition").SingleOrDefault();
                return condition4;
                //ViewBag.PartialStyle = "display: none";
               // ViewBag.Name = "display: none";

            }

        }

        public Feedback LoadMood(Steps step, Patient patient)
        {
            DateTime currentDate = DateTime.Today;
            Feedback feedback1 = db.feedbacks.Where(e => e.date.Day == currentDate.Day && e.date.Month == currentDate.Month && e.date.Year == currentDate.Year && e.type == "Mood").SingleOrDefault();
            Feedback feedback2 = db.feedbacks.Where(e => e.date.Day == currentDate.Day && e.date.Month == currentDate.Month && e.date.Year == currentDate.Year && e.type == "Condition").SingleOrDefault();


            if (feedback1 == null && feedback2 != null)
            {

                Feedback mood = new Feedback();
                mood.type = "Mood";
                mood.PatientNumber = patient.PatientNumber;
                mood.StepId = step.StepId;
                mood.date = DateTime.Today;
                return mood;
                //ViewBag.Name = "display: none";


            }
            else if (feedback1 != null && feedback2 == null)
            {
                Feedback mood1 = db.feedbacks.Where(e => e.date.Day == currentDate.Day && e.date.Month == currentDate.Month && e.date.Year == currentDate.Year && e.type == "Mood").SingleOrDefault();
                return mood1;
                //ViewBag.PartialStyle = "display: none";

            }
            else if (feedback1 == null && feedback2 == null)
            {
                Feedback mood2 = new Feedback();

                mood2.type = "Mood";
                mood2.PatientNumber = patient.PatientNumber;
                mood2.StepId = step.StepId;
                mood2.date = DateTime.Today;
                return mood2;


            }

            else
            {

                Feedback mood4 = db.feedbacks.Where(e => e.date.Day == currentDate.Day && e.date.Month == currentDate.Month && e.date.Year == currentDate.Year && e.type == "Mood").SingleOrDefault();
                return mood4;
                //ViewBag.PartialStyle = "display: none";
                //ViewBag.Name = "display: none";

            }
        }
        public List<Procedure> loadProcedure(List<Steps> steps)
        {
            List<StepProcedure> stepProcedures = new List<StepProcedure>();
            List<Procedure> procedures = new List<Procedure>();
            List<Procedure> prolist = db.procedures.ToList();

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
            return procedures;
        }
        public List<Medicine> loadMedicine(List<Steps> steps)
        {
            List<StepMedicine> stepMedicines = new List<StepMedicine>();
            List<Medicine> medlist = db.medicine.ToList();
            List<Medicine> medicines = new List<Medicine>();

            foreach (Steps steps1 in steps)
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
            return medicines;
        }

        public List<StepMedicine> loadStepMedicines (List<Steps> steps)
        {
            List<StepMedicine> stepMedicines = new List<StepMedicine>();
            List<Medicine> medlist = db.medicine.ToList();
            foreach (Steps steps1 in steps)
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
            return stepMedicines;
        }



        public List<StepProcedure> loadStepProcedures(List<Steps> steps) 
        {
            List<StepProcedure> stepProcedures = new List<StepProcedure>();
            List<Procedure> prolist = db.procedures.ToList();

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
            return stepProcedures;
        }
        public ActionResult Index()//Treatment Plan Homepage
        {
            DateTime currentDate = DateTime.Today;
            string userId = User.Identity.GetUserId();
            Patient patient = db.patients.Where(e => e.ApplicationId == userId).SingleOrDefault();
            Diagnosis diagnosis1 = db.diagnoses.Where(e => e.isCurrent == true).SingleOrDefault();
            PatientDiagnosis patientDiagnosis = db.patientDiagnoses.Where(e => e.PatientNumber == patient.PatientNumber && e.DiagnosisId == diagnosis1.DiagnosisId).SingleOrDefault(); //only one
            Diagnosis diagnosis = db.diagnoses.Where(e => e.DiagnosisId == patientDiagnosis.DiagnosisId).SingleOrDefault();//only one
            Intervention intervention = db.interventions.Where(e => e.InterventionId == diagnosis.InterventionId).SingleOrDefault();
            Steps step = db.steps.Where(e => e.Date.Day == currentDate.Day && e.Date.Month == currentDate.Month && e.Date.Year == currentDate.Year && e.InterventionId == intervention.InterventionId).SingleOrDefault();
            List<Steps> steps = db.steps.Where(e => e.InterventionId == intervention.InterventionId && e.Date >= currentDate).ToList(); //all your steps
       
            Medicine med = db.medicine.Where(e => e.isCurrent).SingleOrDefault();
            List<int> ratings = new List<int>();
            for(int i = 1; i <=10; i++)
            {
                ratings.Add(i);
            }

                Alert appointmentAlert = new Alert();
                appointmentAlert.frequency = 1;
                appointmentAlert.time = DateTime.Today;
                ViewData["Appointment"] = appointmentAlert;

            Feedback feedback1 = db.feedbacks.Where(e => e.date.Day == currentDate.Day && e.date.Month == currentDate.Month && e.date.Year == currentDate.Year && e.type == "Mood").SingleOrDefault();
            Feedback feedback2 = db.feedbacks.Where(e => e.date.Day == currentDate.Day && e.date.Month == currentDate.Month && e.date.Year == currentDate.Year && e.type == "Condition").SingleOrDefault();

            if (feedback1 == null && feedback2 != null)
            {
          
                ViewBag.Name = "display: none";

            }
            else if (feedback1 != null && feedback2 == null)
            {
                ViewBag.PartialStyle = "display: none";

            }
            else if (feedback1 == null && feedback2 == null)
            {
               

            }

            else
            {
           
                ViewBag.PartialStyle = "display: none";
                ViewBag.Name = "display: none";

            }


            ViewData["Mood"] = LoadMood(step, patient);
            ViewData["Condition"] = LoadCondition(step, patient);
            ViewData["Steps"] = steps;
            ViewData["StepsMedicine"] = loadStepMedicines(steps);
            ViewData["StepsProcedure"] = loadStepProcedures(steps);
            ViewData["Procedure"] = loadProcedure(steps);
            ViewData["Medicine"] = loadMedicine(steps);
            ViewData["Diagnosis"] = diagnosis.category;
            ViewData["Glimpse"] = step.day;
            ViewData["Day"] = step.description;
            ViewData["Patient"] = patient;
            ViewData["CurrentMed"] = med.name;       
            ViewBag.DataPoints1 = JsonConvert.SerializeObject(loadDataPointCondition(intervention, diagnosis)); //mood
            ViewBag.DataPoints2 = JsonConvert.SerializeObject(loadDataPointMood(intervention, diagnosis)); //condition

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
        public ActionResult Create(string diagnosis)
        {


            string userId = User.Identity.GetUserId();
            Patient patient = db.patients.Where(e => e.ApplicationId == userId).SingleOrDefault();

            try
            {
                Intervention intervention = interventionLoad(diagnosis, patient);
                db.interventions.Add(intervention);
                db.SaveChanges();
                LoadAllSteps(intervention.duration, intervention.InterventionId, patient.PatientNumber);

                return RedirectToAction("Create", "Patient", new { InterventionId = intervention.InterventionId, PatientNumber = patient.PatientNumber, category = diagnosis });
            }
            catch
            {
                return RedirectToAction("Register", "Account", new { patient.ApplicationId });
            }

        }


        // POST: Intervention/Create
        [HttpPost]
       public ActionResult Create()   //loading functions should be asynchronous
        {

            return View();
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
