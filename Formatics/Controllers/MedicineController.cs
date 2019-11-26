﻿using Formatics.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Formatics.Controllers
{
    public class MedicineController : Controller
    {
        // GET: Medicine
        ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index(int patientNumber) //Standalone Medication page a list of medications
        {

            PatientDiagnosis patientDiagnosis = db.patientDiagnoses.Where(e => e.PatientNumber == patientNumber).SingleOrDefault(); //only one
            Diagnosis diagnosis = db.diagnoses.Where(e => e.DiagnosisId == patientDiagnosis.DiagnosisId).SingleOrDefault();//only one
            Intervention intervention = db.interventions.Where(e => e.InterventionId == diagnosis.InterventionId).SingleOrDefault();
            Patient patient1 = db.patients.Where(e => e.PatientNumber == patientNumber).SingleOrDefault();
            List<Steps> steps = new List<Steps>();
            List<Medicine> medicines = new List<Medicine>();
            List<StepMedicine> stepMedicines = new List<StepMedicine>();

            foreach (StepMedicine stepMedicine in db.stepMedicines.ToList())//finding all steps in db that match patient steps
              {
                Steps step = db.steps.Where(e => e.StepId == stepMedicine.StepId && e.InterventionId == intervention.InterventionId).SingleOrDefault(); //UNIQUE INTERVENTION ID BASED ON UNIQUE DIAGNOSIS
                steps.Add(step);
            
            }
            foreach (Steps steps1 in db.steps.Where(e=>e.InterventionId == intervention.InterventionId))//looping through a list of steps tht only match patients intervention id
            {
                StepMedicine stepMedicine = db.stepMedicines.Where(e => e.StepId == steps1.StepId).SingleOrDefault();
                Medicine medicine = db.medicine.Where(e => e.MedicineId == stepMedicine.MedicineId).SingleOrDefault();
                medicines.Add(medicine);
                stepMedicines.Add(stepMedicine);
             
            }

            
              return View(medicines);
        }

        // GET: Medicine/Details/5
        public ActionResult Details(int id) //Standalone Medicine Details page
        {
            return View();
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
    }
}