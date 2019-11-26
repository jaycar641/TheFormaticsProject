﻿using Formatics.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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

        public ActionResult Alerts(int amount)
        {

            if (amount == 1)
            {
                //view all
            return PartialView();

            }
            else
                 //view recent

            return PartialView();
        }

        public ActionResult TreatmentGlimpse()
        {
            return PartialView();
        }










        //Treatment Plan

        // GET: 
        public ActionResult FeedbackReview() //Feedback review paqge
        {
            return View();
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