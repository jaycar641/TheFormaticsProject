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
    public class MedicineController : Controller
    {
        // GET: Medicine
        ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index() //Standalone Medication page a list of medications
        {
            ///LOAD INFO FUNCTION
            string userId = User.Identity.GetUserId();
            Patient patient = db.patients.Where(e => e.ApplicationId == userId).SingleOrDefault();
            Diagnosis diagnosis1 = db.diagnoses.Where(e => e.isCurrent == true).SingleOrDefault();
            PatientDiagnosis patientDiagnosis = db.patientDiagnoses.Where(e => e.PatientNumber == patient.PatientNumber && e.DiagnosisId == diagnosis1.DiagnosisId).SingleOrDefault(); //only one
            Diagnosis diagnosis = db.diagnoses.Where(e => e.DiagnosisId == patientDiagnosis.DiagnosisId).SingleOrDefault();//only one
            Intervention intervention = db.interventions.Where(e => e.InterventionId == diagnosis.InterventionId).SingleOrDefault();
           
            List<Steps> steps = new List<Steps>();
            List<Medicine> medicines = db.medicine.Where(e=> e.isCurrent == true).ToList();
            Medicine medicine = db.medicine.Where(e => e.isCurrent == true).SingleOrDefault();
            ///////////////////////////////////Load SYMPTOMS
            switch (medicine.name)
            {
                case "Tylenol":
                    List<string> ingredients = new List<string>() { "Acetaminophen", "Cellulose", "Cornstarch" };
                    List<string> symptoms = new List<string>() { "Rash", "Itching", "Loss of appetite" };
                    ViewData["Ingredients"] = ingredients;
                    ViewData["Symptoms"] = symptoms;
                    break;
                case "Xopenex":
                    List<string> ingredients1 = new List<string>() { "Sodium Choloride", "Sulfuric Acid", "levalbuterol" };
                    List<string> symptoms1 = new List<string>() { "Dizziness", "Nervousness", "Tremors" };
                    ViewData["Ingredients"] = ingredients1;
                    ViewData["Symptoms"] = symptoms1;
                    break;
                case "Lunesta":
                    List<string> ingredients2 = new List<string>() { "Eszopiclone", "Calcium phosphate", "Magnesium Stearate" };
                    List<string> symptoms2 = new List<string>() { "Dizziness", "Drowsiness", "Tremors" };
                    ViewData["Ingredients"] = ingredients2;
                    ViewData["Symptoms"] = symptoms2;
                    break;
                case "Zofran":
                    List<string> ingredients3 = new List<string>() { "ondansetron hydrochloride dihydrate", "citric acid anhydrous", "sodium benzoate" };
                    List<string> symptoms3 = new List<string>() { "diarrhea", "headache", "fever" };
                    ViewData["Ingredients"] = ingredients3;
                    ViewData["Symptoms"] = symptoms3;
                    break;



            }

            /////LOAD ALERT
            Alert appointmentAlert = new Alert();
            appointmentAlert.type = "Appointment";
            appointmentAlert.frequency = 1;
            appointmentAlert.time = DateTime.Today.Date;

            ViewData["Appointment"] = appointmentAlert;
            ViewData["Medicines"] = medicines;
            ViewData["Patient"] = patient;
         

            return View();
        }

        // GET: Medicine/Details/5
        public ActionResult Details() //Standalone Medicine Details page
        {
            string userId = User.Identity.GetUserId();
            Patient patient = db.patients.Where(e => e.ApplicationId == userId).SingleOrDefault();
            ViewData["Patient"] = patient; //temporary
            return RedirectToAction("Details", "Patient", new { PatientNumber = patient.PatientNumber });
        }

        // GET: Medicine/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Medicine/Create
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

        // GET: Medicine/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Medicine/Edit/5
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

        // GET: Medicine/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Medicine/Delete/5
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

      public ActionResult MedicalDetails ()
        {
            return PartialView("_detailsMedicine");
        }
        public ActionResult OrderPerscription(int MedicineId)
        {
            Medicine medicine = db.medicine.Where(e => e.MedicineId == MedicineId).SingleOrDefault();
            
            /////////////////////////Load alert
            Alert pickup = new Alert();

            pickup.description = "Your Perscription is ready!";
            pickup.time = DateTime.Now.AddDays(7);
            pickup.type = "Perscription";
            db.alerts.Add(pickup);
            db.SaveChanges();
         
            TwilioClient.Init(twillio.accountSid, twillio.authToken);

            var message = MessageResource.Create(
                body: "Your perscription for " + medicine.name + " has been received!  We will notifiy you when it is available",
                from: new Twilio.Types.PhoneNumber("+12564877936"),
                to: new Twilio.Types.PhoneNumber("+14143887275")
            );
            return RedirectToAction("Index", "Medicine");
            //return an alert to the view from the controller you have ordered a prescription
        }


    }
}
