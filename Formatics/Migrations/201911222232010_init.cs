namespace Formatics.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Alerts",
                c => new
                    {
                        AlertId = c.Int(nullable: false, identity: true),
                        type = c.String(),
                        frequency = c.Int(nullable: false),
                        time = c.DateTime(nullable: false),
                        description = c.String(),
                    })
                .PrimaryKey(t => t.AlertId);
            
            CreateTable(
                "dbo.Diagnosis",
                c => new
                    {
                        DiagnosisId = c.Int(nullable: false, identity: true),
                        InterventionId = c.Int(nullable: false),
                        category = c.String(),
                        dateDiagnosed = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.DiagnosisId)
                .ForeignKey("dbo.Interventions", t => t.InterventionId, cascadeDelete: true)
                .Index(t => t.InterventionId);
            
            CreateTable(
                "dbo.Interventions",
                c => new
                    {
                        InterventionId = c.Int(nullable: false, identity: true),
                        category = c.String(),
                        startDate = c.DateTime(nullable: false),
                        endDate = c.DateTime(),
                        duration = c.Int(nullable: false),
                        expectedOutcome = c.String(),
                    })
                .PrimaryKey(t => t.InterventionId);
            
            CreateTable(
                "dbo.Feedbacks",
                c => new
                    {
                        FeedbackId = c.Int(nullable: false, identity: true),
                        StepId = c.Int(nullable: false),
                        PatientNumber = c.Int(nullable: false),
                        rating = c.Int(nullable: false),
                        comments = c.String(),
                    })
                .PrimaryKey(t => t.FeedbackId)
                .ForeignKey("dbo.Patients", t => t.PatientNumber, cascadeDelete: true)
                .ForeignKey("dbo.Steps", t => t.StepId, cascadeDelete: true)
                .Index(t => t.StepId)
                .Index(t => t.PatientNumber);
            
            CreateTable(
                "dbo.Patients",
                c => new
                    {
                        PatientNumber = c.Int(nullable: false, identity: true),
                        ApplicationId = c.String(maxLength: 128),
                        firstName = c.String(),
                        middleName = c.String(),
                        lastName = c.String(),
                        age = c.Int(nullable: false),
                        sex = c.String(),
                        phoneNumber = c.String(),
                        streetAddress = c.String(),
                        city = c.String(),
                        zipcode = c.String(),
                        state = c.String(),
                        country = c.String(),
                        enrollDate = c.DateTime(nullable: false),
                        insurance = c.String(),
                    })
                .PrimaryKey(t => t.PatientNumber)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationId)
                .Index(t => t.ApplicationId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Steps",
                c => new
                    {
                        StepId = c.Int(nullable: false, identity: true),
                        InterventionId = c.Int(nullable: false),
                        day = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        description = c.String(),
                    })
                .PrimaryKey(t => t.StepId)
                .ForeignKey("dbo.Interventions", t => t.InterventionId, cascadeDelete: true)
                .Index(t => t.InterventionId);
            
            CreateTable(
                "dbo.Medicines",
                c => new
                    {
                        MedicineId = c.Int(nullable: false, identity: true),
                        drugClass = c.String(),
                        name = c.String(),
                    })
                .PrimaryKey(t => t.MedicineId);
            
            CreateTable(
                "dbo.Procedures",
                c => new
                    {
                        ProcedureId = c.Int(nullable: false, identity: true),
                        date = c.DateTime(nullable: false),
                        location = c.String(),
                        category = c.String(),
                    })
                .PrimaryKey(t => t.ProcedureId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Feedbacks", "StepId", "dbo.Steps");
            DropForeignKey("dbo.Steps", "InterventionId", "dbo.Interventions");
            DropForeignKey("dbo.Feedbacks", "PatientNumber", "dbo.Patients");
            DropForeignKey("dbo.Patients", "ApplicationId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Diagnosis", "InterventionId", "dbo.Interventions");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Steps", new[] { "InterventionId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Patients", new[] { "ApplicationId" });
            DropIndex("dbo.Feedbacks", new[] { "PatientNumber" });
            DropIndex("dbo.Feedbacks", new[] { "StepId" });
            DropIndex("dbo.Diagnosis", new[] { "InterventionId" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Procedures");
            DropTable("dbo.Medicines");
            DropTable("dbo.Steps");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Patients");
            DropTable("dbo.Feedbacks");
            DropTable("dbo.Interventions");
            DropTable("dbo.Diagnosis");
            DropTable("dbo.Alerts");
        }
    }
}
