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
            string userId = User.Identity.GetUserId();
            Patient patient1 = db.patients.Where(e => e.ApplicationId == userId ).SingleOrDefault();
            DateTime start1 = new DateTime(2015, 05, 12);
            DateTime start2 = new DateTime(2010, 01, 12);

            Intervention intervention = new Intervention();
            Diagnosis diagnosis1 = new Diagnosis();
            diagnosis1.category = "Acute Pain";
            diagnosis1.dateDiagnosed = start1; ;
            diagnosis1.InterventionId = intervention.InterventionId;
            intervention.category = "Acute Pain Control";
            intervention.startDate = start1; //date time .now format
            intervention.duration = 180;
            intervention.endDate = diagnosis1.dateDiagnosed.AddDays(intervention.duration);
            intervention.expectedOutcome = "Improve";
            PatientDiagnosis patientDiagnosis = new PatientDiagnosis();
            patientDiagnosis.DiagnosisId = diagnosis1.DiagnosisId;
            patientDiagnosis.PatientNumber = patient1.PatientNumber;
            db.patientDiagnoses.Add(patientDiagnosis);


            Medicine medicine = new Medicine();
            List<string> ingredients = new List<string>() { "Acetaminophen", "Cellulose", "Cornstarch" };
            List<string> symptoms = new List<string>() { "Rash", "Itching", "Loss of appetite" };
            medicine.drugClass = "Pain Releif";
            medicine.ingredients = ingredients;
            medicine.name = "Tylenol";
            medicine.symptoms = symptoms; //change medicine symptom property to Ilist symptoms
            medicine.isCurrent = false;
            medicine.startDate = start1;
            medicine.endDate = diagnosis1.dateDiagnosed.AddDays(intervention.duration);
            db.interventions.Add(intervention);
            db.diagnoses.Add(diagnosis1);
            db.medicine.Add(medicine);
            db.SaveChanges();


            Intervention intervention1 = new Intervention();
            Diagnosis diagnosis2 = new Diagnosis();
            diagnosis2.category = "Respiration Alteration";
            diagnosis2.dateDiagnosed = start2;
            diagnosis2.InterventionId = intervention1.InterventionId;
            intervention1.category = "Breathing Excercises";
            intervention1.startDate = start2; //date time .now format
            intervention1.duration = 90;
            intervention1.endDate = diagnosis2.dateDiagnosed.AddDays(intervention1.duration);
            intervention1.expectedOutcome = "Improve";
            PatientDiagnosis patientDiagnosis1 = new PatientDiagnosis();
            patientDiagnosis1.DiagnosisId = diagnosis2.DiagnosisId;
            patientDiagnosis1.PatientNumber = patient1.PatientNumber;
            db.patientDiagnoses.Add(patientDiagnosis1);


            Medicine medicine1 = new Medicine();
            List<string> ingredients1 = new List<string>() { "Sodium Choloride", "Sulfuric Acid", "levalbuterol" };
            List<string> symptoms1 = new List<string>() { "Dizziness", "Nervousness", "Tremors" };
            medicine1.drugClass = "Breathing Medication";
            medicine1.ingredients = ingredients1;
            medicine1.name = "Xopenex";
            medicine1.symptoms = symptoms1; //change medicine symptom property to Ilist symptoms
            medicine1.isCurrent = false;
            medicine1.startDate = start2;
            medicine1.endDate = diagnosis2.dateDiagnosed.AddDays(intervention1.duration);
            db.interventions.Add(intervention1);
            db.diagnoses.Add(diagnosis2);
            db.medicine.Add(medicine1);
            db.SaveChanges();
        

            return View(diagnosis1);
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
