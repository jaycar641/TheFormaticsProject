using Formatics.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Formatics.Controllers
{
    public class StepsController : Controller
    {
        // GET: Steps
        ApplicationDbContext db = new ApplicationDbContext();

       public void LoadAllAlerts()
        {

        }
       public void LoadStepDetails(int patientNumber, string category)
        {
            PatientDiagnosis patientDiagnosis = db.patientDiagnoses.Where(e => e.PatientNumber == patientNumber).SingleOrDefault();
            Diagnosis diagnosis = db.diagnoses.Where(e => e.DiagnosisId == patientDiagnosis.DiagnosisId).SingleOrDefault();
            Intervention intervention = db.interventions.Where(e => e.InterventionId == diagnosis.InterventionId).SingleOrDefault();
            Patient patient1 = db.patients.Where(e => e.PatientNumber == patientNumber).SingleOrDefault();

            switch (category) //Use database lingo 
            {
                case "Acute Pain":
                    IList<PatientStep> stepList = db.patientSteps.Where(e => e.PatientNumber == patient1.PatientNumber).ToList(); //getting all the steps in the table that apply to patient first then manipulating them
                    IList<Steps> steps = new IList<Steps>();

                    foreach (PatientStep patientStep in stepList)//finding all steps in db that match patient steps
                    {
                        Steps step = db.steps.Where(e => e.StepId == patientStep.StepId && e.InterventionId == intervention.InterventionId).SingleOrDefault();
                        steps.Add(step);
                    }
                    foreach (Steps steps1 in steps)//edit these descriptions
                    {
                        if (steps1.day == 4) //Check acute pain treatment plan samples
                        {
                            Steps step2 = db.steps.Where(e => e.StepId == steps1.StepId).SingleOrDefault();
                            Procedure procedure = new Procedure();
                            StepProcedure stepProcedure = new StepProcedure();
                            stepProcedure.ProcedureId = procedure.ProcedureId;
                            stepProcedure.StepId = step2.StepId;
                            procedure.category = "Surgery";
                            procedure.date = step2.Date; //check date logic
                            step2.description = "Surgery";
                            procedure.location = "Froedert Hospital";
                            db.stepProcedures.Add(stepProcedure);
                            db.procedures.Add(procedure);

                        }
                        else //Everyday
                        {
                            IList<string> symptoms = new IList<string>():
                            IList<string> ingredients = new IList<string>():


                            Steps step2 = db.steps.Where(e => e.StepId == steps1.StepId).SingleOrDefault();
                            step2.description = "Remember to stretch";
                            Medicine medicine = new Medicine();
                            StepMedicine stepMedicine = new StepMedicine();
                            stepMedicine.StepId = step2.StepId;
                            stepMedicine.MedicineId = medicine.MedicineId;
                            medicine.drugClass = "Pain Releif";
                            medicine.ingredients = ingredients;
                            medicine.name = "Tylenol";
                            medicine.symptoms = symptoms; //change medicine symptom property to Ilist symptoms
                            db.stepMedicines.Add(stepMedicine);
                            db.medicine.Add(medicine);

                        }
                        db.SaveChanges();

                    }
                    break;

                case "Respiration Alteration":
                    IList<PatientStep> stepList2 = db.patientSteps.Where(e => e.PatientNumber == patient1.PatientNumber).ToList(); //getting all the steps in the table that apply to patient first then manipulating them
                    IList<Steps> steps2 = new IList<Steps>();

                    foreach (PatientStep patientStep in stepList2)//finding all steps in db that match patient steps
                    {
                        Steps step = db.steps.Where(e => e.StepId == patientStep.StepId && e.InterventionId == intervention.InterventionId).SingleOrDefault();
                        steps2.Add(step);
                    }
                    foreach (Steps steps1 in steps2)//edit these descriptions
                    {
                        IList<string> symptoms = new IList<string>():
                            IList<string> ingredients = new IList<string>():

                            Steps step2 = db.steps.Where(e => e.StepId == steps1.StepId).SingleOrDefault();
                            step2.description = "Breathing Excercises";
                            Steps step2 = db.steps.Where(e => e.StepId == steps1.StepId).SingleOrDefault();
                            step2.description = "Remember to go to sleep at 10pm";

                        Medicine medicine = new Medicine();
                        StepMedicine stepMedicine = new StepMedicine();
                        stepMedicine.StepId = step2.StepId;
                        stepMedicine.MedicineId = medicine.MedicineId;
                        medicine.drugClass = "Breathing Medication";
                        medicine.ingredients = ingredients;
                        medicine.name = "Breathing Pill";
                        medicine.symptoms = symptoms; //change medicine symptom property to Ilist symptoms
                        db.stepMedicines.Add(stepMedicine);
                        db.medicine.Add(medicine);

                        db.SaveChanges();

                    }
                    break;

                case "Sleep Pattern Disturbance":
                    IList<PatientStep> stepList3 = db.patientSteps.Where(e => e.PatientNumber == patient1.PatientNumber).ToList(); //getting all the steps in the table that apply to patient first then manipulating them
                    IList<Steps> steps3 = new IList();

                    foreach (PatientStep patientStep in stepList3)//finding all steps in db that match patient steps
                    {
                        Steps step = db.steps.Where(e => e.StepId == patientStep.StepId && e.InterventionId == intervention.InterventionId).SingleOrDefault();
                        steps3.Add(step);
                    }
                    foreach (Steps steps1 in steps3)//just saturday
                    {
                        if (steps1.Date.DayOfWeek == DayOfWeek.Saturday) //Check acute pain treatment plan samples
                        {
                            Steps step2 = db.steps.Where(e => e.StepId == steps1.StepId).SingleOrDefault();
                            step2.description = "Get a lot of sleep on the weekends";
                         

                        }
                        else //Everyday
                        {
                            IList<string> symptoms = new IList<string>():
                            IList<string> ingredients = new IList<string>():

                            Steps step2 = db.steps.Where(e => e.StepId == steps1.StepId).SingleOrDefault();
                            step2.description = "Remember to go to sleep at 10pm";
                    
                            Medicine medicine = new Medicine();
                            StepMedicine stepMedicine = new StepMedicine();
                            stepMedicine.StepId = step2.StepId;
                            stepMedicine.MedicineId = medicine.MedicineId;
                            medicine.drugClass = "Sleep Medication";
                            medicine.ingredients = ingredients;
                            medicine.name = "Nyquil";
                            medicine.symptoms = symptoms; //change medicine symptom property to Ilist symptoms
                            db.stepMedicines.Add(stepMedicine);
                            db.medicine.Add(medicine);

                        }
                        db.SaveChanges();

                    }
                    break;
                default:

                    break;

            }

        }

        public void LoadAllSteps(int duration, int interventionId, int patientNumber)
        {
            PatientDiagnosis patientDiagnosis = db.patientDiagnoses.Where(e => e.PatientNumber == patientNumber).SingleOrDefault();
            Diagnosis diagnosis = db.diagnoses.Where(e => e.DiagnosisId == patientDiagnosis.DiagnosisId).SingleOrDefault();
            Intervention intervention = db.interventions.Where(e => e.InterventionId == diagnosis.InterventionId).SingleOrDefault();
            Patient patient1 = db.patients.Where(e => e.PatientNumber == patientNumber).SingleOrDefault();
           
            for (int i = 0; i <= duration - 1; i++)
            {
                Steps steps = new Steps();
                PatientStep patientStep = new PatientStep();
                steps.InterventionId = interventionId;
                steps.day = i + 1;
                steps.description = null;
                patientStep.StepId = steps.StepId;
                patientStep.PatientNumber = patient1.PatientNumber;
                patientStep.Date = intervention.startDate.AddDays(i);
                //steps.Date = ??//when load page checks date?
                db.Steps.Add(steps);
                db.patientSteps.Add(patientStep);
                //Patientsteps, steps medicine, steps procedure??
             }
            db.SaveChanges();

        }
        public ActionResult Index()
        {
            return View();
        }

        // GET: Steps/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Steps/Create
        public ActionResult Create(int patientNumber)  //Register goes to create step, then edit step then dashboard
        {
            PatientDiagnosis patientDiagnosis = db.patientDiagnoses.Where(e => e.PatientNumber == patientNumber).SingleOrDefault();
            Diagnosis diagnosis = db.diagnoses.Where(e => e.DiagnosisId == patientDiagnosis.DiagnosisId).SingleOrDefault();
            Intervention intervention = db.interventions.Where(e => e.InterventionId == diagnosis.InterventionId).SingleOrDefault();
            Patient patient1 = db.patients.Where(e => e.PatientNumber == patientNumber).SingleOrDefault();
           
            LoadAllSteps(intervention.duration, intervention.InterventionId, patient1.PatientNumber);
            LoadAllAlerts();
            return RedirectToAction("Edit", "Steps", new { id = patient1.PatientNumber });
        }

        // POST: Steps/Create
        [HttpPost]
        public ActionResult Create(int patientNumber, Steps steps )
        {
            try
            {
                // TODO: Add insert logic here
               
            }
            catch
            {
                return View();
            }
        }

        // GET: Steps/Edit/5
        public ActionResult Edit(int patientNumber)
        {
            PatientDiagnosis patientDiagnosis = db.patientDiagnoses.Where(e => e.PatientNumber == patientNumber).SingleOrDefault();
            Diagnosis diagnosis = db.diagnoses.Where(e => e.DiagnosisId == patientDiagnosis.DiagnosisId).SingleOrDefault();
            Intervention intervention = db.interventions.Where(e => e.InterventionId == diagnosis.InterventionId).SingleOrDefault();
            Patient patient1 = db.patients.Where(e => e.PatientNumber == patientNumber).SingleOrDefault();

            LoadStepDetails(patient1.PatientNumber, diagnosis.category);
            return RedirectToAction("Index", "Patient", new { id = patient1.PatientNumber });
        }

        // POST: Steps/Edit/5
        [HttpPost]
        public ActionResult Edit(int patientNumber, FormCollection collection)
        {
            Patient patient1 = db.patients.Where(e => e.PatientNumber == patientNumber).SingleOrDefault();
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index", "Patient", new { id = patient1.PatientNumber });

            }
            catch
            {
                return View();
            }

            return RedirectToAction("Index", "Patient", new { id = patient1.PatientNumber });

        }

        // GET: Steps/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Steps/Delete/5
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
