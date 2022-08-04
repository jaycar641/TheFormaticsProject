using Formatics.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace Formatics.Controllers
{
    public class HomeController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            string userId = User.Identity.GetUserId();
            Patient patient = db.patients.Where(e => e.ApplicationId == userId).SingleOrDefault();
            ViewData["Patient"] = patient; //temporary

            return View();
        }

        public ActionResult About()
        {
            DeleteCurrentUser();
           // ViewBag.Message = "Your application description page.";
            //string userId = User.Identity.GetUserId();
            //Patient patient = db.patients.Where(e => e.ApplicationId == userId).SingleOrDefault();
           // ViewData["Patient"] = patient; //temporary
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            string userId = User.Identity.GetUserId();
            Patient patient = db.patients.Where(e => e.ApplicationId == userId).SingleOrDefault();
            ViewData["Patient"] = patient; //temporary
            return View();
        }

        public ActionResult Details()
        {
            string userId = User.Identity.GetUserId();
            Patient patient = db.patients.Where(e => e.ApplicationId == userId).SingleOrDefault();
            ViewData["Patient"] = patient; //temporary
           return RedirectToAction("Details", "Patient", new { PatientNumber = patient.PatientNumber });
        }

        public void DeleteCurrentUser()
        {
            string userId = User.Identity.GetUserId();

            Patient patient = db.patients.Where(e => e.ApplicationId == userId).SingleOrDefault();

            foreach (Feedback feedback in db.feedbacks)
            {
                if (feedback.FeedbackId > 0)
                {
                    db.feedbacks.Remove(feedback);

                }

            }

            foreach (Alert alert in db.alerts)
            {
                if (alert.AlertId > 0)
                {
                    db.alerts.Remove(alert);

                }

            }

            foreach (Steps step in db.steps)
            {
                if (step.StepId > 0)
                {
                    db.steps.Remove(step);

                }

            }

            foreach (Intervention intervention in db.interventions)
            {
                if (intervention.InterventionId > 0)
                {
                    db.interventions.Remove(intervention);

                }

            }

            foreach (Diagnosis diagnosis in db.diagnoses)
            {
                if (diagnosis.DiagnosisId > 0)
                {
                    db.diagnoses.Remove(diagnosis);

                }

            }

            foreach (PatientDiagnosis patientDiagnosis in db.patientDiagnoses)
            {
                if (patientDiagnosis.PatientDiagnosisId > 0)
                {
                    db.patientDiagnoses.Remove(patientDiagnosis);

                }

            }

            foreach (PatientStep patientStep in db.patientSteps)
            {
                if (patientStep.PatientNumber == patient.PatientNumber)
                {
                    db.patientSteps.Remove(patientStep);

                } 

            }

            foreach (StepMedicine stepmedicine in db.stepMedicines)
            {
                if (stepmedicine.StepMedicineId > 0)
                {
                    db.stepMedicines.Remove(stepmedicine);
                    

                }

            }


            foreach (StepProcedure stepprocedure in db.stepProcedures)
            {
                if (stepprocedure.StepProcedureId > 0)
                {
                    db.stepProcedures.Remove(stepprocedure);

                }

            }

            foreach (Medicine medicine in db.medicine)
            {
                if (medicine.MedicineId > 0)
                {
                    db.medicine.Remove(medicine);

                }

            }


            foreach (Procedure procedure in db.procedures)
            {
                if (procedure.ProcedureId > 0)
                {
                    db.procedures.Remove(procedure);

                }

            }



            db.patients.Remove(patient);
            db.SaveChanges();


           
        }


    }
}