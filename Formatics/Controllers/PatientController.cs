using Formatics.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
namespace Formatics.Controllers
{
    [Authorize]

    public class PatientController : Controller
    {

        // GET: Patient
        ApplicationDbContext db = new ApplicationDbContext();
        public List<string> LoadResources()
        {
            /////////////////////////////////////////LOAD CURRENT INFO FUNCTION
            string userId = User.Identity.GetUserId();
            Patient patient = db.patients.Where(e => e.ApplicationId == userId).SingleOrDefault();
            Diagnosis diagnosis1 = db.diagnoses.Where(e => e.isCurrent == true).SingleOrDefault();

            List<string> resources = new List<string>();
            PatientDiagnosis patientDiagnosis = db.patientDiagnoses.Where(e => e.PatientNumber == patient.PatientNumber && e.DiagnosisId == diagnosis1.DiagnosisId).SingleOrDefault();
            Diagnosis diagnosis = db.diagnoses.Where(e => e.DiagnosisId == patientDiagnosis.DiagnosisId).SingleOrDefault();
            Intervention intervention = db.interventions.Where(e => e.InterventionId == diagnosis.InterventionId).SingleOrDefault();

            switch(diagnosis.category)
            {
                case "Acute Pain":
                    resources.Add("https://www.spine-health.com/glossary/acute-pain");
                    resources.Add("Acute link2");
                    resources.Add("Acute link3");
                    resources.Add("Acute link4");

                    break;
                case "Respiration Alteration":
                    resources.Add("https://www.webmd.com/lung/breathing-problems-causes-tests-treatments");
                    resources.Add("Respiration link2");
                    resources.Add("Respiration link3");
                    resources.Add("Respiration link4");

                    break;
                case "Sleep Pattern Disturbance":
                    resources.Add("https://www.webmd.com/sleep-disorders/insomnia-symptoms-and-causes");
                    resources.Add("Sleep link2");
                    resources.Add("Sleep link3");
                    resources.Add("Sleep link4");

                    break;
                default:
                    resources.Add("https://www.webmd.com/digestive-disorders/digestive-diseases-nausea-vomiting");
                    resources.Add("Nausea link2");
                    resources.Add("Nausea link3");
                    resources.Add("Nausea link4");

                    break;

            }
            return resources;

        }

        public string LoadPersonalization()
        {

            string personalization = "";
            Random random = new Random();
          int test =  random.Next(0, 5);
            switch(test)
            {
                case 0:
                    personalization = "Be Encouraged!";
                    break;

                case 1:
                    personalization = "When life gives you lemons, make lemonade!";
                    break;
                case 2:
                    personalization = "Stay strong and take it one day at a time!";
                    break;
                case 3:
                    personalization = "Have a positive attitude.  It will help get you through the day!";
                    break;
                case 4:
                    personalization = "Focus on your goal and eventually you will accomplish it!";
                    break;
                case 5:
                    personalization = "Beleive in yourself!";
                    break;
                default:
                    break;

            }
            return personalization;


        }
        //public IList<Alert> Alerts(int amount)
        //{
        //    return 
        //}
        public ActionResult Index()//Patient Dashboard
        {


            DateTime currentDate = DateTime.Today;
            Diagnosis diagnosis1 = db.diagnoses.Where(e => e.isCurrent == true).SingleOrDefault();
            string userId = User.Identity.GetUserId();
            Patient patient = db.patients.Where(e => e.ApplicationId == userId).SingleOrDefault();
            PatientDiagnosis patientDiagnosis = db.patientDiagnoses.Where(e => e.PatientNumber == patient.PatientNumber && e.DiagnosisId == diagnosis1.DiagnosisId).SingleOrDefault();
            Diagnosis diagnosis = db.diagnoses.Where(e => e.DiagnosisId == patientDiagnosis.DiagnosisId).SingleOrDefault();
            Intervention intervention = db.interventions.Where(e => e.InterventionId == diagnosis.InterventionId).SingleOrDefault();



            List<Alert> alerts = db.alerts.Where(e=> e.type == "Appointment" || e.type == "Surgery" || e.type == "Perscription" && e.time >= currentDate).ToList();
            List<Alert> shortList = new List<Alert>();
            try
            {
                for (int i = 0; i <= 5; i++)
                {
                    shortList.Add(alerts[i]);
                }
            }
            catch
            {
                shortList = shortList;
            }
            Steps steps = db.steps.Where(e => e.Date.Day == currentDate.Day && e.Date.Month == currentDate.Month && e.Date.Year == currentDate.Year && e.InterventionId == intervention.InterventionId).SingleOrDefault();
            int number = patient.PatientNumber;
            string date = "Today is " + currentDate.ToLongDateString();
            ViewData["AlertData"] = shortList;
            ViewData["Glimpse"] = steps.day;
            ViewData["Day"] = steps.description;
            ViewData["Resources"] = LoadResources();
            ViewData["Personalization"] = LoadPersonalization();
            ViewData["Date"] = date;
            ViewData["Patient"] = patient; //temporary
            ViewData["PatientNumber"] = number;

            return View();
        }

