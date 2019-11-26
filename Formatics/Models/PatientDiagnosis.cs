using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Formatics.Models
{
    public class PatientDiagnosis
    {
        [Key]
        public int PatientDiagnosisId { get; set; }

        [ForeignKey("Diagnosis")]
        public int DiagnosisId { get; set; }
        public Diagnosis Diagnosis { get; set; }

        [ForeignKey("Patient")]
        public int PatientNumber { get; set; }
        public Patient Patient { get; set; }

    }
}