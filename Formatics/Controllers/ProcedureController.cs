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
        

        public Alert LoadDailyAlerts (int count, String category, Steps step, int dayPick)
        {
            Alert alert = new Alert();
            switch (category) 
            {
                case "Acute Pain":
                    if (dayPick == 4)
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

        
        public Steps loadDailyReminders(string diagnosis, Steps steps1)
        {
            Steps step2 = db.steps.Where(e => e.StepId == steps1.StepId).SingleOrDefault();
            
            switch(diagnosis)
            { 

             case "Acute Pain":
               step2.description = "Stretch daily so that your back pain can be minimized";
                    break;
                case "Respiration Alteration":

                    break;
                case "Sleep Pattern Disturbance":

                    break;
                    
                default:

                    break;
            }
            return step2;
            
        }
       
        public ActionResult Index()
        {

            return View();
        }
        
        // GET: Procedure/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }



        // GET: Procedure/Create
        public ActionResult Create(int patientNumber, string category, int interventionId)
        {
            int count3 = 0; ////////////////////////Fix the count so the front end will work in patient

            List<PatientStep> patientStepList = db.patientSteps.Where(e => e.PatientNumber == patientNumber).ToList(); //getting all the steps in the table that apply to patient first then manipulating them
            List<Steps> stepList = new List<Steps>();

            foreach (PatientStep patientStep in patientStepList)//finding all steps in db that match patient steps
            {
                Steps step = db.steps.Where(e => e.StepId == patientStep.StepId && e.InterventionId == interventionId).SingleOrDefault();
                stepList.Add(step);
            }


            foreach (Steps steps1 in stepList) //the list for the diagnosis will be passed so it will be nuetral
            {
                switch (steps1.day)
                {
                    case 4:

                        Procedure procedure = LoadProcedures(steps1, category);
                        db.procedures.Add(procedure);
                        db.SaveChanges();
                        StepProcedure stepProcedure = LoadStepProcedure(procedure, steps1);
                        db.stepProcedures.Add(stepProcedure);
                        db.SaveChanges();
                        Alert alert = LoadDailyAlerts(count3, category, steps1, steps1.day); //patient index
                        db.alerts.Add(alert);
                        db.SaveChanges();
                        count3++;
                        break;
                    default:
                        Steps step2 = loadDailyReminders(category, steps1);
                        count3++;
                        break;

                }
                db.SaveChanges();

            }


            return RedirectToAction("Create", "Medicine", new { PatientNumber = patientNumber, category = category, InterventionId = interventionId });


        }

        // POST: Procedure/Create
        [HttpPost]
        public ActionResult Create()
        {


            return View();
        }

        // GET: Procedure/Edit/5
        public ActionResult Edit(int id)
        {
           
            return View();
        }

        // POST: Procedure/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Diagnosis diagnosis)
        {
            try
            {
              
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
