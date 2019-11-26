using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Formatics.Models
{
    public class StepProcedure
    {
        [Key]
        public int StepProcedureId { get; set; }

        [ForeignKey("Steps")]
        public int StepId { get; set; }
        public Steps Steps { get; set; }

        [ForeignKey("Procedure")]
        public int ProcedureId { get; set; }
        public Procedure Procedure { get; set; }



    }
}