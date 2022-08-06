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
        public string LoadStepDetails(int patientNumber, string diagnosis, int interventionId)
        {

            string page = "";
            switch (diagnosis) //Use database lingo 
            {
                case "Acute Pain":
                    page = "Acute Pain";           
                    break;
                case "Respiration Alteration":
                    page = "Respiration Alteration";
                    break;

                case "Sleep Pattern Disturbance":
                    page = "Sleep Pattern Disturbance";
         
                    break;
                default:
                    page = "Nausea";
                    break;
            }
            return page;

        }


        //in delete current user, if there is an error, or something send to the delete current user, and add delete application user in that function
        //authorize should be across every registration loading function
        [Authorize]
        public ActionResult Index(int PatientNumber)
        {

            string userId = User.Identity.GetUserId();
            Patient patient = db.patients.Where(e => e.PatientNumber == PatientNumber).SingleOrDefault();
           Diagnosis diagnosis1 = db.diagnoses.Where(e => e.isCurrent == true).SingleOrDefault();
            PatientDiagnosis patientDiagnosis = db.patientDiagnoses.Where(e => e.PatientNumber == patient.PatientNumber && e.DiagnosisId == diagnosis1.DiagnosisId).SingleOrDefault();
            Diagnosis diagnosis = db.diagnoses.Where(e => e.DiagnosisId == patientDiagnosis.DiagnosisId).SingleOrDefault();
            Intervention intervention = db.interventions.Where(e => e.InterventionId == diagnosis.InterventionId).SingleOrDefault();

            string page = "";
            try
            {
                page = LoadStepDetails(patient.PatientNumber, diagnosis.category, intervention.InterventionId);
                return RedirectToAction("Create", "Procedure", new { PatientNumber = patient.PatientNumber, category = page, InterventionId = intervention.InterventionId });
            }
            catch
            {
                return RedirectToAction("Register", "Account", patient);

            }

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
