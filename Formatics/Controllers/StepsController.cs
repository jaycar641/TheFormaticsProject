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




        /// <summary>
        /// //////////3 LoadAllSteps////////////////////////////
        /// </summary>
        public void LoadAllSteps(int duration, int interventionId, int patientNumber)
        {
            Diagnosis diagnosis1 = db.diagnoses.Where(e => e.isCurrent == true).SingleOrDefault();
            PatientDiagnosis patientDiagnosis = db.patientDiagnoses.Where(e => e.PatientNumber == patientNumber && e.DiagnosisId == diagnosis1.DiagnosisId).SingleOrDefault();
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



        /// <summary>
        /// //////////6 LoadStepDetails Midpoint can use Procedure and Medicine Controller///////////////////
        /// </summary>
        public void LoadStepDetails(int patientNumber, string category)
        {
            Diagnosis diagnosis1 = db.diagnoses.Where(e => e.isCurrent == true).SingleOrDefault();

            PatientDiagnosis patientDiagnosis = db.patientDiagnoses.Where(e => e.PatientNumber == patientNumber && e.DiagnosisId == diagnosis1.DiagnosisId).SingleOrDefault();
            Diagnosis diagnosis = db.diagnoses.Where(e => e.DiagnosisId == patientDiagnosis.DiagnosisId).SingleOrDefault();
            Intervention intervention = db.interventions.Where(e => e.InterventionId == diagnosis.InterventionId).SingleOrDefault();
            Patient patient1 = db.patients.Where(e => e.PatientNumber == patientNumber).SingleOrDefault();

            switch (category) //Use database lingo 
            {
                case "Acute Pain":
                    int count3 = 0;
                    List<PatientStep> stepList = db.patientSteps.Where(e => e.PatientNumber == patient1.PatientNumber).ToList(); //getting all the steps in the table that apply to patient first then manipulating them
                    List<Steps> steps = new List<Steps>();

                    Medicine medicine = LoadMedicineDetails(intervention, diagnosis, category);
                    db.medicine.Add(medicine);
                    db.SaveChanges();

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
                            Procedure procedure = LoadProcedures(step2, category);
                            db.procedures.Add(procedure);
                            db.SaveChanges();
                            StepProcedure stepProcedure = LoadStepProcedure(procedure, step2);
                            step2.description = "Surgery";
                            db.stepProcedures.Add(stepProcedure);
                            db.SaveChanges();
                            Alert alert = LoadDailyAlerts(count3, category, step2, steps1);
                            /////////////////////Check github for existing code
                            db.alerts.Add(alert);
                            db.SaveChanges();
                            count3++;

                        }
                        else //Everyday
                        {

                            Steps step2 = db.steps.Where(e => e.StepId == steps1.StepId).SingleOrDefault();
                            step2.description = "Stretch daily so that your back pain can be minimized";

                            StepMedicine stepMedicine = LoadStepMedicineDetails(medicine, step2, category, steps1);

                            Alert alert = LoadDailyAlerts(count3, category, step2, steps1);
                            db.alerts.Add(alert);
                            db.stepMedicines.Add(stepMedicine);
                            db.SaveChanges();
                            count3++;

                        }
                        db.SaveChanges();

                    }
                    break;

                case "Respiration Alteration":
                    int count2 = 0;
                    List<PatientStep> stepList2 = db.patientSteps.Where(e => e.PatientNumber == patient1.PatientNumber).ToList(); //getting all the steps in the table that apply to patient first then manipulating them
                    List<Steps> steps2 = new List<Steps>();

                    Medicine medicine1 = LoadMedicineDetails(intervention, diagnosis, category);
                    db.medicine.Add(medicine1);
                    db.SaveChanges();
                    foreach (PatientStep patientStep in stepList2)//finding all steps in db that match patient steps
                    {
                        Steps step = db.steps.Where(e => e.StepId == patientStep.StepId && e.InterventionId == intervention.InterventionId).SingleOrDefault();
                        steps2.Add(step);
                    }
                    foreach (Steps steps1 in steps2)//edit these descriptions
                    {
                        Steps step2 = db.steps.Where(e => e.StepId == steps1.StepId).SingleOrDefault();
                        step2.description = "Breathing Excercises";

                        StepMedicine stepMedicine = LoadStepMedicineDetails(medicine1, step2, category, steps1);
                        Alert alert = LoadDailyAlerts(count2, category, step2, steps1);
                        db.alerts.Add(alert);
                        db.stepMedicines.Add(stepMedicine);
                        db.SaveChanges();
                        count2++;
                    }
                    break;

                case "Sleep Pattern Disturbance":
                    int count1 = 0;
                    List<PatientStep> stepList3 = db.patientSteps.Where(e => e.PatientNumber == patient1.PatientNumber).ToList(); //getting all the steps in the table that apply to patient first then manipulating them
                    List<Steps> steps3 = new List<Steps>();
                    Medicine medicine2 = LoadMedicineDetails(intervention, diagnosis, category);
                    db.medicine.Add(medicine2);
                    db.SaveChanges();

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
                            StepMedicine stepMedicine = LoadStepMedicineDetails(medicine2, step2, category, steps1);
                            Alert alert = LoadDailyAlerts(count1, category, step2, steps1);

                            db.alerts.Add(alert);
                            db.stepMedicines.Add(stepMedicine);
                            db.SaveChanges();
                            count1++;

                        }
                        db.SaveChanges();

                    }
                    break;
                default:
                    int count = 0;
                    List<PatientStep> stepList4 = db.patientSteps.Where(e => e.PatientNumber == patient1.PatientNumber).ToList(); //getting all the steps in the table that apply to patient first then manipulating them
                    List<Steps> steps4 = new List<Steps>();
                    Medicine medicine3 = LoadMedicineDetails(intervention, diagnosis, category);

                    db.medicine.Add(medicine3);
                    db.SaveChanges();
                    foreach (PatientStep patientStep in stepList4)//finding all steps in db that match patient steps
                    {
                        Steps step = db.steps.Where(e => e.StepId == patientStep.StepId && e.InterventionId == intervention.InterventionId).SingleOrDefault();
                        steps4.Add(step);
                    }
                    foreach (Steps steps1 in steps4)//just saturday
                    {
                        Steps step2 = db.steps.Where(e => e.StepId == steps1.StepId).SingleOrDefault();
                        step2.description = "Remember to Drink a lot of water";
                        StepMedicine stepMedicine = LoadStepMedicineDetails(medicine3, step2, category, steps1);
                        Alert alert = LoadDailyAlerts(count, category, step2, steps1);

                        db.alerts.Add(alert);
                        db.stepMedicines.Add(stepMedicine);
                        db.SaveChanges();
                        count++;
                    }

                    break;

            }

        }

       



        // Patient Controller

        /// <summary>
        /// //////////4 LoadAllAlerts////////////////////////////
        /// </summary>
        public void LoadAllAlerts(int interventionId)
        {
            db.stepMedicines.ToList();
        }


        public Alert LoadDailyAlerts (int count, String category, Steps step, Steps dayPick)
        {
            Alert alert = new Alert();
            switch (category) 
            {
                case "Acute Pain":
                    if (dayPick.day == 4)
                    {
                        alert.time = DateTime.Now.AddDays(count);
                        alert.type = "Surgery";
                        alert.frequency = 1;
                        alert.description = "Surgery in 2 hours, do not eat any food!";

                    }
                    else
                    {
                        alert.time = DateTime.Now.AddDays(count);
                        alert.type = "Medication";
                        alert.frequency = 1;
                        alert.description = "Take your medication";
                     
                    }
                    break;
                case "Respiration Alteration":
                    alert.time = DateTime.Now.AddDays(count);
                    alert.type = "Medication";
                    alert.frequency = 1;
                    alert.description = "Take your Medication!";
                    break;
                case "Sleep Pattern Disturbance":
                    alert.time = DateTime.Now.AddDays(count);
                    alert.type = "Medication";
                    alert.frequency = 1;
                    alert.description = "Take your Medication!";
                    break;

                default:
                    alert.time = DateTime.Now.AddDays(count);
                    alert.type = "Medication";
                    alert.frequency = 1;
                    alert.description = "Take your Medication!";
                    break;


            }

            return alert;

        }







        //Medicine Controller
        /// <summary>
        /// //////////7 LoadMedicineDetails//////////////
        /// </summary>
        public Medicine LoadMedicineDetails(Intervention intervention, Diagnosis diagnosis, String category)
        {
            Medicine medicine = new Medicine();
            switch (category)
            {
                case "Acute Pain":
                    List<string> ingredients = new List<string>() { "Acetaminophen", "Cellulose", "Cornstarch" };
                    List<string> symptoms = new List<string>() { "Rash", "Itching", "Loss of appetite" };
                    medicine.drugClass = "Pain Releif";
                    medicine.ingredients = ingredients;
                    medicine.name = "Tylenol";
                    medicine.symptoms = symptoms; //change medicine symptom property to Ilist symptoms
                    medicine.isCurrent = true;
                    medicine.startDate = diagnosis.dateDiagnosed;
                    medicine.endDate = diagnosis.dateDiagnosed.AddDays(intervention.duration);
                    break;
                case "Respiration Alteration":
                    List<string> ingredients1 = new List<string>() { "Sodium Choloride", "Sulfuric Acid", "levalbuterol" };
                    List<string> symptoms1 = new List<string>() { "Dizziness", "Nervousness", "Tremors" };
                    medicine.drugClass = "Breathing Medication";
                    medicine.ingredients = ingredients1;
                    medicine.name = "Xopenex";
                    medicine.symptoms = symptoms1; //change medicine symptom property to Ilist symptoms
                    medicine.isCurrent = true;
                    medicine.startDate = diagnosis.dateDiagnosed;
                    medicine.endDate = diagnosis.dateDiagnosed.AddDays(intervention.duration);
                    break;
                case "Sleep Pattern Disturbance":
                    List<string> ingredients2 = new List<string>() { "Eszopiclone", "Calcium phosphate", "Magnesium Stearate" };
                    List<string> symptoms2 = new List<string>() { "Dizziness", "Drowsiness", "Tremors" };
                    medicine.drugClass = "Sleep Medication";
                    medicine.ingredients = ingredients2;
                    medicine.name = "Lunesta";
                    medicine.symptoms = symptoms2; //change medicine symptom property to Ilist symptoms
                    medicine.isCurrent = true;
                    medicine.startDate = diagnosis.dateDiagnosed;
                    medicine.endDate = diagnosis.dateDiagnosed.AddDays(intervention.duration);
                    break;
                   
                    default:
                    List<string> ingredients3 = new List<string>() { "ondansetron hydrochloride dihydrate", "citric acid anhydrous", "sodium benzoate" };
                    List<string> symptoms3 = new List<string>() { "diarrhea", "headache", "fever" };
                    medicine.drugClass = "Nausea Medication";
                    medicine.ingredients = ingredients3;
                    medicine.name = "Zofran";
                    medicine.symptoms = symptoms3; //change medicine symptom property to Ilist symptoms
                    medicine.isCurrent = true;
                    medicine.startDate = diagnosis.dateDiagnosed;
                    medicine.endDate = diagnosis.dateDiagnosed.AddDays(intervention.duration);
                    break;
            }
            return medicine;
        }
        

        public StepMedicine LoadStepMedicineDetails(Medicine medicine, Steps step, String category, Steps dayPick)
        {
            StepMedicine stepMedicine = new StepMedicine();
            switch (category)
            {
                case "Acute Pain":
                    stepMedicine.StepId = step.StepId;
                    stepMedicine.MedicineId = medicine.MedicineId;
                    break;
                case "Respiration Alteration":
                    stepMedicine.StepId = step.StepId;
                    stepMedicine.MedicineId = medicine.MedicineId;
                    break;
                case "Sleep Pattern Disturbance":
                    stepMedicine.StepId = step.StepId;
                    stepMedicine.MedicineId = medicine.MedicineId;
                    break;

                default:
                    stepMedicine.StepId = step.StepId;
                    stepMedicine.MedicineId = medicine.MedicineId;
                    break;
            }
            return stepMedicine;
        }

        ///Procedure Controller

        public Procedure LoadProcedures(Steps step2, String category) //Loaded for each procedure
        {
            Procedure procedure = new Procedure();
            switch (category)
            {
                case "Acute Pain":
                    procedure.category = "Surgery";
                    procedure.date = step2.Date;
                    procedure.location = "Froedert Hospital";
                    break;
                case "Respiration Alteration":

                    break;
                case "Sleep Pattern Disturbance":

                    break;
                    
                default:

                    break;

            }
            return procedure;

        }
        
        public StepProcedure LoadStepProcedure(Procedure procedure, Steps step2)
        {
            StepProcedure stepProcedure = new StepProcedure();
            stepProcedure.ProcedureId = procedure.ProcedureId;
            stepProcedure.StepId = step2.StepId;
            return stepProcedure;

        }
       



        /// <summary>
        /// //////////1 Entry Point////////////////////////////
        /// </summary>
        /// <returns></returns>
        /// Use Authorize instead
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


        /// <summary>
       /// //////////2 Create ////////////////////////////
        /// </summary>
        public void Create(int patientNumber)  //Register goes to create step, then edit step then dashboard
        {
            Diagnosis diagnosis1 = db.diagnoses.Where(e => e.isCurrent == true).SingleOrDefault();
            PatientDiagnosis patientDiagnosis = db.patientDiagnoses.Where(e => e.PatientNumber == patientNumber && e.DiagnosisId == diagnosis1.DiagnosisId).SingleOrDefault();
            Diagnosis diagnosis = db.diagnoses.Where(e => e.DiagnosisId == patientDiagnosis.DiagnosisId).SingleOrDefault();
            Intervention intervention = db.interventions.Where(e => e.InterventionId == diagnosis.InterventionId).SingleOrDefault();
            Patient patient1 = db.patients.Where(e => e.PatientNumber == patientNumber).SingleOrDefault();
           

            ////Updated: You only need the intervention classs when calling the steps
            LoadAllSteps(intervention.duration, intervention.InterventionId, patient1.PatientNumber);
            LoadAllAlerts(intervention.InterventionId);
            Edit(patientNumber);

        }

        // POST: Steps/Create

        // GET: Steps/Edit/5
        /// <summary>
        /// //////////5 Edit ////////////////////////////
        /// </summary>
        public void Edit(int patientNumber)
        {
            Diagnosis diagnosis1 = db.diagnoses.Where(e => e.isCurrent == true).SingleOrDefault();

            PatientDiagnosis patientDiagnosis = db.patientDiagnoses.Where(e => e.PatientNumber == patientNumber && e.DiagnosisId == diagnosis1.DiagnosisId).SingleOrDefault();
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
