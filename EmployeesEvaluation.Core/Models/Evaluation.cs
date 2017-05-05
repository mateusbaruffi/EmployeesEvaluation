using System;
using System.Collections.Generic;

namespace EmployeesEvaluation.Core.Models
{
    public class Evaluation : IEntityBase
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Introduction { get; set; }           
        public string Disclosure { get; set; }
        public Season Season { get; set; }
        public int SeasonId { get; set; }
        public ApplicationUser DepartmentManager { get; set; }
        public string DepartmentManagerId { get; set; }
        public ICollection<EvaluationQuestion> EvaluationQuestions { get; set; }
        public virtual ICollection<Question> Questions { get; set; }
        public virtual ICollection<EvaluationAssigned> EvaluationsAssigned { get; set; }
        public virtual ICollection<EvaluationResponse> EvaluationResponses { get; set; }

    }
}