using Formatics.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Formatics.Controllers
{
    [Authorize]

    public class PatientController : Controller
    {
        // GET: Patient
        ApplicationDbContext db = new ApplicationDbContext();
        public List<string> LoadResources()
        {
            List<string> resources = new List<string>();
            PatientDiagnosis patientDiagnosis = db.patientDiagnoses.Where(e => e.PatientNumber == patientNumber).SingleOrDefault();
            Diagnosis diagnosis = db.diagnoses.Where(e => e.DiagnosisId == patientDiagnosis.DiagnosisId).SingleOrDefault();
            Intervention intervention = db.interventions.Where(e => e.InterventionId == diagnosis.InterventionId).SingleOrDefault();
            string userId = User.Identity.GetUserId();
            Patient patient = db.patients.Where(e => e.ApplicationId == userId).SingleOrDefault();

            switch(diagnosis.category)
            {
                case "Acute Pain":
                    resources.Add("Acute link1", "Acute link2", "Acute link3", "Acute link4");
                    break;
                case "Respiration Alteration":
                    resources.Add("Respiration link1", "Respiration link2", "Respiration link3", "Respiration link4");
                    break;
                case "Sleep Pattern Disturbance":
                    resources.Add("Sleep link1", "Sleep link2", "Sleep link3", "Sleep link4");
                    break;
                defaul:
                    resources.Add("Nausea link1", "Nausea link2", "Nausea link3", "Nausea link4");
                    break;

            }
            return resources;

        }

        public string LoadPersonalization()
        {
            string personalization = new string();
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
                case 5;
                    personalization = "Beleive in yourself!";
                    break;

                    return personalization;
            }


        }
        //public IList<Alert> Alerts(int amount)
        //{
        //    return 
        //}
        public ActionResult Index()//Patient Dashboard
        {
            PatientDiagnosis patientDiagnosis = db.patientDiagnoses.Where(e => e.PatientNumber == patientNumber).SingleOrDefault();
            Diagnosis diagnosis = db.diagnoses.Where(e => e.DiagnosisId == patientDiagnosis.DiagnosisId).SingleOrDefault();
            Intervention intervention = db.interventions.Where(e => e.InterventionId == diagnosis.InterventionId).SingleOrDefault();

            string userId = User.Identity.GetUserId();
            Patient patient = db.patients.Where(e => e.ApplicationId == userId).SingleOrDefault();
            DateTime currentDate = DateTime.Today;

            List<Alert> alerts = db.alerts.ToList();
            IList<Alert> shortList = new List<Alert>();
                for (int i = 0; i <= 4; i++)
                {
                    shortList.Add(alerts[i]);
                }
            Steps steps = db.steps.Where(e => e.Date == DateTime.Today && e.InterventionId == intervention.InterventionId).SingleOrDefault();

            ViewData["Patient"] = patient;
            ViewData["Glimpse"] = steps;
            ViewData["Resources"] = LoadResources();
            ViewData["Personalization"] = LoadPersonalization();


            return View();
        }

        // GET: Patient/Details/5
        public ActionResult Details(int patientNumber)  //View Profile
        {
            Patient patient = db.patients.Where(e => e.PatientNumber == patientNumber).SingleOrDefault();
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
            return View(patient);
        }

        // POST: Patient/Edit/5
        [HttpPost]
        public ActionResult Edit(int patientNumber, Patient patient)
        {
            try
            {
             Patient patient1 = db.patients.Where(e => e.PatientNumber == patientNumber).SingleOrDefault();

                patient1.age = patient.age;
                patient1.ApplicationId = patient.ApplicationId;
                patient1.enrollDate = patient.enrollDate;
                patient1.firstName = patient.firstName;
                patient1.middleName = patient.middleName;
                patient1.lastName = patient.lastName;
                patient1.phoneNumber = patient.phoneNumber;
                patient1.sex = patient.sex;
                patient1.city = patient.city;
                patient1.country = patient.country;
                patient1.state = patient.state;
                patient1.zipcode = patient.zipcode;
                patient1.streetAddress = patient.streetAddress;
                patient1.PatientNumber = patient.PatientNumber;
                db.SaveChanges();
                // TODO: Add update logic here

                return RedirectToAction("Index");
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
    }
}
