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
    public class MedicineController : Controller
    {
        // GET: Medicine
        ApplicationDbContext db = new ApplicationDbContext();

        
          public void LoadAllAlerts(int interventionId)
        {
            db.alerts.ToList();
        }

        public string loadDailyReminders(string diagnosis, Steps steps1)
        {
            Steps step2 = db.steps.Where(e => e.StepId == steps1.StepId).SingleOrDefault();
            string stepDescription = "";
            switch (diagnosis)
            {

                case "Acute Pain":
                    stepDescription = "Stretch daily so that your back pain can be minimized";
                    break;
                case "Respiration Alteration":
                    stepDescription = "Breathing Excercises";
                    break;
                case "Sleep Pattern Disturbance":
                    if (steps1.Date.DayOfWeek == DayOfWeek.Saturday || steps1.Date.DayOfWeek == DayOfWeek.Sunday)
                    {
                        stepDescription = "Get a lot of sleep on the weekends";
                    }
                    else
                    {
                        step2.description = "Remember to go to sleep early";
                    }
                    break;

                default:
                    stepDescription = "Remember to Drink a lot of water";
                    break;
            }
            return stepDescription;

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


        public Medicine LoadMedicineDetails(int interventionId, Diagnosis diagnosis, String category)
        {
            Medicine medicine = new Medicine();
                        Intervention intervention = db.interventions.Where(e => e.InterventionId == interventionId).SingleOrDefault();

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
        
        public StepMedicine LoadStepMedicineDetails(Medicine medicine, Steps step)
        {
            StepMedicine stepMedicine = new StepMedicine();
            stepMedicine.StepId = step.StepId;
            stepMedicine.MedicineId = medicine.MedicineId;
              
            return stepMedicine;
        }

      
        public ActionResult Index() //Standalone Medication page a list of medications
        {
            ///LOAD INFO FUNCTION
            string userId = User.Identity.GetUserId();
            Patient patient = db.patients.Where(e => e.ApplicationId == userId).SingleOrDefault();
    
            List<Steps> steps = new List<Steps>();
            List<Medicine> medicines = db.medicine.Where(e=> e.isCurrent == true).ToList();
            Medicine medicine = db.medicine.Where(e => e.isCurrent == true).SingleOrDefault();
       
            ViewData["Ingredients"] = medicine.ingredients;
            ViewData["Symptoms"] = medicine.symptoms;
            Alert appointmentAlert = new Alert();
            appointmentAlert.type = "Appointment";
            appointmentAlert.frequency = 1;
            appointmentAlert.time = DateTime.Today.Date;
            ViewData["Appointment"] = appointmentAlert;
            ViewData["Medicines"] = medicines;
            ViewData["Patient"] = patient;
         

            return View();
        }

        // GET: Medicine/Details/5
        public ActionResult Details() //Standalone Medicine Details page
        {
            string userId = User.Identity.GetUserId();
            Patient patient = db.patients.Where(e => e.ApplicationId == userId).SingleOrDefault();
            ViewData["Patient"] = patient; //temporary
            return RedirectToAction("Details", "Patient", new { PatientNumber = patient.PatientNumber });
        }


        // GET: Medicine/Create
        public ActionResult Create(int patientNumber, string category, int interventionId)
        {
         
            int count3 = 0;
            List<PatientStep> patientStepList = db.patientSteps.Where(e => e.PatientNumber == patientNumber).ToList(); //getting all the steps in the table that apply to patient first then manipulating them
            List<Steps> stepList = new List<Steps>();
             foreach (PatientStep patientStep in patientStepList)//finding all steps in db that match patient steps
            {
                Steps step = db.steps.Where(e => e.StepId == patientStep.StepId && e.InterventionId == interventionId).SingleOrDefault();
                stepList.Add(step);
            }


            Diagnosis diagnosis = db.diagnoses.Where(e => e.InterventionId == interventionId).SingleOrDefault();
            Medicine medicine = LoadMedicineDetails(interventionId, diagnosis, category);
            db.medicine.Add(medicine);
            db.SaveChanges();

            foreach (Steps steps1 in stepList)
            {
                try
                {
                    Steps step2 = db.steps.Where(e => e.StepId == steps1.StepId).SingleOrDefault();
                    step2.description = loadDailyReminders(category, steps1);
                    db.SaveChanges();
                    Alert alert = LoadDailyAlerts(count3, category, step2, steps1);
                    db.alerts.Add(alert);
                    db.SaveChanges();
                    StepMedicine stepMedicine = LoadStepMedicineDetails(medicine, step2);
                    db.stepMedicines.Add(stepMedicine);
                    db.SaveChanges();
                    count3++;
                }
                catch(NullReferenceException e)
                {

                    count3++;
                }

            }
            ///   LoadAllAlerts(interventionId);//use twillio

            return RedirectToAction("Index", "Patient", new {PatientNumber = patientNumber});



        }

        // POST: Medicine/Create
        [HttpPost]
        public ActionResult Create()
        {

            return View();
        }

      public ActionResult MedicalDetails ()
        {
            return PartialView("_detailsMedicine");
        }



        public ActionResult OrderPerscription(int MedicineId)
        {
            Medicine medicine = db.medicine.Where(e => e.MedicineId == MedicineId).SingleOrDefault();
            
            Alert pickup = new Alert();

            pickup.description = "Your Perscription is ready!";
            pickup.time = DateTime.Now.AddDays(7);
            pickup.type = "Perscription";
            db.alerts.Add(pickup);
            db.SaveChanges();
         
            TwilioClient.Init(twillio.accountSid, twillio.authToken);

            var message = MessageResource.Create(
                body: "Your perscription for " + medicine.name + " has been received!  We will notifiy you when it is available",
                from: new Twilio.Types.PhoneNumber("+12564877936"),
                to: new Twilio.Types.PhoneNumber("+14143887275")
            );
            return RedirectToAction("Index", "Medicine");
            //return an alert to the view from the controller you have ordered a prescription
        }





    }
}
