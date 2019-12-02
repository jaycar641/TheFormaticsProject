using Formatics.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;

[assembly: OwinStartupAttribute(typeof(Formatics.Startup))]
namespace Formatics
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateRolesAndUsers();
            SendAlert();
        }




        private void CreateRolesAndUsers()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            if (!roleManager.RoleExists("Patient"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Patient";
                roleManager.Create(role);

            }
        }


        private void SendAlert()
        {
            //check list and if they have been sent out
            ApplicationDbContext db = new ApplicationDbContext();
            DateTime date = new DateTime();
            date = DateTime.Today;
            List<Alert> alerts = db.alerts.ToList();

            foreach (Alert alert in alerts)
            {
                if (alert.time.Date == date.Date )
                {
                    //send twillio S
                }

            }
        }

    }
}
