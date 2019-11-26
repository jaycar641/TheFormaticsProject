using Formatics.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Formatics.Controllers
{
    public class PatientController : Controller
    {
        // GET: Patient
        ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index(int patientNumber) //Patient Dashboard
        {
            DateTime currentDate = DateTime.Today;
            Patient patient = db.patients.Where(e => e.PatientNumber == patientNumber).SingleOrDefault();
             
            return View(patient);
        }

        // GET: Patient/Details/5
        public ActionResult Details(int patientNumber)  //View Profile
        {
            Patient patient = db.patients.Where(e => e.PatientNumber == patientNumber).SingleOrDefault();
            return View(patient);
        }

        // GET: Patient/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Patient/Create
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

        // GET: Patient/Edit/5
        public ActionResult Edit(int patientNumber) //Edit patient address and contact info
        {
            Patient patient = db.patients.Where(e => e.PatientNumber == patientNumber).SingleOrDefault();
            return View(patient);
        }

        // POST: Patient/Edit/5
        [HttpPost]
        public ActionResult Edit(int patientNumber, Patient patient)
        {
            try
            {
             Patient patient1 = db.patients.Where(e => e.PatientNumber == patientNumber).SingleOrDefault();

                patient1.age = patient.age;
                patient1.ApplicationId = patient.ApplicationId;
                patient1.enrollDate = patient.enrollDate;
                patient1.firstName = patient.firstName;
                patient1.middleName = patient.middleName;
                patient1.lastName = patient.lastName;
                patient1.phoneNumber = patient.phoneNumber;
                patient1.sex = patient.sex;
                patient1.city = patient.city;
                patient1.country = patient.country;
                patient1.state = patient.state;
                patient1.zipcode = patient.zipcode;
                patient1.streetAddress = patient.streetAddress;
                patient1.PatientNumber = patient.PatientNumber;
                db.SaveChanges();
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Patient/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Patient/Delete/5
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
