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
        
          public Procedure LoadProcedures(Steps step2, String category) //Loaded for each procedure
        {
            Procedure procedure = new Procedure();
            switch (category)
            {
                case "Acute Pain":
                    if (step2.day == 4)
                    {
                        procedure.category = "Surgery";
                        procedure.date = step2.Date;
                        procedure.location = "Froedert Hospital";
                    }
                    else
                    {
                        procedure = null;
                        return procedure;

                    }
                    break;
                case "Respiration Alteration":
                    procedure = null;
                    return procedure;
                    break;
                case "Sleep Pattern Disturbance":
                    procedure = null;
                    return procedure;
                    break;
                    
                default:
                    procedure = null;
                    return procedure;
                    break;

            }
            return procedure;

        }
        
        public StepProcedure LoadStepProcedure(Procedure procedure, Steps step2)
        {
            if (procedure == null)
            {
                return null;
            }
            else
            {
                StepProcedure stepProcedure = new StepProcedure();
                stepProcedure.ProcedureId = procedure.ProcedureId;
                stepProcedure.StepId = step2.StepId;
                return stepProcedure;
            }
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
            int count3 = 0;

            List<PatientStep> patientStepList = db.patientSteps.Where(e => e.PatientNumber == patientNumber).ToList(); 
            List<Steps> stepList = new List<Steps>();

            foreach (PatientStep patientStep in patientStepList)
            {
                Steps step = db.steps.Where(e => e.StepId == patientStep.StepId && e.InterventionId == interventionId).SingleOrDefault();
                stepList.Add(step);
            }


            foreach (Steps steps1 in stepList) //the list for the diagnosis will be passed so it will be nuetral
            {
                
                    Procedure procedure2 = LoadProcedures(steps1, category);
                StepProcedure stepProcedure2 = LoadStepProcedure(procedure2, steps1);
                count3++;


                if (procedure2 == null)
                {
                    continue;

                }
                else
                {
                    db.procedures.Add(procedure2);
                    db.stepProcedures.Add(stepProcedure2);
                    db.SaveChanges();
                }                    
                

            }


            return RedirectToAction("Create", "Medicine", new { PatientNumber = patientNumber, category = category, InterventionId = interventionId });


        }

        // POST: Procedure/Create
        [HttpPost]
        public ActionResult Create()
        {


            return View();
        }

      
        




    }
}
