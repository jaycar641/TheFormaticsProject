using Formatics.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Formatics.Controllers
{
    [Authorize]
    public class StepsController : Controller
    {
        // GET: Steps

        ApplicationDbContext db = new ApplicationDbContext();
       

        public void LoadAllAlerts(int interventionId)
        {
            db.stepMedicines.ToList();
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
                    List<Steps> steps = new List<Steps>();
                    Medicine medicine = new Medicine();
                    List<string> ingredients = new List<string>() { "Acetaminophen", "Cellulose", "Cornstarch" };
                    List<string> symptoms = new List<string>() { "Rash", "Itching", "Loss of appetite" };
                    medicine.drugClass = "Pain Releif";
                    medicine.ingredients = ingredients;
                    medicine.name = "Tylenol";
                    medicine.symptoms = symptoms; //change medicine symptom property to Ilist symptoms
                    medicine.isCurrent = true;
                    db.medicine.Add(medicine);

                    foreach (PatientStep patientStep in stepList)//finding all steps in db that match patient steps
                    {
                        Steps step = db.steps.Where(e => e.StepId == patientStep.StepId && e.InterventionId == intervention.InterventionId).SingleOrDefault();
                        steps.Add(step);
                    }
                    foreach (Steps steps1 in steps)
                    {
                        if (steps1.day == 4) 
                        {
                            Steps step2 = db.steps.Where(e => e.StepId == steps1.StepId).SingleOrDefault();
                            Procedure procedure = new Procedure();
                            StepProcedure stepProcedure = new StepProcedure();
                            stepProcedure.ProcedureId = procedure.ProcedureId;
                            stepProcedure.StepId = step2.StepId;
                            procedure.category = "Surgery";
                            procedure.date = step2.Date; 
                            step2.description = "Surgery";
                            procedure.location = "Froedert Hospital";
                            Alert alert = new Alert();
                            alert.time = DateTime.Now;
                            alert.type = "Surgery";
                            alert.frequency = 1;
                            alert.description = "Surgery in 2 hours, do not eat any food!";
                            db.alerts.Add(alert);
                            db.stepProcedures.Add(stepProcedure);
                            db.procedures.Add(procedure);
                            db.SaveChanges();


                        }
                        else //Everyday
                        {
                     
                            Steps step2 = db.steps.Where(e => e.StepId == steps1.StepId).SingleOrDefault();
                            step2.description = "Stretch daily so that your back pain can be minimized";
                            StepMedicine stepMedicine = new StepMedicine();
                            stepMedicine.StepId = step2.StepId;
                            stepMedicine.MedicineId = medicine.MedicineId;
                            Alert alert = new Alert();
                            alert.time = DateTime.Now;
                            alert.type = "Medication";
                            alert.frequency = 1;
                            alert.description = "Take your medication";
                            db.alerts.Add(alert);
                            db.stepMedicines.Add(stepMedicine);
                            db.SaveChanges();


                        }
                        db.SaveChanges();

                    }
                    break;

                case "Respiration Alteration":
                    IList<PatientStep> stepList2 = db.patientSteps.Where(e => e.PatientNumber == patient1.PatientNumber).ToList(); //getting all the steps in the table that apply to patient first then manipulating them
                    IList<Steps> steps2 = new List<Steps>();
                    List<string> ingredients1 = new List<string>() { "Sodium Choloride", "Sulfuric Acid", "levalbuterol" };
                    List<string> symptoms1 = new List<string>() { "Dizziness", "Nervousness", "Tremors" };

                    Medicine medicine1 = new Medicine();
                    medicine1.drugClass = "Breathing Medication";
                    medicine1.ingredients = ingredients1;
                    medicine1.name = "Xopenex";
                    medicine1.symptoms = symptoms1; //change medicine symptom property to Ilist symptoms
                    medicine1.isCurrent = true;
                    db.medicine.Add(medicine1);

                    foreach (PatientStep patientStep in stepList2)//finding all steps in db that match patient steps
                    {
                        Steps step = db.steps.Where(e => e.StepId == patientStep.StepId && e.InterventionId == intervention.InterventionId).SingleOrDefault();
                        steps2.Add(step);
                    }
                    foreach (Steps steps1 in steps2)//edit these descriptions
                    {
                            Steps step2 = db.steps.Where(e => e.StepId == steps1.StepId).SingleOrDefault();
                            step2.description = "Breathing Excercises";
                  
                        StepMedicine stepMedicine = new StepMedicine();
                        stepMedicine.StepId = step2.StepId;
                        stepMedicine.MedicineId = medicine1.MedicineId;
                       
                        Alert alert = new Alert();
                        alert.time = DateTime.Now;
                        alert.type = "Medication";
                        alert.frequency = 1;
                        alert.description = "Take your Medication!";
                        db.alerts.Add(alert);
                        db.stepMedicines.Add(stepMedicine);
                        db.SaveChanges();

                    }
                    break;

                case "Sleep Pattern Disturbance":
                    IList<PatientStep> stepList3 = db.patientSteps.Where(e => e.PatientNumber == patient1.PatientNumber).ToList(); //getting all the steps in the table that apply to patient first then manipulating them
                    List<Steps> steps3 = new List<Steps>();
                    Medicine medicine2 = new Medicine();

                    List<string> ingredients2 = new List<string>() { "Eszopiclone", "Calcium phosphate", "Magnesium Stearate" };
                    List<string> symptoms2 = new List<string>() { "Dizziness", "Drowsiness", "Tremors" };

                    medicine2.drugClass = "Sleep Medication";
                    medicine2.ingredients = ingredients2;
                    medicine2.name = "Lunesta";
                    medicine2.symptoms = symptoms2; //change medicine symptom property to Ilist symptoms
                    medicine2.isCurrent = true;
                    db.medicine.Add(medicine2);

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

                            Steps step2 = db.steps.Where(e => e.StepId == steps1.StepId).SingleOrDefault();
                            step2.description = "Remember to go to sleep at 10pm";
                    
                            StepMedicine stepMedicine = new StepMedicine();
                            stepMedicine.StepId = step2.StepId;
                            stepMedicine.MedicineId = medicine2.MedicineId;
                          
                            Alert alert = new Alert();
                            alert.time = DateTime.Now;
                            alert.type = "Medication";
                            alert.frequency = 1;
                            alert.description = "Take your Medication!";
                            db.alerts.Add(alert);
                            db.stepMedicines.Add(stepMedicine);
                            db.SaveChanges();


                        }
                        db.SaveChanges();

                    }
                    break;
                default:
                    IList<PatientStep> stepList4 = db.patientSteps.Where(e => e.PatientNumber == patient1.PatientNumber).ToList(); //getting all the steps in the table that apply to patient first then manipulating them
                    List<Steps> steps4 = new List<Steps>();
                    List<string> ingredients3 = new List<string>() { "Eszopiclone", "Calcium phosphate", "Magnesium Stearate" };
                    List<string> symptoms3 = new List<string>() { "Dizziness", "Drowsiness", "Tremors" };
                    Medicine medicine3 = new Medicine();
                    medicine3.drugClass = "Sleep Medication";
                    medicine3.ingredients = ingredients3;
                    medicine3.name = "Lunesta";
                    medicine3.symptoms = ingredients3; //change medicine symptom property to Ilist symptoms
                    medicine3.isCurrent = true;
                    db.medicine.Add(medicine3);

                    foreach (PatientStep patientStep in stepList4)//finding all steps in db that match patient steps
                    {
                        Steps step = db.steps.Where(e => e.StepId == patientStep.StepId && e.InterventionId == intervention.InterventionId).SingleOrDefault();
                        steps4.Add(step);
                    }
                    foreach (Steps steps1 in steps4)//just saturday
                    {
                            Steps step2 = db.steps.Where(e => e.StepId == steps1.StepId).SingleOrDefault();
                            step2.description = "Remember to Drink a lot of water";

                            StepMedicine stepMedicine = new StepMedicine();
                            stepMedicine.StepId = step2.StepId;
                            stepMedicine.MedicineId = medicine3.MedicineId;
                         
                            Alert alert = new Alert();
                            alert.time = DateTime.Now;
                            alert.type = "Medication";
                            alert.frequency = 1;
                            alert.description = "Take your Medication!";
                            db.alerts.Add(alert);
                            db.stepMedicines.Add(stepMedicine);
                            db.SaveChanges();
                        
                    }

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
                patientStep.PatientNumber = patient1.PatientNumber;
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



            foreach (PatientStep patientStep in db.patientSteps.ToList())
            {
                patientStep.PatientNumber = patient1.PatientNumber;
            }
            db.SaveChanges();
        }
        public ActionResult Index()
        {
            string userId = User.Identity.GetUserId();
            Patient patient = db.patients.Where(e => e.ApplicationId == userId).SingleOrDefault();
          

            if (User.Identity.IsAuthenticated == true)
            {
                //RedirectToAction("Create", "Step", new { id = patient.PatientNumber });
                Create(patient.PatientNumber);
                return RedirectToAction("Index", "Patient");

            }
            return View();

        }

        // GET: Steps/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Steps/Create
        public void Create(int patientNumber)  //Register goes to create step, then edit step then dashboard
        {
            PatientDiagnosis patientDiagnosis = db.patientDiagnoses.Where(e => e.PatientNumber == patientNumber).SingleOrDefault();
            Diagnosis diagnosis = db.diagnoses.Where(e => e.DiagnosisId == patientDiagnosis.DiagnosisId).SingleOrDefault();
            Intervention intervention = db.interventions.Where(e => e.InterventionId == diagnosis.InterventionId).SingleOrDefault();
            Patient patient1 = db.patients.Where(e => e.PatientNumber == patientNumber).SingleOrDefault();
           
            LoadAllSteps(intervention.duration, intervention.InterventionId, patient1.PatientNumber);
            LoadAllAlerts(intervention.InterventionId);
            Edit(patientNumber);

        }

        // POST: Steps/Create
  
        // GET: Steps/Edit/5
        public void Edit(int patientNumber)
        {
            PatientDiagnosis patientDiagnosis = db.patientDiagnoses.Where(e => e.PatientNumber == patientNumber).SingleOrDefault();
            Diagnosis diagnosis = db.diagnoses.Where(e => e.DiagnosisId == patientDiagnosis.DiagnosisId).SingleOrDefault();
            Intervention intervention = db.interventions.Where(e => e.InterventionId == diagnosis.InterventionId).SingleOrDefault();
            Patient patient1 = db.patients.Where(e => e.PatientNumber == patientNumber).SingleOrDefault();

            LoadStepDetails(patient1.PatientNumber, diagnosis.category);
        }

        // POST: Steps/Edit/5
       

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