        // GET: Patient/Details/5
        public ActionResult Details(int patientNumber)  //View Profile
        {
            Patient patient = db.patients.Where(e => e.PatientNumber == patientNumber).SingleOrDefault();



            ViewData["Patient"] = patient; //temporary

            return View(patient);
        }

        // GET: Patient/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Patient/Create
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

        // GET: Patient/Edit/5
        public ActionResult Edit(int patientNumber) //Edit patient address and contact info
        {
            Patient patient = db.patients.Where(e => e.PatientNumber == patientNumber).SingleOrDefault();
            ViewData["Patient"] = patient; //temporary

            return View(patient);
        }

        // POST: Patient/Edit/5
        [HttpPost]
        public ActionResult Edit(int patientNumber, Patient patient)
        {
            try
            {
             Patient patient1 = db.patients.Where(e => e.PatientNumber == patientNumber).SingleOrDefault();

                //patient1.age = patient.age;
                //patient1.ApplicationId = patient.ApplicationId;
                //patient1.enrollDate = patient.enrollDate;
                patient1.firstName = patient.firstName;
                patient1.middleName = patient.middleName;
                patient1.lastName = patient.lastName;
                patient1.phoneNumber = patient.phoneNumber;
                //patient1.sex = patient.sex;
                patient1.city = patient.city;
                patient1.country = patient.country;
                patient1.state = patient.state;
                patient1.zipcode = patient.zipcode;
                patient1.streetAddress = patient.streetAddress;
                //patient1.PatientNumber = patient.PatientNumber;
                db.SaveChanges();
                // TODO: Add update logic here

                return RedirectToAction("Index", "Patient");
            }
            catch
            {
                return View();
            }
        }

        // GET: Patient/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Patient/Delete/5
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
        //Treatment Plan
        public ActionResult ViewAll()
        {
            DateTime currentDate = DateTime.Today;

            List<Alert> allAlerts = db.alerts.Where(e => e.type == "Appointment" || e.type == "Surgery" || e.type == "Perscription" && e.time >= currentDate).ToList();
            return PartialView("~/Views/FrontEnd/_Alerts.cshtml", allAlerts);
        }

        public ActionResult Intervention (int patientNumber)
        {
            string userId = User.Identity.GetUserId();
            Patient patient = db.patients.Where(e => e.ApplicationId == userId).SingleOrDefault();
            ViewData["Patient"] = patient; //temporary
            return RedirectToAction("Index", "Intervention");

        }

        public ActionResult Medication(int patientNumber)
        {
            string userId = User.Identity.GetUserId();
            Patient patient = db.patients.Where(e => e.ApplicationId == userId).SingleOrDefault();
            ViewData["Patient"] = patient; //temporary
            return RedirectToAction("Index", "Medication");

        }


        public ActionResult MedicalHistory(int patientNumber)
        {

            string userId = User.Identity.GetUserId();
            Patient patient = db.patients.Where(e => e.ApplicationId == userId).SingleOrDefault();
            Diagnosis diagnosis1 = db.diagnoses.Where(e => e.isCurrent == true).SingleOrDefault();

            PatientDiagnosis patientDiagnosis = db.patientDiagnoses.Where(e => e.PatientNumber == patient.PatientNumber && e.DiagnosisId == diagnosis1.DiagnosisId).SingleOrDefault();
            List<Diagnosis> diagnosis = db.diagnoses.ToList();

            
            DateTime currentDate = DateTime.Today;


            ViewData["Patient"] = patient; //temporary

            List<Medicine> medicine = db.medicine.ToList();
            ViewData["Medicine"] = medicine;
            return View(diagnosis);  // pass diagnosis that is only connected to patient, use patient diagnosis list
        }
    }
}
