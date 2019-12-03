using Formatics.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace Formatics.Controllers
{
    public class FrontEndController : Controller
    {
        // GET: FrontEnd
        ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            return View();
        }

        // GET: FrontEnd/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: FrontEnd/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FrontEnd/Create
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

        // GET: FrontEnd/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: FrontEnd/Edit/5
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

        // GET: FrontEnd/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: FrontEnd/Delete/5
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
                return PartialView();

            }
        }
        //Profile


        //Symptoms Checker


        //Medicine
        // GET: 
        public ActionResult Perscription() //Feedback review paqge
        {
            return PartialView();

        }







        //Dashboard
        public ActionResult Personalization()
        {
            return PartialView();
        }

        public ActionResult Resources()
        {
            return PartialView();
        }

       

        public ActionResult TreatmentGlimpse()
        {
            return PartialView();
        }










     

        // GET: 
        [HttpPost]
        public ActionResult Mood(int FeedbackId, Feedback feedback) //Feedback review paqge
        {
            string userId = User.Identity.GetUserId();
            Patient patient = db.patients.Where(e => e.ApplicationId == userId).SingleOrDefault();
            feedback.date = DateTime.Now;
            Feedback feedback1 = new Feedback(); feedback1.comments = feedback.comments;
            feedback1.rating = feedback.rating;
            feedback1.date = feedback.date;
            feedback1.PatientNumber = patient.PatientNumber;
            feedback1.type = "Mood";
            feedback1.StepId = feedback.StepId;
            db.feedbacks.Add(feedback1);
            ViewBag.PartialStyle1 = "display: none";

            db.SaveChanges();

            return RedirectToAction("Index", "Intervention");
        }

        [HttpPost]
        public ActionResult Condition(int FeedbackId, Feedback feedback) //Feedback review paqge
        {
            string userId = User.Identity.GetUserId();
            Patient patient = db.patients.Where(e => e.ApplicationId == userId).SingleOrDefault();

            feedback.date = DateTime.Now;
            Feedback feedback1 = new Feedback();
            feedback1.comments = feedback.comments;
            feedback1.rating = feedback.rating;
            feedback1.date = feedback.date;
            feedback1.type = "Condition";
            feedback1.StepId = feedback.StepId;
            feedback1.PatientNumber = patient.PatientNumber;
            db.feedbacks.Add(feedback1);
            ViewBag.PartialStyle2 = "display: none";

            db.SaveChanges();
            return RedirectToAction("Index", "Intervention");
        }

        [HttpPost]
        public ActionResult Alert(int AlertId, Alert alert)
        {
          Alert alert1 =  db.alerts.Where(e => e.AlertId == AlertId).SingleOrDefault();
            alert1.description = alert.description;
            alert1.time = alert.time.Date;
            db.SaveChanges();

            TwilioClient.Init(twillio.accountSid, twillio.authToken);

            var message = MessageResource.Create(
                body: "Your Appointment has been sent.  We will send you  reminder the day of",
                from: new Twilio.Types.PhoneNumber("+12056513904"),
                to: new Twilio.Types.PhoneNumber("+14143887275")
            );
            //twillio

            return RedirectToAction("Index", "Intervention");

        }
        public ActionResult Schedule() //View page
        {
            return View();
        }

        // GET: FrontEnd/Edit/5
        public ActionResult SubmitFeedback(int id)
        {
            return PartialView();
        }

        // POST: FrontEnd/Edit/5
        [HttpPost]
        public ActionResult SubmitFeedback(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
                return RedirectToAction("Index");
            }
            catch
            {
                return PartialView();
            }
        }


        public ActionResult DailyPlanner()
        {
            return PartialView();
        }

        // GET: 
        public ActionResult Diagnosis() //Feedback review paqge
        {
            return PartialView();
        }

        // GET: 
      


    }
}
