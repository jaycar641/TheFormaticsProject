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
        /// //////////6 LoadStepDetails Midpoint can use Procedure and Medicine Controller///////////////////
        /// </summary>
        public void LoadStepDetails(int patientNumber, string category)
        {
   

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

       

        /// Use Authorize instead
        public ActionResult Index()
        {

            string userId = User.Identity.GetUserId();
            Patient patient = db.patients.Where(e => e.ApplicationId == userId).SingleOrDefault();
           Diagnosis diagnosis1 = db.diagnoses.Where(e => e.isCurrent == true).SingleOrDefault();
            PatientDiagnosis patientDiagnosis = db.patientDiagnoses.Where(e => e.PatientNumber == patientNumber && e.DiagnosisId == diagnosis1.DiagnosisId).SingleOrDefault();
            Diagnosis diagnosis = db.diagnoses.Where(e => e.DiagnosisId == patientDiagnosis.DiagnosisId).SingleOrDefault();
            Intervention intervention = db.interventions.Where(e => e.InterventionId == diagnosis.InterventionId).SingleOrDefault();

            if (User.Identity.IsAuthenticated == true)
            {
                   LoadStepDetails(patient.PatientNumber, diagnosis.category);
                return RedirectToAction("Index", "Patient");

            }
            return View();

        }



        // GET: Steps/Details/5
        public ActionResult Details(int id)
        {
            return View();
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
