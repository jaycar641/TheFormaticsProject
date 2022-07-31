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
