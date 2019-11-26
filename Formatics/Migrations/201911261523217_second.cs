namespace Formatics.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class second : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PatientDiagnosis",
                c => new
                    {
                        PatientDiagnosisId = c.Int(nullable: false, identity: true),
                        DiagnosisId = c.Int(nullable: false),
                        PatientNumber = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PatientDiagnosisId)
                .ForeignKey("dbo.Diagnosis", t => t.DiagnosisId, cascadeDelete: true)
                .ForeignKey("dbo.Patients", t => t.PatientNumber, cascadeDelete: true)
                .Index(t => t.DiagnosisId)
                .Index(t => t.PatientNumber);
            
            CreateTable(
                "dbo.PatientSteps",
                c => new
                    {
                        PatientStepId = c.Int(nullable: false, identity: true),
                        StepId = c.Int(nullable: false),
                        PatientNumber = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.PatientStepId)
                .ForeignKey("dbo.Patients", t => t.PatientNumber, cascadeDelete: true)
                .ForeignKey("dbo.Steps", t => t.StepId, cascadeDelete: true)
                .Index(t => t.StepId)
                .Index(t => t.PatientNumber);
            
            CreateTable(
                "dbo.StepMedicines",
                c => new
                    {
                        StepMedicineId = c.Int(nullable: false, identity: true),
                        StepId = c.Int(nullable: false),
                        MedicineId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.StepMedicineId)
                .ForeignKey("dbo.Medicines", t => t.MedicineId, cascadeDelete: true)
                .ForeignKey("dbo.Steps", t => t.StepId, cascadeDelete: true)
                .Index(t => t.StepId)
                .Index(t => t.MedicineId);
            
            CreateTable(
                "dbo.StepProcedures",
                c => new
                    {
                        StepProcedureId = c.Int(nullable: false, identity: true),
                        StepId = c.Int(nullable: false),
                        ProcedureId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.StepProcedureId)
                .ForeignKey("dbo.Procedures", t => t.ProcedureId, cascadeDelete: true)
                .ForeignKey("dbo.Steps", t => t.StepId, cascadeDelete: true)
                .Index(t => t.StepId)
                .Index(t => t.ProcedureId);
            
            AddColumn("dbo.Medicines", "isCurrent", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StepProcedures", "StepId", "dbo.Steps");
            DropForeignKey("dbo.StepProcedures", "ProcedureId", "dbo.Procedures");
            DropForeignKey("dbo.StepMedicines", "StepId", "dbo.Steps");
            DropForeignKey("dbo.StepMedicines", "MedicineId", "dbo.Medicines");
            DropForeignKey("dbo.PatientSteps", "StepId", "dbo.Steps");
            DropForeignKey("dbo.PatientSteps", "PatientNumber", "dbo.Patients");
            DropForeignKey("dbo.PatientDiagnosis", "PatientNumber", "dbo.Patients");
            DropForeignKey("dbo.PatientDiagnosis", "DiagnosisId", "dbo.Diagnosis");
            DropIndex("dbo.StepProcedures", new[] { "ProcedureId" });
            DropIndex("dbo.StepProcedures", new[] { "StepId" });
            DropIndex("dbo.StepMedicines", new[] { "MedicineId" });
            DropIndex("dbo.StepMedicines", new[] { "StepId" });
            DropIndex("dbo.PatientSteps", new[] { "PatientNumber" });
            DropIndex("dbo.PatientSteps", new[] { "StepId" });
            DropIndex("dbo.PatientDiagnosis", new[] { "PatientNumber" });
            DropIndex("dbo.PatientDiagnosis", new[] { "DiagnosisId" });
            DropColumn("dbo.Medicines", "isCurrent");
            DropTable("dbo.StepProcedures");
            DropTable("dbo.StepMedicines");
            DropTable("dbo.PatientSteps");
            DropTable("dbo.PatientDiagnosis");
        }
    }
}
