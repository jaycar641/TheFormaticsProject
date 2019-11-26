using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Formatics.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Alert> alerts { get; set; }

        public DbSet<Diagnosis> diagnoses { get; set; }

        public DbSet<Feedback> feedbacks { get; set; }

        public DbSet<Intervention> interventions { get; set; }

        public DbSet<Medicine> medicine { get; set; }

        public DbSet<Patient> patients { get; set; }

        public DbSet<Procedure> procedures { get; set; }

        public DbSet<PatientDiagnosis> patientDiagnoses { get; set; }
        public DbSet<PatientStep> patientSteps { get; set; }
        public DbSet<StepMedicine> stepMedicines { get; set; }
        public DbSet<StepProcedure> stepProcedures { get; set; }

        public DbSet<Steps> steps { get; set; }



        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

     
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}